using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Demo.Models
{
    public class IndexView
    {
        public SchoolModel.ProjectData PopupBanner { get; set; }
        public List<SchoolModel.ProjectData> Banners { get; set; }
    }
}