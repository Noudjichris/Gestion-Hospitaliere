using System;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Collections;
using System.Text;
using System.Windows.Forms;
using System.Drawing.Drawing2D;
using GestionPharmacetique;
using SGSP.AppCode;
using BarcodeLib.Barcode;

namespace SGSP
{
    public partial class PersonnelFrm : Form
    {
        public PersonnelFrm()
        {
            InitializeComponent();
        }

        private void groupBox2_Paint(object sender, PaintEventArgs e)
        {
            Graphics mGraphics = e.Graphics;
            Pen pen1 = new Pen(Color.AliceBlue, 0);
            Rectangle area1 = new Rectangle(0, 0, this.groupBox2.Width - 1, this.groupBox2.Height - 1);
            LinearGradientBrush linearGradientBrush = new LinearGradientBrush(area1,
                Color.AliceBlue, Color.AliceBlue, LinearGradientMode.Vertical);
            mGraphics.FillRectangle(linearGradientBrush, area1);
            mGraphics.DrawRectangle(pen1, area1);
        }

        private void groupBox3_Paint(object sender, PaintEventArgs e)
        {
            Graphics mGraphics = e.Graphics;
            Pen pen1 = new Pen(Color.AliceBlue, 0);
            Rectangle area1 = new Rectangle(0, 0, this.groupBox3.Width - 1, this.groupBox3.Height - 1);
            LinearGradientBrush linearGradientBrush = new LinearGradientBrush(area1,
                Color.AliceBlue, Color.AliceBlue, LinearGradientMode.Vertical);
            mGraphics.FillRectangle(linearGradientBrush, area1);
            mGraphics.DrawRectangle(pen1, area1);
        }

        public static PersonnelFrm frm=new PersonnelFrm();
      
        public static bool ShowBox()
        {
            frm = new PersonnelFrm();
            frm.ShowDialog();
            return true;
        }

        // les proprietes du pelerin
        #region proprietesDuPersonnel
        public string _ancienMatricule;
        DateTime _dateNaissance;
        string _prenom, _nom;
        string _lieuNaissance, _sexe, anciennete, diplome, typeContrat;
        string _telephone1, _telephone2, _photo, _numeroMatricule;
        int _numeroDepartement,age;
        string  _adresse, _email, _numeroCompte, categorie, noCNPS;
        string _echelon, _poste;
        DateTime _dateService;
        private decimal salaireBrut, grilleSalariale, indice;

        #endregion

