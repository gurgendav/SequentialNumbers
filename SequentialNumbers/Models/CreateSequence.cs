using System.ComponentModel.DataAnnotations;

namespace SequentialNumbers.Models
{
    public class CreateSequence
    {
        [Required]
        public string Key { get; set; }
        [Required]
        public long StartValue { get; set; }
    }
}