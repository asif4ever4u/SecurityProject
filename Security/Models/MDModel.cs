using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Globalization;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using System.Linq.Expressions;
namespace Security.Models
{
    public class MDModel<TMaster, TDetail>
        where TMaster : class
        where TDetail : class
    {
        public TMaster Master { get; set; }
        public List<TDetail> DetailList { get; set; }
    }
}
