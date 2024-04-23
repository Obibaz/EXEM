using Azure.Core;
using Models;
using Newtonsoft.Json;
using System.Net.Sockets;
using System.Text;
using System.Windows.Forms;

namespace Admin
{
    public partial class Form1 : Form
    {

        private readonly string _serverAddress = "127.0.0.1"; // Адреса сервера
        private readonly int _port = 9002; // Порт сервера
        private List<Quest> _qw;
        public Form1()
        {
            InitializeComponent();
        }


        private void button1_Click(object sender, EventArgs e)
        {
            _qw.Clear();
            listBox1.Items.Clear();

            var request = new MyRequest
            {
                Header = "Ques",
                quest = new Title_Ques { Title = comboBox1.SelectedItem.ToString() }
            };

                string jsonResponse = TcpClass.Zapros(request);
                var tmp = JsonConvert.DeserializeObject<MyResponse>(jsonResponse);
                _qw = tmp.quests;

                foreach (var item in tmp.quests)
                listBox1.Items.Add(item.Vopros);
        }

        private void button2_Click(object sender, EventArgs e)
        {

            if (listBox1.SelectedIndex != -1)
            {
                _qw[listBox1.SelectedIndex] = new Quest()
                {
                    Id = _qw[listBox1.SelectedIndex].Id,
                    Vopros = textBox1.Text,
                    Quests1 = textBox2.Text,
                    Quests2 = textBox3.Text,
                    Quests3 = textBox4.Text,
                    Quests4 = textBox5.Text,
                    right = textBox6.Text
                };

                var request = new MyRequest
                {
                    Header = "CHANGE",
                    chquest = _qw
                };
                TcpClass.Zapros(request);
               
                this.button1_Click((object)sender, e);
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.button1_Click((object)sender, e);
            textBox1.Text = null;
            textBox2.Text = null;
            textBox3.Text = null;
            textBox4.Text = null;
            textBox5.Text = null;
            textBox6.Text = null;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            _qw = new List<Quest>();
            _qw.Clear();
            comboBox1.Items.Clear();
            listBox1.ClearSelected();

            var request = new MyRequest/////пердати скалність левел
            {
                Header = "Title"
            };

            string jsonResponse = TcpClass.Zapros(request);

            var tmp = JsonConvert.DeserializeObject<MyResponse>(jsonResponse);

            foreach (var item in tmp.str)
                if (!comboBox1.Items.Contains(item.ToString()))
                    comboBox1.Items.Add(item.ToString());
            
            if(comboBox1.Items.Count != 0) 
            {
                comboBox1.SelectedIndex = 0;
                //if(listBox1.Items.Count != 0)
                //listBox1.SelectedIndex = 0;
            }

        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            ///////тут вилітає прога при додаванні теми -------------РОЗІБРАТИСЯ 
            if (listBox1.SelectedIndex == -1) return;
            textBox1.Text = _qw[listBox1.SelectedIndex].Vopros;
            textBox2.Text = _qw[listBox1.SelectedIndex].Quests1;
            textBox3.Text = _qw[listBox1.SelectedIndex].Quests2;
            textBox4.Text = _qw[listBox1.SelectedIndex].Quests3;
            textBox5.Text = _qw[listBox1.SelectedIndex].Quests4;
            textBox6.Text = _qw[listBox1.SelectedIndex].right;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (textBox1.Text == string.Empty || textBox6.Text==string.Empty)
            {
                MessageBox.Show("Заповінть поля");
                return;
            }

            Title_Ques tmp = new Title_Ques()
            {
                Title = comboBox1.Text,
                Quest1 = new List<Quest>()
            };

            tmp.Quest1.Add(new Quest()
            {
                Vopros = textBox1.Text,
                Quests1 = textBox2.Text,
                Quests2 = textBox3.Text,
                Quests3 = textBox4.Text,
                Quests4 = textBox5.Text,
                right = textBox6.Text
            });

            var request = new MyRequest
            {
                Header = "ADD",

                quest = tmp
            };

            TcpClass.Zapros(request);
            this.button1_Click((object)sender, e);
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            _qw.Clear();
            listBox1.Items.Clear();

            button1.Enabled = true;
            button2.Enabled = true;
            button3.Enabled = true;
            button4.Enabled = true;
            button5.Enabled = true;

            if (comboBox1.Items.Count == 0) return;
            var request = new MyRequest
            {
                Header = "Ques",
                quest = new Title_Ques { Title = comboBox1.SelectedItem?.ToString() ?? "NULL" }
            };


                    string jsonResponse = TcpClass.Zapros(request);
                    var tmp = JsonConvert.DeserializeObject<MyResponse>(jsonResponse);
                    _qw = tmp.quests;

                    foreach (var item in tmp.quests)
                        listBox1.Items.Add(item.Vopros);
        }

        private void button5_Click_1(object sender, EventArgs e)
        {
            if (listBox1.SelectedIndex == -1) return;
            var tmp = new Quest()
            {
                Id = _qw[listBox1.SelectedIndex].Id
            };

            List<Quest> tmpq = new List<Quest>();
            tmpq.Add(tmp);

            var request = new MyRequest
            {
                Header = "DEL",
                chquest = tmpq

            };

           TcpClass.Zapros(request);
            this.button1_Click((object)sender, e);
        }

        private void button6_Click(object sender, EventArgs e)
        {
            string userInput = InputBox.Show("Додавання боксу питань", "Введіть назву теми:");
            if (userInput == null) return;
            Title_Ques tmp = new Title_Ques()
            {
                Title = userInput
            };

            var request = new MyRequest
            {
                Header = "ADD_TITLE",
                quest = tmp
            };

            TcpClass.Zapros(request);
            this.Form1_Load((object)sender, e);
            //this.comboBox1_SelectedIndexChanged((object)sender, e);
        }
    }
}
