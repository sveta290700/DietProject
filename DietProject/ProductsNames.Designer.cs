
namespace DietProject
{
    partial class ProductsNames
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ProductsNames));
            this.ProductsNamesLabel = new System.Windows.Forms.Label();
            this.PNProductsNamesListBox = new System.Windows.Forms.ListBox();
            this.PNAddButton = new System.Windows.Forms.Button();
            this.PNDeleteButton = new System.Windows.Forms.Button();
            this.ProductNameTextBox = new DietProject.WaterMarkTextBox();
            this.SuspendLayout();
            // 
            // ProductsNamesLabel
            // 
            resources.ApplyResources(this.ProductsNamesLabel, "ProductsNamesLabel");
            this.ProductsNamesLabel.BackColor = System.Drawing.Color.LightGreen;
            this.ProductsNamesLabel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.ProductsNamesLabel.Name = "ProductsNamesLabel";
            // 
            // PNProductsNamesListBox
            // 
            resources.ApplyResources(this.PNProductsNamesListBox, "PNProductsNamesListBox");
            this.PNProductsNamesListBox.FormattingEnabled = true;
            this.PNProductsNamesListBox.Name = "PNProductsNamesListBox";
            this.PNProductsNamesListBox.TabStop = false;
            // 
            // PNAddButton
            // 
            resources.ApplyResources(this.PNAddButton, "PNAddButton");
            this.PNAddButton.BackColor = System.Drawing.Color.PaleGreen;
            this.PNAddButton.FlatAppearance.BorderColor = System.Drawing.Color.Black;
            this.PNAddButton.Name = "PNAddButton";
            this.PNAddButton.TabStop = false;
            this.PNAddButton.UseVisualStyleBackColor = false;
            this.PNAddButton.Click += new System.EventHandler(this.PNAddButton_Click);
            // 
            // PNDeleteButton
            // 
            resources.ApplyResources(this.PNDeleteButton, "PNDeleteButton");
            this.PNDeleteButton.BackColor = System.Drawing.Color.PaleGreen;
            this.PNDeleteButton.FlatAppearance.BorderColor = System.Drawing.Color.Black;
            this.PNDeleteButton.Name = "PNDeleteButton";
            this.PNDeleteButton.TabStop = false;
            this.PNDeleteButton.UseVisualStyleBackColor = false;
            this.PNDeleteButton.Click += new System.EventHandler(this.PNDeleteButton_Click);
            // 
            // ProductNameTextBox
            // 
            resources.ApplyResources(this.ProductNameTextBox, "ProductNameTextBox");
            this.ProductNameTextBox.Name = "ProductNameTextBox";
            this.ProductNameTextBox.TabStop = false;
            this.ProductNameTextBox.WaterMarkColor = System.Drawing.Color.Gray;
            this.ProductNameTextBox.WaterMarkText = "Введите название продукта";
            // 
            // ProductsNames
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Window;
            this.Controls.Add(this.ProductNameTextBox);
            this.Controls.Add(this.PNDeleteButton);
            this.Controls.Add(this.PNAddButton);
            this.Controls.Add(this.ProductsNamesLabel);
            this.Controls.Add(this.PNProductsNamesListBox);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "ProductsNames";
            this.Load += new System.EventHandler(this.ProductsNames_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label ProductsNamesLabel;
        private System.Windows.Forms.ListBox PNProductsNamesListBox;
        private System.Windows.Forms.Button PNAddButton;
        private System.Windows.Forms.Button PNDeleteButton;
        private WaterMarkTextBox ProductNameTextBox;
    }
}