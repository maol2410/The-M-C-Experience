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
    public partial class FormAdmin_SearchForUser : Form
    {
        private MSScom mss;
        private int customerID;

        public FormAdmin_SearchForUser(MSScom mss)
        {
            this.mss = mss;
            customerID = 0;
            InitializeComponent();
        }

        public int CustomerID
        {
            get { return customerID; }
        }

        private int SelectedID()
        {
            int selectedRowCount = dataGridView1.SelectedRows.Count;
            if (selectedRowCount > 0)
                return (int)dataGridView1.SelectedRows[0].Cells["customerid"].Value;
            return -1;
        }

        // edit customer
        private void button2_Click(object sender, EventArgs e)
        {
            int customerID = SelectedID();
            if (customerID != -1)
            {
                this.customerID = customerID;
                this.Close();
            }
        }

        // search customer
        private void Button1_Click(object sender, EventArgs e)
        {
            if (!mss.TableOfCustomers(textBox1.Text, textBox2.Text, textBox3.Text))
                MessageBox.Show(mss.Error);
            else
                dataGridView1.DataSource = mss.Table;

        }

        // add discount
        private void Button3_Click(object sender, EventArgs e)
        {
            int customerID = SelectedID();
            if (customerID != -1)
            {
                if (mss.UpdateCustomerDiscount(customerID, textBox4.Text))
                    MessageBox.Show("You gave " + textBox4.Text + "% discount.");
                else
                    MessageBox.Show(mss.Error);
            }
            
        }
    }
}
