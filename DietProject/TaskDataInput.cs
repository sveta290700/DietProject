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
            foreach (var item in ProductsNamesList)
            {
                DTProductsNamesListBox.Items.Add(item);
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
    }
}
