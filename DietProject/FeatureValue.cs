﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace DietProject
{
    public partial class FeatureValue : Form
    {
        private SqlDataAdapter adapter;
        private DataTable ProductsNamesTable = new DataTable();
        private DataTable ProductFeaturesTable = new DataTable();

        public FeatureValue()
        {
            InitializeComponent();
            adapter = new SqlDataAdapter("SELECT * FROM ProductsNames", Program.sqlConnection);
            adapter.Fill(ProductsNamesTable);
            FVProductsNamesComboBox.DataSource = ProductsNamesTable;
            FVProductsNamesComboBox.DisplayMember = "Name";
            FVProductsNamesComboBox.ValueMember = "Id";
        }

        private void FVProductsNamesComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            DataRowView itemProd = (DataRowView)FVProductsNamesComboBox.SelectedItem;
            int selectedProductId = (int)itemProd.Row[0];
            ProductFeaturesTable = new DataTable();
            adapter = new SqlDataAdapter("SELECT Id, Name FROM Features JOIN FeatureDescriptions ON Features.Id = FeatureDescriptions.FeatureId WHERE ProductId = " + selectedProductId + ";", Program.sqlConnection);
            adapter.Fill(ProductFeaturesTable);
            FVFeaturesListBox.DataSource = ProductFeaturesTable;
            FVFeaturesListBox.DisplayMember = "Name";
            FVFeaturesListBox.ValueMember = "Id";
        }

        private void FVFeaturesListBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (FVFeaturesListBox.SelectedIndex != -1)
            {
                DataRowView itemProd = (DataRowView)FVProductsNamesComboBox.SelectedItem;
                int selectedProductId = (int)itemProd.Row[0];
                DataRowView itemFeat = (DataRowView)FVFeaturesListBox.SelectedItem;
                int selectedFeatureId = (int)itemFeat.Row[0];
                Program.sqlConnection.Open();
                SqlCommand getValue = new SqlCommand("SELECT Value FROM ProductsFeaturesValues WHERE ProductId = " + selectedProductId + " AND FeatureId = " + selectedFeatureId + ";", Program.sqlConnection);
                object value = getValue.ExecuteScalar();
                SqlCommand getLow = new SqlCommand("SELECT Low FROM PossibleFeaturesValues WHERE FeatureId = " + selectedFeatureId + ";", Program.sqlConnection);
                decimal low = (decimal)getLow.ExecuteScalar();
                SqlCommand getLowIncl = new SqlCommand("SELECT LowIncl FROM PossibleFeaturesValues WHERE FeatureId = " + selectedFeatureId + ";", Program.sqlConnection);
                bool lowIncl = (bool)getLowIncl.ExecuteScalar();
                SqlCommand getHigh = new SqlCommand("SELECT High FROM PossibleFeaturesValues WHERE FeatureId = " + selectedFeatureId + ";", Program.sqlConnection);
                decimal high = (decimal)getHigh.ExecuteScalar();
                SqlCommand getHighIncl = new SqlCommand("SELECT HighIncl FROM PossibleFeaturesValues WHERE FeatureId = " + selectedFeatureId + ";", Program.sqlConnection);
                bool highIncl = (bool)getHighIncl.ExecuteScalar();
                Program.sqlConnection.Close();
                if (!lowIncl)
                {
                    low += FVNumericUpDown.Increment;
                }
                if (!highIncl)
                {
                    high -= FVNumericUpDown.Increment;
                }
                if ((value != null) && low <= (decimal)value && (decimal)value <= high)
                {
                    FVNumericUpDown.Value = (decimal)value;
                }
                else if ((value != null) && !(low <= (decimal)value && (decimal)value <= high))
                {
                    FVNumericUpDown.Value = (decimal)0.0000000;
                    Program.sqlConnection.Open();
                    SqlCommand deleteOld = new SqlCommand("DELETE FROM ProductsFeaturesValues WHERE FeatureId = " + selectedFeatureId + ";", Program.sqlConnection);
                    deleteOld.ExecuteNonQuery();
                    Program.sqlConnection.Close();
                }
                else if (value == null)
                {
                    FVNumericUpDown.Value = (decimal)0.0000000;
                }
            }
        }

        private void FVSaveButton_Click(object sender, EventArgs e)
        {
            string leftBracket = "[";
            string rightBracket = "]";
            Program.sqlConnection.Open();
            DataRowView itemProd = (DataRowView)FVProductsNamesComboBox.SelectedItem;
            int selectedProductId = (int)itemProd.Row[0];
            DataRowView itemFeat = (DataRowView)FVFeaturesListBox.SelectedItem;
            int selectedFeatureId = (int)itemFeat.Row[0];
            SqlCommand getLow = new SqlCommand("SELECT Low FROM PossibleFeaturesValues WHERE FeatureId = " + selectedFeatureId + ";", Program.sqlConnection);
            decimal low = (decimal)getLow.ExecuteScalar();
            SqlCommand getLowIncl = new SqlCommand("SELECT LowIncl FROM PossibleFeaturesValues WHERE FeatureId = " + selectedFeatureId + ";", Program.sqlConnection);
            bool lowIncl = (bool)getLowIncl.ExecuteScalar();
            SqlCommand getHigh = new SqlCommand("SELECT High FROM PossibleFeaturesValues WHERE FeatureId = " + selectedFeatureId + ";", Program.sqlConnection);
            decimal high = (decimal)getHigh.ExecuteScalar();
            SqlCommand getHighIncl = new SqlCommand("SELECT HighIncl FROM PossibleFeaturesValues WHERE FeatureId = " + selectedFeatureId + ";", Program.sqlConnection);
            bool highIncl = (bool)getHighIncl.ExecuteScalar();
            Program.sqlConnection.Close();
            if (!lowIncl)
            {
                leftBracket = "(";
                low += FVNumericUpDown.Increment;
            }
            if (!highIncl)
            {
                rightBracket = ")";
                high -= FVNumericUpDown.Increment;
            }
            if (low <= FVNumericUpDown.Value && FVNumericUpDown.Value <= high)
            {
                Program.sqlConnection.Open();
                SqlCommand ifExists = new SqlCommand("SELECT COUNT (*) FROM ProductsFeaturesValues WHERE ProductId = " + selectedProductId + " AND FeatureId = " + selectedFeatureId + ";", Program.sqlConnection);
                int res = (int)ifExists.ExecuteScalar();
                if (res != 0)
                {
                    SqlCommand deleteExisting = new SqlCommand("DELETE FROM ProductsFeaturesValues WHERE ProductId = " + selectedProductId + " AND FeatureId = " + selectedFeatureId + ";", Program.sqlConnection);
                    deleteExisting.ExecuteNonQuery();
                }
                SqlCommand addValue = new SqlCommand("INSERT INTO ProductsFeaturesValues VALUES (" + selectedProductId + ", " + selectedFeatureId + ", " + (double)FVNumericUpDown.Value + ");", Program.sqlConnection);
                addValue.ExecuteNonQuery();
                Program.sqlConnection.Close();
                int selectedIndex = FVFeaturesListBox.SelectedIndex;
                FVFeaturesListBox.SelectedIndex = -1;
                FVFeaturesListBox.SelectedIndex = selectedIndex;
            }
            else
            {
                ErrorForm ErrorForm = new ErrorForm();
                ErrorForm.ErrorLabel.Text = "Значение лежит вне границ заданного интервала возможных значений выбранного признака " + leftBracket + low + "; " + high + rightBracket + ".";
                ErrorForm.ShowDialog();
            }
        }
    }
}
