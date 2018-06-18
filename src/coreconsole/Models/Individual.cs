using System;
using System.Globalization;

namespace ConsoleSolution.Models
{
    class Individual
    {
        public string FavoriteFruit { get; set; }
        public string Greeting { get; set; }
        public string Longitude { get; set; }
        public string Latitude { get; set; }
        public string Registered { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string Company { get; set; }
        public Name Name { get; set; }
        public string EyeColor { get; set; }
        public int Age { get; set; }
        public string Balance { get; set; }
        public bool IsActive { get; set; }
        public string Id { get; set; }

        public DateTimeOffset? RegisteredTime => 
            DateTimeOffset.TryParse(Registered, out var result) 
                ? result 
                : (DateTimeOffset?) null
            ;

        public decimal? BalanceAmount =>
            decimal.TryParse(
                Balance, 
                NumberStyles.Currency, 
                CultureInfo.CreateSpecificCulture("en-US"), 
                out var result
                )
            ? result
            : (decimal?) null
        ;
    }
}