using System;
using System.Drawing.Drawing2D;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace GestionPharmacetique.Forme
{
    public partial class SplashScreen : Form
    {
        public SplashScreen()
        {
            InitializeComponent();
        }
        private void groupBox1_Paint(object sender, PaintEventArgs e)
        {
            Graphics mGraphics = e.Graphics;
            Pen pen1 = new Pen(Color.DarkSeaGreen, 0);
            Rectangle area1 = new Rectangle(0, 0, this.groupBox1.Width - 1, this.groupBox1.Height - 1);
            LinearGradientBrush linearGradientBrush = new LinearGradientBrush
                (area1, Color.DarkSeaGreen, Color.DarkSeaGreen, LinearGradientMode.BackwardDiagonal);
            mGraphics.FillRectangle(linearGradientBrush, area1);
            mGraphics.DrawRectangle(pen1, area1);
        }
        private void timer1_Tick(object sender, EventArgs e)
        {
            //after 3 sec stop the timer
            
                //display mainform
            var mf = new  Form1();
                mf.Show();
                //hide this form
                
                this.Hide();
                timer1.Stop();
                //timer1.Dispose();
        }

        private void SplashScreen_Shown(object sender, EventArgs e)
        {
            //starts the timer

           
        }

        private void SplashScreen_Load(object sender, EventArgs e)
        {
            timer1.Start();
            
        }
    }
}
