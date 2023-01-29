using System;
using System.Text;
using System.Windows.Forms;
using System.Security.Cryptography;

namespace TK_Projekt
{
    public partial class Login : Form
    {
        public Login()
        {
            InitializeComponent();
        }
        private void button1_Click(object sender, EventArgs e)
        {
            ActiveForm.Close();
        }
        private void Login_Load(object sender, EventArgs e)
        {
            textBox1.Width = (int)(Width * 0.6);
            textBox2.Width = (int)(Width * 0.6);

            textBox1.Left = Width / 2 - textBox1.Width / 2;
            textBox2.Left = Width / 2 - textBox2.Width / 2;
        }
        private void textBox2_MouseLeave(object sender, EventArgs e)
        {
            textBox2.UseSystemPasswordChar = true;
        }
        private void button2_Click(object sender, EventArgs e)
        {
            SHA1 sha = new SHA1CryptoServiceProvider();

            byte[] hashedData = sha.ComputeHash(Encoding.Unicode.GetBytes(textBox2.Text));
            StringBuilder stringBuilder= new StringBuilder();

            foreach (byte b in hashedData)
            {
                stringBuilder.Append(String.Format("{0,2:X2}", b));
            }

            SQLClassLogin login= new SQLClassLogin();
            string pass = login.Connection(textBox1.Text);

            if (pass != null)
            {
                Form1.Login = pass == stringBuilder.ToString() ? true : false;
                if (Form1.Login)
                {
                    ActiveForm.Close();
                }
                else
                {
                    MessageBox.Show("Wrong Password !");
                }
            }
            else
            {
                Form1.Login = false;
                MessageBox.Show("No such user !");
            }
            
        }
        private void textBox2_MouseClick(object sender, MouseEventArgs e)
        {
            textBox2.UseSystemPasswordChar = false;
        }
        private void textBox2_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                button2_Click(sender, e);
            }
        }
    }
}
