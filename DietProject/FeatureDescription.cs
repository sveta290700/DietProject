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
    public partial class FeatureDescription : Form
    {
        private SqlDataAdapter adapter;
        private DataTable ProductsNamesTable = new DataTable();
        private DataTable FeaturesTable = new DataTable();

        public FeatureDescription()
        {
            InitializeComponent();
        }

        private void FeatureDescription_Load(object sender, EventArgs e)
        {
            Program.sqlConnection.Open();
            adapter = new SqlDataAdapter("SELECT * FROM ProductsNames", Program.sqlConnection);
            adapter.Fill(ProductsNamesTable);
            FDProductsComboBox.DataSource = ProductsNamesTable;
            FDProductsComboBox.DisplayMember = "Name";
            FDProductsComboBox.ValueMember = "Id";
            Program.sqlConnection.Close();
        }

        private void FDProductsComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            adapter = new SqlDataAdapter("SELECT * FROM Features", Program.sqlConnection);
            adapter.Fill(FeaturesTable);
            List<String> FeaturesList = FeaturesTable.AsEnumerable().Select(n => n.Field<string>(1)).ToList();
        }
    }
}
