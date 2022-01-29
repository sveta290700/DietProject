
namespace DietProject
{
    partial class FeatureValue
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FeatureValue));
            this.FVProductsNamesComboBox = new System.Windows.Forms.ComboBox();
            this.FVSaveButton = new System.Windows.Forms.Button();
            this.FVProductNameChoiceLabel = new System.Windows.Forms.Label();
            this.FVFeaturesLabel = new System.Windows.Forms.Label();
            this.FVFeaturesListBox = new System.Windows.Forms.ListBox();
            this.FVValueChoiceLabel = new System.Windows.Forms.Label();
            this.FVFromNumericUpDown = new System.Windows.Forms.NumericUpDown();
            ((System.ComponentModel.ISupportInitialize)(this.FVFromNumericUpDown)).BeginInit();
            this.SuspendLayout();
            // 
            // FVProductsNamesComboBox
            // 
            resources.ApplyResources(this.FVProductsNamesComboBox, "FVProductsNamesComboBox");
            this.FVProductsNamesComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.FVProductsNamesComboBox.FormattingEnabled = true;
            this.FVProductsNamesComboBox.Name = "FVProductsNamesComboBox";
            this.FVProductsNamesComboBox.TabStop = false;
            // 
            // FVSaveButton
            // 
            resources.ApplyResources(this.FVSaveButton, "FVSaveButton");
            this.FVSaveButton.BackColor = System.Drawing.Color.PaleGreen;
            this.FVSaveButton.FlatAppearance.BorderColor = System.Drawing.Color.Black;
            this.FVSaveButton.Name = "FVSaveButton";
            this.FVSaveButton.TabStop = false;
            this.FVSaveButton.UseVisualStyleBackColor = false;
            // 
            // FVProductNameChoiceLabel
            // 
            resources.ApplyResources(this.FVProductNameChoiceLabel, "FVProductNameChoiceLabel");
            this.FVProductNameChoiceLabel.Name = "FVProductNameChoiceLabel";
            // 
            // FVFeaturesLabel
            // 
            resources.ApplyResources(this.FVFeaturesLabel, "FVFeaturesLabel");
            this.FVFeaturesLabel.BackColor = System.Drawing.Color.LightGreen;
            this.FVFeaturesLabel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.FVFeaturesLabel.Name = "FVFeaturesLabel";
            // 
            // FVFeaturesListBox
            // 
            resources.ApplyResources(this.FVFeaturesListBox, "FVFeaturesListBox");
            this.FVFeaturesListBox.FormattingEnabled = true;
            this.FVFeaturesListBox.Name = "FVFeaturesListBox";
            this.FVFeaturesListBox.TabStop = false;
            // 
            // FVValueChoiceLabel
            // 
            resources.ApplyResources(this.FVValueChoiceLabel, "FVValueChoiceLabel");
            this.FVValueChoiceLabel.Name = "FVValueChoiceLabel";
            // 
            // FVFromNumericUpDown
            // 
            resources.ApplyResources(this.FVFromNumericUpDown, "FVFromNumericUpDown");
            this.FVFromNumericUpDown.DecimalPlaces = 7;
            this.FVFromNumericUpDown.Increment = new decimal(new int[] {
            1,
            0,
            0,
            458752});
            this.FVFromNumericUpDown.Name = "FVFromNumericUpDown";
            this.FVFromNumericUpDown.TabStop = false;
            // 
            // FeatureValue
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Window;
            this.Controls.Add(this.FVFromNumericUpDown);
            this.Controls.Add(this.FVValueChoiceLabel);
            this.Controls.Add(this.FVProductsNamesComboBox);
            this.Controls.Add(this.FVSaveButton);
            this.Controls.Add(this.FVProductNameChoiceLabel);
            this.Controls.Add(this.FVFeaturesLabel);
            this.Controls.Add(this.FVFeaturesListBox);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FeatureValue";
            ((System.ComponentModel.ISupportInitialize)(this.FVFromNumericUpDown)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox FVProductsNamesComboBox;
        private System.Windows.Forms.Button FVSaveButton;
        private System.Windows.Forms.Label FVProductNameChoiceLabel;
        private System.Windows.Forms.Label FVFeaturesLabel;
        private System.Windows.Forms.ListBox FVFeaturesListBox;
        private System.Windows.Forms.Label FVValueChoiceLabel;
        private System.Windows.Forms.NumericUpDown FVFromNumericUpDown;
    }
}