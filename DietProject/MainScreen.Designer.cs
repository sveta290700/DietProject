﻿
namespace DietProject
{
    partial class MainScreen
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainScreen));
            this.ButtonToKE = new System.Windows.Forms.Button();
            this.ButtonToTS = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // ButtonToKE
            // 
            resources.ApplyResources(this.ButtonToKE, "ButtonToKE");
            this.ButtonToKE.BackColor = System.Drawing.Color.PaleGreen;
            this.ButtonToKE.FlatAppearance.BorderColor = System.Drawing.Color.Black;
            this.ButtonToKE.Name = "ButtonToKE";
            this.ButtonToKE.TabStop = false;
            this.ButtonToKE.UseVisualStyleBackColor = false;
            this.ButtonToKE.Click += new System.EventHandler(this.ButtonToKE_Click);
            // 
            // ButtonToTS
            // 
            resources.ApplyResources(this.ButtonToTS, "ButtonToTS");
            this.ButtonToTS.BackColor = System.Drawing.Color.PaleGreen;
            this.ButtonToTS.FlatAppearance.BorderColor = System.Drawing.Color.Black;
            this.ButtonToTS.Name = "ButtonToTS";
            this.ButtonToTS.TabStop = false;
            this.ButtonToTS.UseVisualStyleBackColor = false;
            // 
            // MainScreen
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Window;
            this.Controls.Add(this.ButtonToTS);
            this.Controls.Add(this.ButtonToKE);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "MainScreen";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button ButtonToKE;
        private System.Windows.Forms.Button ButtonToTS;
    }
}

