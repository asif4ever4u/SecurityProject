using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SecurityEF
{
    public partial class SecurityUserRole
    {
        public bool EnableFlag2
        {
            get
            {
                return (Convert.ToString(this.EnableFlag) == "Y");
            }
            set
            {
                if (value)
                {
                    this.EnableFlag = "Y";
                }
                else
                {
                    this.EnableFlag = "N";
                }

            }
        }

        public string RoleName { get; set; }
    }
}
