using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SecurityEF.Repositories;
using SecurityEF;

namespace SecurityEF.BLL
{
    public class SecurityRoleBLL
    {
        private IRepository<SecurityRole> rep;
        private SecurityDBEntities ctx;

        public SecurityRoleBLL()
        {
            ctx = new SecurityDBEntities();
            rep = new Repository<SecurityRole>(ctx);
        }

        //public long GetMaxID()
        //{
        //    long iNewCode = 0;

        //    try
        //    {
        //        object oNewCode = ctx.SecurityRoles.Max(o => o.ID);
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

        public string GetMaxCode()
        {
            string strNewCode = string.Empty;

            try
            {
                var iNewCode = ctx.SecurityRoles.Max(o => o.RoleCode);
                if (iNewCode != null)
                {
                    strNewCode = Convert.ToString((Convert.ToInt64(iNewCode) + 1)).PadLeft(3, '0');
                }
                else
                {
                    strNewCode = "001";
                }

                return strNewCode;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public SecurityRole GetByCode(string pRoleCode)
        {
            try
            {
                return rep.Find(o => o.RoleCode == pRoleCode).Single();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public SecurityRole GetByID(long pID)
        {
            try
            {
                return rep.Find(pID);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public IEnumerable<SecurityRole> GetAll()
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

        //insert SecurityRole object
        public bool Insert(SecurityRole pSecurityRole)
        {
            try
            {
                //pSecurityRole.ID = GetMaxID();
                pSecurityRole.RoleCode = GetMaxCode();
                pSecurityRole.RoleName = pSecurityRole.RoleName;
                pSecurityRole.Remarks = pSecurityRole.Remarks;
                pSecurityRole.ActiveFlag = pSecurityRole.ActiveFlag;

                rep.Add(pSecurityRole);
                rep.SaveChanges();

                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        //update SecurityRole object
        public bool Update(SecurityRole pSecurityRole)
        {
            try
            {
                if (pSecurityRole.ID > 0)
                {
                    SecurityRole vSecurityRole = rep.Find(pSecurityRole.ID);
                    if (vSecurityRole != null)
                    {
                        //vSecurityRole.ID = pSecurityRole.ID;
                        //vSecurityRole.RoleCode = pSecurityRole.RoleCode;
                        vSecurityRole.RoleName = pSecurityRole.RoleName.ToUpper();
                        vSecurityRole.Remarks = pSecurityRole.Remarks;
                        vSecurityRole.ActiveFlag = pSecurityRole.ActiveFlag;

                        rep.Edit(vSecurityRole);
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

        //delete SecurityRole object by id
        public bool DeleteByID(long pSecurityRoleID)
        {
            try
            {
                if (pSecurityRoleID > 0)
                {
                    SecurityRole vSecurityRole = rep.Find(pSecurityRoleID);
                    if (vSecurityRole != null)
                    {
                        ////Update DelFlag To "Y" For Soft Delete

                        //vSecurityRole.ID = pSecurityRole.ID;
                        //vSecurityRole.RoleCode = pSecurityRole.RoleCode;
                        //vSecurityRole.RoleName = pSecurityRole.RoleName;
                        //vSecurityRole.Remarks = pSecurityRole.Remarks;
                        //vSecurityRole.ActiveFlag = pSecurityRole.ActiveFlag;

                        rep.Edit(vSecurityRole);
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
