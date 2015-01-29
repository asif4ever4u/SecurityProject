using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SecurityEF.Repositories;
using SecurityEF;

namespace SecurityEF.BLL
{
    public class SecurityUserApplicationBLL
    {
        private IRepository<SecurityUserApplication> rep;
        private SecurityDBEntities ctx;

        public SecurityUserApplicationBLL()
        {
            ctx = new SecurityDBEntities();
            rep = new Repository<SecurityUserApplication>(ctx);
        }

        public long GetMaxID()
        {
            long iNewCode = 0;

            try
            {
                object oNewCode = ctx.SecurityUserApplications.Max(o => o.ID);
                if (oNewCode != null)
                {
                    iNewCode = Convert.ToInt64(oNewCode) + 1;
                }
                else
                {
                    iNewCode = 1;
                }

                return iNewCode;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public IEnumerable<SecurityUserApplication> GetAll()
        {
            try
            {
                return rep.GetAll();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public IEnumerable<SecurityUserApplication> GetByUserCode(string pUserCode)
        {
            List<SecurityUserApplication> vList = new List<SecurityUserApplication>();
            //IRepository<SecurityApplication> repSA = new Repository<SecurityApplication>(ctx);

            try
            {
                var query = from app in ctx.SecurityApplications
                            join userApp in ctx.SecurityUserApplications.Where(o=> o.UserCode == pUserCode) on
                            app.ApplicationCode equals userApp.ApplicationCode into Details
                            from ua in Details.DefaultIfEmpty()
                            select new { app.ID, app.ApplicationCode, app.ApplicationName, ua};


                foreach (var eachItem in query)
                {
                    if (eachItem.ua != null)
                    {
                        eachItem.ua.ApplicationID = eachItem.ID;
                        eachItem.ua.ApplicationName = eachItem.ApplicationName;
                        vList.Add(eachItem.ua);
                    }
                    else
                    {
                        SecurityUserApplication oNew = new SecurityUserApplication();
                        oNew.ApplicationID = eachItem.ID;
                        oNew.ApplicationCode = eachItem.ApplicationCode;
                        oNew.UserCode = pUserCode;
                        oNew.AllowFlag = "N";
                        oNew.ApplicationName = eachItem.ApplicationName;
                        vList.Add(oNew);
                    }
                }
                return vList;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool SaveList(List<SecurityUserApplication> pSecurityUserApplicationList)
        {
            try
            {
                foreach (SecurityUserApplication eachItem in pSecurityUserApplicationList)
                {
                    if (eachItem.ID > 0)
                    {
                        rep.Edit(eachItem);
                    }
                    else
                    {
                        rep.Add(eachItem);
                    }
                }
                rep.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool Insert(SecurityUserApplication pSecurityUserApplication)
        {
            try
            {
                rep.Add(pSecurityUserApplication);
                rep.SaveChanges();

                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool Edit(SecurityUserApplication pSecurityUserApplication)
        {
            try
            {
                if (pSecurityUserApplication.ID > 0)
                {
                    SecurityUserApplication vSecurityUserApplication = rep.Find(pSecurityUserApplication.ID);
                    if (vSecurityUserApplication != null)
                    {
                        rep.Edit(vSecurityUserApplication);
                        rep.SaveChanges();
                        return true;
                    }
                }
                return false;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

    }
}
