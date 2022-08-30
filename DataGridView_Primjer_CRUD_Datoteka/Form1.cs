using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace DataGridView_Primjer_CRUD_Datoteka
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        //pohrana i loadanje
        public void PohraniUDatoteku() {
            string vrijednosti = "";
            for (int i = 0; i < dgv.Rows.Count; i++) {
                vrijednosti = vrijednosti + (string)dgv.Rows[i].Cells[0].Value + "\n" + (string)dgv.Rows[i].Cells[1].Value + "\n";
            }
            System.IO.File.WriteAllText(Application.StartupPath + "\\zadaci.txt", vrijednosti);

        }

        public void UcitavanjeIzDatoteke() {
            if (System.IO.File.Exists(Application.StartupPath + "\\zadaci.txt")) {
                string vrijednosti = System.IO.File.ReadAllText(Application.StartupPath + "\\zadaci.txt");
                char[] znak = new char[1];
                znak[0] = '\n';
                string[] v = vrijednosti.Split(znak, StringSplitOptions.RemoveEmptyEntries);
                for (int i = 0; i < v.Length; i += 2) {
                    dgv.Rows.Add(v[i], v[i + 1]);
                }
            }
        }



        private void btnDodaj_Click(object sender, EventArgs e)
        {
            Form2 formaUnos = new Form2();
            if (formaUnos.ShowDialog() == DialogResult.OK) {
                dgv.Rows.Add(formaUnos.txtZadatak.Text.Trim(), formaUnos.txtOpis.Text.Trim());
                PohraniUDatoteku();
            }
        }

        private void btnUredi_Click(object sender, EventArgs e)
        {
            if (dgv.SelectedRows.Count == 0) {
                return;
            }
            Form2 formaUnos = new Form2();
            formaUnos.txtZadatak.Text = (String)dgv.SelectedRows[0].Cells[0].Value;
            formaUnos.txtOpis.Text = (String)dgv.SelectedRows[0].Cells[1].Value;

            if (formaUnos.ShowDialog() == DialogResult.OK) {
                dgv.Rows.Remove(dgv.SelectedRows[0]);
                dgv.Rows.Add(formaUnos.txtZadatak.Text.Trim(), formaUnos.txtOpis.Text.Trim());
                PohraniUDatoteku();
            }


        }

        private void btnObrisi_Click(object sender, EventArgs e)
        {
            if (dgv.SelectedRows.Count == 0) {
                return;
            }
            dgv.Rows.Remove(dgv.SelectedRows[0]);
            PohraniUDatoteku();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            UcitavanjeIzDatoteke();
        }

        
    }
}
