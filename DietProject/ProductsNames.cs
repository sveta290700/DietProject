using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace DietProject
{
    public partial class ProductsNames : Form
    {
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
    }
}
