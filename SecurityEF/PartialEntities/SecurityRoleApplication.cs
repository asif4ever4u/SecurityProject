using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SecurityEF
{
    public partial class SecurityRoleApplication
    {
        //public long RoleID { get; set; }
        //public long ApplicationID { get; set; }

        public bool AllowFlag2
        {
            get
            {
                return (Convert.ToString(this.AllowFlag) == "Y");
            }
            set
            {
                if (value)
                {
                    this.AllowFlag = "Y";
                }
                else
                {
                    this.AllowFlag = "N";
                }

            }
        }

        public string ApplicationName { get; set; }
    }
}
