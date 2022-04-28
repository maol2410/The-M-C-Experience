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
    public partial class FormProfile : System.Windows.Forms.Form
    {
        MSScom mss;
        private int customerID;
        private int profileID;
        private int previousWatchMediaID;

        public FormProfile(MSScom mss, int customerID, int profileID)
        {
            this.mss = mss;
            this.customerID = customerID;
            this.profileID = profileID;
            InitializeComponent();
            UpdateData();
        }

        public void UpdateData()
        {
            mss.TableOfWatchList(customerID, profileID);
            dgvWatchList.DataSource = mss.Table;

            mss.TableOfSeriesWatchList(customerID, profileID);
            dgvSeriesWatchList.DataSource = mss.Table;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            object[] prof = mss.GetProfile(customerID, profileID);
            userNameBox.Text = (string)prof[0];
            comboBox1.SelectedIndex = (int)prof[1] - 1;

            if (prof[2].GetType() == typeof(int))
            {
                previousWatchMediaID = (int)prof[2];
                previousWatchMedia.Text = mss.GetMediaTitle(previousWatchMediaID);
            }
            else
            {
                previousWatchMediaID = -1;
                previousWatchMedia.Text = "You have not watch anything!";
            }

            UpdateData();

        }

        private void searchForMovie_Click(object sender, EventArgs e)
        {
            var searchMovies = new FormSearchMovies(mss, customerID, profileID);

            searchMovies.Show();


        }

        private void searchForSeries_Click(object sender, EventArgs e)
        {
            var searchSeries = new FormSearchSeries(mss, customerID, profileID);

            searchSeries.Show();
        }



        private void previousWatchMedia_Click(object sender, EventArgs e)
        {
            if (previousWatchMediaID != -1)
            {
                this.Visible = false;
                FormMovieStream movieStreamForm = new FormMovieStream(mss, customerID, profileID, previousWatchMediaID);
                movieStreamForm.ShowDialog();
                this.Visible = true;
            }
        }



        private void ReturnToCustomer_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void updateSettings_Click(object sender, EventArgs e)
        {
            if (!mss.UpdateProfile(customerID, profileID, userNameBox.Text, comboBox1.SelectedIndex + 1))
            {
                MessageBox.Show(mss.Error);
            }
            else

                MessageBox.Show("Profile Updated");
        }

        private void Button1_Click_1(object sender, EventArgs e)
        {
            int rateMovie = (int)dgvWatchList.SelectedRows[0].Cells["mediaid"].Value;
            int rate = Int32.Parse(textBox1.Text);

            if (!mss.RateMedia(customerID, profileID, rateMovie, rate))
                MessageBox.Show(mss.Error);
            else
                MessageBox.Show("Rate Media was Successfully added");
            UpdateData();
        }

        private void Button2_Click_1(object sender, EventArgs e)
        {
            int rateSeries = (int)dgvSeriesWatchList.SelectedRows[0].Cells["seriesid"].Value;
            int rate = Int32.Parse(textBox2.Text);

            if (!mss.RateSeries(customerID, profileID, rateSeries, rate))
                MessageBox.Show(mss.Error);
            else
                MessageBox.Show("Rate Series was Successfully added");
            UpdateData();
        }
    }
}
