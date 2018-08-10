using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common;
using Model.Auth;
using Model.Custom;
using Model.Domain;
using NLog;
using Persistence.DbContextScope;
using Persistence.DbContextScope.Extensions;
using Persistence.Repository;

namespace Service
{
    public interface IUserService
    {
        ResponseHelper Update(ApplicationUser model);

        AnexGRIDResponde GetAll(AnexGRID grid);
    }

    public class UserService : IUserService
    {
        private static ILogger _logger = LogManager.GetCurrentClassLogger();
        private readonly IDbContextScopeFactory _dbContextScopeFactory;
        private readonly IRepository<ApplicationUser> _applicationUserRepo;

        public UserService(IDbContextScopeFactory dbContextScopeFactory, IRepository<ApplicationUser> applicationUserRepo)
        {
            _dbContextScopeFactory = dbContextScopeFactory;
            _applicationUserRepo = applicationUserRepo;
        }

        public ResponseHelper Update(ApplicationUser model)
        {
            var rh = new ResponseHelper();

            try
            {
                using (var ctx = _dbContextScopeFactory.Create())
                {
                    var originalModel = _applicationUserRepo.Single(x => x.Id.Equals(model.Id));
                    originalModel.Name = model.Name;
                    originalModel.LastName = model.LastName;
                    _applicationUserRepo.Update(originalModel);
                    ctx.SaveChanges();
                    rh.SetResponse(true);
                }
            }
            catch (Exception e)
            {
                _logger.Error(e.Message);
                rh.SetResponse(false, e.Message);
            }

            return rh;
        }

        public AnexGRIDResponde GetAll(AnexGRID grid)
        {
            grid.Inicializar();

            try
            {
                using (var ctx = _dbContextScopeFactory.Create())
                {
                    var user = ctx.GetEntity<ApplicationUser>();
                    var roles = ctx.GetEntity<ApplicationRole>();
                    var userRoles = ctx.GetEntity<ApplicationUserRole>();
                    var courses = ctx.GetEntity<Course>();
                    var userCourses = ctx.GetEntity<UsersPerCourse>();

                    var queryRoles = (
                            from r in roles
                            from ur in userRoles.Where(x => x.RoleId.Equals(r.Id))
                            select new { UserId = ur.UserId, Role = r.Name }
                            ).AsQueryable();

                    var query = (
                            from u in user
                            select new UserForGridView
                            {
                                Id = u.Id,
                                FullName = u.Name + " " + u.LastName,
                                Email = u.Email,
                                CoursesCreated = courses.Count(x => x.AuthorId.Equals(u.Id)),
                                CoursesTaken = userCourses.Count(x => x.UserId.Equals(u.Id)),
                                Roles = queryRoles.Where(x => x.UserId.Equals(u.Id)).Select(x => x.Role).ToList()

                            }
                            ).AsQueryable();

                    if (grid.columna.Equals("FullName"))
                    {
                        query = grid.columna_orden.Equals("DESC")
                            ? query.OrderByDescending(x => x.FullName)
                            : query.OrderBy(x => x.FullName);
                    }

                    var data = query.Skip(grid.pagina).Take(grid.limite).ToList();

                    var total = query.Count();

                    grid.SetData(data, total);
                }
            }
            catch (Exception e)
            {
                _logger.Error(e.Message);
            }

            return grid.responde();
        }
    }
}
