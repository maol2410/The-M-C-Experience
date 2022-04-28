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
    public partial class FormAdmin_A_D_GInMovie : Form
    {
        private MSScom mss;
        private int mediaID;
        private int choice;

        public FormAdmin_A_D_GInMovie(MSScom mss, int mediaID, int choice)
        {
            this.mss = mss;
            this.mediaID = mediaID;
            this.choice = choice;
            InitializeComponent();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void dataGridView2_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void FormAdmin_A_D_GInMovie_Load(object sender, EventArgs e)
        {
            if (choice == 1)
            {
                mss.TableOfActors();
                dataGridView1.DataSource = mss.Table;
                label1.Text = "Add Actors:";
                label2.Text = "Edit / Remove Actors";
                this.Text = "Add/Remove Actor";
            }
            else if (choice == 2)
            {
                mss.TableOfDirectors();
                dataGridView1.DataSource = mss.Table;
                label1.Text = "Add Directors:";
                label2.Text = "Edit / Remove Directors";
                this.Text = "Add/Remove Directors";
            }
            else if (choice == 3)
            {
                mss.TableOfGenres();
                dataGridView1.DataSource = mss.Table;
                label1.Text = "Add Genres:";
                label2.Text = "Edit / Remove Genres";
                this.Text = "Add/Remove Genres";
            }

            UpdateDataView2();
        }

        private int SelectedID1()
        {
            int selectedRowCount = dataGridView1.SelectedRows.Count;
            if (selectedRowCount > 0)
            {
                if (choice == 1)
                    return (int)dataGridView1.SelectedRows[0].Cells["actorid"].Value;
                else if (choice == 2)
                    return (int)dataGridView1.SelectedRows[0].Cells["directorid"].Value;
                else if (choice == 3)
                    return (int)dataGridView1.SelectedRows[0].Cells["genreid"].Value;
            }
            return -1;
        }

        private int SelectedID2()
        {
            int selectedRowCount = dataGridView2.SelectedRows.Count;
            if (selectedRowCount > 0)
            {
                if (choice == 1)
                    return (int)dataGridView2.SelectedRows[0].Cells["actorid"].Value;
                else if (choice == 2)
                    return (int)dataGridView2.SelectedRows[0].Cells["directorid"].Value;
                else if (choice == 3)
                    return (int)dataGridView2.SelectedRows[0].Cells["genreid"].Value;
            }
            return -1;
        }

        // add to media
        private void DataGridView1_Click(object sender, EventArgs e)
        {
            int idNumber = SelectedID1();
            if (idNumber != -1)
            {
                mss.MediaAddADG(mediaID, idNumber, choice);
                UpdateDataView2();
            }
        }

        // remove from media
        private void DataGridView2_Click(object sender, EventArgs e)
        {
            int idNumber = SelectedID2();
            if (idNumber != -1)
            {
                mss.MediaRemoveADG(mediaID, idNumber, choice);
                UpdateDataView2();
            }
        }

        private void UpdateDataView2()
        {
            if (choice == 1)
            {
                mss.TableOfActorsInMedia(mediaID);
                dataGridView2.DataSource = mss.Table;
            }
            else if (choice == 2)
            {
                mss.TableOfDirectorsInMedia(mediaID);
                dataGridView2.DataSource = mss.Table;
            }
            else if (choice == 3)
            {
                mss.TableOfGenresInMedia(mediaID);
                dataGridView2.DataSource = mss.Table;
            }
        }
    }
}
