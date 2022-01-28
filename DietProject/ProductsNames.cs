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
    public partial class ProductsNames : Form
    {
        private SqlDataAdapter adapter;
        private DataTable ProductsNamesTable = new DataTable();

        public ProductsNames()
        {
            InitializeComponent();
        }

        private void PNAddButton_Click(object sender, EventArgs e)
        {
            if (ProductNameTextBox.Text == "")
            {
                ErrorForm ErrorForm = new ErrorForm();
                ErrorForm.ErrorLabel.Text = "Название продукта не может быть пустым.";
                ErrorForm.ShowDialog();
            }
            else
            {
                Program.sqlConnection.Open();
                SqlCommand cmd = new SqlCommand("INSERT INTO ProductsNames VALUES (N'" + ProductNameTextBox.Text.ToString() + "');", Program.sqlConnection);
                cmd.ExecuteNonQuery();
                Program.sqlConnection.Close();
                ProductNameTextBox.Clear();
                ProductsNamesTable = new DataTable();
                PNProductsNamesListBox.DataSource = ProductsNamesTable;
                Program.sqlConnection.Open();
                adapter = new SqlDataAdapter("SELECT * FROM ProductsNames", Program.sqlConnection);
                adapter.Fill(ProductsNamesTable);
                PNProductsNamesListBox.DataSource = ProductsNamesTable;
                PNProductsNamesListBox.DisplayMember = "Name";
                PNProductsNamesListBox.ValueMember = "Id";
                Program.sqlConnection.Close();
            }
        }

        private void PNDeleteButton_Click(object sender, EventArgs e)
        {
            int choice = PNProductsNamesListBox.SelectedIndex;
            if (choice == -1)
            {
                ErrorForm ErrorForm = new ErrorForm();
                ErrorForm.ErrorLabel.Text = "Выберите название продукта.";
                ErrorForm.ShowDialog();
            }
            else
            {
                Program.sqlConnection.Open();
                DataRowView item = (DataRowView) PNProductsNamesListBox.SelectedItem;
                string nameToDelete = item.Row[1].ToString();
                SqlCommand cmd = new SqlCommand("DELETE FROM ProductsNames WHERE Name = N'" + nameToDelete + "';", Program.sqlConnection);
                cmd.ExecuteNonQuery();
                Program.sqlConnection.Close();
                ProductsNamesTable = new DataTable();
                PNProductsNamesListBox.DataSource = ProductsNamesTable;
                Program.sqlConnection.Open();
                adapter = new SqlDataAdapter("SELECT * FROM ProductsNames", Program.sqlConnection);
                adapter.Fill(ProductsNamesTable);
                PNProductsNamesListBox.DataSource = ProductsNamesTable;
                PNProductsNamesListBox.DisplayMember = "Name";
                PNProductsNamesListBox.ValueMember = "Id";
                Program.sqlConnection.Close();
            }
        }

        private void ProductsNames_Load(object sender, EventArgs e)
        {
            Program.sqlConnection.Open();
            adapter = new SqlDataAdapter("SELECT * FROM ProductsNames", Program.sqlConnection);
            adapter.Fill(ProductsNamesTable);
            PNProductsNamesListBox.DataSource = ProductsNamesTable;
            PNProductsNamesListBox.DisplayMember = "Name";
            PNProductsNamesListBox.ValueMember = "Id";
            Program.sqlConnection.Close();
        }
    }
}
