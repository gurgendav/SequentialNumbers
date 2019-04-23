using System.ComponentModel.DataAnnotations;

namespace SequentialNumbers.Models
{
    public class SequenceEntry
    {
        [Key]
        public string Key { get; set; }
        public long Value { get; set; }
    }
}