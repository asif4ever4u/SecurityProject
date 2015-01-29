using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SecurityEF.Repositories;
using SecurityEF;

namespace SecurityEF.BLL
{
    public class SecurityFormBLL
    {
        private IRepository<SecurityForm> rep;
        private SecurityDBEntities ctx;

        public SecurityFormBLL()
        {
            ctx = new SecurityDBEntities();
            rep = new Repository<SecurityForm>(ctx);
        }

        //public long GetMaxID()
        //{
        //    long iNewCode = 0;

        //    try
        //    {
        //        object oNewCode = ctx.SecurityForms.Max(o => o.ID);
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

        public string GetMaxCode(string pApplicationCode)
        {
            string strNewCode = string.Empty;

            try
            {
                var iNewCode = ctx.SecurityForms.Where(app => app.ApplicationCode == pApplicationCode).Max(o => o.FormCode);
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

        public SecurityForm GetByID(long pID)
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

        public SecurityForm GetByCode(string pApplicationCode, string pFormCode)
        {
            try
            {
                return rep.Find(o => o.ApplicationCode == pApplicationCode && o.FormCode == pFormCode).Single();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        //public SecurityForm GetByCode(string pFormCode)
        //{
        //    try
        //    {
        //        var query = from f in ctx.SecurityForms
        //                    join a in ctx.SecurityApplications on
        //                        f.ApplicationCode equals a.ApplicationCode
        //                    where f.FormCode == pFormCode
        //                    select new { f, a.ApplicationName };
                
        //        foreach (var eachItem in query)
        //        {
        //            eachItem.f.ApplicationName = eachItem.ApplicationName;
        //            return eachItem.f;
        //        }
        //        return null;
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}

        public IEnumerable<SecurityForm> GetAll()
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

        public IEnumerable<SecurityForm> GetByApplicationCode(string pApplicationCode)
        {
            try
            {
                return ctx.SecurityForms.Where(app => app.ApplicationCode == pApplicationCode);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        //insert SecurityForm object
        public bool Insert(SecurityForm pSecurityForm)
        {
            try
            {
                pSecurityForm.ApplicationCode = pSecurityForm.ApplicationCode;
                pSecurityForm.FormCode = GetMaxCode(pSecurityForm.ApplicationCode);
                pSecurityForm.FormName = pSecurityForm.FormName;
                pSecurityForm.ActiveFlag = pSecurityForm.ActiveFlag;

                rep.Add(pSecurityForm);
                rep.SaveChanges();
               
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

    


        //update SecurityForm object
        public bool Update(SecurityForm pSecurityForm)
        {
            try
            {
                if (pSecurityForm.ID > 0)
                {
                    SecurityForm vSecurityForm = rep.Find(pSecurityForm.ID);
                    if (vSecurityForm != null)
                    {
                        //vSecurityForm.ID = pSecurityForm.ID;
                        //vSecurityForm.ApplicationCode = pSecurityForm.ApplicationCode;
                        //vSecurityForm.FormCode = pSecurityForm.FormCode;
                        vSecurityForm.FormName = pSecurityForm.FormName;
                        vSecurityForm.Remarks = pSecurityForm.Remarks;
                        vSecurityForm.ActiveFlag = pSecurityForm.ActiveFlag;
                        //vSecurityForm.ApplicationID = pSecurityForm.ApplicationID;

                        rep.Edit(vSecurityForm);
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

        //delete SecurityForm object by id
        public bool DeleteByID(long pSecurityFormID)
        {
            try
            {
                if (pSecurityFormID > 0)
                {
                    SecurityForm vSecurityForm = rep.Find(pSecurityFormID);
                    if (vSecurityForm != null)
                    {
                        ////Update DelFlag To "Y" For Soft Delete

                        //vSecurityForm.ID = pSecurityForm.ID;
                        //vSecurityForm.ApplicationCode = pSecurityForm.ApplicationCode;
                        //vSecurityForm.FormCode = pSecurityForm.FormCode;
                        //vSecurityForm.FormName = pSecurityForm.FormName;
                        //vSecurityForm.ActiveFlag = pSecurityForm.ActiveFlag;
                        //vSecurityForm.ApplicationID = pSecurityForm.ApplicationID;

                        rep.Edit(vSecurityForm);
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
