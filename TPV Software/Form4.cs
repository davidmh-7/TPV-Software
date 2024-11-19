using System;
using System.Data;
using System.Data.OleDb;
using System.Drawing;
using System.Net;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace TPV_Software
{
    public partial class Form4 : Form
    {
        private string connectionString = @"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=C:\Users\2dam3\Documents\Database1.accdb;";
        private ImageList imageList;

        // Ruta donde se guardarán las imágenes descargadas
        private string imageDirectory = @"C:\Users\2dam3\Documents\TPV_Images\";

        public Form4()
        {
            InitializeComponent();

            // Asegurarse de que el directorio exista
            if (!Directory.Exists(imageDirectory))
            {
                Directory.CreateDirectory(imageDirectory);
            }
        }

        private async void Form4_Load(object sender, EventArgs e)
        {
            // Cargar productos desde la base de datos
        
        }

        

        


        // Estructura para deserializar la respuesta JSON de la API
     

        
        private void button1_Click(object sender, EventArgs e)
        {
            // Acción para el botón de imprimir
            MessageBox.Show("Función de impresión aún no implementada.");
        }

        private void button2_Click(object sender, EventArgs e)
        {
            // Acción para abrir Form3
            Form3 form3 = new Form3();
            form3.Show();
            this.Hide();
        }

        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Acción cuando se selecciona un elemento en el ListView
            if (listView1.SelectedItems.Count > 0)
            {
                MessageBox.Show($"Seleccionado: {listView1.SelectedItems[0].Text}");
            }
        }
    }
}
