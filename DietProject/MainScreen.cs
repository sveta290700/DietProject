using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DietProject
{
    public partial class MainScreen : Form
    {
        public MainScreen()
        {
            InitializeComponent();
        }

        private void MainScreen_Load(object sender, EventArgs e)
        {

        }

        private void ButtonToKE_Click(object sender, EventArgs e)
        {
            KnowledgeEditor KnowledgeEditor = new KnowledgeEditor();
            KnowledgeEditor.ShowDialog();
        }
    }
}
