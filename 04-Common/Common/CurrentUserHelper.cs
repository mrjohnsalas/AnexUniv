using System.Collections.Generic;
using Microsoft.AspNet.Identity;
using Newtonsoft.Json;
using System.Security.Claims;
using System.Web;

namespace Common
{
    public class CurrentUser
    {
        public string UserId { get; set; }
        public string UserName { get; set; }
        public string Name { get; set; }
        public List<string> Roles { get; set; }

        public CurrentUser()
        {
            Roles = new List<string>();
        }

        public bool IsAdmin() => Roles.Contains(RoleNames.Admin);

        public bool IsStudent() => Roles.Contains(RoleNames.Student);

        public bool IsTeacher() => Roles.Contains(RoleNames.Teacher);

        public bool IsUser() => Roles.Contains(RoleNames.User);
    }

    public class CurrentUserHelper
    {
        public static CurrentUser Get
        {
            get
            {
                var user = HttpContext.Current.User;

                if (user == null)
                {
                    return null;
                }
                else if (string.IsNullOrEmpty(user.Identity.GetUserId()))
                {
                    return null;
                }

                var jUser = ((ClaimsIdentity)user.Identity).FindFirst(ClaimTypes.UserData).Value;

                return JsonConvert.DeserializeObject<CurrentUser>(jUser);
            }
        }
    }
}
