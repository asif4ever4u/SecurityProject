using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SecurityEF.Repositories;
using SecurityEF;

namespace SecurityEF.BLL
{
    public class SecurityUserBLL
    {
        private IRepository<SecurityUser> rep;
        private SecurityDBEntities ctx;

        public SecurityUserBLL()
        {
            ctx = new SecurityDBEntities();
            rep = new Repository<SecurityUser>(ctx);
        }

        //public long GetMaxID()
        //{
        //    long iNewCode = 0;

        //    try
        //    {
        //        object oNewCode = ctx.SecurityUsers.Max(o => o.ID);
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
            long iNewUserCode = 0;
            string strUserCode = string.Empty;

            iNewUserCode = ctx.SecurityUsers.Max<SecurityUser>(o => Convert.ToInt64(o.UserCode));
            iNewUserCode = iNewUserCode + 1;
            strUserCode = iNewUserCode.ToString().PadLeft(5, '0');

            return strUserCode;
        }

        public SecurityUser GetByID(long pID)
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

        public SecurityUser GetByCode(string pUserCode)
        {
            try
            {
                return rep.Find(o => o.UserCode == pUserCode).Single();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public SecurityUser GetLikeByLoginName(string pLoginName)
        {
            try
            {
                return rep.Find(o => o.LoginName == pLoginName).SingleOrDefault();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public IEnumerable<SecurityUser> GetAll()
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

        public bool Insert(SecurityUser pSecurityUser)
        {
            try
            {
                //pSecurityUser.ID = GetMaxID();
                pSecurityUser.UserCode = GetMaxCode();
                pSecurityUser.LoginName = pSecurityUser.LoginName.ToUpper();
                pSecurityUser.EmployeeCode = pSecurityUser.EmployeeCode.ToUpper();
                pSecurityUser.EmployeeName = pSecurityUser.EmployeeName.ToUpper();
                pSecurityUser.CreateBy = "Administrator";
                pSecurityUser.CreateDate = DateTime.Now;

                rep.Add(pSecurityUser);
                rep.SaveChanges();

                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool Update(SecurityUser pSecurityUser)
        {
            try
            {
                if (pSecurityUser.ID > 0)
                {
                    SecurityUser oSecurityUser = rep.Find(pSecurityUser.ID);
                    if (oSecurityUser != null)
                    {
                        //oSecurityUser.ID = pSecurityUser.ID;
                        //oSecurityUser.LoginName = pSecurityUser.LoginName;
                        //oSecurityUser.Password = pSecurityUser.Password;
                        oSecurityUser.EmployeeCode = pSecurityUser.EmployeeCode.ToUpper();
                        oSecurityUser.EmployeeName = pSecurityUser.EmployeeName.ToUpper();
                        oSecurityUser.Remarks = pSecurityUser.Remarks;
                        oSecurityUser.ActiveFlag = pSecurityUser.ActiveFlag.ToUpper();
                        //oSecurityUser.CreateDate = pSecurityUser.CreateDate;
                        //oSecurityUser.CreateBy = pSecurityUser.CreateBy;
                        //oSecurityUser.CreateTerm = pSecurityUser.CreateTerm;
                        //oSecurityUser.DeleteFlag = pSecurityUser.DeleteFlag;
                        //oSecurityUser.DeleteDate = pSecurityUser.DeleteDate;
                        //oSecurityUser.DeleteBy = pSecurityUser.DeleteBy;
                    }
                    rep.Edit(oSecurityUser);
                    rep.SaveChanges();
                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool DeleteByID(long pID)
        {
            try
            {
                if (pID > 0)
                {
                    SecurityUser vSecurityUser = rep.Find(pID);
                    if (vSecurityUser != null)
                    {
                        //oSecurityUser.ID = pSecurityUser.ID;
                        //oSecurityUser.LoginName = pSecurityUser.LoginName;
                        //oSecurityUser.Password = pSecurityUser.Password;
                        //oSecurityUser.EmployeeCode = pSecurityUser.EmployeeCode;
                        //oSecurityUser.EmployeeName = pSecurityUser.EmployeeName;
                        //oSecurityUser.Remarks = pSecurityUser.Remarks;
                        //oSecurityUser.ActiveFlag = pSecurityUser.ActiveFlag;
                        //oSecurityUser.CreateDate = pSecurityUser.CreateDate;
                        //oSecurityUser.CreateBy = pSecurityUser.CreateBy;
                        //oSecurityUser.CreateTerm = pSecurityUser.CreateTerm;
                        //oSecurityUser.DeleteFlag = pSecurityUser.DeleteFlag;
                        //oSecurityUser.DeleteDate = pSecurityUser.DeleteDate;
                        //oSecurityUser.DeleteBy = pSecurityUser.DeleteBy;
                    }
                    rep.Edit(vSecurityUser);
                    rep.SaveChanges();
                    return true;
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
