using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Threading.Tasks;
using Twilio;
using Twilio.Rest.Api.V2010.Account;
using Twilio.Types;

namespace Employee_Travel_Booking_System_WebApplication.Services
{
    public class SmsService
    {
        private readonly string _accountSid;
        private readonly string _authToken;
        private readonly string _fromNumber;

        public SmsService(string accountSid, string authToken, string fromNumber)
        {
            _accountSid = accountSid;
            _authToken = authToken;
            _fromNumber = fromNumber;
        }

        public async Task SendSmsAsync(string to, string message)
        {
            try
            {
                // Initialize Twilio client
                TwilioClient.Init(_accountSid, _authToken);

                // Prepare message options
                var messageOptions = new CreateMessageOptions(new PhoneNumber(to))
                {
                    From = new PhoneNumber(_fromNumber),
                    Body = message
                };

                // Send message
                var sentMessage = await MessageResource.CreateAsync(messageOptions);

                // Log message SID for reference
                Console.WriteLine($"Message sent with SID: {sentMessage.Sid}");
            }
            catch (Exception ex)
            {
                // Log exception details
                Console.WriteLine($"An error occurred: {ex.Message}");
            }
        }
    }
}