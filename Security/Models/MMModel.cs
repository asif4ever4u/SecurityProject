using System;
using System.Collections.Generic;
//using System.Data.Entity;
//using System.Linq;
//using System.ComponentModel.DataAnnotations;
//using System.ComponentModel.DataAnnotations.Schema;
//using System.Globalization;
//using System.Web;
//using System.Web.Mvc;
//using System.Web.Security;
//using System.Linq.Expressions;

namespace Security.Models
{
    public class MMModel<TMaster1, TMaster2>
        where TMaster1 : class
        where TMaster2 : class
    {
        public TMaster1 Master1 { get; set; }
        public TMaster2 Master2 { get; set; }
    }
}
