using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SecurityEF.Repositories;
using SecurityEF;

namespace SecurityEF.BLL
{
    public class SecurityUserRoleBLL
    {
        private IRepository<SecurityUserRole> rep;
        private SecurityDBEntities ctx;

        public SecurityUserRoleBLL()
        {
            ctx = new SecurityDBEntities();
            rep = new Repository<SecurityUserRole>(ctx);
        }

        public IEnumerable<SecurityUserRole> GetAll()
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

        public List<SecurityUserRole> GetByUserCode(string pUserCode)
        {
            List<SecurityUserRole> vList = new List<SecurityUserRole>();
            IRepository<SecurityRole> repSA = new Repository<SecurityRole>(ctx);

            try
            {


                var query = from rol in ctx.SecurityRoles
                            join userRol in ctx.SecurityUserRoles.Where(o => o.UserCode == pUserCode) on
                            rol.RoleCode equals userRol.RoleCode into Details
                            from ur in Details.DefaultIfEmpty()
                            select new { rol, ur };


                foreach (var eachItem in query)
                {
                    if (eachItem.ur == null)
                    {
                        SecurityUserRole oNew = new SecurityUserRole();
                        oNew.RoleCode = eachItem.rol.RoleCode;
                        oNew.RoleName = eachItem.rol.RoleName;

                        oNew.UserCode = pUserCode;
                        oNew.EnableFlag = "N";
                        vList.Add(oNew);
                    }
                    else
                    {
                        eachItem.ur.RoleName = eachItem.rol.RoleName;
                        vList.Add(eachItem.ur);
                    }
                }
                return vList;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool SaveList(List<SecurityUserRole> pSecurityUserRoleList)
        {
            try
            {
                foreach (SecurityUserRole eachItem in pSecurityUserRoleList)
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

        public bool Save(SecurityUserRole pSecurityUserRole)
        {
            try
            {
                if (pSecurityUserRole.ID > 0)
                {
                    rep.Edit(pSecurityUserRole);
                }
                else
                {
                    rep.Add(pSecurityUserRole);
                }
                rep.SaveChanges();

                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool Add(SecurityUserRole pSecurityUserRole)
        {
            try
            {
                rep.Add(pSecurityUserRole);
                rep.SaveChanges();

                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool Edit(SecurityUserRole pSecurityUserRole)
        {
            try
            {
                if (pSecurityUserRole.ID > 0)
                {
                    SecurityUserRole vSecurityUserRole = rep.Find(pSecurityUserRole.ID);
                    if (vSecurityUserRole != null)
                    {
                        rep.Edit(vSecurityUserRole);
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