       //ajouter un personnel
        private void btnAjouter_Click(object sender, EventArgs e)
        {
            try
            {
                // mettre la validation pour la date de naissance
                if (!string.IsNullOrEmpty(cmbJour.Text) && !string.IsNullOrEmpty(cmbMois.Text) && !string.IsNullOrEmpty(txtDateNaissance.Text))
                {
                    _dateNaissance = DateTime.Parse(cmbJour.Text + "/" + cmbMois.Text + "/" + txtDateNaissance.Text);

                }
                else if (string.IsNullOrEmpty(cmbJour.Text) && string.IsNullOrEmpty(cmbMois.Text) && !string.IsNullOrEmpty(txtDateNaissance.Text))
                {
                    _dateNaissance = DateTime.Parse("1/1/" + txtDateNaissance.Text);
                }
                else
                {
                    _dateNaissance = DateTime.Now;
                    age = 0;
                    txtAge.Text = age.ToString();
                }

                if (string.IsNullOrEmpty(txtNummatricule.Text))
                {
                    MonMessageBox.ShowBox("Le matricule ne doit être vide", "Erreur");
                    return;
                }
                //valider le champs pour le nom 
                if (string.IsNullOrEmpty(txtNom.Text))
                {
                    MonMessageBox.ShowBox("Veuillez saisir le nom ", "Erreur");
                    return;
                }

                //valider le champs pour le nom du personnel
                if (string.IsNullOrEmpty(txtPrenom.Text))
                {
                    MonMessageBox.ShowBox("Veuillez saisir le prenom ", "Erreur");
                    return;
                }
                
                //valider le champ pour le numero telephone
                if (string.IsNullOrEmpty(txtTelephone1.Text))
                {
                    MonMessageBox.ShowBox("Veuillez saisir le numero de telephone", "Erreur");
                    return;
                }

                //valider le champ pour l adresse
             
                //valider le champ pour le poste
                if (string.IsNullOrEmpty(txtPoste.Text))
                {
                    MonMessageBox.ShowBox("Veuillez saisir le poste du personnel", "Erreur");
                    return;
                }

                //valider le champ pour le departement
                if (string.IsNullOrEmpty(cmbDivision.Text))
                {
                    MonMessageBox.ShowBox("Veuillez selectionner le service du personnel", "Erreur");
                    return;
                }
                //valider le case du sexe du pelerin
                if (!chkFemelle.Checked && !chkMale.Checked)
                {
                    MonMessageBox.ShowBox("Veuillez le cocher pour le sexe", "Erreur");
                    return;
                }
                else if (chkMale.Checked)
                {
                    _sexe = "M";
                }
                else if (chkFemelle.Checked)
                {
                    _sexe = "F";
                }
                
                if (decimal.TryParse(txtSalaire.Text, out salaireBrut) || string.IsNullOrEmpty(txtSalaire.Text))
                {
                    
                }
                else
                {
                    MonMessageBox.ShowBox("Veuillez entrer un chiffre valide pour le salaire de l'employé", "Erreur");
                    return;
                }
                if (string.IsNullOrEmpty(txtSalaire.Text))
                {
                    salaireBrut = 0;
                }
 grilleSalariale = 0;
                

                if (decimal.TryParse(txtIndice.Text, out indice) && !string.IsNullOrEmpty(txtIndice.Text))
                {

                }
                else
                {
                    indice = 0m;
                }
                if (string.IsNullOrEmpty(txtIndice.Text))
                {
                    indice = 0;
                }
                
                var dejaRetraite = false;
                var finContrat = false;
                if (chkNonRetraite.Checked)
                {
                    dejaRetraite = false;
                }
                else if(chkRetraite.Checked)
                {
                    dejaRetraite = true;
                }
                if (chkEnCours.Checked)
                {
                    finContrat = false;
                }
                else if(chkFinContrat.Checked)
                {
                    finContrat = true;
                }
               
                var numeroSalaire = 0;
                ObtenirlesDonneesPersonnel();
                var personnel = new Personnel();
                personnel.NumeroMatricule = _numeroMatricule;
                personnel.Nom = _nom;
                personnel.Prenom = _prenom;
                personnel.Sexe = _sexe;
                personnel.Telephone1 = _telephone1;
                personnel.Telephone2 = _telephone2;
                personnel.NumeroDepartement = _numeroDepartement;
                personnel.LieuNaissance = _lieuNaissance;
                personnel.DateNaissance = _dateNaissance;
                personnel.Adresse = _adresse;
                personnel.Age = age;
                personnel.Email = _email;
                personnel.NumeroCompte = _numeroCompte;
                personnel.CodeBanque = txtCodeBank.Text;
                personnel.CodeGuichet = txtCodeGuichet.Text;
                personnel.Cle = txtClef.Text; 

                var service = new Service();
                service.Anciennete = anciennete;
                service.Categorie = categorie;
                service.Contrat = typeContrat;
                service.DateService = _dateService;
                service.Diplome = diplome;
                service.Echelon = _echelon;
                service.NoCNPS = noCNPS;
                service.NumeroMatricule = _numeroMatricule;
                service.Poste=_poste;
                service.Etat = dejaRetraite;
                service.EtatContrat = finContrat ;
                service.DateFinContrat = dtpFinContrat.Value;
                service.DateRetraite = dtpDateRetraite.Value;

                var salaire = new Salaire();
                salaire.GrilleSalarialle = grilleSalariale;
                salaire.Indice = indice;
                salaire.SalaireBrut = salaireBrut;
                salaire.IDSalaire = numeroSalaire;
                
                 if (etat == "2")
                {
                    if (MonMessageBox.ShowBox("Voulez vous modifier les données de \nce personnel?", "Confirmation") == "1")
                    {
                        if (ConnectionClass.ModifierUnPersonnel(personnel, service,salaire, _ancienMatricule))
                        {
                            ViderLesChamps();
                            this.Close();
                        }
                    }
                }
               else  
                {
                    var dtPers = ConnectionClass.ListeDesPersonnelParNumeroMatricule(txtNummatricule.Text);
                    if (dtPers.Rows.Count > 0)
                    {
                        MonMessageBox.ShowBox("Le matricule que vous avez saisi existe deja dans la base de données", "Erreur");
                        return;
                    }
                    if (ConnectionClass.AjouterUnPersonnel(personnel, service,salaire))
                    {
                        ViderLesChamps();
                    }
                }
            }
            catch (Exception ex)
            {
                MonMessageBox.ShowBox("Enregistrement personnel", ex);
            }
        }

