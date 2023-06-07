using System;
using System.Collections.Generic;
using System.Drawing.Drawing2D;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace SGSP.Formes
{
    public partial class PaiementForme : Form
    {
        public PaiementForme()
        {
            InitializeComponent();
        }

        private void groupBox1_Paint(object sender, PaintEventArgs e)
        {
            Graphics mGraphics = e.Graphics;
            Pen pen1 = new Pen(Color.Silver, 0);
            Rectangle area1 = new Rectangle(0, 0, this.groupBox1.Width - 1, this.groupBox1.Height - 1);
            LinearGradientBrush linearGradientBrush = new LinearGradientBrush(area1,
                Color.SteelBlue, Color.DarkSlateBlue, LinearGradientMode.Vertical);
            mGraphics.FillRectangle(linearGradientBrush, area1);
            mGraphics.DrawRectangle(pen1, area1);
        }
        private void groupBox6_Paint(object sender, PaintEventArgs e)
        {
            Graphics mGraphics = e.Graphics;
            Pen pen1 = new Pen(Color.WhiteSmoke, 0);
            Rectangle area1 = new Rectangle(0, 0, this.groupBox6.Width - 1, this.groupBox6.Height - 1);
            LinearGradientBrush linearGradientBrush = new LinearGradientBrush(area1,
                Color.AliceBlue, Color.AliceBlue, LinearGradientMode.Vertical);
            mGraphics.FillRectangle(linearGradientBrush, area1);
            mGraphics.DrawRectangle(pen1, area1);
        }
        System.Globalization.CultureInfo elGR = System.Globalization.CultureInfo.CreateSpecificCulture("fr-FR");
        private void RemplissageDesChamps()
        {
            try
            {
                var paiement = AppCode.ConnectionClass.PaiementParMatricule(numeroPaiement, numMatricule);

                if (paiement != null)
                {
                    var dt = AppCode.ConnectionClass.ListeDesPersonnelParNumeroMatricule(paiement.NumeroMatricule);
                    var nomEmploye = dt.Rows[0].ItemArray[1].ToString() + " " + dt.Rows[0].ItemArray[2].ToString();
                    var indiceAnciennete = dt.Rows[0].ItemArray[19].ToString();

                    txtSalaireBase.Text = String.Format(elGR, "{0:0,0}", paiement.SalaireBase);
                    lblSalaireBrut.Text = String.Format(elGR, "{0:0,0}", paiement.SalaireBrut);
                    txtNomPersonnel.Text = nomEmploye;
                    if (indiceAnciennete.Contains("%"))
                    {
                        indiceAnciennete.Remove(indiceAnciennete.IndexOf("%"));
                    }
                    double anciennete;
                    if (double.TryParse(indiceAnciennete, out anciennete))
                    {
                    }
                    else
                    {
                        anciennete = 0;
                    }
                    if (paiement.Service != "CDI" || paiement.Service != "CDD")
                    {
                        anciennete = 0;
                    }
                    var salaireGain = System.Math.Round(paiement.SalaireBase * (1 + anciennete / 100));
                    tauxConge = salaireGain / 24;
                    tauxAbscense = salaireGain / 26;
                    lblTauxAbscense.Text = Math.Round(tauxAbscense).ToString();
                    lblTauxConges.Text = Math.Round(tauxConge).ToString();
                    if(paiement.CongeAnnuel>0)
                    txtNbreJourConge.Text = "24";
                    txtCategorie.Text = anciennete.ToString() + "%";
                    txtGainAnciennete.Text = String.Format(elGR, "{0:0,0}",salaireGain);
                    lblMontantConge.Text = String.Format(elGR, "{0:0,0}", paiement.CongeAnnuel);
                    lblMontantAbscenses.Text = String.Format(elGR, "{0:0,0}", paiement.CoutAbsence);
                    lblTotalCnps.Text = String.Format(elGR, "{0:0,0}", paiement.CNPS);
                     var cnps = .0;
                     var cnpsCsdn = .0;
                    if (paiement.CNPS > 0)
                    {
                        lblBaseCNPS .Text = String.Format(elGR, "{0:0,0}", paiement.SalaireBrut);
                        label51.Text = String.Format(elGR, "{0:0,0}", paiement.SalaireBrut);
                        cnps = 3.5;
                        cnpsCsdn = 16.5;
                    }
                    txtCNPSCSND.Text =  cnpsCsdn.ToString();
                    txttauxCNPS.Text = cnps.ToString() ;
                    lblTotalIrpp.Text = String.Format(elGR, "{0:0,0}", paiement.IRPP);
                  var   irpp = .0;
                    if (paiement.IRPP > 0)
                    {
                        irpp = 10.5;
                        lblBaseIRPP.Text = String.Format(elGR, "{0:0,0}", paiement.SalaireBrut - paiement.CNPS);
                    }
                    txttauxIRPP.Text = irpp.ToString();
                    txtOnasa.Text = String.Format(elGR, "{0:0,0}", paiement.ONASA);
                    lblTotalIrpp.Text = String.Format(elGR, "{0:0,0}", paiement.IRPP);
                    lblChargePat.Text = String.Format(elGR, "{0:0,0}", paiement.ChargePatronale);
                    var totalCharge = paiement.ChargePatronale + paiement.ONASA + paiement.IRPP + paiement.CNPS;
                    lblTotalCharges.Text = String.Format(elGR, "{0:0,0}", totalCharge);
                    txtAvanceSurSalaaire.Text = String.Format(elGR, "{0:0,0}", paiement.AvanceSurSalaire);
                //txtSoinFamille.Text = String.Format(elGR, "{0:0,0}", paiement.ChargeSoinFamille);
                    txtAcomptePayer.Text = String.Format(elGR, "{0:0,0}", paiement.AcomptePaye);
          cmbTypeContrat.Text = paiement.Service;
                    var listeAcompte = AppCode.ConnectionClass.ListeDesAccompte(paiement.IDAcompte,numMatricule, paiement.AcomptePaye);
                    if(listeAcompte.Count()>0)
                    {
                        txtAccounptPaye.Text = String.Format(elGR, "{0:0,0}", listeAcompte[0].Rembourser);
                        txtAcompte.Text = String.Format(elGR, "{0:0,0}", listeAcompte[0].MontantAcompte);
                        
                    }
                    else
                    {
                        txtAccounptPaye.Text = "0";
                        txtAcompte.Text = "0";
                    }
                    //String.Format(elGR, "{0:0,0}", paiement.ChargeSoinFamille);
                    txtPrimeLogement.Text = String.Format(elGR, "{0:0,0}", paiement.PrimeLogement);
                    txtPrimeGarde.Text = String.Format(elGR, "{0:0,0}", paiement.PrimeGarde);
                    txtPrimeResponsabilite.Text = String.Format(elGR, "{0:0,0}", paiement.PrimeResponsabilite);
                    txtHeurSupp.Text = String.Format(elGR, "{0:0,0}", paiement.HeureSupplementaire);
                    txtTransport.Text = String.Format(elGR, "{0:0,0}", paiement.Transport);
                    lblTotalPrimes.Text = String.Format(elGR, "{0:0,0}", paiement.PrimeGarde+paiement.PrimeLogement+paiement.PrimeResponsabilite+paiement.Transport+paiement.HeureSupplementaire);
                    txtNetPayer.Text = String.Format(elGR, "{0:0,0}", paiement.SalaireNet);
                    lblCoutSalarial.Text = String.Format(elGR, "{0:0,0}", paiement.CoutDuSalarie);
                    if (paiement.ModePaiement == "Paiement en espèces")
                        rdbEspeces.Checked = true;
                    else if (paiement.ModePaiement == "Paiement par chèques")
                        rdbCheques.Checked = true;
                    else if (paiement.ModePaiement == "Virement bancaire")
                        rdbBancaire.Checked = true;
                    ancienCoutSalarial = paiement.CoutDuSalarie;
                    ancienAcomptePaye = paiement.AcomptePaye;
                    idAccompe = paiement.IDAcompte;
                    comboBox1.Text = paiement.Banque;
                }
            }
            catch (Exception ex)
            {
                GestionPharmacetique.MonMessageBox.ShowBox("Remplissage des donnees", ex);
            }
        }

        private void PaiementForme_Load(object sender, EventArgs e)
        {
            try
            {
                Location = new Point(0, 0);
                var listetypeContrat = new string[]
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
                foreach (var contrat in listetypeContrat)
                {
                    cmbTypeContrat.Items.Add(contrat);
                }
                comboBox1.Items.Add("");
                foreach (DataRow dtR in AppCode.ConnectionClass.ListeBanques().Rows)
                    comboBox1.Items.Add(dtR.ItemArray[1].ToString());
                cmbTypeContrat.Text = typeContrat;
                if (etatModifier == "1")
                {
                    RemplissageDesChamps();
                    changerAuto = false;
                }
                else {
                    changerAuto = true;
                  
                    var moisDeService = 0;
                    if (typeContrat.Equals("CDI"))
                    {
                        moisDeService = (DateTime.Now.Date.Subtract(datePriseService).Days / 30);
                        textBox1.Text = moisDeService.ToString();
                    }

                    var dtSalaire = AppCode.ConnectionClass.ListeSalaire(numMatricule);
                    txttauxCNPS.Text = dtSalaire[0].TauxCNPS.ToString(); ;
                    txtCNPSCSND.Text = dtSalaire[0].TauxCNPSPatronal.ToString();
                    txtOnasa.Text = dtSalaire[0].TauxONASA.ToString();
                    txttauxIRPP.Text = dtSalaire[0].TauxIRPP.ToString();

                    ViderLesControles();
                    txtNomPersonnel.Text = nomEmploye;
                    txtMatricule.Text = numMatricule;
                    txtFonction.Text = fonction;
                    Height = MainForm.height1;
                    //Location = new Point((MainForm.width - Width) / 2, 5);
                    if (!string.IsNullOrWhiteSpace(numeroCompte))
                    {
                        if (GestionPharmacetique.MonMessageBox.ShowBox("Ce salarié possède un compte bancaire, voulez vous proceder par un mode de paiement par virement bancaire?", "Confirmation") == "1")
                        {
                            rdbBancaire.Checked = true;
                        }
                        else
                        {
                            rdbEspeces.Checked = true;
                        }
                    }
                    else
                    {
                        rdbEspeces.Checked = true;
                    }
                    comboBox1.Text = banque;
                    double cat;
                    if (ancienneteDuPersonnel.Contains("%"))
                    {
                        ancienneteDuPersonnel.Remove(ancienneteDuPersonnel.IndexOf("%"));
                    }
                    if (double.TryParse(ancienneteDuPersonnel, out cat))
                    {
                    }
                    else
                    {
                        cat = 0;
                    }
                    //if (typeContrat == "CDI" && typeContrat == "CDD")
                    //{
                    //    //cat = 0;
                    //}
                    //else
                    //{
                    //    cat = 0;
                    //}
                    txtCategorie.Text = cat.ToString();
                    var dtAccompte = AppCode.ConnectionClass.ListeAccompte(numMatricule);

                    var avanceSurSalaire = 0.0;

                    var liste = AppCode.ConnectionClass.ListeAvanceSurSalaire(exercice, mois, numMatricule);
                    if (liste != null)
                    {
                        var montantTotal = 0.0;
                        foreach (var p in liste)
                        {
                            montantTotal += p.AvanceSurSalaire;
                        }
                        avanceSurSalaire = montantTotal;
                        txtAvanceSurSalaaire.Text = montantTotal.ToString();

                    }
                    else
                    {
                        avanceSurSalaire = 0.0;
                        txtAvanceSurSalaaire.Text = "";
                    }

                    txtSalaireBase.Text =  dtSalaire[0].SalaireBase.ToString();

                    Double remboursement, accompte, aPayer;
                    if (dtAccompte.Rows.Count > 0)
                    {
                        if (Double.TryParse(dtAccompte.Rows[0].ItemArray[2].ToString(), out accompte) &&
                            Double.TryParse(dtAccompte.Rows[0].ItemArray[3].ToString(), out remboursement) &&
                            Double.TryParse(dtAccompte.Rows[0].ItemArray[4].ToString(), out aPayer))
                        {
                            idAccompe = Convert.ToInt32(dtAccompte.Rows[0].ItemArray[0].ToString());
                            if (remboursement >= accompte)
                            {
                                txtAcomptePayer.Text = "0";
                                txtAcompte.Text = "0";
                                txtAcomptePayer.Text = "0";
                                aPayer = 0.0;
                            }
                            else
                            {
                                txtAcompte.Text = accompte.ToString();
                                txtAccounptPaye.Text = remboursement.ToString();
                                //txtAcomptePayer.Text = aPayer.ToString();
                                var reste = accompte - remboursement;
                                if (reste >= aPayer)
                                {
                                    txtAcomptePayer.Text = aPayer.ToString();
                                }
                                else
                                {
                                    txtAcomptePayer.Text = reste.ToString();
                                }
                            }
                        }
                        else
                        {
                            idAccompe = 0;
                            accompte = remboursement = aPayer = 0.0;
                            txtAcomptePayer.Text = "0";
                            txtAcompte.Text = "0";
                        }

                    }
                    else
                    {

                        aPayer = 0.0;
                        txtAcomptePayer.Text = "0";
                        txtAcompte.Text = "0";
                    }

                    var salaireGain = System.Math.Round(salaireDeBase * (1 + cat / 100));
                    salaireDeBase = (double)dtSalaire[0].SalaireBase;
                    tauxConge = salaireGain / 24;
                    tauxAbscense = salaireGain / 26;
                    lblTauxAbscense.Text = Math.Round(tauxAbscense).ToString();
                    lblTauxConges.Text = Math.Round(tauxConge).ToString();

                    totalCnps = System.Math.Round(salaireGain * dtSalaire[0].TauxCNPS / 100);
                    totalCnpsCndn = System.Math.Round(salaireGain * dtSalaire[0].TauxCNPSPatronal / 100);
                    totalIRPP = System.Math.Round((salaireGain - totalCnps) * dtSalaire[0].TauxIRPP / 100);

                    lblChargePat.Text = totalCnpsCndn.ToString();
                    lblTotalCnps.Text = totalCnps.ToString();
                    lblTotalIrpp.Text = totalIRPP.ToString();
                    var totalCharge = totalCnps + totalIRPP + dtSalaire[0].TauxONASA;
                    lblTotalCharges.Text = totalCharge.ToString();
                    salaireBrut = salaireGain;
                    lblSalaireBrut.Text = salaireBrut.ToString();
                    netAPayer = salaireBrut - aPayer - avanceSurSalaire;
                    if (salaireGain.ToString().Contains(","))
                    {
                        txtGainAnciennete.Text = salaireGain.ToString().Replace(",", ".");
                    }
                    txtGainAnciennete.Text = salaireGain.ToString();
                    if (typeContrat == "CDI" || typeContrat == "CDD")
                    {
                        txtCategorie.Text = ancienneteDuPersonnel.ToString() + "%";
                    }
                    else
                    {
                        txtCategorie.Text =  "0%";
                    }
                    txtNetPayer.Text = netAPayer.ToString();
                    txtAvanceSurSalaaire.Text = avanceSurSalaire.ToString();
                          NetAPayer();
                }
            }
            catch (Exception ex)
            {
                GestionPharmacetique.MonMessageBox.ShowBox("form load", ex);
            }
        }

        #region
      public static  string  mois, ancienneteDuPersonnel, nomEmploye,banque, numMatricule, numeroCompte,  fonction, typeContrat, etatModifier="0";
        double netAPayer, totalCnps, totalCnpsCndn,ancienCoutSalarial, ancienAcomptePaye,
            totalIRPP, salaireDeBase, salaireBrut, transport, congeAnnuel, tauxConge, tauxAbscense;
        public static DateTime datePaiement;
        private void PaiementForme_Paint(object sender, PaintEventArgs e)
        {
            Graphics mGraphics = e.Graphics;
            Pen pen1 = new Pen(Color.SteelBlue,1);Rectangle area1 = new Rectangle(0, 0, Width - 1, Height - 1);
            LinearGradientBrush linearGradientBrush = new LinearGradientBrush(area1,
                Color.AliceBlue, Color.AliceBlue, LinearGradientMode.ForwardDiagonal);
            mGraphics.FillRectangle(linearGradientBrush, area1);
            mGraphics.DrawRectangle(pen1, area1);
        }

        private void txtNbreJourConge_MouseEnter(object sender, EventArgs e)
        {
            changerAuto = true;
        }

        public static   int idAccompe, numeroPaiement,exercice;
      public static DateTime datePriseService;
      public static AppCode.Paiement paiement;
        public static PaiementForme frm;
        static bool flag, changerAuto;
        
        #endregion

        public static bool ShowBox()
        {
            try
            {
                frm = new PaiementForme();
                frm.ShowDialog();
                return flag;
            }
            catch(Exception ex)
            {
                GestionPharmacetique.MonMessageBox.ShowBox("", ex);
                return false;
            }
        }


        private void NetAPayer()
        {
            try
            {
                if (changerAuto)
                {
                    double priseEnChargeFamille, totalAbsence, totalConges, primeTransport, primeRende, primeGarde, primePerfect, heureSupp, acompte, avanceSurSalaire;

                    if (!string.IsNullOrEmpty(txtSalaireBase.Text))
                    {
                        salaireDeBase = Convert.ToDouble(txtSalaireBase.Text);
                    }
                    else
                    {
                        txtSalaireBase.BackColor = Color.Red;
                        txtGainAnciennete.BackColor = Color.Red;
                        lblSalaireBrut.Text = "";
                        txtGainAnciennete.Text = "";
                        txtNetPayer.Text = "";
                        return;
                    }

                    double cat;
                    var anc = txtCategorie.Text;
                    if (anc.Contains("%"))
                    {
                        anc = anc.Remove(anc.IndexOf("%"));
                    }
                    if (double.TryParse(anc, out cat))
                    {
                    }
                    else
                    {
                        cat = 0;
                    }
                    //if (typeContrat == "CDI" && typeContrat == "CDD")
                    //{
                    //    //cat = 0;
                    //}
                    //else
                    //{
                    //    cat = 0;
                    //}
                    txtGainAnciennete.Text = System.Math.Round(salaireDeBase * (1 + cat / 100)).ToString();
                    //if (!string.IsNullOrEmpty(txtGainAnciennete.Text))
                    //{
                    //    salaireDeBase = Convert.ToDouble(txtGainAnciennete.Text);
                    //}
                    //else
                    //{
                    //    txtSalaireBase.BackColor = Color.Red;
                    //    txtGainAnciennete.BackColor = Color.Red;
                    //    lblSalaireBrut.Text = "";
                    //    txtGainAnciennete.Text = "";
                    //    txtNetPayer.Text = "";
                    //    return;
                    //}

                    #region CongesAbscence
                    if (!string.IsNullOrEmpty(txtNbreJourConge.Text))
                    {
                        //int 
                        if (Double.TryParse(txtNbreJourConge.Text, out totalConges))
                        {
                            totalConges = totalConges * Math.Round(tauxConge);
                            lblMontantConge.Text = totalConges.ToString();
                           
                        }
                        else
                        {
                            
                            txtNbreJourConge.BackColor = Color.Red;
                            txtNbreJourConge.Focus();
                            return;
                        }
                    }
                    else
                    {
                        totalConges = 0;
                        lblMontantConge.Text = "";
                    }

                    if (!string.IsNullOrEmpty(txtNbreJourAbscente.Text))
                    {
                        if (Double.TryParse(txtNbreJourAbscente.Text, out totalAbsence))
                        {
                            totalAbsence = totalAbsence * Math.Round(tauxAbscense);
                            lblMontantAbscenses.Text = totalAbsence.ToString();
                            
                        }
                        else
                        {
                            txtNbreJourAbscente.BackColor = Color.Red;
                            txtNbreJourAbscente.Focus();
                            return;
                        }
                    }
                    else
                    {
                        lblMontantAbscenses.Text = "";
                        totalAbsence = 0;
                    }

                    #endregion

                    #region ChargeDuPersonnel
                    double cnpsCsdn,cnps,irpp,onasa;
                    if (double.TryParse(txtCNPSCSND.Text, out cnpsCsdn))
                    {
                    }
                    else if (txtCNPSCSND.Text.Contains(","))
                    {
                        txtCNPSCSND.Text.Replace(",", ".");
                        {
                            if (double.TryParse(txtCNPSCSND.Text, out cnpsCsdn))
                            {
                            }
                            else
                            {
                                txtCNPSCSND.BackColor = Color.Red;
                                cnpsCsdn = 0;
                            }
                        }
                    }
                    else
                    {
                        txtCNPSCSND.BackColor = Color.Red;
                        cnpsCsdn = 0;
                    }

                    if (double.TryParse(txttauxCNPS.Text, out cnps))
                    {
                    }
                    else if (txttauxCNPS.Text.Contains(","))
                    {
                        txttauxCNPS.Text.Replace(",", ".");
                        {
                            if (double.TryParse(txttauxCNPS.Text, out cnps))
                            {
                            }
                            else
                            {
                                txttauxCNPS.BackColor = Color.Red;
                                cnps = 0;
                            }
                        }
                    }
                    else
                    {
                        txttauxCNPS.BackColor = Color.Red;
                        cnps = 0;
                    }

                    if (double.TryParse(txttauxIRPP.Text, out irpp))
                    {
                    }
                    else if (txttauxIRPP.Text.Contains(","))
                    {
                        txttauxIRPP.Text.Replace(",", ".");
                        {
                            if (double.TryParse(txttauxIRPP.Text, out irpp))
                            {
                            }
                            else
                            {
                                txttauxIRPP.BackColor = Color.Red;
                                irpp = 0;
                            }
                        }
                    }
                    else
                    {
                        txttauxIRPP.BackColor = Color.Red;
                        irpp = 0;
                    }

                    if (double.TryParse(txtOnasa.Text, out onasa))
                    {
                    }
                    else
                    {
                        onasa = 0;
                        txtOnasa.BackColor = Color.Red;
                    }

                    salaireBrut = double.Parse(txtGainAnciennete.Text) + totalConges - totalAbsence;
                    lblSalaireBrut.Text = salaireBrut.ToString();

                    lblBaseCNPS.Text = salaireBrut.ToString();
                    label51.Text = salaireBrut.ToString();

                    totalCnps = System.Math.Round(salaireBrut * cnps / 100);
                    totalCnpsCndn = System.Math.Round(salaireBrut * cnpsCsdn / 100);
             
                       var salaireImposable = (double.Parse(txtGainAnciennete.Text) ) * 12;
                       if (cmbTypeContrat.Text == "CDD" || cmbTypeContrat.Text == "CDI")
                       {
                           cnps = 3.5;
                           cnpsCsdn = 16.5;
                           if (salaireImposable < 800000)
                           {
                               totalIRPP = 0;
                               irpp = 0;
                               onasa = 0;
                           }
                           else
                           {
                               irpp = 10.5;
                               
                               totalIRPP = System.Math.Round((salaireBrut - totalCnps) * irpp / 100);

                               if (totalConges > 0)
                               {
                                   onasa = 80;
                               }
                               else
                               {
                                   onasa = 40;
                               }
                           }
                       }
                       else
                       {
                           irpp = 0;
                           totalIRPP = 0;
                           onasa = 0;
                           cnps = 0;
                           cnpsCsdn = 0;
                       }
                    lblBaseIRPP.Text = (salaireBrut - totalCnps).ToString();
                    txttauxIRPP.Text = irpp.ToString();
                    txtOnasa.Text = onasa.ToString();
                    lblChargePat.Text = totalCnpsCndn.ToString();
                    lblTotalCnps.Text = totalCnps.ToString();
                    lblTotalIrpp.Text = totalIRPP.ToString();
                    var totalCharge = totalCnps + totalIRPP + onasa;
                    lblTotalCharges.Text = totalCharge.ToString();
                    salaireBrut = salaireBrut - totalCharge;
                    #endregion
                    //lblSalaireBrut;
                    #region PrimesIndemnites

                    if (!string.IsNullOrEmpty(txtPrimeLogement.Text))
                    {
                        if (Double.TryParse(txtPrimeLogement.Text, out primeRende))
                        {
                        }
                        else
                        {
                            txtPrimeLogement.BackColor = Color.Red;
                            txtNetPayer.Text = "";
                            return;
                        }
                    }
                    else
                    {
                        primeRende = 0.0;
                    }

                    if (!string.IsNullOrEmpty(txtPrimeGarde.Text))
                    {
                        if (Double.TryParse(txtPrimeGarde.Text, out primeGarde))
                        {
                        }
                        else
                        {
                            txtPrimeGarde.BackColor = Color.Red;
                            txtNetPayer.Text = "";
                            return;
                        }
                    }
                    else
                    {
                        primeGarde = 0.0;
                    }

                    if (!string.IsNullOrEmpty(txtPrimeResponsabilite.Text))
                    {
                        if (Double.TryParse(txtPrimeResponsabilite.Text, out primePerfect))
                        {
                        }
                        else
                        {
                            txtPrimeResponsabilite.BackColor = Color.Red;
                            txtNetPayer.Text = "";
                            return;
                        }
                    }
                    else
                    {
                        primePerfect = 0.0;
                    }
                    //double jourPrestation, fraisPresatation;
                    if (!string.IsNullOrEmpty(txtHeurSupp.Text))
                    {
                        if (Double.TryParse(txtHeurSupp.Text, out heureSupp))
                        {
                        }
                        else
                        {
                            txtHeurSupp.BackColor = Color.Red;
                            txtNetPayer.Text = "";
                            return;
                        }
                    }
                    else
                    {
                        heureSupp = .0;
                    }
                    //       if (Double.TryParse(txtTauxPrestation.Text, out fraisPresatation))
                    //    {
                    //    }
                    //    else
                    //    {
                    //        txtTauxPrestation.BackColor = Color.Red;
                    //        txtNetPayer.Text = "";
                    //        return;
                    //    }
                    //    heureSupp = fraisPresatation * jourPrestation;
                    //    txtHeurSupp.Text = heureSupp.ToString();
                    //}
                    //else
                    //{
                    //    heureSupp = 0.0;
                    //}

                    if (!string.IsNullOrEmpty(txtTransport.Text))
                    {
                        if (Double.TryParse(txtTransport.Text, out primeTransport))
                        {
                        }
                        else
                        {
                            txtTransport.BackColor = Color.Red;
                            txtNetPayer.Text = "";
                            return;
                        }
                    }
                    else
                    {
                        primeTransport = 0.0;
                    }

                    #endregion

                    #region Deductions

                    if (!string.IsNullOrEmpty(txtAcomptePayer.Text))
                    {
                        if (Double.TryParse(txtAcomptePayer.Text, out acompte))
                        {
                        }
                        else
                        {
                            txtAcomptePayer.BackColor = Color.Red;
                            txtNetPayer.Text = "";
                            return;
                        }
                    }
                    else
                    {
                        acompte = 0.0;
                    }
                    if (!string.IsNullOrEmpty(txtAvanceSurSalaaire.Text))
                    {
                        if (double.TryParse(txtAvanceSurSalaaire.Text, out avanceSurSalaire))
                        { }
                        else
                        {
                            txtAvanceSurSalaaire.BackColor = Color.Red;
                            txtAvanceSurSalaaire.Focus();
                            return;
                        }
                    }
                    else
                    {
                        return;
                    }

                   
                    #endregion

                    var totalPrime = primeGarde + primePerfect + primeRende + heureSupp + primeTransport;
                    //var totalDeduction = totalCnps + totalIRPP + onasa;
                    var totalDette = avanceSurSalaire + acompte;
                    netAPayer = salaireBrut + totalPrime - totalDette;
                    lblTotalPrimes.Text = totalPrime.ToString();
                    var totalDeductif = totalDette + totalCharge;
                    lblTotalDeductif.Text = totalDeductif.ToString();
                    txtNetPayer.Text = netAPayer.ToString();
                    var coutSalarie = double.Parse(lblSalaireBrut.Text) + totalCnpsCndn + totalPrime;
                    lblCoutSalarial.Text = coutSalarie.ToString();
                }
            }
            catch (Exception ex)
            {
                GestionPharmacetique.MonMessageBox.ShowBox("Calcul net a payer", ex);
            }

        }

        private void txtGain_TextChanged(object sender, EventArgs e)
        {

        }

        private void btnFermer_Click(object sender, EventArgs e)
        {
            flag = false;
            Dispose();
        }

        private void CalulDuNet_TextChanged(object sender, EventArgs e)
        {
            NetAPayer();
            txtHeurSupp.BackColor = txtPrimeGarde.BackColor = txtPrimeLogement.BackColor=
                txtSalaireBase.BackColor = txttauxCNPS.BackColor = lblSalaireBrut.BackColor =txtNbreJourAbscente.BackColor = txtNbreJourConge.BackColor=
                txtPrimeResponsabilite.BackColor = txtTransport.BackColor = lblMontantConge.BackColor =
             lblCoutSalarial.BackColor=   txtOnasa.BackColor = txtCNPSCSND.BackColor = txttauxIRPP.BackColor = txttauxCNPS.BackColor = txtGainAnciennete.BackColor = Color.AliceBlue;
            lblCoutSalarial.BackColor = lblSalaireBrut.BackColor = Color.SteelBlue;
        }

        void ViderLesControles()
        {
            txtGainAnciennete.Text = "";
            txtCategorie.Text = "";
            txtAcomptePayer.Text = "";
            lblTotalIrpp.Text = "";
            lblMontantConge.Text = "";
            txtAcompte.Text = "";
            txtAcomptePayer.Text = "";
            txtAvanceSurSalaaire.Text = "";
            //cmbMois.Text = "";
            txtHeurSupp.Text = "";
            txtNetPayer.Text = "";
            txtPrimeGarde.Text = "";
            txtPrimeResponsabilite.Text = "";
            txtPrimeLogement.Text = "";
            txtTransport.Text = "";
            lblTotalCnps.Text = "";
            lblChargePat.Text = "";
            txtSalaireBase.Text = "";
            lblSalaireBrut.Text = "";
        }

       AppCode. Paiement CreerLesDetailsDePaie()
        {
            try
            {
                double primeLogement, primeResponsabilite, primeGarde, heureSupp, acompte,onasa, irpp,cnps, salaireBrut, soinFamille, avanceSurSalaire, coutAbscence;
                var p = new AppCode.Paiement();
                p.IDAcompte = idAccompe;

                //if (!string.IsNullOrEmpty(txtJourPrestation.Text))
                //{
                //    double jour;
                //    if (Double.TryParse(txtJourPrestation.Text, out jour))
                //    { p.JourPrestations = jour; }
                //    else
                //    {
                //        p.JourPrestations = 0;
                //    }
                       
                //}
                //else
                //{
                //    p.JourPrestations = 0;
                //}

                if (!string.IsNullOrEmpty(txtSalaireBase.Text))
                {
                    p.SalaireBase = Double.Parse(txtSalaireBase.Text);
                }
                else
                {
                    txtSalaireBase.BackColor = Color.Red;
                    return null;
                }

                if (!string.IsNullOrEmpty(lblSalaireBrut.Text))
                {
                    p.SalaireBrut = Double.Parse(lblSalaireBrut.Text);
                }
                else
                {
                    lblSalaireBrut.BackColor = Color.Red;
                    return null;
                }

                if (!string.IsNullOrEmpty(numMatricule))
                {
                    p.NumeroMatricule = numMatricule;
                }
                else
                {
                    GestionPharmacetique.MonMessageBox.ShowBox("Veuillez selectionner le nom de l'employé sur la liste avant de continuer", "Erreur");
                    return null;
                }
                if (!string.IsNullOrEmpty(txtPrimeLogement.Text))
                {
                    if (Double.TryParse(txtPrimeLogement.Text, out primeLogement))
                    {
                        p.PrimeLogement = primeLogement;
                    }
                    else
                    {
                        txtPrimeLogement.BackColor = Color.Red;
                        txtPrimeLogement.Focus();
                        return null;
                    }
                }
                else
                {
                    {
                        p.PrimeLogement = 0.0;
                    }
                }

                if (!string.IsNullOrEmpty(txtPrimeGarde.Text))
                {
                    if (Double.TryParse(txtPrimeGarde.Text, out primeGarde))
                    {
                        p.PrimeGarde = primeGarde;
                    }
                    else
                    {
                        txtPrimeGarde.BackColor = Color.Red;
                        txtPrimeGarde.Focus();
                        return null;
                    }
                }
                else
                {
                    {
                        p.PrimeGarde = 0.0;
                    }
                }

                if (Double.TryParse(lblTotalCnps.Text, out cnps))
                {
                    p.CNPS = cnps;
                }
                else
                {
                    lblTotalCnps.BackColor = Color.Red;
                    return null;
                }

                if (Double.TryParse(lblTotalIrpp.Text, out irpp))
                {
                    p.IRPP = irpp;
                }
                else
                {
                    lblTotalIrpp.BackColor = Color.Red;
                    return null;
                }


                if (Double.TryParse(lblChargePat.Text, out totalCnpsCndn))
                {
                    p.ChargePatronale = totalCnpsCndn;
                }
                else
                {
                    lblChargePat.BackColor = Color.Red;
                    return null;
                }

                if (Double.TryParse(txtOnasa.Text, out onasa))
                {
                    p.ONASA = onasa;
                }
                else
                {
                    txtOnasa.BackColor = Color.Red;
                    return null;
                }

                if (!string.IsNullOrEmpty(txtPrimeResponsabilite.Text))
                {
                    if (Double.TryParse(txtPrimeResponsabilite.Text, out primeResponsabilite))
                    {
                        p.PrimeResponsabilite = primeResponsabilite;
                    }
                    else
                    {
                        txtPrimeResponsabilite.BackColor = Color.Red;
                        txtPrimeResponsabilite.Focus();
                        return null;
                    }
                }
                else
                {
                    {
                        p.PrimeResponsabilite = 0.0;
                    }
                }


                if (!string.IsNullOrEmpty(txtHeurSupp.Text))
                {
                    if (Double.TryParse(txtHeurSupp.Text, out heureSupp))
                    {
                        p.HeureSupplementaire = heureSupp;
                    }
                    else
                    {
                        txtHeurSupp.BackColor = Color.Red;
                        txtHeurSupp.Focus();
                        return null;
                    }
                }
                else
                {
                    {
                        p.HeureSupplementaire = 0.0;
                    }
                }

                if (!string.IsNullOrEmpty(txtAcomptePayer.Text))
                {
                    if (Double.TryParse(txtAcomptePayer.Text, out acompte))
                    {
                        p.AcomptePaye = acompte;
                    }
                    else
                    {
                        txtAcomptePayer.BackColor = Color.Red;
                        txtAcomptePayer.Focus();
                        return null;
                    }
                }
                else
                {
                    {
                        p.AcomptePaye = 0.0;
                    }
                }

             

                if (!string.IsNullOrEmpty(lblMontantConge.Text))
                {
                    if (Double.TryParse(lblMontantConge.Text, out congeAnnuel))
                    {
                        p.CongeAnnuel = congeAnnuel;
                    }
                    else
                    {
                        lblMontantConge.BackColor = Color.Red;
                        lblMontantConge.Focus();
                        return null;
                    }
                }
                else
                {
                    {
                        p.CongeAnnuel = 0.0;
                    }
                }

                if (!string.IsNullOrEmpty(lblMontantAbscenses.Text))
                {
                    if (Double.TryParse(lblMontantAbscenses.Text, out coutAbscence))
                    {
                        p.CoutAbsence = coutAbscence;
                    }
                    else
                    {
                        lblMontantAbscenses.BackColor = Color.Red;
                        lblMontantAbscenses.Focus();
                        return null;
                    }
                }
                else
                {
                    {
                        p.CoutAbsence = 0.0;
                    }
                }

                if (!string.IsNullOrEmpty(txtAvanceSurSalaaire.Text))
                {
                    if (Double.TryParse(txtAvanceSurSalaaire.Text, out avanceSurSalaire))
                    {
                        p.AvanceSurSalaire = avanceSurSalaire;
                    }
                    else
                    {
                        txtAvanceSurSalaaire.BackColor = Color.Red;
                        txtAvanceSurSalaaire.Focus();
                        return null;
                    }
                }
                else
                {
                    {
                        p.AvanceSurSalaire  = 0.0;
                    }
                }

                if (!string.IsNullOrEmpty(txtTransport.Text))
                {
                    if (Double.TryParse(txtTransport.Text, out transport))
                    {
                        p.Transport = transport;
                    }
                    else
                    {
                        txtTransport.BackColor = Color.Red;
                        txtTransport.Focus();
                        return null;
                    }
                }
                else
                {
                    {
                        p.Transport = 0.0;
                    }
                }

                   
                if (Double.TryParse(txtNetPayer.Text, out netAPayer))
                {
                    p.SalaireNet = netAPayer;
                }
                else
                {
                    txtNetPayer.BackColor = Color.Red;
                    txtNetPayer.Focus();
                    return null;
                }

                if (Double.TryParse(lblSalaireBrut.Text, out salaireBrut))
                {
                    p.SalaireBrut = salaireBrut;
                }
                else
                {
                    lblSalaireBrut.BackColor = Color.Red;
                    lblSalaireBrut.Focus();
                    return null;
                }

                double coutSalarie;
                if (Double.TryParse(lblCoutSalarial.Text, out coutSalarie))
                {
                    p.CoutDuSalarie = coutSalarie ;
                }
                else
                {
                    lblCoutSalarial.BackColor = Color.Red;
                    lblCoutSalarial.Focus();
                    return null;
                }
                if (rdbEspeces.Checked)
                {
                    p.ModePaiement = "Paiement en espèces";
                }
                else if (rdbCheques.Checked)
                {
                    p.ModePaiement = "Paiement par chèques";
                }
                else if (rdbBancaire.Checked)
                {
                    p.ModePaiement = "Virement bancaire";
                }
                else
                {
                    p.ModePaiement = "";
                }
                p.DatePaiement = DateTime.Now;
                p.Service = cmbTypeContrat.Text;
                p.Banque = comboBox1.Text;
                return p;
            }
            catch (Exception ex)
            {
                GestionPharmacetique.MonMessageBox.ShowBox("Creer detail de paie", ex);
                return null;
            }
        }

        private void btnModifier_Click(object sender, EventArgs e)
        {

           try
            {
                 paiement = CreerLesDetailsDePaie();
                if (paiement != null)
                {
                    paiement.MontantTotal = double.Parse(txtNetPayer.Text); ;
                    paiement.IDPaie = numeroPaiement;
                    paiement.IDAcompte = idAccompe;
                    if (etatModifier == "1")
                    {
                        if (AppCode.ConnectionClass.ModifierOrdreDePaiement(paiement,ancienCoutSalarial,ancienAcomptePaye))
                        {
                            flag = true;
                            Dispose();
                        }
                    }
                    else
                    {
                        if (AppCode.ConnectionClass.InsererModifierOrdreDePaiement(paiement, idAccompe))
                        {
                            flag = true;
                            Dispose();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                GestionPharmacetique.MonMessageBox.ShowBox("ajouter les details paiement", ex);
            }
        }

        private void cmbTypeContrat_SelectedIndexChanged(object sender, EventArgs e)
        {

            if (!cmbTypeContrat.Equals("CDI") ||! cmbTypeContrat.Equals("CDI"))
            {
                txtCNPSCSND.Text = "0";
                txttauxCNPS.Text="0";
                txttauxIRPP.Text ="0";
                txtOnasa.Text = "0";
                txtCategorie.Text = "0%";
            }

        }

        }

}
