using System;
using System.Data;
using System.Data.OleDb;
using System.Windows.Forms;

namespace TPV_Software
{
    public partial class Form1 : Form
    {
        // Ruta de tu base de datos (actualízala con la ubicación real del archivo .accdb)
        private string connectionString = $"Provider=Microsoft.ACE.OLEDB.12.0;Data Source={Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "../../../../../", "Database1.accdb")};";

        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            // Obtener el texto de los TextBox
            string textboxlogin = textBox1.Text;
            string textboxloginpwd = textBox2.Text;

            // Llamar a la función de autenticación
            string rol = AutenticarUsuario(textboxlogin, textboxloginpwd);

            // Verificar el rol y abrir la vista correspondiente
            if (rol == "admin")
            {
                Form2 form2 = new Form2();
                form2.Show();
                this.Hide();
            }
            else if (rol == "usuario")
            {
                Form3 form3 = new Form3();
                form3.Show();
                this.Hide();
            }
            else
            {
                label5.Visible = true; 
            }
        }

        private string AutenticarUsuario(string username, string password)
        {
            string rol = null;
            try
            {
                using (OleDbConnection connection = new OleDbConnection(connectionString))
                {
                    connection.Open();
                    string query = "SELECT rol FROM usuarios WHERE user = @username AND password = @password";

                    using (OleDbCommand command = new OleDbCommand(query, connection))
                    {
                        // Parametrizar la consulta para evitar inyección SQL
                        command.Parameters.AddWithValue("@username", username);
                        command.Parameters.AddWithValue("@password", password);

                        // Ejecutar la consulta y obtener el rol
                        object result = command.ExecuteScalar();
                        if (result != null)
                        {
                            rol = result.ToString();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error de conexión: " + ex.Message);
            }
            return rol;
        }
        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
