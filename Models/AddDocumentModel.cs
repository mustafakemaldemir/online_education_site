using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace online_education_site.Models
{
    public class AddDocumentModel
    {
        public int Id { get; set; }
        public IFormFile File { get; set; }
    }
}
