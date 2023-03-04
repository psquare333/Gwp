using Gwp.Services;
using Microsoft.AspNetCore.Mvc;

namespace Gwp.Controllers
{
    [Route("server/api/gwp")]
    [ApiController]
    public class CountryGwp : ControllerBase
    {
        private readonly IGwpServices _gwp;

        public CountryGwp(IGwpServices gwp)
        {
            _gwp = gwp;
        }

        [HttpPost]
        [Route("avg")]
        public Dictionary<string, double> Average(string country, List<string> lob)
        {
            var result = new Dictionary<string, double>();
            result = _gwp.GetAvgGwp(country, lob);
            return result;

        }
    }
}
