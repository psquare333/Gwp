namespace Gwp.Models
{
    public class CounytryPremiums
    {
        public int Id { get; set; }
        public string Country { get; set; }
        public string VariableId { get; set; }
        public string LineOfBusiness { get; set; }
        public int Year { get; set; }
        public double Value { get; set; }
    }
}
