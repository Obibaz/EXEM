using System;
using System.Net.Sockets;
using System.Text;
using System.Windows.Forms;
using Client2;
using Models;
using Newtonsoft.Json;

namespace Test_Code_first_1
{
    public partial class Form1 : Form
    {
        private readonly string _serverAddress = "127.0.0.1"; // Адреса сервера
        private readonly int _port = 9002; // Порт сервера

        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string login = textBox1.Text;
            string passw = textBox2.Text;

            if (string.IsNullOrWhiteSpace(login))
            {
                MessageBox.Show("Ви не ввели значення логіну!", "Попередження", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            else if (string.IsNullOrWhiteSpace(passw))
            {
                MessageBox.Show("Ви не ввели значення Паролю!", "Попередження", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var request = new MyRequest()
            {
                Header = "Login",
               AuthUser = new User() { Login = login,Password = passw}, 
            };

            try
            {
                using (TcpClient client = new TcpClient(_serverAddress, _port))
                {
                    NetworkStream ns = client.GetStream();

                    // Серіалізуємо об'єкт в JSON та відправляємо його на сервер
                    string jsonRequest = JsonConvert.SerializeObject(request);
                    byte[] requestData = Encoding.UTF8.GetBytes(jsonRequest);
                    ns.Write(requestData, 0, requestData.Length);

                    // Отримуємо відповідь від сервера
                    byte[] responseData = new byte[1024];
                    int bytesRead = ns.Read(responseData, 0, responseData.Length);
                    string jsonResponse = Encoding.UTF8.GetString(responseData, 0, bytesRead);

                    // Обробляємо відповідь
                    if (jsonResponse == "SUCCESS")
                    {
                        // Відображаємо повідомлення про успішну авторизацію
                        MessageBox.Show("Ви успішно авторизовані!", "Повідомлення", MessageBoxButtons.OK, MessageBoxIcon.Information);

                        // Створюємо і відображаємо друге вікно
                        var mySecondForm = new Form2();
                        mySecondForm.Show();
                        // Закриваємо поточне (перше) вікно
                        this.Hide();
                    }
                    else if (jsonResponse == "SUCCESS1")
                    {
                        // Відображаємо повідомлення про успішну авторизацію
                        MessageBox.Show("Ви успішно авторизовані!", "Повідомлення", MessageBoxButtons.OK, MessageBoxIcon.Information);

                        // Створюємо і відображаємо друге вікно
                        var mySecondForm = new Admin.Form1();
                        mySecondForm.Show();
                        // Закриваємо поточне (перше) вікно
                        this.Hide();
                    }
                    else
                    {
                        MessageBox.Show("Користувача не знайдено!", "Повідомлення", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Помилка під час авторизації: {ex.Message}", "Помилка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
