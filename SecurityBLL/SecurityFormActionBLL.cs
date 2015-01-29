using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SecurityEF.Repositories;
using SecurityEF;

namespace SecurityEF.BLL
{
    public class SecurityFormActionBLL
    {
        private IRepository<SecurityFormAction> rep;
        private SecurityDBEntities ctx;

        public SecurityFormActionBLL()
        {
            ctx = new SecurityDBEntities();
            rep = new Repository<SecurityFormAction>(ctx);
        }

        //public long GetMaxID()
        //{
        //    long iNewCode = 0;

        //    try
        //    {
        //        object oNewCode = ctx.SecurityFormActions.Max(o => o.ID);
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

        public string GetMaxCode(string pApplicationCode, string pFormCode)
        {
            string strNewCode = string.Empty;

            try
            {
                var iNewCode = ctx.SecurityFormActions
                    .Where(app => app.FormCode == pFormCode && app.ApplicationCode == pApplicationCode)
                    .Max(o => o.FormActionCode);

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

        public SecurityFormAction GetByCode(string pApplicationCode, string pFormCode, string pFormActionCode)
        {
            try
            {
                return rep.Find(o => o.ApplicationCode == pApplicationCode && o.FormCode == pFormCode && o.FormActionCode == pFormActionCode).Single();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public SecurityFormAction GetByID(long pID)
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


        public IEnumerable<SecurityFormAction> GetAll()
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

        public IEnumerable<SecurityFormAction> GetByAppFormCode(string pApplicationCode, string pFormCode)
        {
            try
            {
                return ctx.SecurityFormActions.Where(o => o.FormCode == pFormCode && o.ApplicationCode == pApplicationCode);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        //insert SecurityForm object
        public bool Insert(SecurityFormAction pSecurityFormAction)
        {
            try
            {
                //pSecurityFormAction.ID = GetMaxID();

                pSecurityFormAction.ApplicationCode = pSecurityFormAction.ApplicationCode;
                pSecurityFormAction.FormCode = pSecurityFormAction.FormCode;

                pSecurityFormAction.FormActionCode = GetMaxCode(pSecurityFormAction.ApplicationCode, pSecurityFormAction.FormCode);

                pSecurityFormAction.FormActionName = pSecurityFormAction.FormActionName;
                pSecurityFormAction.Remarks = pSecurityFormAction.Remarks;
                pSecurityFormAction.ActiveFlag = pSecurityFormAction.ActiveFlag;

                rep.Add(pSecurityFormAction);
                rep.SaveChanges();

                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        //update SecurityForm object
        public bool Update(SecurityFormAction pSecurityFormAction)
        {
            try
            {
                if (pSecurityFormAction.ID > 0)
                {
                    SecurityFormAction vSecurityFormAction = rep.Find(pSecurityFormAction.ID);
                    if (vSecurityFormAction != null)
                    {
                        //vSecurityFormAction.ID = pSecurityFormAction.ID;
                        //vSecurityFormAction.ApplicationCode = pSecurityFormAction.ApplicationCode;
                        //vSecurityFormAction.FormCode = pSecurityFormAction.FormCode;
                        //vSecurityFormAction.FormActionCode = pSecurityFormAction.FormActionCode;
                        vSecurityFormAction.FormActionName = pSecurityFormAction.FormActionName;
                        vSecurityFormAction.Remarks = pSecurityFormAction.Remarks;
                        vSecurityFormAction.ActiveFlag = pSecurityFormAction.ActiveFlag;
                        //vSecurityFormAction.ApplicationID = pSecurityFormAction.ApplicationID;
                        //vSecurityFormAction.FormID = pSecurityFormAction.FormID;

                        rep.Edit(vSecurityFormAction);
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

        public bool SaveList(List<SecurityFormAction> pSecurityFormActionList)
        {
            try
            {
                foreach (SecurityFormAction eachItem in pSecurityFormActionList)
                {
                    if (eachItem.ID > 0)
                    {
                        rep.Edit(eachItem);
                    }
                    else
                    {
                        eachItem.FormActionCode = GetMaxCode(eachItem.ApplicationCode, eachItem.FormCode);
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

        public bool InsertList(List<SecurityFormAction> pSecurityFormActionList)
        {
            try
            {
                foreach (SecurityFormAction eachItem in pSecurityFormActionList)
                {
                    eachItem.FormActionCode = GetMaxCode(eachItem.ApplicationCode, eachItem.FormCode);
                    rep.Add(eachItem);
                }
                rep.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool UpdateList(List<SecurityFormAction> pSecurityFormActionList)
        {
            try
            {
                foreach (SecurityFormAction eachItem in pSecurityFormActionList)
                {
                    rep.Edit(eachItem);
                }
                rep.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
