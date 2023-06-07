using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Windows.Forms;
using GestionDuneClinique.AppCode;
using GestionDuneClinique;
using GestionDuneClinique.FormesClinique;
using GestionPharmacetique.AppCode;
namespace GestionPharmacetique.Forme
{
    public partial class EmployeFrm : Form
    {
        public EmployeFrm()
        {
            InitializeComponent();
        }

        private void EmployeFrm_Paint(object sender, PaintEventArgs e)
        {
            Graphics mGraphics = e.Graphics;
            Pen pen1 = new Pen(Color.SteelBlue, 0);
            Rectangle area1 = new Rectangle(0, 0, this.Width - 1, this.Height - 1);
            LinearGradientBrush linearGradientBrush = new LinearGradientBrush(area1, SystemColors.Control, 
                SystemColors.Control, LinearGradientMode.BackwardDiagonal);
            mGraphics.FillRectangle(linearGradientBrush, area1);
            mGraphics.DrawRectangle(pen1, area1);
        }

        private void groupBox1_Paint(object sender, PaintEventArgs e)
        {
            Graphics mGraphics = e.Graphics;
            Pen pen1 = new Pen(Color.SteelBlue, 0);
            Rectangle area1 = new Rectangle(0, 0, this.groupBox1.Width - 1, this.groupBox1.Height - 1);
            LinearGradientBrush linearGradientBrush = new LinearGradientBrush(area1, SystemColors.Control,
                SystemColors.Control, LinearGradientMode.BackwardDiagonal);
            mGraphics.FillRectangle(linearGradientBrush, area1);
            mGraphics.DrawRectangle(pen1, area1);
        }
        private void groupBox3_Paint(object sender, PaintEventArgs e)
        {
            Graphics mGraphics = e.Graphics;
            Pen pen1 = new Pen(Color.SteelBlue, 0);
            Rectangle area1 = new Rectangle(0, 0, this.groupBox3.Width - 1, this.groupBox3.Height - 1);
            LinearGradientBrush linearGradientBrush = new LinearGradientBrush(area1, Color.SteelBlue,
                Color.SteelBlue, LinearGradientMode.BackwardDiagonal);
            mGraphics.FillRectangle(linearGradientBrush, area1);
            mGraphics.DrawRectangle(pen1, area1);
        }
        private void groupBox2_Paint(object sender, PaintEventArgs e)
        {
            Graphics mGraphics = e.Graphics;
            Pen pen1 = new Pen(Color.SteelBlue, 0);
            Rectangle area1 = new Rectangle(0, 0, this.Width - 1, this.Height - 1);
            LinearGradientBrush linearGradientBrush = new LinearGradientBrush(area1, Color.SteelBlue,
               Color.SteelBlue, LinearGradientMode.BackwardDiagonal);
            mGraphics.FillRectangle(linearGradientBrush, area1);
            mGraphics.DrawRectangle(pen1, area1);
        }

        private void EmployeFrm_Load(object sender, EventArgs e)
        {
            button7.Location = new Point(Width - 35, 4);
            ListeDesEmployes(); comboBox1.Items.Clear();
            var dt = ConnectionClassClinique.ListeService();
            for (var i = 0; i < dt.Rows.Count; i++)
                comboBox1.Items.Add(dt.Rows[i].ItemArray[1].ToString());
        }
        //liste des employe
        private void ListeDesEmployes()
        {
            try
            {
               dgvPatient.Rows.Clear();
               List<Employe> listeEmployes = ConnectionClassClinique.ListeDesEmployees();
                var list = from l in listeEmployes
                           where !l.NomEmployee.Contains("EXTERNE")
                              where !l.NomEmployee.Contains("CONSULTANT")
                           select l;
                foreach (var employe in list)
                {
                    var service = "";
                    if (employe.NumeroService > 0)
                    {
                                service = ConnectionClassClinique.ListeService(employe.NumeroService).Rows[0].ItemArray[1].ToString();
                        }
                    dgvPatient.Rows.Add(
                        employe.NumMatricule,
                        employe.NomEmployee.ToUpper(),
                        employe.Addresse.ToUpper(),
                        employe.Telephone1,
                        employe.Telephone2,
                        employe.Email,
                        employe.Titre.ToUpper(),service
                    );
                    editColumn.Image = global::GestionDuneClinique.Properties.Resources.edit;
                    deleteColumn.Image = global::GestionDuneClinique.Properties.Resources.deleteButton;
                }
            }
            catch (Exception exce )
            {
                MonMessageBox.ShowBox( "Liste employe", exce )
                ;
            }
        }

