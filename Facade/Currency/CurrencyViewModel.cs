using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Open.Aids;
using Open.Core;

namespace Open.Facade.Currency
{
    public class CurrencyViewModel : RootObject
    {
        private string name;
        private string alpha3Code;
        private string currencySymbol;

        [Required]
        [RegularExpression(RegularExpressionFor.EnglishTextOnly)]
        public string Name
        {
            get => getValue(ref name, Constants.Unspecified);
            set => name = value;
        }

        [DataType(DataType.Date)]
        [DisplayName("Valid From")]
        public DateTime? ValidFrom
        {
            get;
            set;
        }

        [DataType(DataType.Date)]
        [DisplayName("Valid To")]
        public DateTime? ValidTo
        {
            get;
            set;
        }


        [Required]
        [StringLength(3, MinimumLength = 3)]
        [RegularExpression(RegularExpressionFor.EnglishCapitalsOnly)]
        [DisplayName("ISO Three Letter Code")]
        public string Alpha3Code
        {
            get => getValue(ref alpha3Code, Constants.Unspecified);
            set => alpha3Code = value;
        }

        [Required]
        [StringLength(3, MinimumLength = 3)]
        [RegularExpression(RegularExpressionFor.EnglishCapitalsOnly)]
        [DisplayName("ISO Currency Symbol")]
        public string CurrencySymbol
        {
            get => getValue(ref currencySymbol, Constants.Unspecified);
            set => currencySymbol = value;
        }
    }
}
