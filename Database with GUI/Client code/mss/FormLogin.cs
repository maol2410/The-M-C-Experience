using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Npgsql;

namespace mss
{
    public partial class FormLogin : System.Windows.Forms.Form
    {
        private MSScom mss;
        private int userID;

        public FormLogin(MSScom mss)
        {
            this.mss = mss;
            userID = -1;
            InitializeComponent();
        }

        public int UserID
        {
            get { return userID; }
        }

    private void Form1_Load(object sender, EventArgs e)
        {
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            int userID = mss.Login(txtEmail.Text, txtPassword.Text);
            if (userID > 0)
            {
                this.userID = userID;
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            this.Visible = false;
            FormRegister regiserForm = new FormRegister(mss);
            regiserForm.ShowDialog();
            this.Visible = true;
        }
    }
}
