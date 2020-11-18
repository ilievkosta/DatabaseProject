using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SQLite;
using System.Windows.Forms;

namespace DatabaseProject
{
    public partial class Form1 : Form
    {
        public void LoadData()
        {
            string cs = @"URI=file:C:\Users\Game\source\repos\DatabaseProject\Coordinates.db";

            SQLiteConnection con = new SQLiteConnection(cs);
            con.Open();
            string stm = "SELECT Id,Name,Job,Description,Cord FROM Cord";

            SQLiteCommand cmd = new SQLiteCommand(stm, con);
            SQLiteDataReader rdr = cmd.ExecuteReader();

            while (rdr.Read())
            {
                databaseGritBindingSource.Add(new DatabaseGrit() {Id=rdr.GetInt32(0), Name = rdr.GetString(1), Title = rdr.GetString(2), Description = "Danni", Text = rdr.GetString(3) });

            }
        }
        protected override void OnLoad(EventArgs e)
        {
            LoadData();
            
        }

                public Form1()
        {
            InitializeComponent();
           
        }

        private void buttonToDatabase_Click(object sender, EventArgs e)
        {
            string cs = @"URI=file:C:\Users\Game\source\repos\DatabaseProject\Coordinates.db";

            SQLiteConnection con = new SQLiteConnection(cs);
            con.Open();

            SQLiteCommand cmd = new SQLiteCommand(con);
            string Name = textBoxName.Text;
            string Job = textBoxJobTitle.Text;
            string Cord = richTextBox.Text;
            string Description = textBoxDescription.Text;


            cmd.CommandText = "INSERT INTO Cord(Name, Job, Cord, Description) VALUES(@Name,@Job,@Cord,@Description)";
            cmd.Parameters.AddWithValue("@Name", Name);
            cmd.Parameters.AddWithValue("@Job", Job);
            cmd.Parameters.AddWithValue("@Cord", Cord);
            cmd.Parameters.AddWithValue("@Description", Description);
            cmd.Prepare();

            cmd.ExecuteNonQuery();
            dataGridView1.Rows.Clear();
            dataGridView1.Refresh();
            LoadData();
        }

        

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void labelDescription_Click(object sender, EventArgs e)
        {

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dataGridView1.Columns[e.ColumnIndex].Name == "Delete" & dataGridView1.Rows.Count > 1)
            {
                if (MessageBox.Show("Are you sure", "Message", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    

                    if (dataGridView1.CurrentRow != null)
                    {
                        string cs = @"URI=file:C:\Users\Game\source\repos\DatabaseProject\Coordinates.db";

                        SQLiteConnection con = new SQLiteConnection(cs);
                        con.Open();

                        SQLiteCommand cmd = new SQLiteCommand(con);
                        cmd.CommandText = "DELETE FROM Cord WHERE Id=@Id";
                        int Id = (int)dataGridView1.CurrentRow.Cells[0].Value;
                        cmd.Parameters.AddWithValue("@Id", Id);
                        cmd.Prepare();

                        cmd.ExecuteNonQuery();
                    }
                    databaseGritBindingSource.RemoveCurrent();

                }
            }
        }
    }
}
