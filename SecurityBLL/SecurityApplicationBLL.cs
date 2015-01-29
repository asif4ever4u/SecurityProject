using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SecurityEF.Repositories;
using SecurityEF;

namespace SecurityEF.BLL
{
    public class SecurityApplicationBLL
    {
        private IRepository<SecurityApplication> rep;
        private SecurityDBEntities ctx;

        public SecurityApplicationBLL()
        {
            ctx = new SecurityDBEntities();
            rep = new Repository<SecurityApplication>(ctx);
        }


        //public long GetMaxID()
        //{
        //    long iNewCode = 0;

        //    try
        //    {
        //        object oNewCode = ctx.SecurityApplications.Max(o => o.ID);
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
                var iNewCode = ctx.SecurityApplications.Max(o => o.ApplicationCode);
                if (iNewCode == null)
                {
                    strNewCode = "001";
                }
                else
                {
                    strNewCode = Convert.ToString((Convert.ToInt64(iNewCode) + 1)).PadLeft(3, '0');
                }
                return strNewCode;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public SecurityApplication GetByCode(string pApplicationCode)
        {
            try
            {
                return rep.Find(o => o.ApplicationCode == pApplicationCode).Single();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public SecurityApplication GetByID(long? pID)
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

        public IEnumerable<SecurityApplication> GetAll()
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

        public bool Insert(SecurityApplication pSecurityApplication)
        {
            try
            {
                //pSecurityApplication.ID = GetMaxID();
                pSecurityApplication.ApplicationCode = GetMaxCode();
                pSecurityApplication.ApplicationName = pSecurityApplication.ApplicationName.ToUpper();
                pSecurityApplication.Remarks = pSecurityApplication.Remarks;
                pSecurityApplication.ActiveFlag = pSecurityApplication.ActiveFlag;

                rep.Add(pSecurityApplication);
                rep.SaveChanges();

                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool Update(SecurityApplication pSecurityApplication)
        {
            try
            {
                if (pSecurityApplication.ID > 0)
                {
                    SecurityApplication vSecurityApplication = rep.Find(pSecurityApplication.ID);
                    if (vSecurityApplication != null)
                    {
                        vSecurityApplication.ApplicationName = pSecurityApplication.ApplicationName.ToUpper();
                        vSecurityApplication.FullName = pSecurityApplication.FullName;
                        vSecurityApplication.Remarks = pSecurityApplication.Remarks;
                        vSecurityApplication.ActiveFlag = pSecurityApplication.ActiveFlag;

                        rep.Edit(vSecurityApplication);
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

        //delete SecurityApplication object by id
        public bool DeleteByID(long pSecurityApplicationID)
        {
            try
            {
                if (pSecurityApplicationID > 0)
                {
                    SecurityApplication vSecurityApplication = rep.Find(pSecurityApplicationID);
                    if (vSecurityApplication != null)
                    {
                        ////Update DelFlag To "Y" For Soft Delete

                        //vSecurityApplication.ID = pSecurityApplication.ID;
                        //vSecurityApplication.ApplicationCode = pSecurityApplication.ApplicationCode;
                        //vSecurityApplication.ApplicationName = pSecurityApplication.ApplicationName;
                        //vSecurityApplication.Remarks = pSecurityApplication.Remarks;
                        //vSecurityApplication.ActiveFlag = pSecurityApplication.ActiveFlag;

                        rep.Edit(vSecurityApplication);
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
