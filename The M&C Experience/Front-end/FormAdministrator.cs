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
    public partial class FormAdministrator : Form
    {
        private MSScom mss;
        private int adminID;
        private int customerID;

        public FormAdministrator(MSScom mss, int adminID)
        {
            this.mss = mss;
            this.adminID = adminID;
            customerID = 0;

            InitializeComponent();

            object[] adminInfo = mss.GetAdminInfo(adminID);
            label2.Text = (string)adminInfo[0];
            comboBox1.SelectedIndex = (int)adminInfo[1]-1;
        }

        public int CustomerID
        {
            get { return customerID; }
        }

        // search for media files
        private void button1_Click(object sender, EventArgs e)
        {
            FormAdmin_EditMedia editMedia = new FormAdmin_EditMedia(mss);
            editMedia.ShowDialog();
        }

        // search for series
        private void button2_Click(object sender, EventArgs e)
        {
            FormAdmin_Add_Search_Series admin_Add_Search_Series = new FormAdmin_Add_Search_Series(mss);
            admin_Add_Search_Series.ShowDialog();
        }

        // search information about media
        private void button4_Click(object sender, EventArgs e)
        {
            FormAdmin_Search_Info_Media admin_Search_Info_Media = new FormAdmin_Search_Info_Media(mss);
            admin_Search_Info_Media.ShowDialog();
        }

        // search for users
        private void button3_Click(object sender, EventArgs e)
        {
            this.Hide();
            FormAdmin_SearchForUser admin_SearchForUser = new FormAdmin_SearchForUser(mss);
            admin_SearchForUser.ShowDialog();
            customerID=admin_SearchForUser.CustomerID;
            if (customerID > 0)
                this.Close();
            this.Visible = true;
        }

        private void FormAdministrator_Load(object sender, EventArgs e)
        {

        }
    }
}