        //ajouter un employe
        private void btnAjouter_Click(object sender, EventArgs e)
        {
            try
            {
                if (!string.IsNullOrEmpty(txtNumeroEmploe.Text) && !string.IsNullOrEmpty(txtNomEmploye.Text) &&
                    !string.IsNullOrEmpty(txtAdresse.Text) && !string.IsNullOrEmpty(txtTele1.Text) && !string.IsNullOrEmpty(comboBox1.Text))
                {

                    if (txtNomEmploye.Text.Contains(" "))
                    {
                        if (string.IsNullOrEmpty(txtEmail.Text) || (!string.IsNullOrEmpty(txtEmail.Text) &&
                                                                    txtEmail.Text.Contains("@") &&
                                                                    txtEmail.Text.Contains(".")))
                        {
                            var employe = new Employe();
                            employe.NumMatricule = txtNumeroEmploe.Text;
                            employe.NomEmployee = txtNomEmploye.Text;
                            employe.Addresse = txtAdresse.Text;
                            employe.Telephone1 = txtTele1.Text;
                            employe.Telephone2= txtTele2.Text;
                            employe.Email = txtEmail.Text;
                            employe.Titre = txtTitre.Text;
                           employe.Photo = "";
                            var dt = ConnectionClassClinique.ListeService(comboBox1.Text);
                            if(dt.Rows.Count>0)
                            employe.NumeroService =Convert.ToInt32(dt.Rows[0].ItemArray[0].ToString());

                            if (ConnectionClassClinique.EnregistrerEmployee(employe))
                            {
                                //ConnectionClassPharmacie.AjouterEmployee(employe);
                                ViderLesChamps();
                                comboBox1.Text = "";
                                ListeDesEmployes();
                            }
                        }
                        else
                        {
                            MonMessageBox.ShowBox(
                                "Si l'email de l'employe existe, veuillez pourvoir une adresse email valide", "erreur",
                                "erreur.png");
                        }
                    }
                    else
                    {
                        MonMessageBox.ShowBox("Veuillez entrer le nom et prenom de l'employe", "erreur", "erreur.png");
                    }
                }
                else
                {
                    MonMessageBox.ShowBox("Les champs numero employe, nom, adresse, telephone1 sont à remplir","erreur","erreur.png");
                }
            }
            catch (Exception exception)
            {
                MonMessageBox.ShowBox("Ajouter employe", exception);
            }

        }
        private void ViderLesChamps()
        {
            txtAdresse.Text = "";
            txtEmail.Text = "";
            txtNomEmploye.Text = "";
            txtNumeroEmploe.Text = "";
            txtTele1.Text = "";
            txtTele2.Text = "";
            txtTitre.Text = "";
            
        }

        private void button7_Click(object sender, EventArgs e)
        {
            Dispose();
        }

        private void dgvPatient_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 8)
            {
                txtTele1.Text=dgvPatient.SelectedRows[0].Cells[3].Value.ToString();
                txtAdresse.Text = dgvPatient.SelectedRows[0].Cells[2].Value.ToString();
                txtEmail.Text = dgvPatient.SelectedRows[0].Cells[5].Value.ToString();
                txtNomEmploye.Text = dgvPatient.SelectedRows[0].Cells[1].Value.ToString();
                txtNumeroEmploe.Text = dgvPatient.SelectedRows[0].Cells[0].Value.ToString();
                txtTitre.Text = dgvPatient.SelectedRows[0].Cells[6].Value.ToString();
                txtTele2.Text = dgvPatient.SelectedRows[0].Cells[3].Value.ToString();
                comboBox1.Text = dgvPatient.SelectedRows[0].Cells[7].Value.ToString();
                //var dt = ConnectionClassClinique.ListeService();
                //for (var i = 0; i < dt.Rows.Count; i++)
                //{
                //    if (dt.Rows[i].ItemArray[0].ToString() == dgvPatient.SelectedRows[0].Cells[7].Value.ToString())
                //        comboBox1.Text = dt.Rows[i].ItemArray[1].ToString();
                //}
            }
            else if(e.ColumnIndex==9)
            {
                if (dgvPatient.SelectedRows.Count > 0)
                {
                    if (MonMessageBox.ShowBox("Voulez vous supprimer les données de l'employé " + dgvPatient.SelectedRows[0].Cells[0].Value.ToString() + "?", "Confirmation", "confirmation.png")
                                == "1")
                    ConnectionClassClinique.SupprimerEmployee(dgvPatient.SelectedRows[0].Cells[0].Value.ToString() );
                    ViderLesChamps();
                     ListeDesEmployes();
                }

            }
        }

        private int idService;
        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                if (StringGetterFrm.ShowBox())
                {
                    if (!string.IsNullOrEmpty(StringGetterFrm.libelle))
                    {
                        idService = 0;
                       if( ConnectionClassClinique.EnregistrerService(idService, StringGetterFrm.libelle))
                       {
                           idService = 0;
                           comboBox1.Items.Clear();
                            var dt = ConnectionClassClinique.ListeService();
                            for (var i = 0; i < dt.Rows.Count; i++)
                                comboBox1.Items.Add(dt.Rows[i].ItemArray[1].ToString());
                         
                        }
                    }
                }
            }
            catch (Exception)
            {
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                StringGetterFrm.libelle = comboBox1.Text;
                if (StringGetterFrm.ShowBox())
                {
                    if (!string.IsNullOrEmpty(StringGetterFrm.libelle))
                    {
                        if (ConnectionClassClinique.EnregistrerService(idService, StringGetterFrm.libelle))
                        {
                            idService = 0;
                            comboBox1.Items.Clear();
                            var dt = ConnectionClassClinique.ListeService();
                            for (var i = 0; i < dt.Rows.Count; i++)
                                comboBox1.Items.Add(dt.Rows[i].ItemArray[1].ToString());
                        }
                    }
                }
            }
            catch (Exception)
            {
            }
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                idService = Convert.ToInt32(ConnectionClassClinique.ListeService(comboBox1.Text).Rows[0].ItemArray[0].ToString());
            }
            catch (Exception)
            {
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            try
            {
                if (!string.IsNullOrEmpty(StringGetterFrm.libelle))
                {
                    ConnectionClassClinique.SupprimerService(idService);
                    {
                        idService = 0; comboBox1.Items.Clear();
                        var dt = ConnectionClassClinique.ListeService();
                        for (var i = 0; i < dt.Rows.Count; i++)
                            comboBox1.Items.Add(dt.Rows[i].ItemArray[1].ToString());
                    }
                }

            }
            catch (Exception)
            {
            }
        }
        
        
    }
}
