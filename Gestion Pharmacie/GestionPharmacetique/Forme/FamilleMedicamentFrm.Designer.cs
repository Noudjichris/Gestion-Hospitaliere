namespace GestionPharmacetique
{
    partial class FamilleMedicamentFrm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FamilleMedicamentFrm));
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.listView1 = new System.Windows.Forms.ListView();
            this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader2 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.btnModifier = new System.Windows.Forms.Button();
            this.btnAjouter = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.txtDescription = new System.Windows.Forms.TextBox();
            this.txtNumeroFamille = new System.Windows.Forms.TextBox();
            this.label16 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.borderLabel1 = new cSouza.WinForms.Controls.BorderLabel();
            this.button2 = new System.Windows.Forms.Button();
            this.groupBox2.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox2
            // 
            this.groupBox2.BackColor = System.Drawing.Color.Transparent;
            this.groupBox2.Controls.Add(this.listView1);
            this.groupBox2.Controls.Add(this.btnModifier);
            this.groupBox2.Controls.Add(this.btnAjouter);
            this.groupBox2.Controls.Add(this.label1);
            this.groupBox2.Controls.Add(this.txtDescription);
            this.groupBox2.Controls.Add(this.txtNumeroFamille);
            this.groupBox2.Controls.Add(this.label16);
            this.groupBox2.Font = new System.Drawing.Font("Arial Unicode MS", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox2.Location = new System.Drawing.Point(14, 74);
            this.groupBox2.Margin = new System.Windows.Forms.Padding(5, 6, 5, 6);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Padding = new System.Windows.Forms.Padding(5, 6, 5, 6);
            this.groupBox2.Size = new System.Drawing.Size(456, 675);
            this.groupBox2.TabIndex = 32;
            this.groupBox2.TabStop = false;
            // 
            // listView1
            // 
            this.listView1.CheckBoxes = true;
            this.listView1.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1,
            this.columnHeader2});
            this.listView1.FullRowSelect = true;
            this.listView1.Location = new System.Drawing.Point(25, 154);
            this.listView1.Margin = new System.Windows.Forms.Padding(5, 6, 5, 6);
            this.listView1.Name = "listView1";
            this.listView1.Size = new System.Drawing.Size(421, 509);
            this.listView1.TabIndex = 40;
            this.listView1.UseCompatibleStateImageBehavior = false;
            this.listView1.View = System.Windows.Forms.View.Details;
            this.listView1.SelectedIndexChanged += new System.EventHandler(this.listView1_SelectedIndexChanged);
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "CODE";
            this.columnHeader1.Width = 101;
            // 
            // columnHeader2
            // 
            this.columnHeader2.Text = "FAMILLE PRODUIT";
            this.columnHeader2.Width = 320;
            // 
            // btnModifier
            // 
            this.btnModifier.BackColor = System.Drawing.Color.Blue;
            this.btnModifier.FlatAppearance.BorderSize = 2;
            this.btnModifier.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnModifier.Font = new System.Drawing.Font("Arial Unicode MS", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnModifier.ForeColor = System.Drawing.Color.White;
            this.btnModifier.Image = ((System.Drawing.Image)(resources.GetObject("btnModifier.Image")));
            this.btnModifier.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnModifier.Location = new System.Drawing.Point(299, 108);
            this.btnModifier.Margin = new System.Windows.Forms.Padding(5, 6, 5, 6);
            this.btnModifier.Name = "btnModifier";
            this.btnModifier.Size = new System.Drawing.Size(147, 43);
            this.btnModifier.TabIndex = 37;
            this.btnModifier.Text = "MODIFIER";
            this.btnModifier.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnModifier.UseVisualStyleBackColor = false;
            this.btnModifier.Click += new System.EventHandler(this.btnModifier_Click);
            // 
            // btnAjouter
            // 
            this.btnAjouter.BackColor = System.Drawing.Color.Blue;
            this.btnAjouter.FlatAppearance.BorderSize = 2;
            this.btnAjouter.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnAjouter.Font = new System.Drawing.Font("Arial Unicode MS", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAjouter.ForeColor = System.Drawing.Color.White;
            this.btnAjouter.Image = ((System.Drawing.Image)(resources.GetObject("btnAjouter.Image")));
            this.btnAjouter.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnAjouter.Location = new System.Drawing.Point(152, 108);
            this.btnAjouter.Margin = new System.Windows.Forms.Padding(5, 6, 5, 6);
            this.btnAjouter.Name = "btnAjouter";
            this.btnAjouter.Size = new System.Drawing.Size(148, 43);
            this.btnAjouter.TabIndex = 7;
            this.btnAjouter.Text = "AJOUTER";
            this.btnAjouter.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnAjouter.UseVisualStyleBackColor = false;
            this.btnAjouter.Click += new System.EventHandler(this.btnAjouter_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Arial Unicode MS", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(64)))));
            this.label1.Location = new System.Drawing.Point(8, 75);
            this.label1.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(128, 26);
            this.label1.TabIndex = 35;
            this.label1.Text = "Designation :";
            // 
            // txtDescription
            // 
            this.txtDescription.Font = new System.Drawing.Font("Arial Unicode MS", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtDescription.Location = new System.Drawing.Point(152, 72);
            this.txtDescription.Margin = new System.Windows.Forms.Padding(5, 6, 5, 6);
            this.txtDescription.Name = "txtDescription";
            this.txtDescription.Size = new System.Drawing.Size(294, 34);
            this.txtDescription.TabIndex = 3;
            this.txtDescription.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // txtNumeroFamille
            // 
            this.txtNumeroFamille.Font = new System.Drawing.Font("Arial Unicode MS", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtNumeroFamille.Location = new System.Drawing.Point(152, 35);
            this.txtNumeroFamille.Margin = new System.Windows.Forms.Padding(5, 6, 5, 6);
            this.txtNumeroFamille.Name = "txtNumeroFamille";
            this.txtNumeroFamille.Size = new System.Drawing.Size(294, 34);
            this.txtNumeroFamille.TabIndex = 1;
            this.txtNumeroFamille.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.Font = new System.Drawing.Font("Arial Unicode MS", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label16.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(64)))));
            this.label16.Location = new System.Drawing.Point(8, 34);
            this.label16.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(134, 26);
            this.label16.TabIndex = 25;
            this.label16.Text = "Code famille :";
            // 
            // groupBox1
            // 
            this.groupBox1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(192)))));
            this.groupBox1.Controls.Add(this.button2);
            this.groupBox1.Controls.Add(this.borderLabel1);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBox1.Location = new System.Drawing.Point(0, 0);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(5, 6, 5, 6);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(5, 6, 5, 6);
            this.groupBox1.Size = new System.Drawing.Size(476, 62);
            this.groupBox1.TabIndex = 33;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "groupBox1";
            this.groupBox1.Paint += new System.Windows.Forms.PaintEventHandler(this.groupBox1_Paint);
            // 
            // borderLabel1
            // 
            this.borderLabel1.BackColor = System.Drawing.Color.Transparent;
            this.borderLabel1.Font = new System.Drawing.Font("Elephant", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.borderLabel1.ForeColor = System.Drawing.Color.Blue;
            this.borderLabel1.Location = new System.Drawing.Point(11, 0);
            this.borderLabel1.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.borderLabel1.Name = "borderLabel1";
            this.borderLabel1.Size = new System.Drawing.Size(459, 51);
            this.borderLabel1.TabIndex = 34;
            this.borderLabel1.Text = "Famille des médicaments";
            // 
            // button2
            // 
            this.button2.BackColor = System.Drawing.Color.Transparent;
            this.button2.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("button2.BackgroundImage")));
            this.button2.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.button2.FlatAppearance.BorderSize = 2;
            this.button2.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button2.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button2.ForeColor = System.Drawing.Color.White;
            this.button2.Location = new System.Drawing.Point(424, 4);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(46, 54);
            this.button2.TabIndex = 158;
            this.button2.UseVisualStyleBackColor = false;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // FamilleMedicamentFrm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 24F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(476, 764);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.groupBox2);
            this.Font = new System.Drawing.Font("Arial Unicode MS", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Margin = new System.Windows.Forms.Padding(5, 6, 5, 6);
            this.Name = "FamilleMedicamentFrm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "FamilleMedicamentFrm";
            this.Load += new System.EventHandler(this.FamilleMedicamentFrm_Load);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox2;
        public System.Windows.Forms.Button btnModifier;
        public System.Windows.Forms.Button btnAjouter;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtDescription;
        private System.Windows.Forms.TextBox txtNumeroFamille;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.ListView listView1;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.ColumnHeader columnHeader2;
        private System.Windows.Forms.GroupBox groupBox1;
        private cSouza.WinForms.Controls.BorderLabel borderLabel1;
        public System.Windows.Forms.Button button2;
    }
}