namespace ImpoterLesDonneesExcels
{
    partial class Form1
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
            this.excelDataGridView = new System.Windows.Forms.DataGridView();
            this.excelFileTextBox = new System.Windows.Forms.TextBox();
            this.button1 = new System.Windows.Forms.Button();
            this.worksheetsComboBox = new System.Windows.Forms.ComboBox();
            this.button2 = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.excelDataGridView)).BeginInit();
            this.SuspendLayout();
            // 
            // excelDataGridView
            // 
            this.excelDataGridView.AllowUserToAddRows = false;
            this.excelDataGridView.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.excelDataGridView.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.excelDataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.excelDataGridView.Location = new System.Drawing.Point(11, 77);
            this.excelDataGridView.Name = "excelDataGridView";
            this.excelDataGridView.Size = new System.Drawing.Size(850, 325);
            this.excelDataGridView.TabIndex = 0;
            // 
            // excelFileTextBox
            // 
            this.excelFileTextBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.excelFileTextBox.Location = new System.Drawing.Point(59, 7);
            this.excelFileTextBox.Name = "excelFileTextBox";
            this.excelFileTextBox.Size = new System.Drawing.Size(404, 29);
            this.excelFileTextBox.TabIndex = 1;
            this.excelFileTextBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // button1
            // 
            this.button1.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button1.Location = new System.Drawing.Point(469, 7);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(97, 29);
            this.button1.TabIndex = 3;
            this.button1.Text = "...";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // worksheetsComboBox
            // 
            this.worksheetsComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.worksheetsComboBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.worksheetsComboBox.FormattingEnabled = true;
            this.worksheetsComboBox.Location = new System.Drawing.Point(59, 42);
            this.worksheetsComboBox.Name = "worksheetsComboBox";
            this.worksheetsComboBox.Size = new System.Drawing.Size(404, 28);
            this.worksheetsComboBox.TabIndex = 4;
            this.worksheetsComboBox.SelectedIndexChanged += new System.EventHandler(this.worksheetsComboBox_SelectedIndexChanged);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(588, 3);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(142, 33);
            this.button2.TabIndex = 5;
            this.button2.Text = "Importer données famille";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(588, 37);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(142, 33);
            this.button3.TabIndex = 6;
            this.button3.Text = "Importer données stocks";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(873, 402);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.worksheetsComboBox);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.excelFileTextBox);
            this.Controls.Add(this.excelDataGridView);
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.excelDataGridView)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView excelDataGridView;
        private System.Windows.Forms.TextBox excelFileTextBox;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.ComboBox worksheetsComboBox;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button3;
    }
}