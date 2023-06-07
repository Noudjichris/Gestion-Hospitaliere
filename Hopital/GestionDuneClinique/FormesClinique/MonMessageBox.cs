using System;
using System.IO;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Text;
using System.Windows.Forms;

namespace GestionDuneClinique
{
    public partial class MonMessageBox : Form
    {
        public MonMessageBox()
        {
            InitializeComponent();
        }


        static string boutonID;
        static MonMessageBox newMsgBox;
        public static string ShowBox(string txtMessage, string txtTitle, string image)
        {

            try
            {
                newMsgBox = new MonMessageBox();
                newMsgBox.lblMessage.Text = txtMessage;
                newMsgBox.label2.Text = txtTitle;

                Image imagePath = null;
                if (image == "erreur.png")
                {
                    imagePath = global::GestionDuneClinique.Properties.Resources.erreur;
                    newMsgBox.button2.Visible = false;
                }
                else if (image == "affirmation.png")
                {
                    imagePath = global::GestionDuneClinique.Properties.Resources.affirmation;
                    newMsgBox.button2.Visible = false;
                }
                else if (image == "confirmation.png")
                {
                    imagePath = global::GestionDuneClinique.Properties.Resources.confirmation;
                }

                newMsgBox.pictureBox1.Image = imagePath;
                newMsgBox.ShowDialog();
            }
            catch (Exception)
            {
            }
            return boutonID;
        }

        public static string ShowBox(string txtMessage, string txtTitle, Exception exception, string image)
        {
            try
            {
                newMsgBox = new MonMessageBox();
                newMsgBox.lblMessage.Text = txtMessage;
                newMsgBox.label2.Text = txtTitle;


                string filePath = Application.StartupPath.Substring(0, Application.StartupPath.Length - 10) + "\\log\\log_erreur.txt";
                //global::GestionPharmacetique.Properties.Resources.log_erreur;
                if (File.Exists(filePath))
                {
                    StreamWriter sw = new StreamWriter(filePath, true);
                    sw.WriteLine("*****************Erreur apparue le " + DateTime.Now.ToString() + "**********************");
                    sw.WriteLine(txtTitle);
                    sw.WriteLine(exception.GetType().Name);
                    sw.WriteLine(exception.Message);
                    sw.WriteLine(exception.ToString());
                    sw.WriteLine("\n");
                    sw.Close();

                }
                Image imagePath = null;
                if (image == "erreur.png")
                {
                    imagePath = global::GestionDuneClinique.Properties.Resources.erreur;
                    newMsgBox.button2.Visible = false;
                }
                else if (image == "affirmation.png")
                {
                    imagePath = global::GestionDuneClinique.Properties.Resources.affirmation;
                    newMsgBox.button2.Visible = false;
                }
                else if (image == "confirmation.png")
                {
                    imagePath = global::GestionDuneClinique.Properties.Resources.confirmation;
                }

                newMsgBox.pictureBox1.Image = imagePath;
                newMsgBox.ShowDialog();

            }
            catch (Exception)
            {
            }
            return boutonID;

        }

        public static string ShowBox(string txtTitle, Exception exception)
        {
            newMsgBox = new MonMessageBox();
            try
            {

                // inserer les erreurs dans un fichier text

                string filePath = Application.StartupPath.Substring(0, Application.StartupPath.Length - 10) + "\\log\\log_erreur.txt";
                //string filePath1 =   "C:\\log\\log_erreur.txt";
                //System.IO.cre.cr(filePath1);
                if (File.Exists(filePath))
                {
                    StreamWriter sw = new StreamWriter(filePath, true);
                    //var sw1 = new StreamWriter(filePath1, true);
                    sw.WriteLine("*****************Erreur apparue le " + DateTime.Now.ToString() + "**********************");
                    sw.WriteLine(txtTitle);
                    sw.WriteLine(exception.GetType().Name);
                    sw.WriteLine(exception.Message);
                    sw.WriteLine(exception.ToString());
                    sw.WriteLine();
                    sw.Close();
                    //sw1.WriteLine("*****************Erreur apparue le " + DateTime.Now.ToString() + "**********************");
                    //sw1.WriteLine(txtTitle);
                    //sw1.WriteLine(exception.GetType().Name);
                    //sw1.WriteLine(exception.Message);
                    //sw1.WriteLine(exception.ToString());
                    //sw1.WriteLine();
                    //sw1.Close();
                }
            }
            catch (Exception)
            {
            }

            return boutonID;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            boutonID = "1";
            newMsgBox.Dispose();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            boutonID = "2";
            newMsgBox.Dispose();
        }

        private void MonMessageBox_Paint(object sender, PaintEventArgs e)
        {
            Graphics mGraphics = e.Graphics;
            Pen pen1 = new Pen(SystemColors.Control, 0);
            Rectangle area1 = new Rectangle(0, 0, this.Width - 1, this.Height - 1);
            LinearGradientBrush linearGradientBrush = new LinearGradientBrush(area1, Color.White, Color.White, LinearGradientMode.Vertical);
            mGraphics.FillRectangle(linearGradientBrush, area1);
            mGraphics.DrawRectangle(pen1, area1);
        }

        private void groupBox1_Paint(object sender, PaintEventArgs e)
        {
            Graphics mGraphics = e.Graphics;
            Pen pen1 = new Pen(SystemColors.Control, 0);
            Rectangle area1 = new Rectangle(0, 0, this.groupBox1.Width - 1, this.groupBox1.Height - 1);
            LinearGradientBrush linearGradientBrush = new LinearGradientBrush(area1, Color.SteelBlue, Color.DodgerBlue, LinearGradientMode.Vertical);
            mGraphics.FillRectangle(linearGradientBrush, area1);
            mGraphics.DrawRectangle(pen1, area1);
        }

        private void MonMessageBox_Load(object sender, EventArgs e)
        {

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }
    }
}
