using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using ExperiencePortal.DataAccess;
using ExperiencePortal.Service.Extensions;

namespace ExperiencePortal.Service
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "Service1" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select Service1.svc or Service1.svc.cs at the Solution Explorer and start debugging.
    [ServiceBehavior(IncludeExceptionDetailInFaults =true)]
    public class DataAccessService : IDataAccessService
    {
        public DataAccess.DataContext dataContext = null;

        public Models.User AuthenticateUser(string authentivationToken)
        {
            using (dataContext = new DataAccess.DataContext())
            {
                var usr =  dataContext.GetByEntity<User>().All().FirstOrDefault(u => u.AuthenticationToken == authentivationToken).ToModel();
                return usr;
            }
        }

        public Models.User RegisterUser(string userName, string authenticationToken)
        {
            try
            {
                using (dataContext = new DataAccess.DataContext())
                {
                    User newUser = new DataAccess.User() { AuthenticationToken = authenticationToken, UserName = userName };
                    dataContext.GetByEntity<User>().Add(newUser);
                    return newUser.ToModel();
                }
            }
            catch
            {
                return null;
            }
        }

        public int Sum(int first, int second)
        {
            return first + second;
        }
    }
}