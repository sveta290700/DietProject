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
    public partial class ProductsCategories : Form
    {
        private SqlDataAdapter adapter;
        private DataTable CategoriesTable = new DataTable();
        private DataTable CategoriesTable2 = new DataTable();
        private DataTable ProductsNamesTable = new DataTable();
        private List<int> CategoriesIdList = new List<int>();
        private List<int> CategoriesIdList2 = new List<int>();
        private List<string> ProductsNamesList = new List<string>();

        public ProductsCategories()
        {
            InitializeComponent();
            adapter = new SqlDataAdapter("SELECT * FROM Categories", Program.sqlConnection);
            adapter.Fill(CategoriesTable);
            CategoriesIdList = CategoriesTable.AsEnumerable().Select(n => n.Field<int>(0)).ToList();
            adapter = new SqlDataAdapter("SELECT CategoryId FROM ProductsOfCategories", Program.sqlConnection);
            adapter.Fill(CategoriesTable2);
            CategoriesIdList2 = CategoriesTable2.AsEnumerable().Select(n => n.Field<int>(0)).ToList();
            adapter = new SqlDataAdapter("SELECT * FROM ProductsNames", Program.sqlConnection);
            adapter.Fill(ProductsNamesTable);
            ProductsNamesList = ProductsNamesTable.AsEnumerable().Select(n => n.Field<string>(1)).ToList();
            Program.sqlConnection.Open();
            SqlCommand countPOC = new SqlCommand("SELECT COUNT(*) FROM ProductsOfCategories;", Program.sqlConnection);
            int res = (int)countPOC.ExecuteScalar();
            //initialize
            if (res == 0)
            {
                foreach (var categoryId in CategoriesIdList)
                {
                    SqlCommand initPOC = new SqlCommand("INSERT INTO ProductsOfCategories VALUES (" + categoryId + ", N'');", Program.sqlConnection);
                    initPOC.ExecuteNonQuery();
                }
            }
            else
            {
                //delete rows for non-existent categories
                foreach (var categoryId2 in CategoriesIdList2)
                {
                    if (!CategoriesIdList.Contains(categoryId2))
                    {
                        SqlCommand delOld = new SqlCommand("DELETE FROM ProductsOfCategories WHERE Id = " + categoryId2 + ";", Program.sqlConnection);
                        delOld.ExecuteNonQuery();
                    }
                }
                //add rows for new categories
                foreach (var categoryId in CategoriesIdList)
                {
                    if (!CategoriesIdList2.Contains(categoryId))
                    {
                        SqlCommand insNew = new SqlCommand("INSERT INTO ProductsOfCategories VALUES (" + categoryId + ", N'');", Program.sqlConnection);
                        insNew.ExecuteNonQuery();
                    }
                }
                Program.sqlConnection.Close();
                PCCategoriesComboBox.DataSource = CategoriesTable;
                PCCategoriesComboBox.DisplayMember = "Name";
                PCCategoriesComboBox.ValueMember = "Id";
            }
        }

        private List<string> getSelectedList(string prodList, int prodListCategoryId)
        {
            List<string> ProductsNamesListToFill = new List<string>();
            int flag;
            if (prodList.Length != 0)
            {
                string myId = "";
                for (int i = 0; i < prodList.Length; i++)
                {
                    if ((prodList[i] != ',') && (prodList[i] != ' '))
                    {
                        myId += prodList[i];
                    }
                    else if (prodList[i] == ',')
                    {
                        SqlCommand getProdNameById = new SqlCommand("SELECT Name FROM ProductsNames WHERE Id = " + myId + ";", Program.sqlConnection);
                        string prodName = (string)getProdNameById.ExecuteScalar();
                        SqlCommand checkIfProdNameExists = new SqlCommand("SELECT COUNT(*) FROM ProductsNames WHERE Id = " + myId + ";", Program.sqlConnection);
                        flag = (int)checkIfProdNameExists.ExecuteScalar();
                        if (flag != 0)
                        {
                            ProductsNamesListToFill.Add(prodName);
                            myId = "";
                            i++;
                        }
                        else
                        {
                            myId = "";
                            i++;
                        }
                    }
                    else 
                    {
                        i++;
                    }
                }
            }
            string newList = restoreList(ProductsNamesListToFill);
            SqlCommand updateList = new SqlCommand("UPDATE ProductsOfCategories SET ProductsList = N'" + newList + "' WHERE CategoryId = " + prodListCategoryId + ";", Program.sqlConnection);
            updateList.ExecuteNonQuery();
            return ProductsNamesListToFill;
        }

        private string restoreList(List<string> productList)
        {
            string result = "";
            foreach (var productItem in productList)
            {
                SqlCommand getProdId = new SqlCommand("SELECT Id FROM ProductsNames WHERE Name = N'" + productItem + "';", Program.sqlConnection);            
                int prodId = (int)getProdId.ExecuteScalar();
                result = result + prodId.ToString() + ",";
                if (productItem != productList.Last())
                {
                    result += " ";
                }
            }
            return result;
        }

        private void PCCategoriesComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            PCProductsNamesListBox.Items.Clear();
            PCProductsCategoryListBox.Items.Clear();
            Program.sqlConnection.Open();
            DataRowView item = (DataRowView)PCCategoriesComboBox.SelectedItem;
            int selectedCategoryId = (int)item.Row[0];
            SqlCommand getListSelectedCategoryId = new SqlCommand("SELECT ProductsList FROM ProductsOfCategories WHERE CategoryId = " + selectedCategoryId + ";", Program.sqlConnection);
            string prodListSelectedCategoryId = (string)getListSelectedCategoryId.ExecuteScalar();
            List<string> leftList = new List<string>(ProductsNamesList);
            List<string> rightList = getSelectedList(prodListSelectedCategoryId, selectedCategoryId);
            foreach (var categoryId2 in CategoriesIdList2)
            {
                SqlCommand getListCategoryId2 = new SqlCommand("SELECT ProductsList FROM ProductsOfCategories WHERE CategoryId = " + categoryId2 + ";", Program.sqlConnection);
                string prodListCategoryId2 = (string)getListCategoryId2.ExecuteScalar();
                leftList = leftList.Except(getSelectedList(prodListCategoryId2, categoryId2)).ToList();
            }
            foreach (var itemLeft in leftList)
            {
                PCProductsNamesListBox.Items.Add(itemLeft);
            }
            foreach (var itemRight in rightList)
            {
                PCProductsCategoryListBox.Items.Add(itemRight);
            }
            Program.sqlConnection.Close();
        }
    }
}