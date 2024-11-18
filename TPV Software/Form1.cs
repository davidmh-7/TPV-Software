namespace TPV_Software
{
    public partial class Form1 : Form
    {

        private string usuario= "user";
        private string Admin= "admin";
        private string AdminContra= "admin";
        private string UserContra="12345";

        public Form1()
        {
            InitializeComponent();

        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            //Vista para el administrador
            string textboxlogin = textBox1.Text;
            string textboxloginpwd = textBox2.Text;

            if (textboxlogin == Admin && AdminContra == textboxloginpwd)
            {
                MessageBox.Show("Bienvenido Administrador");
                Form2 form2 = new Form2();
                form2.Show();
                this.Hide();
            }

            //Vista para el Usuario
            else if (textboxlogin == usuario && UserContra == textboxloginpwd)
            {
                MessageBox.Show("Bienvenido Usuario");
                Form3 form3 = new Form3();
                form3.Show();
                this.Hide();
            }
            else {
                label5.Visible = true;
            }
        }
    }
}
