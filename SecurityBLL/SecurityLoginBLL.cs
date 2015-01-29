using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SecurityEF.Repositories;
using SecurityEF;

namespace SecurityEF.BLL
{
    public class SecurityLoginBLL
    {
        private IRepository<SecurityLogin> rep;
        private SecurityDBEntities ctx;

        public SecurityLoginBLL()
        {
            ctx = new SecurityDBEntities();
            rep = new Repository<SecurityLogin>(ctx);
        }

        public SecurityLogin GetByLoginName(string pLoginName)
        {
            try
            {
                //return rep.Find(o => o.LoginName == pLoginName).First();
                return rep.Find(o => o.LoginName == pLoginName).FirstOrDefault();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
