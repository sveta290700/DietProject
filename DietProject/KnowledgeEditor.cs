using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace DietProject
{
    public partial class KnowledgeEditor : Form
    {
        public KnowledgeEditor()
        {
            InitializeComponent();
        }

        private void KEChoiceButton_Click(object sender, EventArgs e)
        {
            int choice = KLSectionsListBox.SelectedIndex;
            switch (choice)
            {
                case -1:
                    {
                        ErrorForm ErrorForm = new ErrorForm();
                        ErrorForm.ErrorLabel.Text = "Выберите раздел.";
                        ErrorForm.ShowDialog();
                        break;
                    }
                case 0:
                    {
                        ProductsNames ProductsNames = new ProductsNames();
                        ProductsNames.ShowDialog();
                        break;
                    }
                default:
                    break;
            }
        }
    }
}
