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
    public partial class FormManageAccount : Form
    {
        MSScom mss;
        int customerID;
        public FormManageAccount(MSScom mss, int customerID)
        {
            this.mss = mss;
            this.customerID = customerID;
            InitializeComponent();
        }

        private void label10_Click(object sender, EventArgs e)
        {

        }

        // loadning info about customer
        private void FormManageAccount_Load_1(object sender, EventArgs e)
        {
            object[] cus = mss.GetCustomerInfo(customerID);
            textBox1.Text = (string)cus[0];
            textBox2.Text = (string)cus[1];
            dateTimePicker1.Value = (DateTime)cus[2];
            textBox3.Text = (string)cus[3];
            textBox4.Text = (string)cus[4];
            textBox5.Text = (string)cus[5];
            textBox6.Text = (string)cus[6];
            textBox7.Text = (string)cus[7];
            textBox8.Text = (string)cus[8];
        }

        private void Button1_Click_1(object sender, EventArgs e)
        {
            if (!mss.UpdateCustomer(customerID, textBox7.Text, textBox8.Text, textBox1.Text, textBox2.Text, dateTimePicker1.Value, textBox3.Text, textBox6.Text, textBox5.Text, textBox4.Text))
            {
                MessageBox.Show(mss.Error);
            }
            else

                MessageBox.Show("Updated");
        }

        private void Button2_Click_1(object sender, EventArgs e)
        {
            mss.DeleteCustomer(customerID);
            MessageBox.Show("Customer Deleted, closing program");
            Application.Exit();
        }
    }
}
