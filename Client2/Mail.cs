using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace Client2
{
    internal class Mail
    {
        private MailAddress _from;
        private MailAddress _to;
        private MailMessage _message;

        private int port;
        private string server;

        private string login;
        private string passw;
        private SmtpClient _client;

        public Mail(string mails, string rez) 
        {
            port = 587;
            server = "sandbox.smtp.mailtrap.io";
            //server = "smtp.mailgun.org";    //test
            login = "f9ab07e3ec6c49";
            //login = "postmaster@sandbox56702d7536d448e7918bbcb336b3ba4f.mailgun.org";     //test
            passw = "cd9bf5ae8331c3";
            //passw = "1c28f1d67337d9284792e172fb23266f-4b670513-c8fde9d8";    //test
            _client = new SmtpClient(server, port);
            _client.Credentials = new NetworkCredential(login, passw);
            _client.EnableSsl = true; /// шифрування 
            _from = new MailAddress("my_email@ukr.net");
            string to = mails;
            string subfect = "Результати Вашого тесту!";
            string message = rez;

            if (string.IsNullOrEmpty(to) ||
                string.IsNullOrEmpty(subfect) ||
                string.IsNullOrEmpty(message))
            {
                MessageBox.Show("Заповініть!");
            }
            else
            {
                try
                {
                    _to = new MailAddress(to);
                    _message = new MailMessage(_from, _to);
                    _message.Subject = subfect;
                    _message.Body = message;
                    _client.Send(_message);
                }
                catch (Exception ex)
                {

                    MessageBox.Show(ex.Message);
                }
                

            }
        }
    }
}
