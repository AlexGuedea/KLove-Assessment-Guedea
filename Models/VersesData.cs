using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace KLove.Models
{
    public class VersesData
    {
        [Required]
        [DataType(DataType.Date)]
        [ValidDateRange]
        [Display(Name = "Verse Date")]
        public DateTime VerseDate { get; set; }
        
        [Required]
        [Range(1, 100, ErrorMessage = "Please enter a number between {1} and {2}")]
        [Display(Name = "Number of Verses")]
        public int NumberOfVerses { get; set; }

    }
    public class ValidDateRangeAttribute : ValidationAttribute
    {
        private readonly DateTime _minValue = DateTime.Now.AddYears(-100);
        private readonly DateTime _maxValue = DateTime.Now;

        public override bool IsValid(object value)
        {
            DateTime val = (DateTime)value;
            return val >= _minValue && val <= _maxValue;
        }

        public override string FormatErrorMessage(string name)
        {
            return string.Format("Please enter a valid date rangbe between {0} and {1}", _minValue, _maxValue);
        }
    }
}
