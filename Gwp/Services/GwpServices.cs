using Gwp.Data;
using Serilog;
using System.Linq;

namespace Gwp.Services
{
    public interface IGwpServices
    {
        Dictionary<string, double> GetAvgGwp(string country, List<string> lob);
    }
    public class GwpServices : IGwpServices
    {
        private readonly GwpContext _context;

        public GwpServices(GwpContext context)
        {
            _context = context;
        }
        public Dictionary<string, double> GetAvgGwp(string country, List<string> lob)
        {
            var result = new Dictionary<string, double>();
            try
            {
                result = _context.CounytryPremiums
                       .Where(c => c.Country == country && lob.Contains(c.LineOfBusiness))
                       .GroupBy(c => c.LineOfBusiness)
                       .Select(g => new
                       {
                           LineOfBusiness = g.Key,
                           AverageValue = g.Average(c => c.Value)
                       })
                       .ToDictionary(x => x.LineOfBusiness, x => x.AverageValue);
            }
            catch (Exception ex)
            {
                Log.Error("An error occurred while retrieving the average GWP.");
                throw new Exception("An error occurred while retrieving the average GWP.", ex);
            }
            return result;
        }
    }
}
