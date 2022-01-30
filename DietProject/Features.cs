using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace DietProject
{
    public partial class Features : Form
    {
        private SqlDataAdapter adapter;
        private DataTable FeaturesTable = new DataTable();

        public Features()
        {
            InitializeComponent();
        }

        private void FAddButton_Click(object sender, EventArgs e)
        {
            if (FeatureTextBox.Text == "")
            {
                ErrorForm ErrorForm = new ErrorForm();
                ErrorForm.ErrorLabel.Text = "Название признака продукта не может быть пустым.";
                ErrorForm.ShowDialog();
            }
            else
            {
                Program.sqlConnection.Open();
                SqlCommand cmd = new SqlCommand("INSERT INTO Features VALUES (N'" + FeatureTextBox.Text.ToString() + "');", Program.sqlConnection);
                cmd.ExecuteNonQuery();
                FeatureTextBox.Clear();
                FeaturesTable = new DataTable();
                FFeaturesListBox.DataSource = FeaturesTable;
                adapter = new SqlDataAdapter("SELECT * FROM Features", Program.sqlConnection);
                adapter.Fill(FeaturesTable);
                FFeaturesListBox.DataSource = FeaturesTable;
                FFeaturesListBox.DisplayMember = "Name";
                FFeaturesListBox.ValueMember = "Id";
                Program.sqlConnection.Close();
            }
        }

        private void FDeleteButton_Click(object sender, EventArgs e)
        {
            int choice = FFeaturesListBox.SelectedIndex;
            if (choice == -1)
            {
                ErrorForm ErrorForm = new ErrorForm();
                ErrorForm.ErrorLabel.Text = "Выберите признак продукта.";
                ErrorForm.ShowDialog();
            }
            else
            {
                DataRowView item = (DataRowView)FFeaturesListBox.SelectedItem;
                string nameToDelete = item.Row[1].ToString();
                if (nameToDelete == "стоимость за 1 кг продукта")
                {
                    ErrorForm ErrorForm = new ErrorForm();
                    ErrorForm.ErrorLabel.Text = "Признак стоимости является зафиксированным. Его удаление невозможно.";
                    ErrorForm.ShowDialog();
                }
                else
                {
                    Program.sqlConnection.Open();
                    SqlCommand cmd = new SqlCommand("DELETE FROM Features WHERE Name = N'" + nameToDelete + "';", Program.sqlConnection);
                    cmd.ExecuteNonQuery();
                    FeaturesTable = new DataTable();
                    FFeaturesListBox.DataSource = FeaturesTable;
                    adapter = new SqlDataAdapter("SELECT * FROM Features", Program.sqlConnection);
                    adapter.Fill(FeaturesTable);
                    FFeaturesListBox.DataSource = FeaturesTable;
                    FFeaturesListBox.DisplayMember = "Name";
                    FFeaturesListBox.ValueMember = "Id";
                    Program.sqlConnection.Close();
                }
            }
        }

        private void Features_Load(object sender, EventArgs e)
        {
            Program.sqlConnection.Open();
            SqlCommand cmd = new SqlCommand("SELECT COUNT(*) FROM Features WHERE Name = N'стоимость за 1 кг продукта';", Program.sqlConnection);
            int res = (int)cmd.ExecuteScalar();
            if (res == 0)
            {
                SqlCommand cm = new SqlCommand("INSERT INTO Features VALUES (N'стоимость за 1 кг продукта');", Program.sqlConnection);
                cm.ExecuteNonQuery();
            }
            adapter = new SqlDataAdapter("SELECT * FROM Features", Program.sqlConnection);
            adapter.Fill(FeaturesTable);
            FFeaturesListBox.DataSource = FeaturesTable;
            FFeaturesListBox.DisplayMember = "Name";
            FFeaturesListBox.ValueMember = "Id";
            Program.sqlConnection.Close();
        }
    }
}
