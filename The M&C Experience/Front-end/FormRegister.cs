using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace mss
{
    public partial class FormRegister : System.Windows.Forms.Form
    {
        MSScom mss;

        public FormRegister(MSScom mss)
        {
            this.mss = mss;
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {

            if(!mss.MakeCustomer(textBox7.Text, textBox8.Text, textBox1.Text, textBox2.Text, dateTimePicker1.Value, textBox3.Text, textBox6.Text, textBox5.Text, textBox4.Text))
                MessageBox.Show(mss.Error);
            else
            {
                MessageBox.Show("Success!");
                this.Close();
            }
        }
    }
}
