using System;
using System.Data;
using System.Data.OleDb;
using System.IO;
using System.Windows.Forms;
using System.Threading.Tasks;

namespace TPV_Software
{
    public partial class Form4 : Form
    {
        private readonly string connectionString = $"Provider=Microsoft.ACE.OLEDB.12.0;Data Source={Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "../../../../../", "Database1.accdb")};";

        public Form4()
        {
            InitializeComponent();

            this.Load += Form4_Load;
            dataGridView1.CellClick += DataGridView1_CellClick;
            dataGridView2.CellContentClick += DataGridView2_CellContentClick;
        }

        private async void Form4_Load(object sender, EventArgs e)
        {
            await CargarProductosAsync();
            ConfigurarDataGridView2();
        }

        public async Task CargarProductosAsync()
        {
            try
            {
                using (OleDbConnection connection = new OleDbConnection(connectionString))
                {
                    await connection.OpenAsync();
                    string query = "SELECT Articulo, Cantidad, Importe FROM productos";

                    using (OleDbDataAdapter adapter = new OleDbDataAdapter(query, connection))
                    {
                        DataTable dataTable = new DataTable();
                        adapter.Fill(dataTable);

                        ConfigurarDataGridView1();

                        foreach (DataRow row in dataTable.Rows)
                        {
                            string nombreProducto = row["Articulo"]?.ToString() ?? "Producto desconocido";
                            string cantidad = row["Cantidad"]?.ToString() ?? "0";
                            string importe = row["Importe"]?.ToString() ?? "0.00";

                          
                            dataGridView1.Rows.Add("Seleccionar", nombreProducto, cantidad, importe);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar productos: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ConfigurarDataGridView1()
        {
            dataGridView1.Columns.Clear();
            dataGridView1.Rows.Clear();

            var buttonColumn = new DataGridViewButtonColumn
            {
                Name = "Seleccionar",
                HeaderText = "Seleccionar",
                Text = "Seleccionar",
                UseColumnTextForButtonValue = true
            };
            dataGridView1.Columns.Add(buttonColumn);

            dataGridView1.Columns.Add("Articulo", "Artículo");
            dataGridView1.Columns.Add("Cantidad", "Cantidad");
            dataGridView1.Columns.Add("Importe", "Precio (€)");
        }

        private void ConfigurarDataGridView2()
        {
            dataGridView2.Columns.Clear();
            dataGridView2.Rows.Clear();

            dataGridView2.Columns.Add("Articulo", "Artículo");
            dataGridView2.Columns.Add("Cantidad", "Cantidad");
            dataGridView2.Columns.Add("Importe", "Precio (€)");
            dataGridView2.Columns.Add("Total", "Total (€)");

            DataGridViewButtonColumn eliminarColumn = new DataGridViewButtonColumn
            {
                Name = "Eliminar",
                HeaderText = "Eliminar",
                Text = "Eliminar",
                UseColumnTextForButtonValue = true
            };
            dataGridView2.Columns.Add(eliminarColumn);
        }

        private void DataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            // Solo agrega el producto cuando se hace click
            if (e.RowIndex >= 0 && e.ColumnIndex == dataGridView1.Columns["Seleccionar"].Index)
            {
              
                string nombreProducto = dataGridView1.Rows[e.RowIndex].Cells["Articulo"].Value.ToString();
                string cantidad = dataGridView1.Rows[e.RowIndex].Cells["Cantidad"].Value.ToString();
                string importe = dataGridView1.Rows[e.RowIndex].Cells["Importe"].Value.ToString();

                AñadirADataGridView2(nombreProducto, cantidad, importe);
                ActualizarTotal();
            }
        }

        private void AñadirADataGridView2(string nombreProducto, string cantidad, string importe)
        {
            bool productoExistente = false;
            foreach (DataGridViewRow row in dataGridView2.Rows)
            {
                if (row.Cells["Articulo"].Value != null && row.Cells["Articulo"].Value.ToString() == nombreProducto)
                {
                    int cantidadActual = int.Parse(row.Cells["Cantidad"].Value.ToString());
                    int nuevaCantidad = cantidadActual + 1;
                    row.Cells["Cantidad"].Value = nuevaCantidad.ToString();

                    double precioUnitario = double.Parse(importe);
                    double nuevoTotal = nuevaCantidad * precioUnitario;
                    row.Cells["Total"].Value = nuevoTotal.ToString("F2");

                    productoExistente = true;
                    break;
                }
            }

            if (!productoExistente)
            {
                var total = double.Parse(importe);
                dataGridView2.Rows.Add(nombreProducto, "1", importe, total.ToString("F2"));
            }
        }


        private void ActualizarTotal()
        {
            double totalGeneral = 0;

            foreach (DataGridViewRow row in dataGridView2.Rows)
            {
                if (row.Cells["Total"].Value != null)
                {
                    totalGeneral += double.Parse(row.Cells["Total"].Value.ToString());
                }
            }

            label3.Text = $"Total: {totalGeneral:F2} €";
        }

        private void DataGridView2_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            // Eliminar fila del datagrid al hacer click
            if (e.RowIndex >= 0 && e.ColumnIndex == dataGridView2.Columns["Eliminar"].Index)
            {
                try
                {
                    dataGridView2.Rows.RemoveAt(e.RowIndex);
                    ActualizarTotal();
                }
                catch (Exception ex)
                {
                    
                    MessageBox.Show("Error al eliminar el producto: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Form3 form3 = new Form3();
            form3.Show();
            this.Hide();
        }

        private async void button1_Click(object sender, EventArgs e)
        {
            // Generar el ticket con los productos del datagrid
            string userName = Environment.UserName; 
            string path = @$"C:\Users\{userName}\Documents\TPV\";
            string fileName = "ticket.txt";
            string fullPath = Path.Combine(path, fileName);

            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }

            using (StreamWriter sw = new StreamWriter(fullPath))
            {
                sw.WriteLine("Ticket de compra");
                sw.WriteLine("Fecha: " + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));
                sw.WriteLine("-------------------------------");

                foreach (DataGridViewRow row in dataGridView2.Rows)
                {
                    if (row.Cells["Articulo"].Value != null)
                    {
                        string nombreProducto = row.Cells["Articulo"].Value.ToString();
                        string cantidad = row.Cells["Cantidad"].Value.ToString();
                        string importe = row.Cells["Importe"].Value.ToString();
                        string total = row.Cells["Total"].Value.ToString();

                        sw.WriteLine($"{nombreProducto} x{cantidad} - {total} €");
                    }
                }

                sw.WriteLine("-------------------------------");
                sw.WriteLine(label3.Text);
            }

            MessageBox.Show("Ticket generado correctamente", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);

            // Abrir el ticket generado
            System.Diagnostics.Process.Start("notepad.exe", fullPath);

            // Actualiza la cantidad en la base de datos
            try
            {
                using (OleDbConnection connection = new OleDbConnection(connectionString))
                {
                    await connection.OpenAsync();

                    foreach (DataGridViewRow row in dataGridView2.Rows)
                    {
                        if (row.Cells["Articulo"].Value != null)
                        {
                            string nombreProducto = row.Cells["Articulo"].Value.ToString();
                            int cantidadVendida = int.Parse(row.Cells["Cantidad"].Value.ToString());

                            string query = "UPDATE productos SET Cantidad = Cantidad - ? WHERE Articulo = ?";
                            using (OleDbCommand command = new OleDbCommand(query, connection))
                            {
                                command.Parameters.AddWithValue("?", cantidadVendida);
                                command.Parameters.AddWithValue("?", nombreProducto);

                                await command.ExecuteNonQueryAsync();
                            }
                        }
                    }
                }
                Form3 form3 = new Form3();
                form3.Show();
                this.Hide();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al actualizar la base de datos: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {
        
        }

        private void button3_Click(object sender, EventArgs e)
        {
            
        }
    }
}
