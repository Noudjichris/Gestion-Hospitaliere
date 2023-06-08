namespace GestionPharmacetique.Forme
{
    partial class LivraisonFournisseurFrm
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(LivraisonFournisseurFrm));
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label11 = new System.Windows.Forms.Label();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.txtPrixPublic = new System.Windows.Forms.TextBox();
            this.txtQte = new System.Windows.Forms.TextBox();
            this.txtPrixTotal = new System.Windows.Forms.TextBox();
            this.txtPrixCession = new System.Windows.Forms.TextBox();
            this.dgvVente = new System.Windows.Forms.DataGridView();
            this.Column1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.cl1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.cl2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.cl3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.cl4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.cl5 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.cl6 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.txtRechercherProduit = new System.Windows.Forms.TextBox();
            this.groupBox5 = new System.Windows.Forms.GroupBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.comboBox2 = new System.Windows.Forms.ComboBox();
            this.cmbFournisseur = new System.Windows.Forms.ComboBox();
            this.dateTimePicker1 = new System.Windows.Forms.DateTimePicker();
            this.txtNoLivraisom = new System.Windows.Forms.TextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.txtMntFact = new System.Windows.Forms.TextBox();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.label3 = new System.Windows.Forms.Label();
            this.txtMontantTTC = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.txtAutresCharges = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.groupBox13 = new System.Windows.Forms.GroupBox();
            this.button1 = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.btnEnregistreLivraison = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.btnAnnuerCmd = new System.Windows.Forms.Button();
            this.btnFermer = new System.Windows.Forms.Button();
            this.button6 = new System.Windows.Forms.Button();
            this.button7 = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.groupBox3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvVente)).BeginInit();
            this.groupBox5.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.groupBox13.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(192)))));
            this.groupBox1.Controls.Add(this.label11);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBox1.Location = new System.Drawing.Point(0, 0);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(1028, 41);
            this.groupBox1.TabIndex = 32;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "groupBox1";
            this.groupBox1.Paint += new System.Windows.Forms.PaintEventHandler(this.groupBox1_Paint);
            // 
            // label11
            // 
            this.label11.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.label11.AutoSize = true;
            this.label11.BackColor = System.Drawing.Color.Transparent;
            this.label11.Font = new System.Drawing.Font("Times New Roman", 24F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label11.ForeColor = System.Drawing.Color.White;
            this.label11.Location = new System.Drawing.Point(3, 0);
            this.label11.Margin = new System.Windows.Forms.Padding(0);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(469, 36);
            this.label11.TabIndex = 55;
            this.label11.Text = "Gestion de livraison des produits ";
            // 
            // groupBox3
            // 
            this.groupBox3.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox3.Controls.Add(this.txtPrixPublic);
            this.groupBox3.Controls.Add(this.txtQte);
            this.groupBox3.Controls.Add(this.txtPrixTotal);
            this.groupBox3.Controls.Add(this.txtPrixCession);
            this.groupBox3.Controls.Add(this.dgvVente);
            this.groupBox3.Controls.Add(this.txtRechercherProduit);
            this.groupBox3.Font = new System.Drawing.Font("Arial Unicode MS", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox3.Location = new System.Drawing.Point(4, 130);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(1014, 422);
            this.groupBox3.TabIndex = 33;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "groupBox3";
            this.groupBox3.Paint += new System.Windows.Forms.PaintEventHandler(this.groupBox3_Paint);
            // 
            // txtPrixPublic
            // 
            this.txtPrixPublic.BackColor = System.Drawing.Color.White;
            this.txtPrixPublic.Enabled = false;
            this.txtPrixPublic.Font = new System.Drawing.Font("Arial Unicode MS", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtPrixPublic.Location = new System.Drawing.Point(490, 2);
            this.txtPrixPublic.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.txtPrixPublic.Name = "txtPrixPublic";
            this.txtPrixPublic.Size = new System.Drawing.Size(138, 32);
            this.txtPrixPublic.TabIndex = 7;
            this.txtPrixPublic.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtPrixPublic.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtPrixPublic_KeyDown);
            // 
            // txtQte
            // 
            this.txtQte.BackColor = System.Drawing.Color.White;
            this.txtQte.Enabled = false;
            this.txtQte.Font = new System.Drawing.Font("Arial Unicode MS", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtQte.Location = new System.Drawing.Point(628, 2);
            this.txtQte.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.txtQte.Name = "txtQte";
            this.txtQte.Size = new System.Drawing.Size(138, 32);
            this.txtQte.TabIndex = 6;
            this.txtQte.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtQte.TextChanged += new System.EventHandler(this.txtQte_TextChanged);
            this.txtQte.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtQte_KeyDown);
            // 
            // txtPrixTotal
            // 
            this.txtPrixTotal.BackColor = System.Drawing.Color.White;
            this.txtPrixTotal.Enabled = false;
            this.txtPrixTotal.Font = new System.Drawing.Font("Arial Unicode MS", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtPrixTotal.Location = new System.Drawing.Point(764, 2);
            this.txtPrixTotal.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.txtPrixTotal.Name = "txtPrixTotal";
            this.txtPrixTotal.Size = new System.Drawing.Size(138, 32);
            this.txtPrixTotal.TabIndex = 5;
            this.txtPrixTotal.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // txtPrixCession
            // 
            this.txtPrixCession.BackColor = System.Drawing.Color.White;
            this.txtPrixCession.Enabled = false;
            this.txtPrixCession.Font = new System.Drawing.Font("Arial Unicode MS", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtPrixCession.Location = new System.Drawing.Point(354, 2);
            this.txtPrixCession.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.txtPrixCession.Name = "txtPrixCession";
            this.txtPrixCession.Size = new System.Drawing.Size(138, 32);
            this.txtPrixCession.TabIndex = 4;
            this.txtPrixCession.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtPrixCession.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtPrixCession_KeyDown);
            // 
            // dgvVente
            // 
            this.dgvVente.AllowUserToAddRows = false;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Arial Unicode MS", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.Color.DeepSkyBlue;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.Color.Black;
            this.dgvVente.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.dgvVente.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvVente.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvVente.BackgroundColor = System.Drawing.SystemColors.Control;
            this.dgvVente.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.dgvVente.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.RaisedVertical;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.SteelBlue;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Arial Unicode MS", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle2.Padding = new System.Windows.Forms.Padding(0, 7, 0, 7);
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvVente.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.dgvVente.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvVente.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Column1,
            this.dataGridViewTextBoxColumn1,
            this.cl1,
            this.cl2,
            this.cl3,
            this.cl4,
            this.cl5,
            this.cl6});
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.Color.PaleTurquoise;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Arial Unicode MS", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.Color.Transparent;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvVente.DefaultCellStyle = dataGridViewCellStyle3;
            this.dgvVente.EnableHeadersVisualStyles = false;
            this.dgvVente.GridColor = System.Drawing.Color.White;
            this.dgvVente.Location = new System.Drawing.Point(0, 37);
            this.dgvVente.Name = "dgvVente";
            this.dgvVente.ReadOnly = true;
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle4.BackColor = System.Drawing.SystemColors.ActiveCaption;
            dataGridViewCellStyle4.Font = new System.Drawing.Font("Arial Unicode MS", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle4.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle4.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle4.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvVente.RowHeadersDefaultCellStyle = dataGridViewCellStyle4;
            this.dgvVente.RowHeadersVisible = false;
            dataGridViewCellStyle5.BackColor = System.Drawing.SystemColors.InactiveBorder;
            dataGridViewCellStyle5.Font = new System.Drawing.Font("Arial Unicode MS", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle5.ForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle5.SelectionBackColor = System.Drawing.Color.DeepSkyBlue;
            dataGridViewCellStyle5.SelectionForeColor = System.Drawing.Color.Black;
            this.dgvVente.RowsDefaultCellStyle = dataGridViewCellStyle5;
            this.dgvVente.RowTemplate.DefaultCellStyle.NullValue = null;
            this.dgvVente.RowTemplate.Height = 25;
            this.dgvVente.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvVente.Size = new System.Drawing.Size(1008, 377);
            this.dgvVente.TabIndex = 3;
            this.dgvVente.CellEndEdit += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvVente_CellEndEdit);
            this.dgvVente.DoubleClick += new System.EventHandler(this.dgvVente_DoubleClick);
            this.dgvVente.KeyDown += new System.Windows.Forms.KeyEventHandler(this.dgvVente_KeyDown);
            // 
            // Column1
            // 
            this.Column1.HeaderText = "ID";
            this.Column1.Name = "Column1";
            this.Column1.ReadOnly = true;
            this.Column1.Visible = false;
            // 
            // dataGridViewTextBoxColumn1
            // 
            this.dataGridViewTextBoxColumn1.HeaderText = "CODE";
            this.dataGridViewTextBoxColumn1.Name = "dataGridViewTextBoxColumn1";
            this.dataGridViewTextBoxColumn1.ReadOnly = true;
            this.dataGridViewTextBoxColumn1.Visible = false;
            // 
            // cl1
            // 
            this.cl1.HeaderText = "DESIGNATION";
            this.cl1.Name = "cl1";
            this.cl1.ReadOnly = true;
            // 
            // cl2
            // 
            this.cl2.HeaderText = "PRIX CESSION";
            this.cl2.Name = "cl2";
            this.cl2.ReadOnly = true;
            // 
            // cl3
            // 
            this.cl3.HeaderText = "PRIX PUBLIC";
            this.cl3.Name = "cl3";
            this.cl3.ReadOnly = true;
            // 
            // cl4
            // 
            this.cl4.HeaderText = "QTE";
            this.cl4.Name = "cl4";
            this.cl4.ReadOnly = true;
            // 
            // cl5
            // 
            this.cl5.HeaderText = "PRIX TOTAL";
            this.cl5.Name = "cl5";
            this.cl5.ReadOnly = true;
            // 
            // cl6
            // 
            this.cl6.HeaderText = "PEREMPTION";
            this.cl6.Name = "cl6";
            this.cl6.ReadOnly = true;
            this.cl6.Visible = false;
            // 
            // txtRechercherProduit
            // 
            this.txtRechercherProduit.BackColor = System.Drawing.Color.White;
            this.txtRechercherProduit.Enabled = false;
            this.txtRechercherProduit.Font = new System.Drawing.Font("Arial Unicode MS", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtRechercherProduit.Location = new System.Drawing.Point(2, 2);
            this.txtRechercherProduit.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.txtRechercherProduit.Name = "txtRechercherProduit";
            this.txtRechercherProduit.Size = new System.Drawing.Size(345, 32);
            this.txtRechercherProduit.TabIndex = 1;
            this.txtRechercherProduit.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtRechercherProduit.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtRechercherProduit_KeyDown);
            // 
            // groupBox5
            // 
            this.groupBox5.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox5.Controls.Add(this.label1);
            this.groupBox5.Controls.Add(this.label9);
            this.groupBox5.Controls.Add(this.comboBox2);
            this.groupBox5.Controls.Add(this.cmbFournisseur);
            this.groupBox5.Controls.Add(this.dateTimePicker1);
            this.groupBox5.Controls.Add(this.txtNoLivraisom);
            this.groupBox5.Controls.Add(this.label10);
            this.groupBox5.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox5.Location = new System.Drawing.Point(4, 93);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Size = new System.Drawing.Size(1014, 34);
            this.groupBox5.TabIndex = 49;
            this.groupBox5.TabStop = false;
            this.groupBox5.Paint += new System.Windows.Forms.PaintEventHandler(this.groupBox5_Paint);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Font = new System.Drawing.Font("Arial Unicode MS", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(3, 5);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(122, 25);
            this.label1.TabIndex = 100;
            this.label1.Text = "Fournisseur :";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.BackColor = System.Drawing.Color.Transparent;
            this.label9.Font = new System.Drawing.Font("Arial Unicode MS", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label9.ForeColor = System.Drawing.Color.White;
            this.label9.Location = new System.Drawing.Point(604, 5);
            this.label9.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(63, 25);
            this.label9.TabIndex = 85;
            this.label9.Text = "Date :";
            // 
            // comboBox2
            // 
            this.comboBox2.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.comboBox2.Font = new System.Drawing.Font("Arial Unicode MS", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.comboBox2.ForeColor = System.Drawing.Color.Black;
            this.comboBox2.FormattingEnabled = true;
            this.comboBox2.Items.AddRange(new object[] {
            "Ordre de saisie",
            "Désignation"});
            this.comboBox2.Location = new System.Drawing.Point(812, 3);
            this.comboBox2.Name = "comboBox2";
            this.comboBox2.Size = new System.Drawing.Size(136, 29);
            this.comboBox2.TabIndex = 89;
            this.comboBox2.Text = "[ Ordonné par : ]";
            this.comboBox2.SelectedIndexChanged += new System.EventHandler(this.comboBox2_SelectedIndexChanged);
            // 
            // cmbFournisseur
            // 
            this.cmbFournisseur.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbFournisseur.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cmbFournisseur.Font = new System.Drawing.Font("Arial Unicode MS", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbFournisseur.ForeColor = System.Drawing.Color.Navy;
            this.cmbFournisseur.FormattingEnabled = true;
            this.cmbFournisseur.Location = new System.Drawing.Point(125, 3);
            this.cmbFournisseur.Name = "cmbFournisseur";
            this.cmbFournisseur.Size = new System.Drawing.Size(235, 29);
            this.cmbFournisseur.TabIndex = 99;
            this.cmbFournisseur.SelectedIndexChanged += new System.EventHandler(this.cmbFournisseur_SelectedIndexChanged);
            // 
            // dateTimePicker1
            // 
            this.dateTimePicker1.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dateTimePicker1.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dateTimePicker1.Location = new System.Drawing.Point(670, 4);
            this.dateTimePicker1.Name = "dateTimePicker1";
            this.dateTimePicker1.Size = new System.Drawing.Size(123, 29);
            this.dateTimePicker1.TabIndex = 84;
            // 
            // txtNoLivraisom
            // 
            this.txtNoLivraisom.Font = new System.Drawing.Font("Arial Unicode MS", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtNoLivraisom.Location = new System.Drawing.Point(469, 4);
            this.txtNoLivraisom.Name = "txtNoLivraisom";
            this.txtNoLivraisom.Size = new System.Drawing.Size(134, 29);
            this.txtNoLivraisom.TabIndex = 98;
            this.txtNoLivraisom.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtNoLivraisom.TextChanged += new System.EventHandler(this.textBox1_TextChanged);
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.BackColor = System.Drawing.Color.Transparent;
            this.label10.Font = new System.Drawing.Font("Arial Unicode MS", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label10.ForeColor = System.Drawing.Color.White;
            this.label10.Location = new System.Drawing.Point(364, 4);
            this.label10.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(110, 25);
            this.label10.TabIndex = 82;
            this.label10.Text = "No facture :";
            // 
            // comboBox1
            // 
            this.comboBox1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.comboBox1.Font = new System.Drawing.Font("Arial Unicode MS", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.comboBox1.ForeColor = System.Drawing.Color.Black;
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Location = new System.Drawing.Point(5, 4);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(230, 29);
            this.comboBox1.TabIndex = 81;
            this.comboBox1.Text = "[Information facture]";
            this.comboBox1.SelectedIndexChanged += new System.EventHandler(this.comboBox1_SelectedIndexChanged);
            // 
            // txtMntFact
            // 
            this.txtMntFact.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtMntFact.Location = new System.Drawing.Point(308, 1);
            this.txtMntFact.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.txtMntFact.Name = "txtMntFact";
            this.txtMntFact.ReadOnly = true;
            this.txtMntFact.Size = new System.Drawing.Size(126, 31);
            this.txtMntFact.TabIndex = 81;
            this.txtMntFact.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.label3);
            this.groupBox4.Controls.Add(this.txtMontantTTC);
            this.groupBox4.Controls.Add(this.label4);
            this.groupBox4.Controls.Add(this.txtAutresCharges);
            this.groupBox4.Controls.Add(this.label7);
            this.groupBox4.Controls.Add(this.txtMntFact);
            this.groupBox4.Controls.Add(this.comboBox1);
            this.groupBox4.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.groupBox4.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox4.Location = new System.Drawing.Point(0, 568);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(1028, 34);
            this.groupBox4.TabIndex = 50;
            this.groupBox4.TabStop = false;
            this.groupBox4.Paint += new System.Windows.Forms.PaintEventHandler(this.groupBox4_Paint);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.BackColor = System.Drawing.Color.Transparent;
            this.label3.Font = new System.Drawing.Font("Arial Unicode MS", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.Color.White;
            this.label3.Location = new System.Drawing.Point(670, 8);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(75, 21);
            this.label3.TabIndex = 92;
            this.label3.Text = "Mx TTC :";
            // 
            // txtMontantTTC
            // 
            this.txtMontantTTC.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtMontantTTC.Location = new System.Drawing.Point(748, 0);
            this.txtMontantTTC.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.txtMontantTTC.Name = "txtMontantTTC";
            this.txtMontantTTC.ReadOnly = true;
            this.txtMontantTTC.Size = new System.Drawing.Size(126, 31);
            this.txtMontantTTC.TabIndex = 93;
            this.txtMontantTTC.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.BackColor = System.Drawing.Color.Transparent;
            this.label4.Font = new System.Drawing.Font("Arial Unicode MS", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.ForeColor = System.Drawing.Color.White;
            this.label4.Location = new System.Drawing.Point(441, 6);
            this.label4.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(89, 21);
            this.label4.TabIndex = 90;
            this.label4.Text = "Autres Mx :";
            // 
            // txtAutresCharges
            // 
            this.txtAutresCharges.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtAutresCharges.Location = new System.Drawing.Point(532, 1);
            this.txtAutresCharges.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.txtAutresCharges.Name = "txtAutresCharges";
            this.txtAutresCharges.Size = new System.Drawing.Size(126, 31);
            this.txtAutresCharges.TabIndex = 91;
            this.txtAutresCharges.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtAutresCharges.TextChanged += new System.EventHandler(this.txtAutresCharges_TextChanged);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.BackColor = System.Drawing.Color.Transparent;
            this.label7.Font = new System.Drawing.Font("Arial Unicode MS", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.ForeColor = System.Drawing.Color.White;
            this.label7.Location = new System.Drawing.Point(238, 6);
            this.label7.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(65, 21);
            this.label7.TabIndex = 24;
            this.label7.Text = "Mx HT :";
            // 
            // groupBox13
            // 
            this.groupBox13.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox13.BackColor = System.Drawing.Color.Transparent;
            this.groupBox13.Controls.Add(this.button1);
            this.groupBox13.Controls.Add(this.button3);
            this.groupBox13.Controls.Add(this.btnEnregistreLivraison);
            this.groupBox13.Controls.Add(this.button2);
            this.groupBox13.Controls.Add(this.btnAnnuerCmd);
            this.groupBox13.Controls.Add(this.btnFermer);
            this.groupBox13.Controls.Add(this.button6);
            this.groupBox13.Controls.Add(this.button7);
            this.groupBox13.Location = new System.Drawing.Point(5, 47);
            this.groupBox13.Name = "groupBox13";
            this.groupBox13.Size = new System.Drawing.Size(1014, 41);
            this.groupBox13.TabIndex = 65;
            this.groupBox13.TabStop = false;
            this.groupBox13.Paint += new System.Windows.Forms.PaintEventHandler(this.groupBox13_Paint);
            // 
            // button1
            // 
            this.button1.BackColor = System.Drawing.Color.Transparent;
            this.button1.FlatAppearance.BorderColor = System.Drawing.Color.DarkGray;
            this.button1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.button1.Image = ((System.Drawing.Image)(resources.GetObject("button1.Image")));
            this.button1.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.button1.Location = new System.Drawing.Point(213, 3);
            this.button1.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(99, 35);
            this.button1.TabIndex = 64;
            this.button1.Text = "RETIRER";
            this.button1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.button1.UseVisualStyleBackColor = false;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // button3
            // 
            this.button3.BackColor = System.Drawing.Color.Transparent;
            this.button3.FlatAppearance.BorderColor = System.Drawing.Color.DarkGray;
            this.button3.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button3.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button3.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.button3.Image = ((System.Drawing.Image)(resources.GetObject("button3.Image")));
            this.button3.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.button3.Location = new System.Drawing.Point(728, 3);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(105, 35);
            this.button3.TabIndex = 63;
            this.button3.Text = "PRODUIT";
            this.button3.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.button3.UseVisualStyleBackColor = false;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // btnEnregistreLivraison
            // 
            this.btnEnregistreLivraison.BackColor = System.Drawing.Color.Transparent;
            this.btnEnregistreLivraison.Enabled = false;
            this.btnEnregistreLivraison.FlatAppearance.BorderColor = System.Drawing.Color.DarkGray;
            this.btnEnregistreLivraison.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnEnregistreLivraison.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnEnregistreLivraison.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.btnEnregistreLivraison.Image = ((System.Drawing.Image)(resources.GetObject("btnEnregistreLivraison.Image")));
            this.btnEnregistreLivraison.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnEnregistreLivraison.Location = new System.Drawing.Point(116, 3);
            this.btnEnregistreLivraison.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnEnregistreLivraison.Name = "btnEnregistreLivraison";
            this.btnEnregistreLivraison.Size = new System.Drawing.Size(96, 35);
            this.btnEnregistreLivraison.TabIndex = 62;
            this.btnEnregistreLivraison.Text = "INSERER";
            this.btnEnregistreLivraison.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnEnregistreLivraison.UseVisualStyleBackColor = false;
            this.btnEnregistreLivraison.Click += new System.EventHandler(this.button8_Click);
            // 
            // button2
            // 
            this.button2.BackColor = System.Drawing.Color.Transparent;
            this.button2.FlatAppearance.BorderColor = System.Drawing.Color.DarkGray;
            this.button2.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button2.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.button2.Image = ((System.Drawing.Image)(resources.GetObject("button2.Image")));
            this.button2.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.button2.Location = new System.Drawing.Point(437, 3);
            this.button2.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(168, 35);
            this.button2.TabIndex = 61;
            this.button2.Text = "RETOUR PRODUIT";
            this.button2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.button2.UseVisualStyleBackColor = false;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // btnAnnuerCmd
            // 
            this.btnAnnuerCmd.BackColor = System.Drawing.Color.Transparent;
            this.btnAnnuerCmd.FlatAppearance.BorderColor = System.Drawing.Color.DarkGray;
            this.btnAnnuerCmd.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAnnuerCmd.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAnnuerCmd.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.btnAnnuerCmd.Image = ((System.Drawing.Image)(resources.GetObject("btnAnnuerCmd.Image")));
            this.btnAnnuerCmd.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnAnnuerCmd.Location = new System.Drawing.Point(313, 3);
            this.btnAnnuerCmd.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnAnnuerCmd.Name = "btnAnnuerCmd";
            this.btnAnnuerCmd.Size = new System.Drawing.Size(122, 35);
            this.btnAnnuerCmd.TabIndex = 57;
            this.btnAnnuerCmd.Text = "SUPPRIMER";
            this.btnAnnuerCmd.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnAnnuerCmd.UseVisualStyleBackColor = false;
            this.btnAnnuerCmd.Click += new System.EventHandler(this.btnAnnuerCmd_Click);
            // 
            // btnFermer
            // 
            this.btnFermer.AccessibleDescription = "FERMER";
            this.btnFermer.BackColor = System.Drawing.Color.Transparent;
            this.btnFermer.FlatAppearance.BorderColor = System.Drawing.Color.DarkGray;
            this.btnFermer.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnFermer.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnFermer.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.btnFermer.Image = ((System.Drawing.Image)(resources.GetObject("btnFermer.Image")));
            this.btnFermer.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnFermer.Location = new System.Drawing.Point(835, 3);
            this.btnFermer.Name = "btnFermer";
            this.btnFermer.Size = new System.Drawing.Size(101, 35);
            this.btnFermer.TabIndex = 38;
            this.btnFermer.Text = "FERMER";
            this.btnFermer.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnFermer.UseVisualStyleBackColor = false;
            this.btnFermer.Click += new System.EventHandler(this.btnFermer_Click);
            // 
            // button6
            // 
            this.button6.BackColor = System.Drawing.Color.Transparent;
            this.button6.FlatAppearance.BorderColor = System.Drawing.Color.DarkGray;
            this.button6.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button6.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button6.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.button6.Image = ((System.Drawing.Image)(resources.GetObject("button6.Image")));
            this.button6.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.button6.Location = new System.Drawing.Point(606, 3);
            this.button6.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.button6.Name = "button6";
            this.button6.Size = new System.Drawing.Size(121, 35);
            this.button6.TabIndex = 59;
            this.button6.Text = "IMPRIMER";
            this.button6.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.button6.UseVisualStyleBackColor = false;
            this.button6.Click += new System.EventHandler(this.button6_Click);
            // 
            // button7
            // 
            this.button7.BackColor = System.Drawing.Color.Transparent;
            this.button7.FlatAppearance.BorderColor = System.Drawing.Color.DarkGray;
            this.button7.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button7.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button7.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.button7.Image = ((System.Drawing.Image)(resources.GetObject("button7.Image")));
            this.button7.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.button7.Location = new System.Drawing.Point(2, 3);
            this.button7.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.button7.Name = "button7";
            this.button7.Size = new System.Drawing.Size(112, 35);
            this.button7.TabIndex = 58;
            this.button7.Text = "NOUVELLE";
            this.button7.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.button7.UseVisualStyleBackColor = false;
            this.button7.Click += new System.EventHandler(this.button7_Click);
            // 
            // LivraisonFournisseurFrm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(1028, 602);
            this.Controls.Add(this.groupBox13);
            this.Controls.Add(this.groupBox4);
            this.Controls.Add(this.groupBox5);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "LivraisonFournisseurFrm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "FournisseurFrm";
            this.Load += new System.EventHandler(this.FournisseurFrm_Load);
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.FournisseurFrm_Paint);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvVente)).EndInit();
            this.groupBox5.ResumeLayout(false);
            this.groupBox5.PerformLayout();
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            this.groupBox13.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        public System.Windows.Forms.Button btnFermer;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.DataGridView dgvVente;
        private System.Windows.Forms.GroupBox groupBox5;
        private System.Windows.Forms.TextBox txtMntFact;
        private System.Windows.Forms.TextBox txtRechercherProduit;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.GroupBox groupBox13;
        public System.Windows.Forms.Button button2;
        public System.Windows.Forms.Button btnAnnuerCmd;
        public System.Windows.Forms.Button button6;
        public System.Windows.Forms.Button button7;
        public System.Windows.Forms.Button btnEnregistreLivraison;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.DateTimePicker dateTimePicker1;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.ComboBox comboBox1;
        public System.Windows.Forms.Button button3;
        private System.Windows.Forms.TextBox txtNoLivraisom;
        public System.Windows.Forms.Button button1;
        private System.Windows.Forms.ComboBox cmbFournisseur;
        private System.Windows.Forms.TextBox txtPrixPublic;
        private System.Windows.Forms.TextBox txtQte;
        private System.Windows.Forms.TextBox txtPrixTotal;
        private System.Windows.Forms.TextBox txtPrixCession;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox comboBox2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtMontantTTC;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtAutresCharges;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column1;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn1;
        private System.Windows.Forms.DataGridViewTextBoxColumn cl1;
        private System.Windows.Forms.DataGridViewTextBoxColumn cl2;
        private System.Windows.Forms.DataGridViewTextBoxColumn cl3;
        private System.Windows.Forms.DataGridViewTextBoxColumn cl4;
        private System.Windows.Forms.DataGridViewTextBoxColumn cl5;
        private System.Windows.Forms.DataGridViewTextBoxColumn cl6;
    }
}