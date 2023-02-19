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
    public partial class FormSearchSeries : System.Windows.Forms.Form
    {

        MSScom mss;
        int customerID;
        int profileID;

        public FormSearchSeries(MSScom mss, int customerID, int profileID)
        {
            this.mss = mss;
            this.customerID = customerID;
            this.profileID = profileID;
            InitializeComponent();

        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void Button1_Click_1(object sender, EventArgs e)
        {

            mss.TableOfSeries(textBox1.Text, null);
            dgvSearchSeries.DataSource = mss.Table;

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
        private int SelectedSeriesID()
        {
            int selectedRowCount = dgvSearchSeries.SelectedRows.Count;
            if (selectedRowCount > 0)
                return (int)dgvSearchSeries.SelectedRows[0].Cells["seriesid"].Value;
            return -1;
        }

        private void Button2_Click_1(object sender, EventArgs e)
        {
            int episode = SelectedSeriesID();

            if (episode != -1)
                mss.TableOfEpisodes(episode);
            dataGridView1.DataSource = mss.Table;

        }
    }
}
