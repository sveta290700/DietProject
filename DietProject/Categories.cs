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
    public partial class Categories : Form
    {
        private SqlDataAdapter adapter;
        private DataTable CategoriesTable = new DataTable();

        public Categories()
        {
            InitializeComponent();
        }

        private void CAddButton_Click(object sender, EventArgs e)
        {
            if (CategoryTextBox.Text == "")
            {
                ErrorForm ErrorForm = new ErrorForm();
                ErrorForm.ErrorLabel.Text = "Название категории не может быть пустым.";
                ErrorForm.ShowDialog();
            }
            else
            {
                Program.sqlConnection.Open();
                SqlCommand addCategory = new SqlCommand("INSERT INTO Categories VALUES (N'" + CategoryTextBox.Text.ToString() + "');", Program.sqlConnection);
                addCategory.ExecuteNonQuery();
                CategoryTextBox.Clear();
                CategoriesTable = new DataTable();
                CCategoriesListBox.DataSource = CategoriesTable;
                adapter = new SqlDataAdapter("SELECT * FROM Categories", Program.sqlConnection);
                adapter.Fill(CategoriesTable);
                CCategoriesListBox.DataSource = CategoriesTable;
                CCategoriesListBox.DisplayMember = "Name";
                CCategoriesListBox.ValueMember = "Id";
                Program.sqlConnection.Close();
            }
        }

        private void CDeleteButton_Click(object sender, EventArgs e)
        {
            int choice = CCategoriesListBox.SelectedIndex;
            if (choice == -1)
            {
                ErrorForm ErrorForm = new ErrorForm();
                ErrorForm.ErrorLabel.Text = "Выберите название категории.";
                ErrorForm.ShowDialog();
            }
            else
            {
                Program.sqlConnection.Open();
                DataRowView item = (DataRowView)CCategoriesListBox.SelectedItem;
                string nameToDelete = item.Row[1].ToString();
                SqlCommand deleteCategory = new SqlCommand("DELETE FROM Categories WHERE Name = N'" + nameToDelete + "';", Program.sqlConnection);
                deleteCategory.ExecuteNonQuery();
                CategoriesTable = new DataTable();
                CCategoriesListBox.DataSource = CategoriesTable;
                adapter = new SqlDataAdapter("SELECT * FROM Categories", Program.sqlConnection);
                adapter.Fill(CategoriesTable);
                CCategoriesListBox.DataSource = CategoriesTable;
                CCategoriesListBox.DisplayMember = "Name";
                CCategoriesListBox.ValueMember = "Id";
                Program.sqlConnection.Close();
            }
        }

        private void Categories_Load(object sender, EventArgs e)
        {
            adapter = new SqlDataAdapter("SELECT * FROM Categories", Program.sqlConnection);
            adapter.Fill(CategoriesTable);
            CCategoriesListBox.DataSource = CategoriesTable;
            CCategoriesListBox.DisplayMember = "Name";
            CCategoriesListBox.ValueMember = "Id";
        }
    }
}
