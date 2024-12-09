using Microsoft.Win32.SafeHandles;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.OleDb;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Reflection.Emit;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Aspose.Pdf;
using Aspose.Pdf.Text;



namespace TPV_Software
{
    public partial class Form2 : Form
    {
        private string connectionString = $"Provider=Microsoft.ACE.OLEDB.12.0;Data Source={Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "../../../../../", "Database1.accdb")};";
        private Boolean stock = false;
        private Boolean user = false;
        private Boolean reserva = false;
        public Form2()
        {
            InitializeComponent();
           
            iniciotablas();
            dataGridView1.SelectionChanged += dataGridSeleccion;
        }

        private void button5_Click(object sender, EventArgs e)
        {
            Form1 form1 = new Form1();
            form1.Show();
            this.Hide();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            ensenarUser();
        }
        private void RedondearBoton(Button btn)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            eliminarUserStock();

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void Form2_Load(object sender, EventArgs e)
        {

        }

        private void button3_Click_1(object sender, EventArgs e)
        {
            iniciotablas();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void button4_Click(object sender, EventArgs e)
        {
            ensenarReserva();
        }

        private void iniciotablas()
        {
            try
            {
                using (OleDbConnection connection = new OleDbConnection(connectionString))
                {
                    connection.Open();
                    string query = "SELECT * FROM productos";


                    using (OleDbDataAdapter adapter = new OleDbDataAdapter(query, connection))
                    {
                        DataTable dataTable = new DataTable();
                        adapter.Fill(dataTable);

                        // Muestra el DataTable en el DataGridView
                        dataGridView1.DataSource = dataTable;
                        user = false;
                        stock = true;
                        reserva = false;
                        label2.Text = "";
                        label5.Text = "";
                        label2.Text = "Precio";
                        label5.Text = "Cantidad";
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar productos: " + ex.Message);
            }
        }


        private void dataGridSeleccion(object sender, EventArgs e)
        {


            if (dataGridView1.CurrentRow != null && stock == true)
            {
                textBox4.Text = dataGridView1.CurrentRow.Cells["ID"].Value.ToString();
                textBox5.Text = dataGridView1.CurrentRow.Cells[1].Value.ToString();
                textBox3.Text = dataGridView1.CurrentRow.Cells[4].Value.ToString();
                textBox2.Text = dataGridView1.CurrentRow.Cells[2].Value.ToString();

            }

            if (dataGridView1.CurrentRow != null && user == true)
            {


                textBox4.Text = dataGridView1.CurrentRow.Cells["ID"].Value.ToString();
                textBox5.Text = dataGridView1.CurrentRow.Cells[1].Value.ToString();
                textBox3.Text = dataGridView1.CurrentRow.Cells[2].Value.ToString();
                textBox2.Text = dataGridView1.CurrentRow.Cells[3].Value.ToString();



            }
            if (dataGridView1.CurrentRow != null && reserva == true)
            {
                textBox4.Text = dataGridView1.CurrentRow.Cells["ID"].Value.ToString();
                textBox5.Text = dataGridView1.CurrentRow.Cells[1].Value.ToString();
                textBox3.Text = dataGridView1.CurrentRow.Cells[2].Value.ToString();
                textBox2.Text = dataGridView1.CurrentRow.Cells[3].Value.ToString();

            }


        }

        private void button27_Click(object sender, EventArgs e)
        {

            if (stock == true)
            {
                string id = textBox4.Text;
                string nombre = textBox5.Text;
                string precio = textBox3.Text;
                string cantidad = textBox2.Text;
                string impuesto = "21";

                try
                {
                    using (OleDbConnection connection = new OleDbConnection(connectionString))
                    {
                        connection.Open();

                        // Consulta para actualizar los datos
                        string updateQuery = "UPDATE productos SET Articulo = ?, Cantidad = ?, Impuesto = ?, Importe = ? WHERE Id = ?";

                        using (OleDbCommand command = new OleDbCommand(updateQuery, connection))
                        {
                            command.Parameters.AddWithValue("?", nombre);
                            command.Parameters.AddWithValue("?", cantidad);
                            command.Parameters.AddWithValue("?", impuesto);
                            command.Parameters.AddWithValue("?", precio);
                            command.Parameters.AddWithValue("?", id);

                            // Ejecutar la consulta de actualización
                            int rowsAffected = command.ExecuteNonQuery();

                            if (rowsAffected > 0)
                            {
                                MessageBox.Show("Producto actualizado correctamente.");
                            }
                            else
                            {
                                // Si no se encontró el producto, insertar uno nuevo
                                string insertQuery = "INSERT INTO productos (Id, Articulo, Cantidad, Impuesto, Importe) VALUES (?, ?, ?, ?, ?)";

                                using (OleDbCommand insertCommand = new OleDbCommand(insertQuery, connection))
                                {
                                    insertCommand.Parameters.AddWithValue("?", id);
                                    insertCommand.Parameters.AddWithValue("?", nombre);
                                    insertCommand.Parameters.AddWithValue("?", cantidad);
                                    insertCommand.Parameters.AddWithValue("?", impuesto);
                                    insertCommand.Parameters.AddWithValue("?", precio);

                                    insertCommand.ExecuteNonQuery();
                                    MessageBox.Show("Producto insertado correctamente.");
                                }
                            }
                            iniciotablas();
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: " + ex.Message);
                }


            }

            if (user == true)
            {
                string id = textBox4.Text;
                string user = textBox5.Text;
                string password = textBox3.Text;
                string rol = textBox2.Text;
                string vacio = "21";

                try
                {
                    using (OleDbConnection connection = new OleDbConnection(connectionString))
                    {
                        connection.Open();

                        // Consulta de actualización
                        string updateQuery = "UPDATE usuarios SET [user] = ?, [password] = ?, rol = ?, vacio = ? WHERE Id = ?";

                        using (OleDbCommand command = new OleDbCommand(updateQuery, connection))
                        {
                            command.Parameters.AddWithValue("?", user);
                            command.Parameters.AddWithValue("?", password);
                            command.Parameters.AddWithValue("?", rol);
                            command.Parameters.AddWithValue("?", vacio);
                            command.Parameters.AddWithValue("?", id);

                            // Ejecutar la consulta de actualización
                            int rowsAffected = command.ExecuteNonQuery();

                            if (rowsAffected > 0)
                            {
                                MessageBox.Show("Usuario actualizado correctamente.");
                            }
                            else
                            {
                                // Si no se encontró el usuario, se inserta uno nuevo
                                string insertQuery = "INSERT INTO usuarios (Id, [user], [password], rol, vacio) VALUES (?, ?, ?, ?, ?)";

                                using (OleDbCommand insertCommand = new OleDbCommand(insertQuery, connection))
                                {
                                    insertCommand.Parameters.AddWithValue("?", id);
                                    insertCommand.Parameters.AddWithValue("?", user);
                                    insertCommand.Parameters.AddWithValue("?", password);
                                    insertCommand.Parameters.AddWithValue("?", rol);
                                    insertCommand.Parameters.AddWithValue("?", vacio);

                                    insertCommand.ExecuteNonQuery();
                                    MessageBox.Show("Usuario insertado correctamente.");
                                }
                            }
                            ensenarUser();
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: " + ex.Message);
                }


            }

            if (reserva == true)
            {
                string id = textBox4.Text;
                string nombre = textBox5.Text;
                string mesa = textBox3.Text;
                string numero = textBox2.Text;
                string Vacio = dataGridView1.CurrentRow.Cells[4].Value.ToString();

                try
                {
                    using (OleDbConnection connection = new OleDbConnection(connectionString))
                    {
                        connection.Open();

                        // Consulta de actualización
                        string updateQuery = "UPDATE reservas SET Nombre = ?, Mesa = ?, Numero = ?, Fecha = ? WHERE Id = ?";

                        using (OleDbCommand command = new OleDbCommand(updateQuery, connection))
                        {
                            command.Parameters.AddWithValue("?", nombre);
                            command.Parameters.AddWithValue("?", mesa);
                            command.Parameters.AddWithValue("?", numero);
                            command.Parameters.AddWithValue("?", Vacio);
                            command.Parameters.AddWithValue("?", id);

                            // Ejecutar la consulta de actualización
                            int rowsAffected = command.ExecuteNonQuery();

                            if (rowsAffected > 0)
                            {
                                MessageBox.Show("Reserva actualizada correctamente.");
                            }
                            else
                            {
                                // Si no se encontró la reserva, se inserta una nueva
                                string insertQuery = "INSERT INTO reservas (Id, Nombre, Mesa, Numero, Fecha) VALUES (?, ?, ?, ?, ?)";

                                using (OleDbCommand insertCommand = new OleDbCommand(insertQuery, connection))
                                {
                                    insertCommand.Parameters.AddWithValue("?", id);
                                    insertCommand.Parameters.AddWithValue("?", nombre);
                                    insertCommand.Parameters.AddWithValue("?", mesa);
                                    insertCommand.Parameters.AddWithValue("?", numero);
                                    insertCommand.Parameters.AddWithValue("?", Vacio);

                                    insertCommand.ExecuteNonQuery();
                                    MessageBox.Show("Reserva insertada correctamente.");
                                }
                            }
                            ensenarReserva();
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: " + ex.Message);
                }

            }


        }


        public void ensenarReserva()
        {
            try
            {
                using (OleDbConnection connection = new OleDbConnection(connectionString))
                {
                    connection.Open();
                    string query = "SELECT * FROM reservas";


                    using (OleDbDataAdapter adapter = new OleDbDataAdapter(query, connection))
                    {
                        DataTable dataTable = new DataTable();
                        adapter.Fill(dataTable);

                        // Muestra el DataTable en el DataGridView
                        dataGridView1.DataSource = dataTable;

                        user = false;
                        stock = false;
                        reserva = true;
                        label2.Text = "";
                        label5.Text = "";
                        label2.Text = "Mesa";
                        label5.Text = "Nº invitados";
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar productos: " + ex.Message);
            }

        }

        public void ensenarUser()
        {
            try
            {
                using (OleDbConnection connection = new OleDbConnection(connectionString))
                {
                    connection.Open();
                    string query = "SELECT * FROM usuarios";


                    using (OleDbDataAdapter adapter = new OleDbDataAdapter(query, connection))
                    {
                        DataTable dataTable = new DataTable();
                        adapter.Fill(dataTable);

                        // Muestra el DataTable en el DataGridView
                        dataGridView1.DataSource = dataTable;
                        user = true;
                        stock = false;
                        reserva = false;
                        label2.Text = "";
                        label5.Text = "";
                        label2.Text = "Password";
                        label5.Text = "Rol";
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar productos: " + ex.Message);
            }

        }




        public void eliminarUserStock()
        {

            string id = textBox4.Text;
            string tableName = stock ? "productos" : user ? "usuarios" : "reservas";
            string idColumnName = "Id";

            try
            {
                using (OleDbConnection connection = new OleDbConnection(connectionString))
                {
                    connection.Open();
                    string deleteQuery = $"DELETE FROM {tableName} WHERE {idColumnName} = ?";

                    using (OleDbCommand command = new OleDbCommand(deleteQuery, connection))
                    {
                        command.Parameters.AddWithValue("?", id);
                        int rowsAffected = command.ExecuteNonQuery();

                        if (rowsAffected > 0)
                        {
                            MessageBox.Show($"Eliminado correctamente.");
                            if (stock)
                            {
                                iniciotablas();
                            }
                            if (user)
                            {
                                ensenarUser();
                            }
                            if (reserva)
                            {
                                ensenarReserva();
                            }

                        }
                        else
                        {
                            MessageBox.Show("No se encontró el registro para eliminar.");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }




        }

        private void monthCalendar1_DateChanged(object sender, DateRangeEventArgs e)
        {
            DateTime fechaSeleccionada = e.Start; // Obtiene la fecha seleccionada en el calendario
            MostrarReservasPorFecha(fechaSeleccionada);
        }

        // Método para mostrar las reservas de una fecha específica
        private void MostrarReservasPorFecha(DateTime fecha)
        {
            if (reserva == true)
            {

                try
                {
                    using (OleDbConnection connection = new OleDbConnection(connectionString))
                    {
                        connection.Open();
                        string fechaFormatoString = fecha.ToString("dd/MM/yyyy");

                        string query = "SELECT * FROM reservas WHERE Fecha = ?";

                        using (OleDbDataAdapter adapter = new OleDbDataAdapter(query, connection))
                        {
                            adapter.SelectCommand.Parameters.AddWithValue("?", fechaFormatoString);

                            DataTable dataTable = new DataTable();
                            adapter.Fill(dataTable);


                            dataGridView1.DataSource = dataTable;
                            reserva = true;
                            user = false;
                            stock = false;
                            label2.Text = "Mesa";
                            label5.Text = "Nº invitados";
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error al cargar reservas: " + ex.Message);
                }
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            TransferirDatosAInforme();
            GenerarInformeStock();

        }

        private void TransferirDatosAInforme()
        {
            try
            {
                using (OleDbConnection connection = new OleDbConnection(connectionString))
                {
                    connection.Open();

                    // Eliminar datos existentes en la tabla informe
                    string deleteQuery = "DELETE FROM informe";
                    using (OleDbCommand deleteCommand = new OleDbCommand(deleteQuery, connection))
                    {
                        deleteCommand.ExecuteNonQuery();
                    }

                    // Insertar datos de la tabla productos en la tabla informe
                    string insertQuery = @"
                        INSERT INTO informe (id, articulo, cantidad_disponible, precio, mínimo_disponible)
                        SELECT Id, Articulo, Cantidad, Importe, 10 FROM productos";

                    using (OleDbCommand insertCommand = new OleDbCommand(insertQuery, connection))
                    {
                        insertCommand.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al transferir datos: " + ex.Message);
            }
        }


        
     private void GenerarInformeStock()
        {
            try
            {
                TransferirDatosAInforme();

                using (OleDbConnection connection = new OleDbConnection(connectionString))
                {
                    connection.Open();
                    string query = "SELECT * FROM informe";

                    using (OleDbDataAdapter adapter = new OleDbDataAdapter(query, connection))
                    {
                        DataTable dataTable = new DataTable();
                        adapter.Fill(dataTable);

                        Aspose.Pdf.Document document = new Aspose.Pdf.Document();

                        string pdfPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "../../../../../", "InformeStock.pdf");

                        Page page = document.Pages.Add();
                   
                        TextFragment title = new TextFragment("Informe de Stock Disponible");
                        title.TextState.FontSize = 18;
                        title.TextState.FontStyle = FontStyles.Bold;
                        title.HorizontalAlignment = Aspose.Pdf.HorizontalAlignment.Center;
                        page.Paragraphs.Add(title);

                        page.Paragraphs.Add(new TextFragment(" "));

                        string header = $"{"ID",-5} {"Artículo",-11} {"Cantidad Disponible",-15} {"Precio",-10} {"Mínimo Disponible",-10}";
                        TextFragment headerFragment = new TextFragment(header);
                        headerFragment.TextState.FontStyle = FontStyles.Bold;
                        headerFragment.TextState.Font = FontRepository.FindFont("Courier New");
                        page.Paragraphs.Add(headerFragment);

                        TextFragment separator = new TextFragment(new string('-', 69));
                        separator.TextState.Font = FontRepository.FindFont("Courier New"); 
                        page.Paragraphs.Add(separator);

                        foreach (DataRow row in dataTable.Rows)
                        {
                            string line = $"{row["id"],-5} {row["articulo"],-12} {row["cantidad_disponible"],-19} {row["precio"],-13} {row["mínimo_disponible"],-20}";
                            TextFragment textFragment = new TextFragment(line);
                            textFragment.TextState.Font = FontRepository.FindFont("Courier New"); 

                            //cambio el color
                            if (Convert.ToInt32(row["cantidad_disponible"]) < Convert.ToInt32(row["mínimo_disponible"]))
                            {
                                textFragment.TextState.ForegroundColor = Aspose.Pdf.Color.Red;
                            }
                            page.Paragraphs.Add(textFragment);
                        }


                        document.Save(pdfPath);

                        MessageBox.Show("Informe de stock generado correctamente en " + pdfPath);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al generar el informe: " + ex.Message);
            }
        }




    }
}