        private void ObtenirlesDonneesPersonnel()
        {
            _numeroCompte = txtNoCompte.Text;
            _nom = txtNom.Text;
            _prenom = txtPrenom.Text;
            _telephone1 = txtTelephone1.Text;
            _telephone2  = txtTelephone2.Text;
            _lieuNaissance = txtLieuNaissance.Text;
            _adresse = txtAdresse.Text;
            _email = txtEmail.Text;
            categorie = txtCategorie.Text;
            age = Int32.Parse(txtAge.Text);
            var  dtPersonnel = ConnectionClass.ListeDepartement(cmbDivision.Text);
            _numeroDepartement = Int32.Parse(dtPersonnel.Rows[0].ItemArray[0].ToString());
            _poste = txtPoste.Text;
            _numeroMatricule = txtNummatricule.Text;
            anciennete = txtAnciennete.Text;
            noCNPS = txtCNPS.Text;
            diplome = cmbDiplome.Text;
            typeContrat = cmbTypeContrat.Text;
            _echelon = txtGrade.Text;
            _dateService = dtpDateservice.Value.Date;
        }

        //vider les champs
        private void ViderLesChamps()
        {
            txtCodeBank.Text = "";
            txtCodeGuichet.Text = "";
            txtClef.Text = "";
            txtNoCompte.Text = "";
            txtSalaire.Text = "";
            txtIndice.Text = "";
            txtEmail.Text = "";
            txtNummatricule.Text = "";
            txtPrenom.Text = "";
            txtTelephone1.Text = "";
            txtNom.Text = "";
            txtLieuNaissance.Text = "";
            txtDateNaissance.Text = "";
            txtCategorie.Text = "";
            txtAnciennete.Text = "";
            chkFemelle.Checked = false;
            chkMale.Checked = false;
            btnAjouter.Enabled = true;
            txtNummatricule.Focus();
            txtAge.Text = "";
            txtCNPS.Text = "";
            txtAnciennete.Text = "";
            cmbDiplome.Text = "";
            txtAdresse.Text = "";
            txtTelephone2.Text = "";
            txtTelephone1.Text = "";
            txtPoste.Text = "";
            txtGrade.Text = "";
        }
        //nouveau personnel
        private void button1_Click(object sender, EventArgs e)
        {
          ViderLesChamps();
        }

        //quitter la forme
        private void btnFermer_Click(object sender, EventArgs e)
        {
            this.Close();
        }
     
