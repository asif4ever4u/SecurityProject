using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SecurityEF.Repositories;
using SecurityEF;

namespace SecurityEF.BLL
{
    public class SecurityRoleAccessBLL
    {
        private IRepository<SecurityRoleAccess> rep;
        private SecurityDBEntities ctx;

        public SecurityRoleAccessBLL()
        {
            ctx = new SecurityDBEntities();
            rep = new Repository<SecurityRoleAccess>(ctx);
        }

        public long GetMaxID()
        {
            long iNewCode = 0;

            try
            {
                object oNewCode = ctx.SecurityRoleAccesses.Max(o => o.ID);
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

        public string GetMaxCode()
        {
            long iNewCode = 0;
            string strNewCode = string.Empty;

            //iNewCode = ctx.SecurityUserRights.Max<SecurityUserRight>(o => Convert.ToInt64(o.SecurityUserRightCode));
            //iNewCode = iNewCode + 1;
            //strNewCode = iNewCode.ToString().PadLeft(10, '0');

            return strNewCode;
        }

        public SecurityRoleAccess GetByID(long pID)
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

        public IEnumerable<SecurityRoleAccess> GetByRoleCode(string pRoleCode)
        {
            try
            {
                return rep.GetAll(o => o.RoleCode == pRoleCode);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        //000000000
        public List<SecurityRoleAccess> GetByRoleForm(SecurityRole pSecurityRole, SecurityForm pSecurityForm)
        {

            List<SecurityRoleAccess> vList = new List<SecurityRoleAccess>();
            IRepository<SecurityRoleAccess> repSUA = new Repository<SecurityRoleAccess>(ctx);

            try
            {
                var query = from a in ctx.SecurityFormActions
                            join b in ctx.SecurityRoleAccesses.Where(o => o.RoleCode == pSecurityRole.RoleCode) on
                                 new { a.ApplicationCode, a.FormCode, a.FormActionCode } equals new { b.ApplicationCode, b.FormCode, b.FormActionCode } into Details
                            from ra in Details.DefaultIfEmpty()
                            where a.ApplicationCode == pSecurityForm.ApplicationCode && a.FormCode == pSecurityForm.FormCode
                            select new { a, ra };

                //string strQuery = "exec sp_SecurityUserActionALL @UserID='" + pSecurityUser.ID + "', @UserCode='" + pSecurityUser.UserCode + "', @FormID='" + pSecurityForm.ID + "'";
               

                foreach (var eachItem in query)
                {
                    if (eachItem.ra == null)
                    {
                        SecurityRoleAccess oNew = new SecurityRoleAccess();
                        oNew.RoleCode = pSecurityRole.RoleCode;
                        oNew.ApplicationCode = eachItem.a.ApplicationCode;
                        oNew.FormCode = eachItem.a.FormCode;

                        oNew.FormActionCode = eachItem.a.FormActionCode;
                        oNew.FormActionName = eachItem.a.FormActionName;
                        oNew.ActionValue = "N";

                        vList.Add(oNew);
                    }
                    else
                    {
                        eachItem.ra.FormActionName = eachItem.a.FormActionName;
                        vList.Add(eachItem.ra);
                    }
                }

                return vList;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public IEnumerable<SecurityRoleAccess> GetAll()
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

        public bool SaveList(List<SecurityRoleAccess> pSecurityRoleAccessList)
        {
            try
            {
                foreach (SecurityRoleAccess eachItem in pSecurityRoleAccessList)
                {
                    if (eachItem.ID > 0)
                    {
                        rep.Edit(eachItem);
                    }
                    else
                    {
                        eachItem.ID = GetMaxID();
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

        //insert SecurityUserRight object
        public bool Insert(SecurityRoleAccess pSecurityRoleAccess)
        {
            try
            {
                //pSecurityRoleAccess.ID = GetMaxID();
                pSecurityRoleAccess.RoleCode = pSecurityRoleAccess.RoleCode;
                pSecurityRoleAccess.ApplicationCode = pSecurityRoleAccess.ApplicationCode;
                pSecurityRoleAccess.FormCode = pSecurityRoleAccess.FormCode;
                pSecurityRoleAccess.FormActionCode = pSecurityRoleAccess.FormActionCode;
                pSecurityRoleAccess.ActionValue = pSecurityRoleAccess.ActionValue;

                rep.Add(pSecurityRoleAccess);
                rep.SaveChanges();

                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        //update SecurityUserRight object
        public bool Update(SecurityRoleAccess pSecurityRoleAccess)
        {
            try
            {
                if (pSecurityRoleAccess.ID > 0)
                {
                    SecurityRoleAccess vSecurityRoleAccess = rep.Find(pSecurityRoleAccess.ID);
                    if (vSecurityRoleAccess != null)
                    {
                        //vSecurityUserRight.ID = pSecurityUserRight.ID;
                        vSecurityRoleAccess.RoleCode = pSecurityRoleAccess.RoleCode;
                        vSecurityRoleAccess.ApplicationCode = pSecurityRoleAccess.ApplicationCode;
                        vSecurityRoleAccess.FormCode = pSecurityRoleAccess.FormCode;
                        vSecurityRoleAccess.FormActionCode = pSecurityRoleAccess.FormActionCode;
                        vSecurityRoleAccess.ActionValue = pSecurityRoleAccess.ActionValue;

                        rep.Edit(vSecurityRoleAccess);
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
