using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WizLib_Model.Models
{
    public class BookDetail
    {
        [Key]
        public int BookDetail_id { get; set; }

        [Required]
        public int NumberOfChapters { get; set; }
        public int NumberOfPages { get; set; }
        public int Weigth { get; set; }

        public Book Book { get; set; }

    }
}
