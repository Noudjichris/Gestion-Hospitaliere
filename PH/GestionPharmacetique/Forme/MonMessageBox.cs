using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing.Drawing2D;
using System.Drawing;
using System.IO;
using System.Text;
using System.Windows.Forms;

namespace GestionPharmacetique
{
    public partial class MonMessageBox : Form
    {
        public MonMessageBox()
        {
            InitializeComponent();
        }

        static string boutonID;
        static MonMessageBox newMsgBox;
        int disposeFormTimer;
        


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
                    imagePath = global::GestionPharmacetique.Properties.Resources.erreur;
                    newMsgBox.button2.Visible = false;
                }
                else if (image == "affirmation.png")
                {
                    imagePath = global::GestionPharmacetique.Properties.Resources.affirmation;
                    newMsgBox.button2.Visible = false;
                }
                else if (image == "confirmation.png")
                {
                    imagePath = global::GestionPharmacetique.Properties.Resources.confirmation;
                }

                newMsgBox.pictureBox1.Image = imagePath;
                newMsgBox.ShowDialog();
            }
            catch (Exception ex)
            {
                MonMessageBox.ShowBox("", ex);
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
                string filePath1 = @"\\SVR\log\log_erreur.txt";
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

                if (File.Exists(filePath1))
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
                    imagePath = global::GestionPharmacetique.Properties.Resources.erreur;
                    newMsgBox.button2.Visible = false;
                }
                else if (image == "affirmation.png")
                {
                    imagePath = global::GestionPharmacetique.Properties.Resources.affirmation;
                    newMsgBox.button2.Visible = false;
                }
                else if (image == "confirmation.png")
                {
                    imagePath = global::GestionPharmacetique.Properties.Resources.confirmation;
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
            try
            {
                newMsgBox = new MonMessageBox();

                // inserer les erreurs dans un fichier text

                string filePath = Application.StartupPath.Substring(0, Application.StartupPath.Length - 10) + "\\log\\log_erreur.txt";
                string filePath1 = @"\\SVR\log\log_erreur.txt";
                if (File.Exists(filePath))
                {
                    StreamWriter sw = new StreamWriter(filePath, true);
                    sw.WriteLine("*****************Erreur apparue le " + DateTime.Now.ToString() + "**********************");
                    sw.WriteLine(txtTitle);
                    sw.WriteLine(exception.GetType().Name);
                    sw.WriteLine(exception.Message);
                    sw.WriteLine(exception.ToString());
                    sw.WriteLine();
                    sw.Close();

                }
                if (File.Exists(filePath1))
                {
                    StreamWriter sw = new StreamWriter(filePath, true);
                    sw.WriteLine("*****************Erreur apparue le " + DateTime.Now.ToString() + "**********************");
                    sw.WriteLine(txtTitle);
                    sw.WriteLine(exception.GetType().Name);
                    sw.WriteLine(exception.Message);
                    sw.WriteLine(exception.ToString());
                    sw.WriteLine();
                    sw.Close();

                }
            }
            catch { }
            return boutonID;
        }


        private void button1_Click_1(object sender, EventArgs e)
        {
            try
            {
                boutonID = "1";
                newMsgBox.Dispose();
                newMsgBox.Close();
            }
            catch (Exception ex){MonMessageBox.ShowBox("",ex);}
        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            try
            {
                boutonID = "2";
                //newMsgBox.Dispose();
                newMsgBox.Close();
            }
            catch { }
        }



        private void MonMessageBox_Paint(object sender, PaintEventArgs e)
        {
            Graphics mGraphics = e.Graphics;
            Pen pen1 = new Pen(Color.LightSkyBlue, 5);
            Rectangle area1 = new Rectangle(0, 0, this.Width - 1, this.Height - 1);
            LinearGradientBrush linearGradientBrush = new LinearGradientBrush(area1, SystemColors.Control, SystemColors.ControlLightLight, LinearGradientMode.BackwardDiagonal);
            mGraphics.FillRectangle(linearGradientBrush, area1);
            mGraphics.DrawRectangle(pen1, area1);
        }

        private void groupBox1_Paint(object sender, PaintEventArgs e)
        { Graphics mGraphics = e.Graphics;
        Pen pen1 = new Pen(Color.CornflowerBlue, 0);
            Rectangle area1 = new Rectangle(0, 0, this.groupBox1.Width - 1, this.groupBox1.Height - 1);
            LinearGradientBrush linearGradientBrush = new LinearGradientBrush(area1, Color.CornflowerBlue, Color.CadetBlue, LinearGradientMode.BackwardDiagonal);
            mGraphics.FillRectangle(linearGradientBrush, area1);
            mGraphics.DrawRectangle(pen1, area1);
            
        }

     

    }
}