        private void button6_Click(object sender, EventArgs e)
        {
            try
            {
                if (!string.IsNullOrEmpty(txtNummatricule.Text))
                {
                    var  open = new OpenFileDialog();
                    //open.InitialDirectory = Application.StartupPath.Substring(0, Application.StartupPath.Length - 10);
                    open.Filter = "Image Files (*.jpg)|*.jpg|all files(*.*)|*.*";
                    open.FilterIndex = 1;
                    if (open.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                    {
                        if (open.CheckFileExists)
                        {
                             _photo = System.IO.Path.GetFileName(open.FileName);
                             var imagePath = AppCode.GlobalVariable.rootPathPersonnel + txtNom.Text + " " + txtPrenom.Text + "//";
                             if (!System.IO.Directory.Exists(imagePath))
                             {
                                 System.IO.Directory.CreateDirectory(imagePath);
                             }
                             imagePath = imagePath + _photo;
                             if (!System.IO.File.Exists(imagePath))
                               {
                                   System.IO.File.Copy(open.FileName, imagePath);
                                   pictureBox1.Image = Image.FromFile(open.FileName);
                                   ConnectionClass.InsereImage(txtNummatricule.Text, imagePath);
                                   MonMessageBox.ShowBox("Photo transferée avec succés ", "Information Photo");
                               }
                               else
                               {
                                   if (MonMessageBox.ShowBox("Ce fichier existe deja Voulez vous faire une copie?", "confirmation") == "1")
                                   {
                                       var rootPath1 = imagePath.Substring(0, imagePath.LastIndexOf(".")) + "_copie1" + imagePath.Substring(imagePath.LastIndexOf("."));
                                       if (!System.IO.File.Exists(rootPath1))
                                       {
                                           System.IO.File.Copy(open.FileName, rootPath1);
                                           MonMessageBox.ShowBox("fichier transferé avec succés ", "Information Photo");
                                       }
                                       else
                                       {

                                           var rootPath2 = imagePath.Substring(0, imagePath.LastIndexOf(".")) + "_copie2" + imagePath.Substring(imagePath.LastIndexOf("."));
                                           if (!System.IO.File.Exists(rootPath2))
                                           {
                                               System.IO.File.Copy(open.FileName, rootPath2);
                                               MonMessageBox.ShowBox("fichier transferé avec succés ", "Information Photo");
                                           }
                                           else
                                           {
                                               var rootPath3 = imagePath.Substring(0, imagePath.LastIndexOf(".")) + "_copie3" + imagePath.Substring(imagePath.LastIndexOf("."));
                                               if (!System.IO.File.Exists(rootPath3))
                                               {
                                                   System.IO.File.Copy(open.FileName, rootPath3);
                                                   MonMessageBox.ShowBox("fichier transferé avec succés ", "Information Photo");
                                               }
                                               else
                                               {
                                                   var rootPath4 = imagePath.Substring(0, imagePath.LastIndexOf(".")) + "_copie4" + imagePath.Substring(imagePath.LastIndexOf("."));
                                                   if (!System.IO.File.Exists(rootPath4))
                                                   {
                                                       System.IO.File.Copy(open.FileName, rootPath4);
                                                       MonMessageBox.ShowBox("fichier transferé avec succés ", "Information Photo");
                                                   }
                                               }
                                           }
                                       }

                                   }
                               } 
                        }
                    }
                }
                else
                {
                    MonMessageBox.ShowBox("Veuillez selectionner les données du personnel. Puis réessayez ", "Information Photo");
                }
            }

            catch (Exception ex) { MonMessageBox.ShowBox("Transfert de l'image", ex); }
        }

        private void cmbMois_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtDateNaissance.Focus();
        }
 
        private void groupBox7_Paint(object sender, PaintEventArgs e)
        {
            Graphics mGraphics = e.Graphics;
            Pen pen1 = new Pen(Color.Silver, 0);
            Rectangle area1 = new Rectangle(0, 0, this.groupBox7.Width - 1, this.groupBox7.Height - 1);
            LinearGradientBrush linearGradientBrush = new LinearGradientBrush(area1,
                Color.SteelBlue, Color.SteelBlue, LinearGradientMode.Vertical);
            mGraphics.FillRectangle(linearGradientBrush, area1);
            mGraphics.DrawRectangle(pen1, area1);
        }

        private void groupBox4_Paint(object sender, PaintEventArgs e)
        {
            Graphics mGraphics = e.Graphics;
            Pen pen1 = new Pen(Color.AliceBlue, 0);
            Rectangle area1 = new Rectangle(0, 0, this.groupBox4.Width - 1, this.groupBox4.Height - 1);
            LinearGradientBrush linearGradientBrush = new LinearGradientBrush(area1,
                Color.AliceBlue, Color.AliceBlue, LinearGradientMode.Vertical);
            mGraphics.FillRectangle(linearGradientBrush, area1);
            mGraphics.DrawRectangle(pen1, area1);
        }
        private void PersonnelFrm_Paint(object sender, PaintEventArgs e)
        {
            Graphics mGraphics = e.Graphics;
            Pen pen1 = new Pen(Color.SteelBlue, 2);
            Rectangle area1 = new Rectangle(0, 0, this.Width - 1, this.Height - 1);
            LinearGradientBrush linearGradientBrush = new LinearGradientBrush(area1,
                SystemColors.Control, Color.WhiteSmoke, LinearGradientMode.Vertical);
            mGraphics.FillRectangle(linearGradientBrush, area1);
            mGraphics.DrawRectangle(pen1, area1);
        }
     
