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
                MessageFormSmall ErrorForm = new MessageFormSmall();
                ErrorForm.LabelText.Text = "Название продукта не может быть пустым.";
                ErrorForm.Text = "Ошибка";
                ErrorForm.ShowDialog();
            }
            else
            {
                Program.sqlConnection.Open();
                SqlCommand checkIsUnique = new SqlCommand("SELECT COUNT(*) FROM ProductsNames WHERE Name = N'" + ProductNameTextBox.Text.ToString() + "';", Program.sqlConnection);
                int res = (int)checkIsUnique.ExecuteScalar();
                if (res == 0)
                {
                    SqlCommand addProductName = new SqlCommand("INSERT INTO ProductsNames VALUES (N'" + ProductNameTextBox.Text.ToString() + "');", Program.sqlConnection);
                    addProductName.ExecuteNonQuery();
                    ProductNameTextBox.Clear();
                    ProductsNamesTable = new DataTable();
                    PNProductsNamesListBox.DataSource = ProductsNamesTable;
                    adapter = new SqlDataAdapter("SELECT * FROM ProductsNames", Program.sqlConnection);
                    adapter.Fill(ProductsNamesTable);
                    PNProductsNamesListBox.DataSource = ProductsNamesTable;
                    PNProductsNamesListBox.DisplayMember = "Name";
                    PNProductsNamesListBox.ValueMember = "Id";
                }
                else
                {
                    MessageFormSmall ErrorForm = new MessageFormSmall();
                    ErrorForm.LabelText.Text = "Продукт с таким названием уже существует.";
                    ErrorForm.Text = "Ошибка";
                    ErrorForm.ShowDialog();
                }
                Program.sqlConnection.Close();
            }
        }

        private void PNDeleteButton_Click(object sender, EventArgs e)
        {
            int choice = PNProductsNamesListBox.SelectedIndex;
            if (choice == -1)
            {
                MessageFormSmall ErrorForm = new MessageFormSmall();
                ErrorForm.LabelText.Text = "Выберите название продукта.";
                ErrorForm.Text = "Ошибка";
                ErrorForm.ShowDialog();
            }
            else
            {
                Program.sqlConnection.Open();
                DataRowView item = (DataRowView) PNProductsNamesListBox.SelectedItem;
                string nameToDelete = item.Row[1].ToString();
                SqlCommand deleteProductName = new SqlCommand("DELETE FROM ProductsNames WHERE Name = N'" + nameToDelete + "';", Program.sqlConnection);
                deleteProductName.ExecuteNonQuery();
                ProductsNamesTable = new DataTable();
                PNProductsNamesListBox.DataSource = ProductsNamesTable;
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
            adapter = new SqlDataAdapter("SELECT * FROM ProductsNames", Program.sqlConnection);
            adapter.Fill(ProductsNamesTable);
            PNProductsNamesListBox.DataSource = ProductsNamesTable;
            PNProductsNamesListBox.DisplayMember = "Name";
            PNProductsNamesListBox.ValueMember = "Id";
        }
    }
}
