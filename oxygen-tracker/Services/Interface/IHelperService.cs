using oxygen_tracker.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace oxygen_tracker.Services.Interface
{
    public interface IHelperService
    {
        public Task<string> GetStateNameAsync(string postalCode);
    }
}
