using System;
using OtpNet;

namespace OTP
{
    public class OTP
    {
        private const string userId = "UserIdabcd";

        private Totp totp;

        public OTP()
        {
            var bytes = Base32Encoding.ToBytes(userId);
            totp = new Totp(bytes, step: 30);
        }
        public string GenarateTOTP()
        {
            return totp.ComputeTotp(DateTime.UtcNow);
        }

        public void ValidateOTP(string result, string input)
        {

            var window = new VerificationWindow(previous: 1, future: 1);
            bool verify = totp.VerifyTotp(input, out long timeWindowUsed, window: window);

            //Console.WriteLine("{0}-:{1}", "timeStepMatched", timeWindowUsed);
            Console.WriteLine("{0}-:{1}", "Remaining seconds", totp.RemainingSeconds());
            Console.WriteLine("{0}-:{1}", "verify", verify);
        }

        public static void Main(string[] args)
        {
            OTP otp1 = new OTP();
            string otp = otp1.GenarateTOTP();

            // Get the current time
            DateTime currentTime = DateTime.Now;
            Console.WriteLine("User Id is : {0}", userId);
            Console.WriteLine("Current Date: {0}", currentTime);
            Console.WriteLine("One time password is : {0}", otp);
            Console.WriteLine("Insert one time password for validation :");

            var input = Console.ReadLine();
            if (input == null)
            {
                Console.WriteLine("Please insert a valid input");
                return;
            }
            otp1.ValidateOTP(otp, input);
        }
    }
}
