﻿using System;
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
    public partial class DayNorms : Form
    {
        private SqlDataAdapter adapter;
        private DataTable SubstancesTable = new DataTable();
        private DataTable SubstancesIdTable = new DataTable();
        private DataTable SubstancesIdTable2 = new DataTable();
        private List<string> SubstancesList = new List<string>();
        private List<int> SubstancesIdList = new List<int>();
        private List<int> SubstancesIdList2 = new List<int>();

        public DayNorms()
        {
            InitializeComponent();
            adapter = new SqlDataAdapter("SELECT Id FROM Features WHERE Id != 1", Program.sqlConnection);
            adapter.Fill(SubstancesIdTable);
            SubstancesIdList = SubstancesIdTable.AsEnumerable().Select(n => n.Field<int>(0)).ToList();
            Program.sqlConnection.Open();
            SqlCommand countDN = new SqlCommand("SELECT COUNT(*) FROM DayNorms;", Program.sqlConnection);
            int res = (int)countDN.ExecuteScalar();
            adapter = new SqlDataAdapter("SELECT SubstanceId FROM DayNorms", Program.sqlConnection);
            adapter.Fill(SubstancesIdTable2);
            SubstancesIdList2 = SubstancesIdTable2.AsEnumerable().Select(n => n.Field<int>(0)).ToList();
            SqlCommand countSubstances = new SqlCommand("SELECT COUNT(*) FROM Features WHERE Id != 1;", Program.sqlConnection);
            int resAll = (int)countSubstances.ExecuteScalar();
            if (res == 0)
            {
                foreach (var substanceId in SubstancesIdList)
                {
                    SqlCommand initDN = new SqlCommand("INSERT INTO DayNorms VALUES (" + substanceId + ", 0.0);", Program.sqlConnection);
                    initDN.ExecuteNonQuery();
                }
            }
            else if (res != resAll)
            {
                foreach (var substanceId in SubstancesIdList)
                {
                    if (!SubstancesIdList2.Contains(substanceId))
                    {
                        SqlCommand insNew = new SqlCommand("INSERT INTO DayNorms VALUES (" + substanceId + ", 0.0);", Program.sqlConnection);
                        insNew.ExecuteNonQuery();
                    }
                }
            }
            Program.sqlConnection.Close();
            adapter = new SqlDataAdapter("SELECT * FROM Features WHERE Id != 1", Program.sqlConnection);
            adapter.Fill(SubstancesTable);
            SubstancesList = SubstancesTable.AsEnumerable().Select(n => n.Field<string>(1)).ToList();
            DNSubstanceComboBox.DataSource = SubstancesTable;
            DNSubstanceComboBox.DisplayMember = "Name";
            DNSubstanceComboBox.ValueMember = "Id";
        }

        private void DNSubstanceComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (DNSubstanceComboBox.SelectedIndex != -1)
            {
                Program.sqlConnection.Open();
                DataRowView item = (DataRowView)DNSubstanceComboBox.SelectedItem;
                int selectedSubstanceId = (int)item.Row[0];
                SqlCommand getDayNorm = new SqlCommand("SELECT Value FROM DayNorms WHERE SubstanceId = " + selectedSubstanceId + ";", Program.sqlConnection);
                decimal dayNorm = (decimal)getDayNorm.ExecuteScalar();
                Program.sqlConnection.Close();
                DNNumericUpDown.Value = dayNorm;
            }
        }

        private void DNAddButton_Click(object sender, EventArgs e)
        {
            Program.sqlConnection.Open();
            DataRowView item = (DataRowView)DNSubstanceComboBox.SelectedItem;
            int selectedSubstanceId = (int)item.Row[0];
            SqlCommand deleteOld = new SqlCommand("DELETE FROM DayNorms WHERE SubstanceId = " + selectedSubstanceId + ";", Program.sqlConnection);
            deleteOld.ExecuteNonQuery();
            string a = DNNumericUpDown.Value.ToString();
            a = a.Replace(',', '.');
            SqlCommand insertNewRecord = new SqlCommand("INSERT INTO DayNorms VALUES (" + selectedSubstanceId + ", CONVERT(DECIMAL(10, 7), " + a + "));", Program.sqlConnection);
            insertNewRecord.ExecuteNonQuery();
            Program.sqlConnection.Close();
            int selectedIndex = DNSubstanceComboBox.SelectedIndex;
            DNSubstanceComboBox.SelectedIndex = -1;
            DNSubstanceComboBox.SelectedIndex = selectedIndex;
        }
    }
}
