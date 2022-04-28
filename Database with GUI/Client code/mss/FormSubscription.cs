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

namespace mss
{
    public partial class FormSubscription : System.Windows.Forms.Form
    {
        MSScom mss;
        int customerID;
        public FormSubscription(MSScom mss, int customerID)
        {
            this.mss = mss;
            this.customerID = customerID;
            InitializeComponent();
        }

        private void subscriptioin_Load(object sender, EventArgs e)
        {

            // Load customer Subscription info
            object[] sub = mss.GetSubscriptionPlan(customerID);
            if (sub != null)
            {
                comboBox1.SelectedItem = (string)sub[0];
                textBox1.Text = (string)sub[1];
                dateTimePicker1.Value = (DateTime)sub[2];
                textBox2.Text = (string)sub[3];
                dateTimePicker2.Value = (DateTime)sub[4];
                label5.Text = (string)sub[5];
                label12.Text = sub[6].ToString();
                comboBox2.SelectedIndex = (int)sub[7] - 1;
            }
        }

        // add/update subscription info
        private void Button2_Click_1(object sender, EventArgs e)
        {
            if (!mss.UpdateSubscriptionPlan(customerID, (comboBox2.SelectedIndex + 1), dateTimePicker2.Value, textBox2.Text, textBox1.Text, dateTimePicker1.Value, comboBox1.SelectedItem.ToString()))
            {
                MessageBox.Show(mss.Error);
            }
            else
                MessageBox.Show("Success!");

        }

        // Delete Subscription
        private void Button3_Click_1(object sender, EventArgs e)
        {
            if (!mss.CancelSubscriptionplan(customerID))
            {
                MessageBox.Show(mss.Error);
            }
            else
                MessageBox.Show("Success!");
        }
    }
}
