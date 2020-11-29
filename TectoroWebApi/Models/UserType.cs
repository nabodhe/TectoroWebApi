using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;

namespace TectoroWebApi.Models
{
    public class UserType : Users
    {
        IEnumerable<Users> userTypeList;
        string userTypeStr;
        public UserType(Users item,string userType)
        {
            this.Alias = item.Alias;
            this.Email = item.Email;
            this.FirstName = item.FirstName;
            this.LastName = item.LastName;
            this.Level = item.Level;
            this.ManagerId = item.ManagerId;
            this.Position = item.Position;
            this.UserId = item.UserId;
            this.UserName = item.UserName;
            this.userTypeStr = userType;
        }

        public string UserCategory
        {
            get { return userTypeStr; }
        }

        public IEnumerable<Users> UserTypeList
        {
            get
            {
                return userTypeList;
            }
        }

        public static IReadOnlyCollection<Users> Clients(IEnumerable<Users> users, UserType userType)
        {
            userType.userTypeList = users.Where(u => u.ManagerId == userType.UserId);
            return new ReadOnlyCollection<Users>(new List<Users>(userType.userTypeList));
        }

        public static IReadOnlyCollection<Users> ManagerList(IEnumerable<Users> users, UserType userType)
        {
            userType.userTypeList = users.Where(u => !u.Level.HasValue && userType.ManagerId == u.UserId);
            return new ReadOnlyCollection<Users>(new List<Users>(userType.userTypeList));
        }
    }
}
