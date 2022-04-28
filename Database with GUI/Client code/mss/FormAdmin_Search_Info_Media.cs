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
    public partial class FormAdmin_Search_Info_Media : Form
    {
        MSScom mss;
        int choice;
        public FormAdmin_Search_Info_Media(MSScom mss)
        {
            this.mss = mss;
            choice = 1;
            InitializeComponent();
            UpdateData();
        }

        // open Genre Form
        private void button2_Click(object sender, EventArgs e)
        {
            FormAdmin_Add_Genre admin_Add_Genre = new FormAdmin_Add_Genre(mss);
            admin_Add_Genre.ShowDialog();
        }
        private void UpdateData()
        {
            if (choice == 1)
            {
                mss.TableOfActors();
                dataGridView1.DataSource = mss.Table;
            }
            else if (choice == 2)
            {
                mss.TableOfDirectors();
                dataGridView1.DataSource = mss.Table;
            }
        }

        private void Button6_Click(object sender, EventArgs e)
        {
            if (choice == 1)
            {
                choice = 2;
                button3.Text = "Add Director";
                button4.Text = "Edit Director";
                button5.Text = "Remove Director";
                button6.Text = "Actor";
                label3.Text = "Add Director:";
            }
            else
            {
                choice = 1;
                button3.Text = "Add Actor";
                button4.Text = "Edit Actor";
                button5.Text = "Remove Actor";
                button6.Text = "Director";
                label3.Text = "Add Actor:";
            }
            UpdateData();
        }

        // add actor/director
        private void Button3_Click(object sender, EventArgs e)
        {
            if (mss.MakeActorOrDirector(textBox1.Text, textBox2.Text, textBox4.Text, textBox3.Text, choice))
                UpdateData();
            else
                MessageBox.Show(mss.Error);
        }

        // edit actor/director
        private void Button4_Click(object sender, EventArgs e)
        {
            int idNumber = SelectedID();
            if (idNumber != -1)
            {
                if (!mss.UpdateActorOrDirector(idNumber, textBox1.Text, textBox2.Text, textBox4.Text, textBox3.Text, choice))
                    MessageBox.Show(mss.Error);
                UpdateData();
            }
        }

        // remove actor/director
        private void Button5_Click(object sender, EventArgs e)
        {
            int idNumber = SelectedID();
            if (idNumber != -1)
            {
                mss.DeleteActorOrDirector(idNumber, choice);
                UpdateData();
            }
        }

        // search
        private void Button1_Click(object sender, EventArgs e)
        {

        }

        private int SelectedID()
        {
            int selectedRowCount = dataGridView1.SelectedRows.Count;
            if (selectedRowCount > 0)
            {
                if (choice == 1)
                    return (int)dataGridView1.SelectedRows[0].Cells["actorid"].Value;
                else if (choice == 2)
                    return (int)dataGridView1.SelectedRows[0].Cells["directorid"].Value;
            }
            return -1;
        }
    }
}
