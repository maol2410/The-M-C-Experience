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
    public partial class FormAdmin_EditMedia : Form
    {
        MSScom mss;

        public FormAdmin_EditMedia(MSScom mss)
        {
            this.mss = mss;
            InitializeComponent();
        }

        // Actor
        private void button3_Click(object sender, EventArgs e)
        {
            int mediaID = SelectedMediaID();
            if (mediaID != -1)
            {
                FormAdmin_A_D_GInMovie admin_A_D_GInMovie = new FormAdmin_A_D_GInMovie(mss,mediaID,1);
                admin_A_D_GInMovie.ShowDialog();
            }
        }

        // Director
        private void button5_Click(object sender, EventArgs e)
        {
            int mediaID = SelectedMediaID();
            if (mediaID != -1)
            {
                FormAdmin_A_D_GInMovie admin_A_D_GInMovie = new FormAdmin_A_D_GInMovie(mss, mediaID, 2);
                admin_A_D_GInMovie.ShowDialog();
            }
        }

        // Genre
        private void button6_Click(object sender, EventArgs e)
        {
            int mediaID = SelectedMediaID();
            if (mediaID != -1)
            {
                FormAdmin_A_D_GInMovie admin_A_D_GInMovie = new FormAdmin_A_D_GInMovie(mss, mediaID, 3);
                admin_A_D_GInMovie.ShowDialog();
            }
        }

        private void FormAdmin_EditMedia_Load(object sender, EventArgs e)
        {

        }


        // sequel
        private void button10_Click(object sender, EventArgs e)
        {
            int mediaID = SelectedMediaID();
            if (mediaID != -1)
            {
                FormAdmin_Remake_Sequal_Prequel admin_Remake_Sequal_Prequel = new FormAdmin_Remake_Sequal_Prequel(mss, mediaID, 1);
                admin_Remake_Sequal_Prequel.ShowDialog();
            }
        }

        // prequel
        private void button8_Click(object sender, EventArgs e)
        {
            int mediaID = SelectedMediaID();
            if (mediaID != -1)
            {
                FormAdmin_Remake_Sequal_Prequel admin_Remake_Sequal_Prequel = new FormAdmin_Remake_Sequal_Prequel(mss, mediaID, 2);
                admin_Remake_Sequal_Prequel.ShowDialog();
            }
        }

        // remake
        private void button9_Click(object sender, EventArgs e)
        {
            int mediaID = SelectedMediaID();
            if (mediaID != -1)
            {
                FormAdmin_Remake_Sequal_Prequel admin_Remake_Sequal_Prequel = new FormAdmin_Remake_Sequal_Prequel(mss, mediaID, 3);
                admin_Remake_Sequal_Prequel.ShowDialog();
            }
        }

        // search for media
        private void Button7_Click(object sender, EventArgs e)
        {
            if (mss.TableOfMovies(textBox1.Text, textBox2.Text, textBox3.Text, textBox4.Text, textBox5.Text, textBox6.Text, textBox7.Text))
                dataGridView1.DataSource = mss.Table;
            else
                MessageBox.Show(mss.Error);
        }

        private void DataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        // delete a media
        private void Button4_Click(object sender, EventArgs e)
        {
            int mediaID = SelectedMediaID();
            if (mediaID != -1)
            {
                mss.DeleteMedia(mediaID);
                dataGridView1.Rows.RemoveAt(dataGridView1.SelectedRows[0].Index);
            }
        }

        // update a media
        private void Button2_Click(object sender, EventArgs e)
        {
            int mediaID = SelectedMediaID();
            if (mediaID!=-1)
            {
                if (!mss.UpdateMedia(mediaID, textBox1.Text, textBox2.Text, textBox3.Text, textBox4.Text, textBox6.Text))
                    MessageBox.Show(mss.Error);
            }
        }

        // add a media
        private void Button1_Click(object sender, EventArgs e)
        {
            if (!mss.MakeMedia(textBox1.Text, textBox2.Text, textBox3.Text, textBox4.Text, textBox6.Text))
                MessageBox.Show(mss.Error);
        }

        private int SelectedMediaID()
        {
            int selectedRowCount = dataGridView1.SelectedRows.Count;
            if (selectedRowCount > 0)
                return (int)dataGridView1.SelectedRows[0].Cells["mediaid"].Value;
            return -1;
        }
    }
}
