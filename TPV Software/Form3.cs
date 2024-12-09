using Microsoft.VisualBasic.ApplicationServices;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.OleDb;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TPV_Software
{
    public partial class Form3 : Form
    {
        private string connectionString = $"Provider=Microsoft.ACE.OLEDB.12.0;Data Source={Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "../../../../../", "Database1.accdb")};";
        private Boolean stock = false;
        private Boolean user = false;
        private Boolean reserva = false;
        public Form3()
        {
            InitializeComponent();
            dataGridView1.SelectionChanged += dataGridSeleccion;
            iniciareservas();
        }

        private void button1_Click(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {

        }

        private void button5_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
        private void RedondearBoton(Button btn)
        {

        }
        private void button2_Click(object sender, EventArgs e)
        {
            if (dataGridView1.CurrentRow != null)
            {
                try
                {
                    int id = Convert.ToInt32(dataGridView1.CurrentRow.Cells["ID"].Value);

                    // Confirmar la eliminación
                    DialogResult result = MessageBox.Show("¿Estás seguro de que deseas eliminar esta reserva?", "Confirmación", MessageBoxButtons.YesNo);
                    if (result == DialogResult.Yes)
                    {
                        // Eliminar la fila del DataGridView
                        dataGridView1.Rows.RemoveAt(dataGridView1.CurrentRow.Index);

                        // Eliminar la fila correspondiente en la base de datos
                        using (OleDbConnection connection = new OleDbConnection(connectionString))
                        {
                            connection.Open();
                            string query = "DELETE FROM reservas WHERE ID = ?";
                            using (OleDbCommand command = new OleDbCommand(query, connection))
                            {
                                command.Parameters.AddWithValue("?", id);
                                command.ExecuteNonQuery();
                            }
                        }

                        MessageBox.Show("Reserva eliminada correctamente.");
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error al eliminar la reserva: " + ex.Message);
                }
            }
            else
            {
                MessageBox.Show("Por favor, selecciona una fila para eliminar.");
            }
        }


        private void button1_Click_1(object sender, EventArgs e)
        {

            iniciareservas();


        }

        private void button3_Click_1(object sender, EventArgs e)
        {
            Form4 form4 = new Form4();
            form4.Show();
            this.Hide();



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
        public void iniciareservas()
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


        //Reservas Calendario
        private void monthCalendar1_DateChanged(object sender, DateRangeEventArgs e)
        {
            DateTime fechaSeleccionada = e.Start;
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

        private void monthCalendar1_DateChanged_1(object sender, DateRangeEventArgs e)
        {
            DateTime fechaSeleccionada = e.Start;
            MostrarReservasPorFecha(fechaSeleccionada);
        }

        private void button27_Click(object sender, EventArgs e)
        {
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
                            iniciareservas();
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: " + ex.Message);
                }

            }
        }

        private void button5_Click_1(object sender, EventArgs e)
        {
            Form1 form1 = new Form1();
            form1.Show();
            this.Hide();
        }

        private void button4_Click(object sender, EventArgs e)
        {

        }
    }
}
