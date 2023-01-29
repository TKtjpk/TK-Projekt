using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using System.IO;

namespace TK_Projekt
{
    public partial class Form1 : Form
    {
        private List<Pojazd> Pojazds = new List<Pojazd>();
        static private bool login = false;
        private int counter = 0;
        static public bool Login { get { return login; } set { login = value; } }
        private void LoadList()
        {
            Wyposazenie Wyposazenie = new Wyposazenie();
            Wyposazenie = new SQLClassWyp().Connection(Pojazds[Table1.CurrentRow.Index].Id);

            MemoryStream image = new MemoryStream(Pojazds[Table1.CurrentRow.Index].Image);
            pictureBox1.Image = Image.FromStream(image);
            label1.Text = "Marka: " + Pojazds[Table1.CurrentRow.Index].MarkaSamochodu.Trim();
            label2.Text = "Model: " + Pojazds[Table1.CurrentRow.Index].ModelSamochodu.Trim();
            string met = Pojazds[Table1.CurrentRow.Index].CzyMetallic ? " - Metalic" : "";
            label3.Text = "Kolor: " + Pojazds[Table1.CurrentRow.Index].KolorSamochodu.Trim() + met;
            label4.Text = "Rok Produkcji:  " + Pojazds[Table1.CurrentRow.Index].RokProdukcji;
            label5.Text = "Pojemność silnika: " + Pojazds[Table1.CurrentRow.Index].Silnik;

            label6.Visible = Wyposazenie.Klimatyzacja ? true : false;
            label7.Visible = Wyposazenie.Radio ? true : false;
            label8.Visible = Wyposazenie.Szyberdach ? true : false;
            label9.Visible = Wyposazenie.Nawigacja ? true : false;
            label10.Visible = Wyposazenie.CarPlay ? true : false;

            SQLClassRating some = new SQLClassRating();
            label12.Text = some.Connection(Pojazds[Table1.CurrentRow.Index].Id).ToString();
        }
        private void FullList(string marka= "", string model = "", string kolor = "", int rok= 0, float silnik=0, bool metalic=false)//, Image image=null)
        {
            Table1.Rows.Clear();

            Pojazds.Clear();

            Pojazds = new SQLClassFull().Connection(marka, model, kolor, rok, silnik, metalic);

            for (int i = 0; i < Pojazds.Count; i++)
            {
                
                Table1.Rows.Add(Pojazds[i].MarkaSamochodu, Pojazds[i].ModelSamochodu, Pojazds[i].KolorSamochodu, Pojazds[i].RokProdukcji, Pojazds[i].Silnik, Pojazds[i].CzyMetallic);
            }

            for (int i = 0; i <Table1.Rows.Count; i++)
            {
                Table1.Rows[i].Height = 18;
            }

            Table1.Height = Table1.Rows.Count == 0 ? 0 : Table1.Rows.Count * Table1.Rows[0].Height > 300 ? 300 : Table1.Rows.Count * Table1.Rows[0].Height + 23;
        }
        public Form1()
        {
            InitializeComponent();

            ComboBox[] lista = { comboBox1, comboBox2, comboBox3, comboBox4, comboBox5 };

            int count = 0;

            foreach (ComboBox comboBox in lista)
            {
                comboBox.Width = (Width - 100) / (lista.Count() + 1);
                comboBox.Left = count * comboBox.Width + 20 + count*10;
                comboBox.Top = 50;
                count++;
            }

            checkBox1.Width = comboBox1.Width;
            checkBox1.Left = count * comboBox1.Width + 20 + count*10;
            checkBox1.Top = 55;

            comboBox1.Focus();

            comboBox2.Visible= false;
            comboBox3.Visible= false;
            comboBox4.Visible= false;
            comboBox5.Visible= false;

            checkBox1.Visible= false;

            groupBox1.Width = Width * 970 / 1856;

            label6.Visible= false;
            label7.Visible= false;
            label8.Visible= false;
            label9.Visible= false;
            label10.Visible= false;

            edycjaToolStripMenuItem.Visible = Login;

            
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            FullList();

            if (Pojazds != null)
            {
                for (int i = 0; i < Pojazds.Count; i++)
                {
                    if (!comboBox1.Items.Contains(Pojazds[i].MarkaSamochodu.Trim()))
                    {
                        comboBox1.Items.Add(Pojazds[i].MarkaSamochodu.Trim());
                    }
                }
            }
            LoadList();
        }
        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            string marka = comboBox1.SelectedItem.ToString();

            FullList(marka);

            comboBox2.Items.Clear();
            comboBox3.Items.Clear();
            comboBox4.Items.Clear();
            comboBox5.Items.Clear();
            checkBox1.Checked = false;

            for (int i = 0; i < Pojazds.Count; i++)
            {
                if (!comboBox2.Items.Contains(Pojazds[i].ModelSamochodu.Trim()))
                {
                    comboBox2.Items.Add(Pojazds[i].ModelSamochodu.Trim());
                }
            }

            comboBox2.Visible = true;

