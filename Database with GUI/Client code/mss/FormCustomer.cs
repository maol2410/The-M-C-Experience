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
    public partial class FormCustomer : System.Windows.Forms.Form
    {
        private MSScom mss;
        private int customerID;
        private int profileID;

        public FormCustomer(MSScom mss, int customerID)
        {
            this.mss = mss;
            this.customerID = customerID;
            InitializeComponent();
            UpdateData();
        }

        public int ProfileID
        {
            get { return profileID; }

        }

        public void UpdateData()
        {
            mss.TableOfProfiles(customerID);
            dataGridView1.DataSource = mss.Table;
        }

        private void FormCustomer_Load(object sender, EventArgs e)
        {
            int selectedRowCount = dataGridView1.SelectedRows.Count;
            if (selectedRowCount > 0)
            {
                profileID = (int)dataGridView1.SelectedRows[0].Cells["profileid"].Value;
                // do something with profileID
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            int profileID = SelectedProfileID();
            if (SelectedProfileID() != -1)
            {
                this.DialogResult = DialogResult.OK;
                this.profileID = profileID;
                this.Close();
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            FormSubscription subscription = new FormSubscription(mss, customerID);
            subscription.ShowDialog();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            FormManageAccount manageAccount = new FormManageAccount(mss, customerID);
            manageAccount.ShowDialog();
        }

        private void tableLayoutPanel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void Button2_Click(object sender, EventArgs e)
        {
            int selectedRowCount = dataGridView1.SelectedRows.Count;
            if (selectedRowCount > 0)
            {
                profileID = (int)dataGridView1.SelectedRows[0].Cells["profileid"].Value;
                mss.DeleteProfile(customerID, profileID);
                UpdateData();
            }

        }

        private void Button1_Click_1(object sender, EventArgs e)
        {
            if (mss.MakeProfile(customerID))
            {
                UpdateData();
                MessageBox.Show("Profile made");

            }
            else
                MessageBox.Show(mss.Error);
        }

        private int SelectedProfileID()
        {
            int selectedRowCount = dataGridView1.SelectedRows.Count;
            if (selectedRowCount > 0)
                return (int)dataGridView1.SelectedRows[0].Cells["profileid"].Value;
            return -1;
        }
    }
}
