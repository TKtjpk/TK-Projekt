using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TK_Projekt
{
    public partial class Form1 : Form
    {
        private void GetMarka()
        {
            SQLConnMarka marka = new SQLConnMarka();
            marka.Connection();
            if (marka.Connection() != null)
            {
                for (int i = 0; i < marka.Connection().Count; i++)
                {
                    comboBox1.Items.Add(marka.Connection()[i]);
                }
            }
        }
        private void GetModel(string marka)
        {
            SQLClassModel model = new SQLClassModel();
            model.Connection(marka);
            if (model.Connection(marka) != null)
            {
                comboBox2.Items.Clear();
                for (int i = 0; i < model.Connection(marka).Count; i++)
                {
                    comboBox2.Items.Add(model.Connection(marka)[i]);
                }
            }
        }
        private void FullList(string marka)//, string model, string kolor="", int rokProdukcji=0, float silnik=0, bool metallic=false, Image image=null)
        {
            listBox1.Items.Clear();
            listBox2.Items.Clear();
            listBox3.Items.Clear();
            SQLClassFull pojazd = new SQLClassFull();
            for (int i = 0; i < pojazd.Connection(marka).Count; i++)
            {
                Pojazd car = pojazd.Connection(marka)[i];
                listBox1.Items.Add(car.MarkaSamochodu);
                listBox2.Items.Add(car.ModelSamochodu);
                listBox3.Items.Add(car.KolorSamochodu);
            }
        }
        public Form1()
        {
            InitializeComponent();
            comboBox1.Focus();
            comboBox2.Visible= false;
            listBox1.MultiColumn = true;
            listBox1.ColumnWidth = 50;
            
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            GetMarka();
            FullList("");
        }
        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            string marka = comboBox1.SelectedItem.ToString();
            GetModel(marka);
            FullList(marka);
            comboBox2.Visible = true;
            comboBox2.Focus();
        }
    }
}
