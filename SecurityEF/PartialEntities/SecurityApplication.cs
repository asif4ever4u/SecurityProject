using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SecurityEF
{
    public partial class SecurityApplication
    {
        public bool ActiveFlag2
        {
            get
            {
                return (Convert.ToString(this.ActiveFlag) == "Y");
            }
            set
            {
                if (value)
                {
                    this.ActiveFlag = "Y";
                }
                else
                {
                    this.ActiveFlag = "N";
                }
                    
            }
        }
    }
}