        private void groupBox1_Paint(object sender, PaintEventArgs e)
        {
            Graphics mGraphics = e.Graphics;
            Pen pen1 = new Pen(Color.AliceBlue, 0);
            Rectangle area1 = new Rectangle(0, 0, this.groupBox1.Width - 1, this.groupBox1.Height - 1);
            LinearGradientBrush linearGradientBrush = new LinearGradientBrush(area1,
                Color.AliceBlue, Color.AliceBlue, LinearGradientMode.Vertical);
            mGraphics.FillRectangle(linearGradientBrush, area1);
            mGraphics.DrawRectangle(pen1, area1);
        }
        public static string etat, numeroMatricule;
        private void PersonnelFrm_Load(object sender, EventArgs e)
        {
            try
            {
                if (etat == "2")
                {
                    var dtpersonnel = ConnectionClass.ListeDesPersonnelParNumeroMatricule(numeroMatricule);
                    var dtService = ConnectionClass.ListeDesServices(numeroMatricule);
                    var dtsalaire = ConnectionClass.ListeSalaire(dtpersonnel.Rows[0].ItemArray[1].ToString(),
                        dtpersonnel.Rows[0].ItemArray[2].ToString());
                    var frmPersonnel = new PersonnelFrm();
                    _ancienMatricule = numeroMatricule;
                    cmbDivision.DropDownStyle = ComboBoxStyle.DropDown;
                    cmbDiplome.DropDownStyle = ComboBoxStyle.DropDown;
                    cmbTypeContrat.DropDownStyle = ComboBoxStyle.DropDown;
                    txtNummatricule.Text = dtpersonnel.Rows[0].ItemArray[0].ToString();
                    txtNom.Text = dtpersonnel.Rows[0].ItemArray[1].ToString();
                    txtPrenom.Text = dtpersonnel.Rows[0].ItemArray[2].ToString();
                    var datenaissance = DateTime.Parse(dtpersonnel.Rows[0].ItemArray[3].ToString());
                    cmbJour.Text = datenaissance.Day.ToString();
                    txtNoCompte.Text =
                    cmbMois.Text = datenaissance.Month.ToString();
                    txtDateNaissance.Text = datenaissance.Year.ToString();
                    txtLieuNaissance.Text = dtpersonnel.Rows[0].ItemArray[4].ToString();
                    txtAdresse.Text = dtpersonnel.Rows[0].ItemArray[5].ToString();
                    txtTelephone1.Text = dtpersonnel.Rows[0].ItemArray[6].ToString();
                    txtTelephone2.Text = dtpersonnel.Rows[0].ItemArray[7].ToString();
                    txtEmail.Text = dtpersonnel.Rows[0].ItemArray[8].ToString();
                    cmbDivision.Text = dtpersonnel.Rows[0].ItemArray[11].ToString();
                    txtGrade.Text = dtService.Rows[0].ItemArray[4].ToString();
                    dtpDateservice.Value = DateTime.Parse(dtService.Rows[0].ItemArray[1].ToString());
                    txtPoste.Text = dtService.Rows[0].ItemArray[2].ToString();
                    txtAnciennete.Text = dtService.Rows[0].ItemArray[6].ToString();
                    txtCategorie.Text = dtService.Rows[0].ItemArray[5].ToString();
                    txtCNPS.Text = dtService.Rows[0].ItemArray[7].ToString();
                    cmbDiplome.DropDownStyle = ComboBoxStyle.DropDown;
                    cmbTypeContrat.DropDownStyle = ComboBoxStyle.DropDown;
                    cmbDiplome.Text = dtService.Rows[0].ItemArray[8].ToString();
                    cmbTypeContrat.Text = dtService.Rows[0].ItemArray[9].ToString();
                    txtCodeBank.Text = dtpersonnel.Rows[0].ItemArray[23].ToString();
                    txtClef.Text = dtpersonnel.Rows[0].ItemArray[25].ToString();
                    txtCodeGuichet.Text = dtpersonnel.Rows[0].ItemArray[24].ToString();
                    DateTime dateRetraite, dateFinContrat;
                    if (DateTime.TryParse(dtService.Rows[0].ItemArray[12].ToString(), out dateRetraite))
                    {
                    }
                    else
                    {
                        dateRetraite = DateTime.Now;
                    }
                    if (DateTime.TryParse(dtService.Rows[0].ItemArray[13].ToString(), out dateFinContrat))
                    {
                    }
                    else
                    {
                        dateFinContrat = DateTime.Now;
                    }

                    if (dtService.Rows[0].ItemArray[10].ToString() == "1")
                    {
                        chkRetraite.Checked = true;
                    }
                    else
                    {
                        chkNonRetraite.Checked = true;
                    }

                    if (dtService.Rows[0].ItemArray[11].ToString() == "1")
                    {
                        chkFinContrat.Checked = true;
                    }
                    else
                    {
                        chkEnCours.Checked = true;
                    }

                    dtpDateRetraite.Value = dateRetraite;
                    dtpFinContrat.Value = dateFinContrat;

                    if (dtsalaire.Rows.Count > 0)
                    {
                        txtSalaire.Text = dtsalaire.Rows[0].ItemArray[0].ToString();
                        txtIndice.Text = dtsalaire.Rows[0].ItemArray[2].ToString();
                    }
                    txtNoCompte.Text = dtpersonnel.Rows[0].ItemArray[18].ToString();
                    var image = dtpersonnel.Rows[0].ItemArray[10].ToString();
                    if (System.IO.File.Exists(image))
                    {
                        pictureBox1.Image = Image.FromFile(image);
                    }
                    else
                    {
                        pictureBox1.Image = null;
                    }
                    if (dtpersonnel.Rows[0].ItemArray[9].ToString().ToUpper() == "F")
                    {
                        chkFemelle.Checked = true;
                    }
                    else if (dtpersonnel.Rows[0].ItemArray[9].ToString().ToUpper() == "M")
                    {
                        chkMale.Checked = true;
                    }

                    btnAjouter.Enabled = true;
                }
                else
                {
                    chkEnCours.Checked = true;
                    chkNonRetraite.Checked = true;
                }
                btnFermer.Location = new Point(Width - 45, btnFermer.Location.Y);
                //liste des departements
                var dtDepartement = ConnectionClass.ListeDepartement();
                foreach (DataRow dtRow in dtDepartement.Rows)
                {
                    cmbDivision.Items.Add(dtRow.ItemArray[1].ToString());
                }
                var typeContrat = new string[]
            {
                "CDD",
                "CDI",
                "Decisionaire",
                "Decreté",
                "Detaché",
                "Journalier",
                 "Prestataire",
                "Stage"
            };
                var listeDiplome = new string[]
            {
                "CEP/T",
                "BEPC/T",
                "Baccalaureat",
                "Diplôme BTS",
                "Licence",
                "Maitrise",
                "DEA",
                "Doctorat",
                "Autres"
            };

                foreach (var contrat in typeContrat)
                {
                    cmbTypeContrat.Items.Add(contrat);
                }
                foreach (string diplome in listeDiplome)
                {
                    cmbDiplome.Items.Add(diplome);
                }
                //chkEnCours.Checked = true;
                //chkNonRetraite.Checked = true;
            }
            catch { }
        }

