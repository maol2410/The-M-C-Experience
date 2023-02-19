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
    public partial class FormAdmin_Add_Search_Series : Form
    {
        MSScom mss;
        public FormAdmin_Add_Search_Series(MSScom mss)
        {
            this.mss = mss;
            InitializeComponent();
        }

        // edit episodes
        private void button3_Click(object sender, EventArgs e)
        {
            int seriesID = SelectedSeriesID();
            if (seriesID != -1)
            {
                FormAdmin_AddEpisode admin_AddEpisode = new FormAdmin_AddEpisode(mss, seriesID);
                admin_AddEpisode.ShowDialog();
            }
        }

        // search series
        private void Button2_Click(object sender, EventArgs e)
        {
            if (mss.TableOfSeries(textBox6.Text, textBox7.Text))
                dataGridView1.DataSource = mss.Table;
            else
                MessageBox.Show(mss.Error);
        }

        // add series
        private void Button1_Click(object sender, EventArgs e)
        {
            if (!mss.MakeSeries(textBox6.Text, textBox7.Text))
                MessageBox.Show(mss.Error);
        }

        // delete series
        private void Button4_Click(object sender, EventArgs e)
        {
            int seriesID = SelectedSeriesID();
            if (seriesID != -1)
            {
                mss.DeleteSeries(seriesID);
                dataGridView1.Rows.RemoveAt(dataGridView1.SelectedRows[0].Index);
            }
        }

        private int SelectedSeriesID()
        {
            int selectedRowCount = dataGridView1.SelectedRows.Count;
            if (selectedRowCount > 0)
                return (int)dataGridView1.SelectedRows[0].Cells["seriesid"].Value;
            return -1;
        }
    }
}