            comboBox2.Focus();
        }
        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            string marka = comboBox1.SelectedItem.ToString();
            string model = comboBox2.SelectedItem.ToString();

            FullList(marka, model);

            comboBox3.Items.Clear();
            comboBox4.Items.Clear();
            comboBox5.Items.Clear();
            checkBox1.Checked = false;

            for (int i = 0; i < Pojazds.Count; i++)
            {
                if (!comboBox3.Items.Contains(Pojazds[i].KolorSamochodu.Trim()))
                {
                    comboBox3.Items.Add(Pojazds[i].KolorSamochodu.Trim());
                }
            }

            comboBox3.Visible = true;

            comboBox3.Focus();
        }
        private void comboBox3_SelectedIndexChanged(object sender, EventArgs e)
        {
            string marka = comboBox1.SelectedItem.ToString();
            string model = comboBox2.SelectedItem.ToString();
            string kolor = comboBox3.SelectedItem.ToString();

            FullList(marka, model, kolor);

            comboBox4.Items.Clear();
            comboBox5.Items.Clear();
            checkBox1.Checked = false;

            for (int i = 0; i < Pojazds.Count; i++)
            {
                if (!comboBox4.Items.Contains(Pojazds[i].RokProdukcji))
                {
                    comboBox4.Items.Add(Pojazds[i].RokProdukcji);
                }
            }

            comboBox4.Visible = true;

            comboBox4.Focus();
        }
        private void comboBox4_SelectedIndexChanged(object sender, EventArgs e)
        {
            string marka = comboBox1.SelectedItem.ToString();
            string model = comboBox2.SelectedItem.ToString();
            string kolor = comboBox3.SelectedItem.ToString();
            int rok = (int)comboBox4.SelectedItem;

            FullList(marka, model, kolor, rok);

            comboBox5.Items.Clear();

            for (int i = 0; i < Pojazds.Count; i++)
            {
                if (!comboBox5.Items.Contains(Pojazds[i].Silnik))
                {
                    comboBox5.Items.Add(Pojazds[i].Silnik);
                }
            }

            comboBox5.Visible = true;
            checkBox1.Checked = false;

            comboBox5.Focus();
        }
        private void comboBox5_SelectedIndexChanged(object sender, EventArgs e)
        {
            string marka = comboBox1.SelectedItem.ToString();
            string model = comboBox2.SelectedItem.ToString();
            string kolor = comboBox3.SelectedItem.ToString();
            int rok = (int)comboBox4.SelectedItem;
            float silnik = (float)comboBox5.SelectedItem;

            checkBox1.Checked = false;

            FullList(marka, model, kolor, rok, silnik);

            checkBox1.Visible = true;

            checkBox1.Focus();
        }
        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            string marka = comboBox1.SelectedItem == null ? "" : comboBox1.SelectedItem.ToString();
            string model = comboBox2.SelectedItem == null ? "" : comboBox2.SelectedItem.ToString();
            string kolor = comboBox3.SelectedItem == null ? "" : comboBox3.SelectedItem.ToString();
            int rok = comboBox4.SelectedItem == null ? 0 : (int)comboBox4.SelectedItem;
            float silnik = comboBox5.SelectedItem == null ? 0 : (float)comboBox5.SelectedItem;
            bool IsChecked = checkBox1.Checked;

            FullList(marka, model, kolor, rok, silnik, IsChecked);
        }
        private void Form1_Resize(object sender, EventArgs e)
        {
            ComboBox[] lista = { comboBox1, comboBox2, comboBox3, comboBox4, comboBox5 };
            
            int count = 0;

            foreach (ComboBox comboBox in lista)
            {
                comboBox.Width = (Width - 100) / (lista.Count() + 1);
                comboBox.Left = count * comboBox.Width + 20 + count * 10;
                comboBox.Top = 50;
                count++;
            }

            checkBox1.Width = comboBox1.Width;
            checkBox1.Left = count * comboBox1.Width + 20 + count * 10;
            checkBox1.Top = 55;

            groupBox1.Width = Width * 970 / 1856;
        }
        private void Table1_Cell_Click(object sender, DataGridViewCellEventArgs e)
        {
            LoadList();
        }
        private void button1_MouseClick(object sender, MouseEventArgs e)
        {
            SQLClassRating some = new SQLClassRating();
            if (MessageBox.Show("Do you want to rate selected car ?", "Confirm", MessageBoxButtons.OKCancel) == DialogResult.OK)
            {
                some.Rate(Pojazds[Table1.CurrentRow.Index].Id, ratingControl1.Rating1);
            }
        }
        private void zalogujToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Login form = new Login();
            form.ShowDialog();
        }
        private void wylogujToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Login = false;
        }
        private void timer1_Tick(object sender, EventArgs e)
        {
            if (Login)
            {
                counter++;

                if (counter > 1200)
                {
                    counter = 0;
                    Login = false;
                }
            }
            edycjaToolStripMenuItem.Visible = Login;
        }
    }
}
