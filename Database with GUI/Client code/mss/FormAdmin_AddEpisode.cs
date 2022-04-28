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
    public partial class FormAdmin_AddEpisode : Form
    {
        private MSScom mss;
        private int seriesID;

        public FormAdmin_AddEpisode(MSScom mss, int seriesID)
        {
            this.mss = mss;
            this.seriesID = seriesID;
            InitializeComponent();
            UpdateData();
        }

        // add episode
        private void Button2_Click(object sender, EventArgs e)
        {
            if (!mss.MakeEpisode(seriesID, textBox1.Text, textBox2.Text, textBox3.Text))
                MessageBox.Show(mss.Error);
            else
                UpdateData();
        }

        // remove episode
        private void Button1_Click(object sender, EventArgs e)
        {
            int mediaID = SelectedEpisodeID();
            if (mediaID != -1)
            {
                mss.DeleteEpisode(mediaID);
                dataGridView1.Rows.RemoveAt(dataGridView1.SelectedRows[0].Index);
            }
        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void dataGridView2_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void tableLayoutPanel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void UpdateData()
        {
            mss.TableOfEpisodes(seriesID);
            dataGridView1.DataSource = mss.Table;
        }

        private int SelectedEpisodeID()
        {
            int selectedRowCount = dataGridView1.SelectedRows.Count;
            if (selectedRowCount > 0)
                return (int)dataGridView1.SelectedRows[0].Cells["mediaid"].Value;
            return -1;
        }
    }
}
