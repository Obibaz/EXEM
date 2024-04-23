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
        private readonly string _serverAddress = "127.0.0.1"; // ������ �������
        private readonly int _port = 9002; // ���� �������

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
                MessageBox.Show("�� �� ����� �������� �����!", "������������", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            else if (string.IsNullOrWhiteSpace(passw))
            {
                MessageBox.Show("�� �� ����� �������� ������!", "������������", MessageBoxButtons.OK, MessageBoxIcon.Warning);
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

                    // ��������� ��'��� � JSON �� ����������� ���� �� ������
                    string jsonRequest = JsonConvert.SerializeObject(request);
                    byte[] requestData = Encoding.UTF8.GetBytes(jsonRequest);
                    ns.Write(requestData, 0, requestData.Length);

                    // �������� ������� �� �������
                    byte[] responseData = new byte[1024];
                    int bytesRead = ns.Read(responseData, 0, responseData.Length);
                    string jsonResponse = Encoding.UTF8.GetString(responseData, 0, bytesRead);

                    // ���������� �������
                    if (jsonResponse == "SUCCESS")
                    {
                        // ³��������� ����������� ��� ������ �����������
                        MessageBox.Show("�� ������ �����������!", "�����������", MessageBoxButtons.OK, MessageBoxIcon.Information);

                        // ��������� � ���������� ����� ����
                        var mySecondForm = new Form2();
                        mySecondForm.Show();
                        // ��������� ������� (�����) ����
                        this.Hide();
                    }
                    else if (jsonResponse == "SUCCESS1")
                    {
                        // ³��������� ����������� ��� ������ �����������
                        MessageBox.Show("�� ������ �����������!", "�����������", MessageBoxButtons.OK, MessageBoxIcon.Information);

                        // ��������� � ���������� ����� ����
                        var mySecondForm = new Admin.Form1();
                        mySecondForm.Show();
                        // ��������� ������� (�����) ����
                        this.Hide();
                    }
                    else
                    {
                        MessageBox.Show("����������� �� ��������!", "�����������", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"������� �� ��� �����������: {ex.Message}", "�������", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
