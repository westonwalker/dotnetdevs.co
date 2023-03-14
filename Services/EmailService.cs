using dotnetdevs.Models;
using MailKit.Net.Smtp;
using MailKit;
using MimeKit;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;
using dotnetdevs.ViewModels;

namespace dotnetdevs.Services
{
    public class EmailService
    {
        private string _user;
        private string _password;
        private string _adminEmail;

        public EmailService()
        {
            _user = Environment.GetEnvironmentVariable("EMAIL_USER");
            _password = Environment.GetEnvironmentVariable("EMAIL_PASSWORD");
            _adminEmail = Environment.GetEnvironmentVariable("EMAIL_ADMIN");
        }

        public void SendAdminAlert(ApplicationUser user)
        {
            string body = $@"
                <!DOCTYPE html>
                <html>
                <head>
                    <meta charset=""UTF-8"">
                    <meta name=""viewport"" content=""width=device-width"" initial-scale=""1"">
                    <title>New user signed up</title>
                </head>
                <body>
                    <p style=""color:red"">New user signed up</p>
                    <p>Email: {user.Email}</p>
                </body>
                </html>
            ";
            SendAlert(_user, "Dotnet Devs - New user signup", body, _adminEmail);
        }

        public void SendAdminAlert(Developer developer, ApplicationUser user)
        {
            string body = $@"
                <!DOCTYPE html>
                <html>
                <head>
                    <meta charset=""UTF-8"">
                    <meta name=""viewport"" content=""width=device-width"" initial-scale=""1"">
                    <title>New developer profile</title>
                </head>
                <body>
                    <p style=""color:red"">New developer profile</p>
                    <p>Name: {developer.FullName}</p>
                    <p>Email: {user.Email}</p>
                    <p>Bio: {developer.Bio}</p>
                </body>
                </html>
            ";
            SendAlert(_user, "Dotnet Devs - New developer profile", body, _adminEmail);
        }

        public void SendSignupAdminAlert(CompanySignup company) {
            string body = $@"
                <!DOCTYPE html>
                <html>
                <head>
                    <meta charset=""UTF-8"">
                    <meta name=""viewport"" content=""width=device-width"" initial-scale=""1"">
                    <title>New company signuip</title>
                </head>
                <body>
                    <p style=""color:red"">New company signup</p>
                    <p>Email: {company.CompanyName}</p>
                    <p>Name: {company.PersonalName}</p>
                    <p>Email: {company.Email}</p>
                    <p>Email: {company.Website}</p>
                    <p>Bio: {company.Bio}</p>
                </body>
                </html>
            ";
            SendAlert(_user, "Dotnet Devs - New company signup", body, _adminEmail);
        }

        public void SendAdminAlert(Company company, ApplicationUser user)
        {
            string body = $@"
                <!DOCTYPE html>
                <html>
                <head>
                    <meta charset=""UTF-8"">
                    <meta name=""viewport"" content=""width=device-width"" initial-scale=""1"">
                    <title>New company profile</title>
                </head>
                <body>
                    <p style=""color:red"">New company profile</p>
                    <p>Company: {company.CompanyName}</p>
                    <p>Name: {company.PersonalName}</p>
                    <p>Title: {company.JobTitle}</p>
                    <p>Email: {user.Email}</p>
                    <p>Bio: {company.Bio}</p>
                </body>
                </html>
            ";
            SendAlert(_user, "Dotnet Devs - New company profile", body, _adminEmail);
        }

        public void SendWelcomeAlert(ApplicationUser user)
        {
            throw new NotImplementedException();
		}

		public void SendNewConversationAlert(string senderName, string recieverName, ApplicationUser user)
		{
			var firstName = getFirstName(recieverName);
			string body = $@"
                <!DOCTYPE html>
                <html>
                <head>
                    <meta charset=""UTF-8"">
                    <meta name=""viewport"" content=""width=device-width"" initial-scale=""1"">
                    <title>A company has messaged you on DotnetDevs!</title>
                </head>
                <body>
                    <p>Hi {firstName},</p>
                    <p>{senderName} is interested in hiring you.</p>
                    <p>View your <a href=""https://dotnetdevs.co/conversations"">conversations</a> to see what they said.</p>
                    <br>
                    <p>Weston Walker</p>
                    <p><a href=""mailto:wes@dotnetdevs.com"">wes@dotnetdevs.co</a></p>
                    <p>Founder of DotnetDevs</p>
                </body>
                </html>
            ";
			SendAlert(user.Email, $"A company has messaged you on DotnetDevs!", body, _user);
		}

