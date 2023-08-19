using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using WizLib_Model.Models;

namespace WizLib_Model.ViewModels
{
    public class BookVM
    {

        //Property for a book
        public Book Book { get; set; }

        //Property for a: Drop down list of Publisher
        public IEnumerable<Microsoft.AspNetCore.Mvc.Rendering.SelectListItem> PublisherList { get; set; }

    }
}
