using DbLayer;
using Models;


namespace Server_1
{
    public partial class Form1 : Form
    {
        Thread _thread;
        bool _disposed = false;

        public Form1()
        {
            InitializeComponent();
            var factory = new ProductsDbContextFactory();
            using (var db = factory.CreateDbContext(null))
            {

            }




        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            Server_lisengs server = new Server_lisengs();



            _thread = new Thread(server.Start);
            _thread.IsBackground = true;

            _thread.Start();

            //_thread.Abort();

            this.BackColor = Color.Green;
            button1.Enabled = false;

        }

        private void button2_Click(object sender, EventArgs e)
        {

        }
    }
}

