using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace RepositoryLayer.Entity
{
    //create a table product(id, pname, brand)
    //add and fetch data from the table
    public class Product
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long ProductId { get; set; }
        public string pname {  get; set; }
        public string brand {  get; set; }
    }
}
