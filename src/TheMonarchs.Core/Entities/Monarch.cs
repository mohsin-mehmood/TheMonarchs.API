namespace TheMonarchs.Core.Entities
{
    public class Monarch
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Country { get; set; }

        public string House { get; set; }

        public int? YearStart { get; set; }

        public int? YearEnd { get; set; }


        public int? YearsRuled => YearEnd - YearStart;

    }
}
