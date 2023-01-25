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
            SQLConn marka = new SQLConn();
            marka.Connection();
            if (marka.Connection() != null)
            {
                for (int i = 0; i < marka.Connection().Count; i++)
                {
                    comboBox1.Items.Add(marka.Connection()[i]);
                }
            }
        }
        public Form1()
        {
            InitializeComponent();
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            GetMarka();
        }
        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            
        }
        private void ratingControl1_Click(object sender, EventArgs e)
        {
            label1.Text = ratingControl1.Rating1.ToString();
        }
    }
}
