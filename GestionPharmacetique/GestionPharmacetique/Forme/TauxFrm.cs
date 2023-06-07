using System;
using System.Drawing.Drawing2D;
using System.Text;
using System.Windows.Forms;
using System.Drawing;

namespace SGDP.Formes
{
    public partial class TauxFrm : Form
    {
        public TauxFrm()
        {
            InitializeComponent();
        }

        private void TauxFrm_Load(object sender, EventArgs e)
        {

        }

        public static double taux;
        static bool flag; static TauxFrm tauxFrm;

        private void txtProduit_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (double.TryParse(txtTaux.Text, out taux))
                {
                    flag = true ;
                    Dispose();
                }
                else
                {
                    txtTaux.Focus();
                    txtTaux.Text = "";
                    txtTaux.BackColor = Color.Red;
                }
            }
        }

        private void txtTaux_TextChanged(object sender, EventArgs e)
        {
            txtTaux.BackColor = Color.White;
        }

        public static bool ShowBox()
        {
            tauxFrm = new TauxFrm();
            tauxFrm.ShowDialog();
            return flag;
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            flag = false;
            Dispose();
        }

        private void TauxFrm_Paint(object sender, PaintEventArgs e)
        {

            Graphics mGraphics = e.Graphics;
            Pen pen1 = new Pen(SystemColors.ControlDarkDark, 3);
            Rectangle area1 = new Rectangle(0, 0, this.Width - 1, this.Height - 1);
            LinearGradientBrush linearGradientBrush = new LinearGradientBrush(area1,Color.White
                , Color.White, LinearGradientMode.BackwardDiagonal);
            mGraphics.FillRectangle(linearGradientBrush, area1);
            mGraphics.DrawRectangle(pen1, area1);
        }
    }
}
