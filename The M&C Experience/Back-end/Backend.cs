using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Npgsql;

namespace mss
{
    class Backend
    {
        public void Run()
        {
            string cs = "Host="; // Add host url
            NpgsqlConnection con = new NpgsqlConnection(cs);
            try
            {
                con.Open();
            }
            catch (Exception)
            {
                MessageBox.Show("Failed to connect.");

                return;
            }

            MSScom mss = new MSScom(con);
            FormLogin login = new FormLogin(mss);
            login.ShowDialog();
            int userID = login.UserID;
            int customerID = mss.GetCustomer(userID);

            int adminID = mss.getAdministrator(userID);
            if (adminID > 0)
            {
                FormAdministrator adminForm = new FormAdministrator(mss, adminID);
                adminForm.ShowDialog();
                customerID = adminForm.CustomerID;
            }

            if (customerID > 0)
            {
                DialogResult dialogResult = DialogResult.OK;
                while (dialogResult == DialogResult.OK)
                {
                    FormCustomer customer = new FormCustomer(mss, customerID);
                    dialogResult = customer.ShowDialog();
                    if (dialogResult == DialogResult.OK)
                    {
                        int profileID = customer.ProfileID;
                        FormProfile profile = new FormProfile(mss, customerID, profileID);
                        dialogResult = profile.ShowDialog();
                    }

                }
            }
        }
    }
}