        private void cmbDepartement_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtAnciennete.Focus();
        }

        private void txtDateNaissance_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (txtDateNaissance.Text.Length > 4)
                {
                    txtDateNaissance.Text = txtDateNaissance.Text.Substring(0, 4);
                }
                var annee = Int32.Parse(txtDateNaissance.Text);
                var anneeActuel = DateTime.Now.Year;
                var moisActuel = DateTime.Now.Month;
                var mois = Int32.Parse(cmbMois.Text );
                if (moisActuel >= mois)
                {
                    age = anneeActuel - annee;
                    txtAge.Text = age.ToString();
                }
                else
                {
                    age = anneeActuel - annee - 1;
                    txtAge.Text = age.ToString();
                }
            }
            catch
            {
            }
        }

        private void cmbDepartement_Click(object sender, EventArgs e)
        {
            //liste des departements
            cmbDivision.Items.Clear();
            DataTable dtDepartement = ConnectionClass.ListeDepartement();
            foreach (DataRow dtRow in dtDepartement.Rows)
            {
                cmbDivision.Items.Add(dtRow.ItemArray[1].ToString());
            }
        }

        private void cmbTypeContrat_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtCNPS.Focus();
        }

        private void chkNonRetraite_CheckedChanged(object sender, EventArgs e)
        {
            if (chkNonRetraite.Checked)
            {
                chkRetraite.Checked = false;
            }
        }

        private void chkRetraite_CheckedChanged(object sender, EventArgs e)
        {
            if (chkRetraite.Checked)
            {
                chkNonRetraite.Checked = false;
            }
        }

        private void chkEnCours_CheckedChanged(object sender, EventArgs e)
        {
            if(chkEnCours.Checked)
            chkFinContrat.Checked = false;
        }

        private void chkFinContrat_CheckedChanged(object sender, EventArgs e)
        {
            if (chkFinContrat.Checked == true)
            {
                chkEnCours.Checked = false;
            }
        }
        
        private void PersonnelFrm_FormClosed(object sender, FormClosedEventArgs e)
        {
            etat = "1";
        }

    }
}
