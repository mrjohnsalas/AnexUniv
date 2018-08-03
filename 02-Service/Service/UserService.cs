using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common;
using Model.Auth;
using NLog;
using Persistence.DbContextScope;
using Persistence.Repository;

namespace Service
{
    public interface IUserService
    {
        ResponseHelper Update(ApplicationUser model);
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
    }
}
