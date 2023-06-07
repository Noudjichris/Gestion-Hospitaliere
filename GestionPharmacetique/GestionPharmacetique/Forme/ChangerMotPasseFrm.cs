using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using GestionPharmacetique;
using GestionPharmacetique.AppCode;

namespace SystemeGestionAgenceVoyage.GUI
{
    public partial class ChangerMotPasseFrm : Form
    {
        public ChangerMotPasseFrm()
        {
            InitializeComponent();
        }

        private void ChangerMotPasseFrm_Paint(object sender, PaintEventArgs e)
        {
            Graphics mGraphics = e.Graphics;
            Pen pen1 = new Pen(System.Drawing.Color.Violet, 5);
            Rectangle area1 = new Rectangle(0, 0, this.Width - 1, this.Height - 1);
            LinearGradientBrush LGB = new LinearGradientBrush(area1, System.Drawing.SystemColors.Control, System.Drawing.SystemColors.ScrollBar, LinearGradientMode.ForwardDiagonal);
            mGraphics.FillRectangle(LGB, area1);
            mGraphics.DrawRectangle(pen1, area1);
        }

        private void button5_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

    
        //cahnger mot de passe
        private void button1_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtMotPasse.Text) && txtMotPasse.Text.Length>=7)
            {
                if (string.Equals(txtMotPasse.Text, textBox1.Text))
                {
                    ConnectionClass.ModifierMotDePasse(label1.Text,
                        txtAncienMotPasse.Text .GetHashCode().ToString(), 
                        txtMotPasse.Text .GetHashCode().ToString());
                   
                    InsererDansLog(label1.Text);
                    this.Dispose();
                }
                else
                {
                    MonMessageBox.ShowBox("Les mots de passe ne sont pas conformes","Conformite mot de passe","erreur.png");
                }
            }
            else
            {                
                MonMessageBox.ShowBox("Le nouveau mot de passe ne doit etre vide et doit comporter au moins 7 caractere.", "Erreur saisie","erreur.png");
            }
        }

        private void InsererDansLog(string utilisateur)
        {
            try
            {
                string paths = Application.StartupPath.Substring(0, Application.StartupPath.Length - 10);
                string filePath = paths + @"\log\log_account.txt";
                if (System.IO.File.Exists(filePath))
                {
                    System.IO.StreamWriter sw = new System.IO.StreamWriter(filePath, true);
                    sw.WriteLine("***************** Date et Heure de connection " + DateTime.Now.ToString() + "  **********************");
                    sw.WriteLine(" {0} a change son mot de passe", utilisateur);
                    sw.WriteLine();
                    sw.Close();

                }
            }
            catch { }
        }

        private void ChangerMotPasseFrm_Load(object sender, EventArgs e)
        {

        }

        private void groupBox1_Paint(object sender, PaintEventArgs e)
        {
            Graphics mGraphics = e.Graphics;
            Pen pen1 = new Pen(Color.White, 0);
            Rectangle area1 = new Rectangle(0, 0, this.Width - 1, this.Height - 1);
            LinearGradientBrush LGB = new LinearGradientBrush(area1, System.Drawing.SystemColors.Control, 
                System.Drawing.SystemColors.ScrollBar, LinearGradientMode.ForwardDiagonal);
            mGraphics.FillRectangle(LGB, area1);
            mGraphics.DrawRectangle(pen1, area1);
        }
        string _photo;
        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            try
            {
                var open = new OpenFileDialog();
                //open.InitialDirectory = Application.StartupPath.Substring(0, Application.StartupPath.Length - 10);
                open.Filter = "Image Files (*.jpg)|*.jpg|all files(*.*)|*.*";
                open.FilterIndex = 1;
                if (open.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    if (open.CheckFileExists)
                    {
                        _photo = System.IO.Path.GetFileName(open.FileName);
                        var imagePath = "C:\\Image Pharmacie\\Photo Employe\\";

                        System.IO.Directory.CreateDirectory(imagePath);
                        if (System.IO.Directory.Exists(imagePath))
                        {
                            var image = imagePath + _photo;
                            if (!System.IO.File.Exists(image))
                            {
                                System.IO.File.Copy(open.FileName, image);
                                pictureBox2.Image = Image.FromFile(open.FileName);
                                if (!string.IsNullOrEmpty(_photo))
                                {
                                    ConnectionClass.ModifierEmployee(Form1.numEmploye, _photo);
                                }
                                MonMessageBox.ShowBox("Photo transferée avec succés ", "Information Photo", "affirmation.png");
                            }
                            else
                            {
                                MonMessageBox.ShowBox(
                                    "Une image existe deja sous ce nom. Rénommer le fichier et réessayez.",
                                    "Erreur insertion", "erreur.png");
                            }
                        }

                    }
                }

            }

            catch (Exception ex) { MonMessageBox.ShowBox("Transfert de l'image", ex); }
        }
    }
}
