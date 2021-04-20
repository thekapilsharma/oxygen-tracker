using Microsoft.Extensions.Configuration;
using oxygen_tracker.Constants;
using oxygen_tracker.Models;
using System;
using System.Threading.Tasks;
using Twilio;
using Twilio.Exceptions;
using Twilio.Rest.Verify.V2.Service;

namespace oxygen_tracker.Services
{
    public class Verification : IVerification
    {
        private readonly Configuration.Twilio twilioConfigurations;

        public Verification(IConfiguration configuration)
        {
            twilioConfigurations = configuration.GetSection("Twilio").Get<Configuration.Twilio>();
            TwilioClient.Init(twilioConfigurations.AccountSid, twilioConfigurations.AuthToken);
        }

        public async Task<VerificationResult> CheckVerificationAsync(string phoneNumber, string code)
        {
            try
            {
                var verificationCheckResource = await VerificationCheckResource.CreateAsync(
                    to: phoneNumber,
                    code: code,
                    pathServiceSid: twilioConfigurations.VerificationSid
                );
                return verificationCheckResource.Status.Equals("approved", StringComparison.Ordinal) ?
                    new VerificationResult(verificationCheckResource.Sid) :
                    new VerificationResult(DefaultValues.ErrorCodes.OtpIncorrect);
            }
            catch (TwilioException)
            {
                return new VerificationResult(DefaultValues.ErrorCodes.SomethingWentWrong);
            }
        }

        public async Task<VerificationResult> StartVerificationAsync(string phoneNumber)
        {
            try
            {
                var verificationResource = await VerificationResource.CreateAsync(
                    to: phoneNumber,
                    channel: twilioConfigurations.Channel,
                    pathServiceSid: twilioConfigurations.VerificationSid
                );
                return new VerificationResult(verificationResource.Sid);
            }
            catch (TwilioException)
            {
                return new VerificationResult(DefaultValues.ErrorCodes.SomethingWentWrong);
            }
        }
    }
}