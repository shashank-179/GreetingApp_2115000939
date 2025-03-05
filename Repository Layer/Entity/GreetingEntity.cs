using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository_Layer.Entity
{
    public class GreetingEntity
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Message { get; set; }
        public DateTime dateTime{ get; set; }= DateTime.Now;
    }
}