		public void SendNewMessageAlert(string senderName, string recieverName, ApplicationUser user)
		{
			var firstName = getFirstName(recieverName);
			string body = $@"
                <!DOCTYPE html>
                <html>
                <head>
                    <meta charset=""UTF-8"">
                    <meta name=""viewport"" content=""width=device-width"" initial-scale=""1"">
                    <title>New message from {senderName} on DotnetDevs!</title>
                </head>
                <body>
                    <p>Hi {firstName},</p>
                    <p>{senderName} has responded to you on DotnetDevs.</p>
                    <p>View your <a href=""https://dotnetdevs.co/conversations"">conversations</a> to see what they said.</p>
                    <br>
                    <p>Weston Walker</p>
                    <p><a href=""mailto:wes@dotnetdevs.com"">wes@dotnetdevs.co</a></p>
                    <p>Founder of DotnetDevs</p>
                </body>
                </html>
            ";
			SendAlert(user.Email, $"New message from {senderName} on DotnetDevs!", body, _user);
		}

		public void SendWelcomeAlert(Developer developer, ApplicationUser user)
        {
            var firstName = getFirstName(developer.FullName);
            string body = $@"
                <!DOCTYPE html>
                <html>
                <head>
                    <meta charset=""UTF-8"">
                    <meta name=""viewport"" content=""width=device-width"" initial-scale=""1"">
                    <title>Welcome to DotnetDevs!</title>
                </head>
                <body>
                    <p>Hi {firstName},</p>
                    <p>I'm Wes, the founder of <a href=""https://dotnetdevs.co/"">DotnetDevs</a>. Thanks for adding your profile!</p>
                    <p>Here are some tips for increasing your chances of getting contacted by a company:</p>
                    <ul>
                        <li>Fill out as much of your profile as possible.</li>
                        <li>Use your bio to share your unique experience as a .NET developer.</li>
                        <li>This is your chance to sell yourself - brag a little!</li>
                    </ul>
                    <p>Reply to this email if you'd like help improving your profile or run into any issues using DotnetDevs.</p>
                    <br>
                    <p>Weston Walker</p>
                    <p><a href=""mailto:wes@dotnetdevs.com"">wes@dotnetdevs.co</a></p>
                    <p>Founder of DotnetDevs</p>
                </body>
                </html>
            ";
            SendAlert(user.Email, "Welcome to DotnetDevs!", body, _user);
        }

        public void SendWelcomeAlert(Company company, ApplicationUser user)
        {
            var firstName = getFirstName(company.PersonalName);
            string body = $@"
                <!DOCTYPE html>
                <html>
                <head>
                    <meta charset=""UTF-8"">
                    <meta name=""viewport"" content=""width=device-width"" initial-scale=""1"">
                    <title>Welcome to DotnetDevs!</title>
                </head>
                <body>
                    <p>Hi {firstName},</p>
                    <p>I'm Wes, the founder of <a href=""https://dotnetdevs.co/"">DotnetDevs</a>. I saw that you added your company and wanted to reach out.</p>
                    <p>Are you looking to recruit on behalf of other businesses or is {company.CompanyName} directly hiring?</p>
                    <br>
                    <p>Weston Walker</p>
                    <p><a href=""mailto:wes@dotnetdevs.com"">wes@dotnetdevs.co</a></p>
                    <p>Founder of DotnetDevs</p>
                </body>
                </html>
            ";
            SendAlert(user.Email, "Welcome to DotnetDevs!", body, _user);
        }

        private void SendAlert(string to, string subject, string body, string from)
        {
            var message = new MimeMessage();
            message.From.Add(new MailboxAddress("Wes from DotnetDevs", from));
            message.To.Add(new MailboxAddress("", to));
            message.Subject = subject;
            message.Body = new TextPart("html")
            {
                Text = body
            };

            using (var client = new SmtpClient())
            {
                client.Connect("smtp.gmail.com", 587, false);
                // Note: only needed if the SMTP server requires authentication
                client.Authenticate(_user, _password);
                client.Send(message);
                client.Disconnect(true);
            }
        }

        private string getFirstName(string fullname)
        {
            var names = fullname.Split(' ');
            string firstName = "";
            if (names.Length >= 1 )
            {
                firstName = names[0];
            }
            return firstName;
        }
    }
}
