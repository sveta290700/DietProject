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
                case 1:
                    {
                        Categories Categories = new Categories();
                        Categories.ShowDialog();
                        break;
                    }
                case 2:
                    {
                        ProductsCategories ProductsCategories = new ProductsCategories();
                        ProductsCategories.ShowDialog();
                        break;
                    }
                case 3:
                    {
                        Features Features = new Features();
                        Features.ShowDialog();
                        break;
                    }
                case 4:
                    {
                        PossibleValues PossibleValues = new PossibleValues();
                        PossibleValues.ShowDialog();
                        break;
                    }
                case 5:
                    {
                        FeatureDescription FeatureDescription = new FeatureDescription();
                        FeatureDescription.ShowDialog();
                        break;
                    }
                case 6:
                    {
                        FeatureValue FeatureValue = new FeatureValue();
                        FeatureValue.ShowDialog();
                        break;
                    }
                case 7:
                    {
                        DayNorms DayNorms = new DayNorms();
                        DayNorms.ShowDialog();
                        break;
                    }
                default:
                    break;
            }
        }
    }
}
