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
        private DataTable CategoriesTablePOC = new DataTable();
        private DataTable ProductsNamesTable = new DataTable();
        private DataTable SelectedProductsNamesTable = new DataTable();
        private DataTable NotSelectedProductsNamesTable = new DataTable();
        private List<int> CategoriesIdPOCList = new List<int>();
        private List<string> ProductsNamesList = new List<string>();
        private List<string> leftList = new List<string>();
        private List<string> rightList = new List<string>();
        private List<string> NotSelectedProductsNamesList = new List<string>();

        public ProductsCategories()
        {
            InitializeComponent();
            adapter = new SqlDataAdapter("SELECT * FROM Categories", Program.sqlConnection);
            adapter.Fill(CategoriesTable);
            adapter = new SqlDataAdapter("SELECT CategoryId FROM ProductsOfCategories", Program.sqlConnection);
            adapter.Fill(CategoriesTablePOC);
            CategoriesIdPOCList = CategoriesTablePOC.AsEnumerable().Select(n => n.Field<int>(0)).ToList();
            adapter = new SqlDataAdapter("SELECT * FROM ProductsNames", Program.sqlConnection);
            adapter.Fill(ProductsNamesTable);
            ProductsNamesList = ProductsNamesTable.AsEnumerable().Select(n => n.Field<string>(1)).ToList();
            PCCategoriesComboBox.DataSource = CategoriesTable;
            PCCategoriesComboBox.DisplayMember = "Name";
            PCCategoriesComboBox.ValueMember = "Id";
        }

        private void PCCategoriesComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            PCProductsNamesListBox.Items.Clear();
            PCProductsCategoryListBox.Items.Clear();
            DataRowView item = (DataRowView)PCCategoriesComboBox.SelectedItem;
            int selectedCategoryId = (int)item.Row[0];
            leftList = new List<string>(ProductsNamesList);
            SelectedProductsNamesTable = new DataTable();
            adapter = new SqlDataAdapter("SELECT Name FROM ProductsNames JOIN ProductsOfCategories ON ProductsNames.Id = ProductsOfCategories.ProductId WHERE CategoryId = " + selectedCategoryId + ";", Program.sqlConnection);
            adapter.Fill(SelectedProductsNamesTable);
            rightList = SelectedProductsNamesTable.AsEnumerable().Select(n => n.Field<string>(0)).ToList();
            foreach (var categoryId in CategoriesIdPOCList)
            {
                NotSelectedProductsNamesTable = new DataTable();
                adapter = new SqlDataAdapter("SELECT Name FROM ProductsNames JOIN ProductsOfCategories ON ProductsNames.Id = ProductsOfCategories.ProductId WHERE CategoryId = " + categoryId + ";", Program.sqlConnection);
                adapter.Fill(NotSelectedProductsNamesTable);
                NotSelectedProductsNamesList = NotSelectedProductsNamesTable.AsEnumerable().Select(n => n.Field<string>(0)).ToList();
                leftList = leftList.Except(NotSelectedProductsNamesList).ToList();
            }
            foreach (var itemLeft in leftList)
            {
                PCProductsNamesListBox.Items.Add(itemLeft);
            }
            foreach (var itemRight in rightList)
            {
                PCProductsCategoryListBox.Items.Add(itemRight);
            }
        }
    }
}