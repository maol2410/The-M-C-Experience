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
    public partial class FormMovieStream : System.Windows.Forms.Form
    {
        private MSScom mss;
        private int customerID;
        private int profileID;
        private int mediaID;
        private int time;
        private int timeLimit;

        public FormMovieStream(MSScom mss, int customerID, int profileID, int mediaID)
        {
            InitializeComponent();
            this.mss = mss;
            this.mediaID = mediaID;
            this.customerID = customerID;
            this.profileID = profileID;
            time = 0;
            timeLimit = mss.GetMediaLength(mediaID);
            label3.Text = timeLimit + " min";
            label2.Text = mss.GetMediaTitle(mediaID);
            UpdateTime();
        }

        private void UpdateTime()
        {
            if (time < 10)
                label1.Text = "0:0" + time + ":00";
            else if (time < 60)
                label1.Text = "0:" + time + ":00";
            else
                label1.Text = (time / 60) + ":" + (time % 60) + ":00";
        }

        private void ChangeTime(int change)
        {
            time += change;
            if (time < 0)
                time = 0;
            else if (time > timeLimit)
                time = timeLimit;
            UpdateTime();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        // -5 min
        private void Button1_Click(object sender, EventArgs e)
        {
            ChangeTime(-5);
        }

        // +5 min
        private void Button2_Click(object sender, EventArgs e)
        {
            ChangeTime(5);
        }

        private void FormMovieStream_FormClosing(object sender, FormClosingEventArgs e)
        {
            int progressTime = time * 100 / timeLimit;
            mss.UpdateProfileWatchList(customerID, profileID, mediaID, progressTime);
        }
    }
}
