using System.Net.Sockets;
using System.Text;
using Newtonsoft.Json;
using Models;

namespace Client2
{
    public partial class Form2 : Form
    {
        private readonly string _serverAddress = "127.0.0.1"; // Адреса сервера
        private readonly int _port = 9002; // Порт сервера
        private List<Quest> _list = new List<Quest>();
        private User _user = new User();
        private int _counts = 0;
        private int _max_counts;
        private string[] t_f;
        bool tmp = false;
        int point = 0;
        private int remainingTime;
      
        public Form2()
        {
            InitializeComponent();
         

        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (comboBox1.SelectedIndex != -1)
            {
                _list.Clear();
                button1.Enabled = false;
                var request = new MyRequest/////пердати скалність левел
                {
                    Header = "Ques",
                    quest = new Title_Ques { Title = comboBox1.Text }
                };



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


                    var tmp = JsonConvert.DeserializeObject<MyResponse>(jsonResponse);
                    foreach (var item in tmp.quests)
                    {
                        _list.Add(item);
                    }

                    _max_counts = tmp.quests.Count;
                    t_f = new string[_max_counts];

                    Zapoln();
                    Action();

                    button1.Enabled = false;
                    button2.Enabled = true;

                    this.Height = 400;
                    button2.Text = "Наступне питання";

                    remainingTime = _max_counts  * 60;
                    timer1.Start();

                }

            }

        }

        private void Form2_Load(object sender, EventArgs e)
        {
        

            textBox2.Visible = false;
            var request = new MyRequest
            {
                Header = "Title"
            };

            using (TcpClient client = new TcpClient(_serverAddress, _port))
            {
                NetworkStream ns = client.GetStream();


                string jsonRequest = JsonConvert.SerializeObject(request);
                byte[] requestData = Encoding.UTF8.GetBytes(jsonRequest);
                ns.Write(requestData, 0, requestData.Length);


                byte[] responseData = new byte[1024];
                int bytesRead = ns.Read(responseData, 0, responseData.Length);
                string jsonResponse = Encoding.UTF8.GetString(responseData, 0, bytesRead);


                var tmp = JsonConvert.DeserializeObject<MyResponse>(jsonResponse);
                foreach (var item in tmp.str)
                    if (!comboBox1.Items.Contains(item.ToString()))
                    {
                        comboBox1.Items.Add(item.ToString());
                    }

            }

        }

        private void button2_Click(object sender, EventArgs e)
        {
            Chekeds();
            if (_counts < _max_counts - 1)
            {
                _counts++;
                Zapoln();
                Action();
            }

            if (tmp)
            {
                for (int i = 0; i < _list.Count; i++)
                {
                    if (t_f[i]!=null)
                    if (_list[i].right == t_f[i].ToString())
                        point++;
                }
                //MessageBox.Show($"Ваш результат: {point}/{t_f.Length}");
                radioButton1.Enabled = false;
                radioButton2.Enabled = false;
                radioButton3.Enabled = false;
                radioButton4.Enabled = false;
                button2.Enabled = false;
                this.Height = 450;

               
                _list.Clear();

                
                tmp = false;
                
                button1.Enabled = true;

                timer1.Stop();

                MessageBox.Show($"Ваш результат: {point}/{t_f.Length} \n Час: {_max_counts * 60 - remainingTime}");
                t_f = null;
                _counts = 0;
                _max_counts = 0;
                point = 0;
            }

            if (_counts == _max_counts - 1)
            {
                button2.Text = "Закінчити Тест";
                tmp = true;
            }



        }

        private void button3_Click(object sender, EventArgs e)
        {
            string tmp = point.ToString();
            tmp += "/";
            tmp += _max_counts.ToString();

            Mail ml = new Mail(textBox1.Text, tmp);
        }

        void Action()
        {


            textBox2.Text = string.Empty;
            int tmp;
            bool ch = int.TryParse(_list[_counts].right, out tmp);
            if (ch)
            {
                label8.Visible = false;
                textBox2.Visible = false;
                label2.Visible = true;
                label3.Visible = true;
                label4.Visible = true;
                label5.Visible = true;
                radioButton1.Visible = true;
                radioButton2.Visible = true;
                radioButton3.Visible = true;
                radioButton4.Visible = true;
                radioButton1.Enabled = true;
                radioButton2.Enabled = true;
                radioButton3.Enabled = true;
                radioButton4.Enabled = true;
                radioButton1.Checked = false;
                radioButton2.Checked = false;
                radioButton3.Checked = false;
                radioButton4.Checked = false;

            }
            else
            {

                label8.Visible = true;
                textBox2.Visible = true;
                label2.Visible = false;
                label3.Visible = false;
                label4.Visible = false;
                label5.Visible = false;
                radioButton2.Visible = false;
                radioButton3.Visible = false;
                radioButton1.Visible = false;
                radioButton4.Visible = false;
            }
        }

        void Zapoln()
        {
            label1.Text = _list[_counts].Vopros.ToString();
            label2.Text = _list[_counts].Quests1.ToString();
            label3.Text = _list[_counts].Quests2.ToString();
            label4.Text = _list[_counts].Quests3.ToString();
            label5.Text = _list[_counts].Quests4.ToString();
        }

        void Chekeds()
        {
            int tmp;
            bool ch = int.TryParse(_list[_counts].right, out tmp);
            if (ch)
            {
                if (radioButton1.Checked || radioButton2.Checked || radioButton3.Checked || radioButton4.Checked)
                {

                    if (radioButton1.Checked)
                    {
                        t_f[_counts] = "1";
                    }
                    else if (radioButton2.Checked)
                    {
                        t_f[_counts] = "2";
                    }
                    else if (radioButton3.Checked)
                    {
                        t_f[_counts] = "3";
                    }
                    else if (radioButton4.Checked)
                    {
                        t_f[_counts] = "4";
                    }
                }
            }
            else
            {
                t_f[_counts] = textBox2.Text;
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            remainingTime--;


            TimeSpan timeSpan = TimeSpan.FromSeconds(remainingTime);
            label9.Text = timeSpan.ToString();

            
           
            label10.Text = (_counts+1).ToString();
            label10.Text += " з ";
            label10.Text += (_max_counts).ToString();



            // Перевірити, чи час вийшов
            if (remainingTime <= 0)
            {
                timer1.Stop();
                MessageBox.Show("Час вийшов!");
            }
        }
    }
}
