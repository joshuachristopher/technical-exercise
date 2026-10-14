using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using StargateAPI.Business.Interfaces;
using StargateAPI.Data;

namespace StargateAPI.Business.Services
{
    public class PersonService : IPersonService
    {
        private readonly StargateContext _context;

        public PersonService(StargateContext context)
        {
            _context = context;
        }

        public async Task<bool> IsPersonUnique(string name)
        {
            var result = await _context.People.AnyAsync(p => p.Name.ToLower() == name.ToLower());

            return !result;
        }
    }
}
