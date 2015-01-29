namespace SecurityEF
{
    using System;
    using System.Collections.Generic;

    public partial class SecurityLogin
    {
        public bool RememberMe { get; set; }
        //public bool AdministratorFlag
        //{
        //    get
        //    {
        //        if (LoginName.ToLower() == "Administrator".ToLower())
        //        {
        //            return true;
        //        }
        //        else
        //        {
        //            return false;
        //        }
        //    }
        //}

        //public bool IsValidLogin()
        //{
        //    //if (LoginName.ToLower() == "Administrator".ToLower() && Password == ("SaSp" + DateTime.Now.ToString("HHmm")))//system admin secure password 555
        //    //{
        //    //    return true;
        //    //}

        //    //if (LoginName.ToLower() == "Admin".ToLower() && Password == ("SaSp" + DateTime.Now.ToString("HHmm")))//system admin secure password 555
        //    //{
        //    //    return true;
        //    //}
        //    //return false;

        //    if (LoginName.ToLower() == "Administrator".ToLower() && Password == "SaSp555")//system admin secure password 555
        //    {
        //        return true;
        //    }

        //    if (LoginName.ToLower() == "Admin".ToLower() && Password == "Sasp1E2R3p")//system admin secure password 555
        //    {
        //        return true;
        //    }
        //    return false;
        //}
    }
}