using oxygen_tracker.Models;
using System.Threading.Tasks;

namespace oxygen_tracker.Services
{
    public interface IVerification
    {
        Task<VerificationResult> StartVerificationAsync(string phoneNumber);

        Task<VerificationResult> CheckVerificationAsync(string phoneNumber, string code);
    }
}