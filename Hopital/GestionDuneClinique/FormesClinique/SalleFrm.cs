using System;
using System.Text;
using System.Windows.Forms;
using System.Linq;
namespace GestionDesPelerinsTchad.Formes
{
    public partial class SalleFrm : Form
    {
        public SalleFrm()
        {
            InitializeComponent();
        }

        private void dataGridView1_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                int numero; double prix;
                var rowIndex = dataGridView1.CurrentRow.Index;
                if (Int32.TryParse(dataGridView1.Rows[rowIndex].Cells[0].Value.ToString(), out numero))
                {
                    if (double.TryParse(dataGridView1.Rows[rowIndex].Cells[2].Value.ToString(), out prix ))
                    {
                        var salle = new GestionDuneClinique.AppCode.Occupation();
                        salle.NoSalle = numero;
                        salle.SalleLit = dataGridView1.Rows[rowIndex].Cells[1].Value.ToString();
                        salle.Prix = prix;
                        if (GestionDuneClinique.AppCode.ConnectionClassClinique.EnregistrerUneSalle(salle, "2"))
                        {
                            ListeSalle();
                        }
                    }
                }
                //else
                //{
                //    etat="1";
                //    typeDocument.NumeroType = 0;
                //}
                //if (AppCode.ConnectionClass.EnregistrerUnTypeDocument(typeDocument, etat))
                //{
                //    dataGridView1.Rows.Clear();
                //    var liste = AppCode.ConnectionClass.ListeDesTypesDocuments();
                //    foreach (var l in liste)
                //    {
                //        dataGridView1.Rows.Add(l.NumeroType, l.TypeDocument);
                //        Column6.Image = global::GestionDesPelerinsTchad.Properties.Resources.DeleteRed1;
                //    }

                //}
            }
            catch (Exception ex)
            {
                //MonMessageBox.ShowBox("Edit type de document", ex);
            }
        }

        private void TypeDocumentFrm_Load(object sender, EventArgs e)
        {
            try
            {
                ListeSalle();
              
            }
            catch { }
        }

        public static SalleFrm frm;
        static  bool state;

        public static bool ShowBox()
        {
            frm = new SalleFrm();
            frm.ShowDialog();
            return state;
        }

        private void TypeDocumentFrm_FormClosed(object sender, FormClosedEventArgs e)
        {
            state = true;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var salle = new GestionDuneClinique.AppCode.Occupation();
            salle.SalleLit = "";
            salle.Prix = 0;
            if (GestionDuneClinique.AppCode.ConnectionClassClinique.EnregistrerUneSalle(salle, "1"))
            {
                ListeSalle();
            }
        }

        void ListeSalle()
        {
            try
            {
                dataGridView1.Rows.Clear();
                var liste = from l in GestionDuneClinique.AppCode.ConnectionClassClinique.ListeSalles()
                                where l.NoSalle >0
                                select l;
                foreach (var s in liste)
                {
                    dataGridView1.Rows.Add
                        (
                        s.NoSalle,s.SalleLit,s.Prix
                        );
                    Column6.Image = global::GestionDuneClinique.Properties.Resources.deleteButton;
                }
            }
            catch { }
        }
        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.ColumnIndex == 3)
                {
                    int numero;
                    var rowIndex = dataGridView1.CurrentRow.Index;
                   
                    if (Int32.TryParse(dataGridView1.Rows[rowIndex].Cells[0].Value.ToString(), out numero))
                    {
                        if (GestionDuneClinique. MonMessageBox.ShowBox("Voulez vous vous supprimer ce type de document?", "Confirmation","confirmation.png") == "1")
                        {
                          GestionDuneClinique.  AppCode.ConnectionClassClinique.SupprimerUneSalle(numero);
                            dataGridView1.Rows.Remove(dataGridView1.Rows[rowIndex]);
                        }
                    }

                }
            }
            catch { }
        }

    }
}
