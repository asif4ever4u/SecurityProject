using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SecurityEF
{
    public partial class SecurityUserAccess
    {

        public string ApplicationName { get; set; }
        public string FormName { get; set; }
        public string FormActionName { get; set; }

        public bool ActionValue2
        {
            get
            {
                return (Convert.ToString(this.ActionValue) == "Y");
            }
            set
            {
                if (value)
                {
                    this.ActionValue = "Y";
                }
                else
                {
                    this.ActionValue = "N";
                }

            }
        }

    }
}
