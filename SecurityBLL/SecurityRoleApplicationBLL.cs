using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SecurityEF.Repositories;
using SecurityEF;

namespace SecurityEF.BLL
{
    public class SecurityRoleApplicationBLL
    {
        private IRepository<SecurityRoleApplication> rep;
        private SecurityDBEntities ctx;

        public SecurityRoleApplicationBLL()
        {
            ctx = new SecurityDBEntities();
            rep = new Repository<SecurityRoleApplication>(ctx);
        }

        //public long GetMaxID()
        //{
        //    long iNewCode = 0;

        //    try
        //    {
        //        long? oNewCode = ctx.SecurityRoleApplications.Max(o => o.ID);
        //        if (oNewCode != null)
        //        {
        //            iNewCode = Convert.ToInt64(oNewCode) + 1;
        //        }
        //        else
        //        {
        //            iNewCode = 1;
        //        }

        //        return iNewCode;
        //    }
        //    catch (Exception ex)
        //    {

        //        throw ex;
        //    }
        //}

        public IEnumerable<SecurityRoleApplication> GetAll()
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

        public List<SecurityRoleApplication> GetByRole(SecurityRole pRole)
        {
            List<SecurityRoleApplication> vList = new List<SecurityRoleApplication>();
            IRepository<SecurityApplication> repSA = new Repository<SecurityApplication>(ctx);

            try
            {
                var query = from app in ctx.SecurityApplications
                            join roleApp in ctx.SecurityRoleApplications.Where(o=> o.RoleCode == pRole.RoleCode) on
                            app.ApplicationCode equals roleApp.ApplicationCode into Details
                            from rapp in Details.DefaultIfEmpty()
                            select new { app, rapp };

                foreach (var eachItem in query)
                {
                    if (eachItem.rapp == null)
                    {
                        SecurityRoleApplication oNew = new SecurityRoleApplication();
                        oNew.ApplicationCode = eachItem.app.ApplicationCode;
                        oNew.RoleCode = pRole.RoleCode;
                        oNew.AllowFlag = "N";
                        oNew.ApplicationName = eachItem.app.ApplicationName;
                        vList.Add(oNew);
                    }
                    else
                    {
                        eachItem.rapp.ApplicationName = eachItem.app.ApplicationName;
                        vList.Add(eachItem.rapp);
                    }
                }
                return vList;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool SaveList(List<SecurityRoleApplication> pSecurityRoleApplicationList)
        {
            try
            {
                foreach (SecurityRoleApplication eachItem in pSecurityRoleApplicationList)
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

        public bool Insert(SecurityRoleApplication pSecurityRoleApplication)
        {
            try
            {
                rep.Add(pSecurityRoleApplication);
                rep.SaveChanges();

                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool Edit(SecurityRoleApplication pSecurityRoleApplication)
        {
            try
            {
                if (pSecurityRoleApplication.ID > 0)
                {
                    SecurityRoleApplication vSecurityRoleApplication = rep.Find(pSecurityRoleApplication.ID);
                    if (vSecurityRoleApplication != null)
                    {
                        rep.Edit(vSecurityRoleApplication);
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
