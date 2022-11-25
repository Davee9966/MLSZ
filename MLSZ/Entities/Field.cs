using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;

namespace MLSZ.Entities
{
    public class Field
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string Name { get; set; }
        public int Postalcode { get; set; }
        public string Address { get; set; }
        public string AuthLevel { get; set; }
        public int IFACode { get; set; }

        //több felhasználó kell majd -- db-be átírni
        public User Owner { get; set; }

        public Field()
        {

        }
    }
}
