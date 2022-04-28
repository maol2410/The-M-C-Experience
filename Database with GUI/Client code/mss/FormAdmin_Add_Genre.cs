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
    public partial class FormAdmin_Add_Genre : Form
    {
        MSScom mss;

        public FormAdmin_Add_Genre(MSScom mss)
        {
            this.mss = mss;
            InitializeComponent();
            UpdateData();
        }

        private void UpdateData()
        {
            mss.TableOfGenres();
            dataGridView1.DataSource = mss.Table;
        }

        private int SelectedID()
        {
            int selectedRowCount = dataGridView1.SelectedRows.Count;
            if (selectedRowCount > 0)
                return (int)dataGridView1.SelectedRows[0].Cells["genreid"].Value;
            return -1;
        }

        // add genre
        private void Button1_Click(object sender, EventArgs e)
        {
            if (mss.MakeGenre(textBox1.Text))
                UpdateData();
            else
                MessageBox.Show(mss.Error);
        }

        // edit genre
        private void Button2_Click(object sender, EventArgs e)
        {
            int genreID = SelectedID();
            if (genreID != -1)
            {
                if (mss.UpdateGenre(genreID, textBox1.Text))
                    UpdateData();
                else
                    MessageBox.Show(mss.Error);
            }
        }

        // remove genre
        private void Button3_Click(object sender, EventArgs e)
        {
            int genreID = SelectedID();
            if (genreID != -1)
            {
                mss.DeleteGenre(genreID);
                UpdateData();
            }
        }
    }
}
