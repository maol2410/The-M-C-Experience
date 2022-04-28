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
    public partial class FormAdmin_Remake_Sequal_Prequel : Form
    {
        MSScom mss;
        int mediaID;
        int choice;

        public FormAdmin_Remake_Sequal_Prequel(MSScom mss, int mediaID, int choice)
        {
            this.mss = mss;
            this.mediaID = mediaID;
            this.choice = choice;
            InitializeComponent();
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            int otherMediaID = TryParse(textBox1.Text);
            if (otherMediaID != -1) {
                if (choice == 1)
                {
                    if (!mss.MediaSequel(mediaID, otherMediaID))
                        MessageBox.Show(mss.Error);
                    else
                        MessageBox.Show("Success!");
                }
                else if (choice == 2)
                {
                    if (!mss.MediaSequel(otherMediaID, mediaID))
                        MessageBox.Show(mss.Error);
                    else
                        MessageBox.Show("Success!");
                }
                else if (choice == 3)
                {
                    if (!mss.MediaRemake(mediaID, otherMediaID))
                        MessageBox.Show(mss.Error);
                    else
                        MessageBox.Show("Success!");
                }
            }
        }

        private int TryParse(string number)
        {
            int intNumber;
            if (int.TryParse(number, out intNumber))
                return intNumber;
            return -1;
        }
    }
}
