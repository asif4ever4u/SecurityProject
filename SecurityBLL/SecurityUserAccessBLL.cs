using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SecurityEF.Repositories;
using SecurityEF;

namespace SecurityEF.BLL
{
    public class SecurityUserAccessBLL
    {
        private IRepository<SecurityUserAccess> rep;
        private SecurityDBEntities ctx;

        public SecurityUserAccessBLL()
        {
            ctx = new SecurityDBEntities();
            rep = new Repository<SecurityUserAccess>(ctx);
        }

        //public long GetMaxID()
        //{
        //    long iNewCode = 0;

        //    try
        //    {
        //        object oNewCode = ctx.SecurityUserAccesses.Max(o => o.ID);
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
            long iNewCode = 0;
            string strNewCode = string.Empty;

            //iNewCode = ctx.SecurityUserRights.Max<SecurityUserRight>(o => Convert.ToInt64(o.SecurityUserRightCode));
            //iNewCode = iNewCode + 1;
            //strNewCode = iNewCode.ToString().PadLeft(10, '0');

            return strNewCode;
        }

        public SecurityUserAccess GetByID(long pID)
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

        public IEnumerable<SecurityUserAccess> GetByUserCode(string pUserCode)
        {
            try
            {
                return rep.GetAll(o => o.UserCode == pUserCode);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        //000000000
        public List<SecurityUserAccess> GetByUserAppForm(SecurityUser pSecurityUser, SecurityForm pSecurityForm)
        {
            List<SecurityUserAccess> vList = new List<SecurityUserAccess>();
            IRepository<SecurityUserAccess> repSUA = new Repository<SecurityUserAccess>(ctx);

            try
            {
                var query = from a in ctx.SecurityFormActions
                            join b in ctx.SecurityUserAccesses.Where(o => o.UserCode == pSecurityUser.UserCode) on
                                 new { a.ApplicationCode, a.FormCode, a.FormActionCode } equals new { b.ApplicationCode, b.FormCode, b.FormActionCode } into Details
                            from ua in Details.DefaultIfEmpty()
                            where a.ApplicationCode == pSecurityForm.ApplicationCode && a.FormCode == pSecurityForm.FormCode
                            select new { a, ua };

                //string strQuery = "exec sp_SecurityUserActionALL @UserID='" + pSecurityUser.ID + "', @UserCode='" + pSecurityUser.UserCode + "', @FormID='" + pSecurityForm.ID + "'";

                foreach (var eachItem in query)
                {
                    if (eachItem.ua == null)
                    {
                        SecurityUserAccess oNew = new SecurityUserAccess();
                        oNew.UserCode = pSecurityUser.UserCode;
                        oNew.ApplicationCode = eachItem.a.ApplicationCode;
                        oNew.FormCode = eachItem.a.FormCode;
                        oNew.FormName = pSecurityForm.FormName;

                        oNew.FormActionCode = eachItem.a.FormActionCode;
                        oNew.FormActionName = eachItem.a.FormActionName;
                        oNew.ActionValue = "N";

                        vList.Add(oNew);
                    }
                    else
                    {
                        eachItem.ua.FormActionName = eachItem.a.FormActionName;
                        vList.Add(eachItem.ua);
                    }
                }

                return vList;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public IEnumerable<SecurityUserAccess> GetAll()
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

        public bool SaveList(List<SecurityUserAccess> pSecurityUserAccessList)
        {
            try
            {
                foreach (SecurityUserAccess eachItem in pSecurityUserAccessList)
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

        //insert SecurityUserRight object
        public bool Insert(SecurityUserAccess pSecurityUserAccess)
        {
            try
            {
                //pSecurityUserAccess.ID = GetMaxID();
                pSecurityUserAccess.UserCode = pSecurityUserAccess.UserCode;
                pSecurityUserAccess.ApplicationCode = pSecurityUserAccess.ApplicationCode;
                pSecurityUserAccess.FormCode = pSecurityUserAccess.FormCode;
                pSecurityUserAccess.FormActionCode = pSecurityUserAccess.FormActionCode;
                pSecurityUserAccess.ActionValue = pSecurityUserAccess.ActionValue;

                rep.Add(pSecurityUserAccess);
                rep.SaveChanges();

                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        //update SecurityUserRight object
        public bool Update(SecurityUserAccess pSecurityUserAccess)
        {
            try
            {
                if (pSecurityUserAccess.ID > 0)
                {
                    SecurityUserAccess vSecurityUserAccess = rep.Find(pSecurityUserAccess.ID);
                    if (vSecurityUserAccess != null)
                    {
                        //vSecurityUserRight.ID = pSecurityUserRight.ID;
                        vSecurityUserAccess.UserCode = pSecurityUserAccess.UserCode;
                        vSecurityUserAccess.ApplicationCode = pSecurityUserAccess.ApplicationCode;
                        vSecurityUserAccess.FormCode = pSecurityUserAccess.FormCode;
                        vSecurityUserAccess.FormActionCode = pSecurityUserAccess.FormActionCode;
                        vSecurityUserAccess.ActionValue = pSecurityUserAccess.ActionValue;

                        rep.Edit(vSecurityUserAccess);
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

        //delete SecurityUserRight object by id
        public bool DeleteByID(long pSecurityUserActionID)
        {
            try
            {
                if (pSecurityUserActionID > 0)
                {
                    SecurityUserAccess vSecurityUserAction = rep.Find(pSecurityUserActionID);
                    if (vSecurityUserAction != null)
                    {
                        ////Update DelFlag To "Y" For Soft Delete

                        //vSecurityUserRight.ID = pSecurityUserRight.ID;
                        //vSecurityUserRight.UserCode = pSecurityUserRight.UserCode;
                        //vSecurityUserRight.ApplicationCode = pSecurityUserRight.ApplicationCode;
                        //vSecurityUserRight.FormCode = pSecurityUserRight.FormCode;
                        //vSecurityUserRight.FormRightCode = pSecurityUserRight.FormRightCode;
                        //vSecurityUserRight.RightValue = pSecurityUserRight.RightValue;
                        //vSecurityUserRight.UserID = pSecurityUserRight.UserID;
                        //vSecurityUserRight.ApplicationID = pSecurityUserRight.ApplicationID;
                        //vSecurityUserRight.FormID = pSecurityUserRight.FormID;
                        //vSecurityUserRight.FormRightID = pSecurityUserRight.FormRightID;

                        rep.Edit(vSecurityUserAction);
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
