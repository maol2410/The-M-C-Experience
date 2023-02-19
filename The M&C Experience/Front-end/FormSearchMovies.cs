using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Npgsql;

namespace mss
{
    public partial class FormSearchMovies : System.Windows.Forms.Form
    {

        MSScom mss;
        int customerID;
        int profileID;
        public FormSearchMovies(MSScom mss, int customerID, int profileID)
        {
            this.mss = mss;
            this.customerID = customerID;
            this.profileID = profileID;
            InitializeComponent();
            SelectedMediaID();
        }

        private void FormSearchMovies_Load(object sender, EventArgs e)
        {

        }

        private int SelectedMediaID()
        {
            int selectedRowCount = dataGridView1.SelectedRows.Count;
            if (selectedRowCount > 0)
                return (int)dataGridView1.SelectedRows[0].Cells["mediaid"].Value;
            return -1;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (mss.TableOfMovies(customerID, profileID, textBox6.Text, textBox5.Text, textBox1.Text, movieText.Text, textBox7.Text, textBox4.Text, textBox3.Text, textBox2.Text))
                dataGridView1.DataSource = mss.Table;
            else
                MessageBox.Show(mss.Error);
        }

        private void DataGridView1_Click(object sender, EventArgs e)
        {
            int mediaID = SelectedMediaID();
            if (mediaID != -1)
            {
                mss.TableOfGenresInMedia(mediaID);
                dataGridView2.DataSource = mss.Table;
                mss.TableOfActorsInMedia(mediaID);
                dataGridView3.DataSource = mss.Table;
                mss.TableOfDirectorsInMedia(mediaID);
                dataGridView4.DataSource = mss.Table;
            }

        }

        private void dataGridView2_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void DataGridView1_DoubleClick(object sender, EventArgs e)
        {

            int mediaID = SelectedMediaID();
            if (mediaID != -1)
            {
                this.Visible = false;
                FormMovieStream movieStreamForm = new FormMovieStream(mss, customerID, profileID, mediaID);
                movieStreamForm.ShowDialog();
                this.Visible = true;
            }
        }
    }
}
