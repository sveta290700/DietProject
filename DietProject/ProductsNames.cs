using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.IO;
using System.Reflection;

namespace DietProject
{
    public partial class ProductsNames : Form
    {
        private SqlConnection sqlConnection = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=" + new DirectoryInfo(Application.StartupPath).Parent.Parent.Parent.FullName + @"\Database.mdf;Integrated Security=True");
        private SqlDataAdapter adapter;
        private DataTable table = new DataTable();
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
        }

        private void PNDeleteButton_Click(object sender, EventArgs e)
        {
            {
                int choice = PNProductsNamesListBox.SelectedIndex;
                switch (choice)
                {
                    case -1:
                        {
                            ErrorForm ErrorForm = new ErrorForm();
                            ErrorForm.ErrorLabel.Text = "Выберите название продукта.";
                            ErrorForm.ShowDialog();
                            break;
                        }
                    default:
                        break;
                }
            }
        }

        private void ProductsNames_Load(object sender, EventArgs e)
        {
            adapter = new SqlDataAdapter("SELECT * FROM ProductsNames", sqlConnection);
            adapter.Fill(table);
            PNProductsNamesListBox.DataSource = table;
            PNProductsNamesListBox.DisplayMember = "Name";
            PNProductsNamesListBox.ValueMember = "Id";
        }
    }
}
