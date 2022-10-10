using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace EngineersMatrimony.Models
{
    public class FileUploadModel
    {
        [Key]
        public HttpPostedFileBase file { get; set; }
    }
}