using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Linq;

namespace DietProject
{
    public partial class TaskDataInput : Form
    {
        private SqlDataAdapter adapter;
        private DataTable ProductsNamesTable = new DataTable();
        private List<string> ProductsNamesList = new List<string>();

        public TaskDataInput()
        {
            InitializeComponent();
        }

        private void TaskDataInput_Load(object sender, EventArgs e)
        {
            adapter = new SqlDataAdapter("SELECT * FROM ProductsNames", Program.sqlConnection);
            adapter.Fill(ProductsNamesTable);
            ProductsNamesList = ProductsNamesTable.AsEnumerable().Select(n => n.Field<string>(1)).ToList();
            foreach (var productName in ProductsNamesList)
            {
                DTProductsNamesListBox.Items.Add(productName);
            }
        }

        private void MoveSelectedItems(ListBox lstFrom, ListBox lstTo)
        {
            while (lstFrom.SelectedItems.Count > 0)
            {
                string item = (string)lstFrom.SelectedItems[0];
                lstTo.Items.Add(item);
                lstFrom.Items.Remove(item);
            }
        }

        private void DTSelectButton_Click(object sender, EventArgs e)
        {
            MoveSelectedItems(DTProductsNamesListBox, DietProductsListBox);
        }

        private void DTUnselectButton_Click(object sender, EventArgs e)
        {
            MoveSelectedItems(DietProductsListBox, DTProductsNamesListBox);
        }

        private void TDSolveButton_Click(object sender, EventArgs e)
        {
            if (DietProductsListBox.Items.Cast<string>().ToList().Count != 0)
            {
                List<string> notCompatibleMessagesList = new List<string>();
                string notCompatibleMessagesString = "";
                Program.sqlConnection.Open();
                foreach (var dietProductName1 in DietProductsListBox.Items)
                {
                    SqlCommand getDietProductId1 = new SqlCommand("SELECT Id FROM ProductsNames WHERE Name = N'" + dietProductName1 + "';", Program.sqlConnection);
                    int dietProductId1 = (int)getDietProductId1.ExecuteScalar();
                    SqlCommand getDietProductCategoryId1 = new SqlCommand("SELECT CategoryId FROM ProductsOfCategories WHERE ProductId = " + dietProductId1 + ";", Program.sqlConnection);
                    int dietProductCategoryId1 = (int)getDietProductCategoryId1.ExecuteScalar();
                    SqlCommand getDietProductCategoryName1 = new SqlCommand("SELECT Name FROM Categories WHERE Id = " + dietProductCategoryId1 + ";", Program.sqlConnection);
                    string dietProductCategoryName1 = (string)getDietProductCategoryName1.ExecuteScalar();
                    List<string> dietProductsNamesExceptThis = new List<string>(DietProductsListBox.Items.Cast<string>().ToList());
                    dietProductsNamesExceptThis.Remove((string)dietProductName1);
                    foreach (var dietProductName2 in dietProductsNamesExceptThis)
                    {
                        SqlCommand getDietProductId2 = new SqlCommand("SELECT Id FROM ProductsNames WHERE Name = N'" + dietProductName2 + "';", Program.sqlConnection);
                        int dietProductId2 = (int)getDietProductId2.ExecuteScalar();
                        SqlCommand getDietProductCategoryId2 = new SqlCommand("SELECT CategoryId FROM ProductsOfCategories WHERE ProductId = " + dietProductId2 + ";", Program.sqlConnection);
                        int dietProductCategoryId2 = (int)getDietProductCategoryId2.ExecuteScalar();
                        SqlCommand getDietProductCategoryName2 = new SqlCommand("SELECT Name FROM Categories WHERE Id = " + dietProductCategoryId2 + ";", Program.sqlConnection);
                        string dietProductCategoryName2 = (string)getDietProductCategoryName2.ExecuteScalar();
                        SqlCommand checkIfCompatible = new SqlCommand("SELECT COUNT(*) FROM CompatibleCategories WHERE CategoryId1 = " + dietProductCategoryId1 + " AND CategoryId2 = " + dietProductCategoryId2 + ";", Program.sqlConnection);
                        int checkIfCompatibleRes = (int)checkIfCompatible.ExecuteScalar();
                        if (checkIfCompatibleRes == 0)
                        {
                            string stringResult1 = "Продукты " + dietProductName1 + " (категория: " + dietProductCategoryName1 + ") и " + dietProductName2 + " (категория: " + dietProductCategoryName2 + ") несовместимы!\n\n";
                            string stringResult2 = "Продукты " + dietProductName2 + " (категория: " + dietProductCategoryName2 + ") и " + dietProductName1 + " (категория: " + dietProductCategoryName1 + ") несовместимы!\n\n";
                            if (!notCompatibleMessagesList.Contains(stringResult2))
                            {
                                notCompatibleMessagesList.Add(stringResult1);
                                notCompatibleMessagesString += stringResult1;
                            }
                        }
                    }
                }
                if (notCompatibleMessagesString.Length == 0)
                {

                }
                else
                {
                    ErrorFormLarge ErrorFormIncompatible = new ErrorFormLarge();
                    ErrorFormIncompatible.ErrorLabel.Text = notCompatibleMessagesString;
                    ErrorFormIncompatible.Text = "Найдены несовместимые продукты";
                    ErrorFormIncompatible.ShowDialog();
                }
                Program.sqlConnection.Close();
            }
            else
            {
                ErrorForm ErrorForm = new ErrorForm();
                ErrorForm.ErrorLabel.Text = "В рацион должен входить хотя бы один продукт.";
                ErrorForm.ShowDialog();
            }
        }
    }
}
