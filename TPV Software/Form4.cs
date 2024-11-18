using System;
using System.Data;
using System.Data.OleDb;
using System.Drawing;
using System.Net;
using System.Threading.Tasks;
using System.Windows.Forms;
using Newtonsoft.Json;
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
            await LoadProductosAsync();
        }

        private async Task LoadProductosAsync()
        {
            // Obtener los productos de la base de datos
            string query = "SELECT Articulo FROM productos";
            using (OleDbConnection connection = new OleDbConnection(connectionString))
            {
                OleDbDataAdapter adapter = new OleDbDataAdapter(query, connection);
                DataTable dt = new DataTable();
                adapter.Fill(dt);

                // Recorrer cada producto
                foreach (DataRow row in dt.Rows)
                {
                    string articulo = row["Articulo"].ToString();
                    string imagePath = await GetImageForArticuloAsync(articulo);

                    // Mostrar la imagen en el ListView o hacer lo que necesites con ella
                    // Aquí es donde se agregarían los elementos al ListView
                    // listView1.Items.Add(new ListViewItem { Text = articulo, ImageKey = imagePath });
                }
            }
        }

        private async Task<string> GetImageForArticuloAsync(string articulo)
        {
            string imagePath = Path.Combine(imageDirectory, $"{articulo}.jpg");

            // Si la imagen ya está guardada localmente, no hacer la petición
            if (File.Exists(imagePath))
            {
                return imagePath;
            }

            // Si no existe, hacer la solicitud a la API de Google Custom Search
            string apiUrl = $"https://www.googleapis.com/customsearch/v1?key=AIzaSyCOLFoNrBZ8ne11BY-jm9tKvPUizjcIbU8&cx=81c5064808165442e&q={articulo}&searchType=image&num=1";
            using (WebClient client = new WebClient())
            {
                string response = await client.DownloadStringTaskAsync(apiUrl);
                var searchResults = JsonConvert.DeserializeObject<GoogleSearchResponse>(response);

                if (searchResults.items != null && searchResults.items.Count > 0)
                {
                    string imageUrl = searchResults.items[0].link;

                    // Descargar la imagen y guardarla localmente
                    await DownloadImageAsync(imageUrl, imagePath);
                    return imagePath;
                }
                else
                {
                    // Si no hay imagen, devolver una imagen predeterminada
                    return Path.Combine(imageDirectory, "default.jpg");
                }
            }
        }

        private async Task DownloadImageAsync(string imageUrl, string imagePath)
        {
            using (WebClient client = new WebClient())
            {
                await client.DownloadFileTaskAsync(new Uri(imageUrl), imagePath);
            }
        }

        // Estructura para deserializar la respuesta JSON de la API
        public class GoogleSearchResponse
        {
            public List<SearchItem> items { get; set; }
        }

        public class SearchItem
        {
            public string link { get; set; }
        }
        
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
