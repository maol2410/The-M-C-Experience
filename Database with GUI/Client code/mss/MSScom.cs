using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Npgsql;

namespace mss
{
    public class MSScom
    {
        private string privateErrorMessage;
        private string publicErrorMessage;
        private string errorCode;
        private DataTable privateTable;
        private DataTable publicTable;
        private NpgsqlConnection con;
        private NpgsqlCommand cmd;

        #region Constructor & Properties
        public MSScom(NpgsqlConnection connection)
        {
            con = connection;
            cmd = new NpgsqlCommand();
            cmd.Connection = con;
            privateErrorMessage = null;
            publicErrorMessage = null;
            errorCode = null;
            privateTable = new DataTable();
            publicTable = null;
        }

        public string Error
        {
            get { return publicErrorMessage; }
        }

        public DataTable Table
        {
            get { return publicTable; }
        }
        #endregion

        #region Basic Functions
        private bool Query(string sqlQuery)
        {
            Debug.WriteLine("1 Query: " + sqlQuery);
            try
            {
                cmd.CommandText = sqlQuery;
                privateTable = new DataTable();
                privateTable.Load(cmd.ExecuteReader());
                return true;
            }
            catch (Exception err)
            {
                Debug.WriteLine("1 ------ERROR: " + err.Message);
                privateErrorMessage = err.Message;
                if (err.GetType() == typeof(PostgresException))
                    errorCode = ((PostgresException)err).SqlState;
                return false;
            }
        }

        private bool NonQuery(string sqlInstruction)
        {
            Debug.WriteLine("2 NonQuery: " + sqlInstruction);
            try
            {
                cmd.CommandText = sqlInstruction;
                cmd.ExecuteNonQuery();
                return true;
            }
            catch (Exception err)
            {
                Debug.WriteLine("2 ------ERROR: " + err.Message);
                privateErrorMessage = err.Message;
                if (err.GetType() == typeof(PostgresException))
                    errorCode = ((PostgresException)err).SqlState;
                return false;
            }
        }

        private int GetScalarInteger(string sqlQuery)
        {
            Debug.WriteLine("3 GetScalarInteger: " + sqlQuery);
            try
            {
                cmd.CommandText = sqlQuery;
                object cell = cmd.ExecuteScalar();
                if (cell == null || cell.GetType() == typeof(DBNull))
                    return -1;
                if (cell.GetType() == typeof(int))
                    return (int)cell;
                if (cell.GetType() == typeof(Int64))
                    return Convert.ToInt32(cell);
            }
            catch (Exception err)
            {
                Debug.WriteLine("3 -----ERROR: " + err.Message);
                privateErrorMessage = err.Message;
            }
            return int.MinValue;
        }

        private string GetScalarString(string sqlQuery)
        {
            Debug.WriteLine("4 GetScalarString: " + sqlQuery);
            try
            {
                cmd.CommandText = sqlQuery;
                object cell = cmd.ExecuteScalar();
                if (cell == null || cell.GetType() == typeof(DBNull))
                    return "NULL";
                else
                    return cell.ToString();
            }
            catch (Exception err)
            {
                Debug.WriteLine("4 -----ERROR: " + err.Message);
                privateErrorMessage = err.Message;
            }
            return string.Empty;
        }

        private int GetNextTableID(string tableName)
        {
            string query = "SELECT MAX(" + IDstring(tableName) + ") FROM " + tableName + ";";
            int scalar = GetScalarInteger(query);
            if (scalar == -1)
                return 1;
            else if (scalar > -1)
                return scalar + 1;
            else
                return -1;
        }

        private bool InsertInto(string schema, string values)
        {
            string sqlInstruction = "INSERT INTO " + schema + " VALUES" + values + ";";
            return NonQuery(sqlInstruction);
        }

        private bool UpdateTuple(string tableName, string set, string where)
        {
            string sqlInstruction = "UPDATE " + tableName + " SET " + set + " WHERE " + where + ";";
            return NonQuery(sqlInstruction);
        }

        private bool DeleteTuple(string tableName, int idNumber)
        {
            string sqlInstruction = "DELETE FROM " + tableName + " WHERE " + IDstring(tableName) + "=" + idNumber.ToString() + ";";
            return NonQuery(sqlInstruction);
        }

        private bool DeleteDiamondTuples(string diamondName, string idName, int idNumber)
        {
            string sqlInstruction = "DELETE FROM " + diamondName + " WHERE " + idName + "=" + idNumber.ToString() + ";";
            return NonQuery(sqlInstruction);
        }

        private string IDstring(string tableName)
        {
            tableName = tableName.ToLower();
            switch (tableName)
            {
                case "administrator":
                    return "adminid";
                case "episode":
                    return "mediaid";
                case "users":
                    return "userid";
                case "subscriptionplan":
                    return "subscriptionID";
                default:
                    return tableName + "id";
            }
        }

        private string[] NameSplitter(string name)
        {
            int i = 0;
            while (i < name.Length && name[i] != ' ') { }
            if (i == 0 || i + 1 >= name.Length)
                return null;

            string[] names = { name.Substring(0, i), name.Substring(i + 1, name.Length - i - 1) };
            return names;

        }
        #endregion

        #region Customer
        public bool MakeCustomer(string email, string password, string firstName, string lastName, DateTime dateOfBirth, string phoneNumber, string country, string city, string street)
        {
            // get next available id numbers
            int loginID = GetNextTableID("login");
            if (loginID == -1)
            {
                publicErrorMessage = "Something is wrong with the Login table!";
                return false;
            }
            int nameID = GetNextTableID("name");
            if (nameID == -1)
            {
                publicErrorMessage = "Something is wrong with the Name table!";
                return false;
            }
            int userID = GetNextTableID("users");
            if (userID == -1)
            {
                publicErrorMessage = "Something is wrong with the Users table!";
                return false;
            }
            int addressID = GetNextTableID("address");
            if (addressID == -1)
            {
                publicErrorMessage = "Something is wrong with the Address table!";
                return false;
            }
            int customerID = GetNextTableID("customer");
            if (customerID == -1)
            {
                publicErrorMessage = "Something is wrong with the Customer table!";
                return false;
            }

            // make Login tuple
            string schema = "login(loginid, email, password)";
            string values = "(" + loginID.ToString() + ", '" + email + "', '" + password + "')";
            if (!InsertInto(schema, values))
            {
                switch (errorCode)
                {
                    case "23514":
                        publicErrorMessage = "Password too short!";
                        break;
                    case "22001":
                        publicErrorMessage = "Password too long!";
                        break;
                    case "23505":
                        publicErrorMessage = "The email address is already in use! Try another.";
                        break;
                    default:
                        publicErrorMessage = privateErrorMessage;
                        break;
                }
                return false;
            }

            // make Name tuple
            schema = "name(nameid, firstname, lastname)";
            values = "(" + nameID.ToString() + ", '" + firstName + "', '" + lastName + "')";
            if (!InsertInto(schema, values))
            {
                DeleteTuple("login", loginID);
                publicErrorMessage = privateErrorMessage;
                return false;
            }

            // make Users tuple
            schema = "users(userid, dateofbirth, phonenumber)";
            values = "(" + userID.ToString() + ", '" + dateOfBirth.ToString("yyyy'-'MM'-'dd") + "', '" + phoneNumber + "')";
            if (!InsertInto(schema, values))
            {
                DeleteTuple("login", loginID);
                DeleteTuple("name", nameID);
                publicErrorMessage = privateErrorMessage;
                return false;
            }

            // make Address tuple
            schema = "address(addressid, country, city, street)";
            values = "(" + addressID.ToString() + ", '" + country + "', '" + city + "', '" + street + "')";
            if (!InsertInto(schema, values))
            {
                DeleteTuple("login", loginID);
                DeleteTuple("name", nameID);
                DeleteTuple("users", userID);
                publicErrorMessage = privateErrorMessage;
                return false;
            }

            // make Customer tuple
            schema = "customer(customerid, discountpercentage)";
            values = "(" + customerID.ToString() + ", 0)";
            if (!InsertInto(schema, values))
            {
                DeleteTuple("login", loginID);
                DeleteTuple("name", nameID);
                DeleteTuple("users", userID);
                DeleteTuple("address", addressID);
                publicErrorMessage = privateErrorMessage;
                return false;
            }

            // make Diamonds
            InsertInto("username(userid,nameid)", "(" + userID + "," + nameID + ")");
            InsertInto("logininuser(userid,loginid)", "(" + userID + "," + loginID + ")");
            InsertInto("addressinuser(userid,addressid)", "(" + userID + "," + addressID + ")");
            InsertInto("customeruser(userid,customerid)", "(" + userID + "," + customerID + ")");

            return true;
        }

        public bool UpdateCustomer(int customerID, string email, string password, string firstName, string lastName, DateTime dateOfBirth, string phoneNumber, string country, string city, string street)
        {
            string sqlQuery = "SELECT * FROM(SELECT * FROM customeruser WHERE customerid = " + customerID + ") AS temp NATURAL JOIN username NATURAL JOIN addressinuser NATURAL JOIN logininuser;";
            Query(sqlQuery);
            if (privateTable.Rows.Count != 1)
            {
                publicErrorMessage = "Customer Error!";
                return false;
            }
            DataRow row = privateTable.Rows[0];
            int userID = (int)row["userid"];
            int loginID = (int)row["loginid"];
            int nameID = (int)row["nameid"];
            int addressID = (int)row["addressid"];

            string set = $"dateofbirth='{dateOfBirth.ToString("yyyy'-'MM'-'dd")}', phoneNumber='{phoneNumber}'";
            string where = "userid=" + userID;
            if (UpdateTuple("users", set, where))
            {
                set = $"email='{email}', password='{password}'";
                where = "loginid=" + loginID;
                if (UpdateTuple("login", set, where))
                {
                    set = $"firstname='{firstName}', lastname='{lastName}'";
                    where = "nameid=" + nameID;
                    if (UpdateTuple("name", set, where))
                    {
                        set = $"country='{country}', city='{city}', street='{street}'";
                        where = "addressid=" + addressID;
                        if (UpdateTuple("address", set, where))
                        {
                            return true;
                        }
                    }
                }
            }
            publicErrorMessage = privateErrorMessage;
            return false;
        }

        public void DeleteCustomer(int customerID)
        {
            DeleteSubscriptionPlan(customerID);
            DeleteProfile(customerID, 1);
            DeleteProfile(customerID, 2);
            DeleteProfile(customerID, 3);

            string sqlQuery = $"SELECT userid FROM CustomerUser WHERE customerid={customerID};";
            int userID = GetScalarInteger(sqlQuery);
            sqlQuery = $"SELECT loginid FROM logininuser WHERE userid={userID};";
            int loginID = GetScalarInteger(sqlQuery);
            sqlQuery = $"SELECT nameid FROM UserName WHERE userid={userID};";
            int nameID = GetScalarInteger(sqlQuery);
            sqlQuery = $"SELECT addressid FROM AddressInUser WHERE userid={userID};";
            int addressID = GetScalarInteger(sqlQuery);

            DeleteDiamondTuples("CustomerUser", "userid", userID);
            DeleteDiamondTuples("logininuser", "userid", userID);
            DeleteDiamondTuples("UserName", "userid", userID);
            DeleteDiamondTuples("AddressInUser", "userid", userID);

            DeleteTuple("Customer", customerID);
            DeleteTuple("User", userID);
            DeleteTuple("Login", loginID);
            DeleteTuple("Name", nameID);
            DeleteTuple("Address", addressID);
        }

        public bool UpdateCustomerDiscount(int customerID, string discountPercentage)
        {
            string set = "discountpercentage=" + discountPercentage;
            string where = "customerid=" + customerID;
            if (UpdateTuple("customer", set, where))
                return true;
            publicErrorMessage = privateErrorMessage;
            return false;
        }

        public object[] GetCustomerInfo(int customerID)
        {
            string query = $"SELECT firstName,lastName,dateOfBirth,phoneNumber,street,city,country,email,password FROM(SELECT userID, customerID FROM CustomerUser  WHERE customerID={customerID}) AS temp NATURAL JOIN Users NATURAL JOIN UserName NATURAL JOIN Name NATURAL JOIN AddressInUser NATURAL JOIN Address NATURAL JOIN LoginInUser NATURAL JOIN Login;";
            if (Query(query) && privateTable.Rows.Count == 1)
            {
                DataRow row = privateTable.Rows[0];
                object[] customerInfo = row.ItemArray;
                return customerInfo;
            }
            return null;
        }
        #endregion

        #region Profile
        public bool MakeProfile(int customerID)
        {
            // create a table of all the subscription plans for the customer
            string sqlQuery = "SELECT subscriptiontypeid, startdate, expirationdate FROM customer NATURAL JOIN subscriptionincustomer NATURAL JOIN subscriptionplan NATURAL JOIN subscriptiontypeinplan NATURAL JOIN subscriptiontype WHERE customerID = " + customerID + ";";
            if (!Query(sqlQuery))
            {
                publicErrorMessage = privateErrorMessage;
                return false;
            }

            // find what subscription type is active and how many profiles the customer can have right now 
            DateTime now = DateTime.Now.Date;
            int profileLimit = 0;
            foreach (DataRow row in privateTable.Rows)
            {
                bool started = now >= (DateTime)row["startdate"];
                bool notExpired = row["expirationdate"].GetType() == typeof(DBNull) || now <= (DateTime)row["expirationdate"];
                if (started && notExpired && profileLimit < (int)row["subscriptiontypeid"])
                    profileLimit = (int)row["subscriptiontypeid"];
            }

            // find how many profiles the customer has
            sqlQuery = "SELECT COUNT(*) FROM profile WHERE customerid = " + customerID + ";";
            int profileCount = GetScalarInteger(sqlQuery);
            if (profileCount < 0)
            {
                publicErrorMessage = "Error when creating profile!" + profileCount;
                return false;
            }
            else if (profileCount >= profileLimit)
            {
                publicErrorMessage = "You cannot create more profiles!";
                return false;
            }

            // create a profile
            for (int i = 1; i <= 3; i++)
            {
                sqlQuery = "SELECT COUNT(*) FROM profile WHERE customerid = " + customerID + " AND profileid = " + i + ";";
                if (GetScalarInteger(sqlQuery) == 0)
                {
                    string schema = "profile(customerid,profileid,username,previouswatchmedia,profiletype)";
                    string values = "(" + customerID + ", " + i + ", 'profile" + i + "', NULL, 3)";
                    return InsertInto(schema, values);
                }
            }

            return false;
        }

        public bool UpdateProfile(int customerID, int profileID, string userName, int profileType)
        {
            string sqlInstruction = $"UPDATE profile SET username='{userName}', profiletype={profileType} WHERE customerid={customerID} AND profileid={profileID};";
            if (NonQuery(sqlInstruction))
                return true;
            publicErrorMessage = privateErrorMessage;
            return false;
        }

        public void DeleteProfile(int customerID, int profileID)
        {
            string sqlInstruction = $"DELETE FROM watchlist WHERE customerid={customerID} AND profileid={profileID};";
            NonQuery(sqlInstruction);
            sqlInstruction = $"DELETE FROM serieswatchlist WHERE customerid={customerID} AND profileid={profileID};";
            NonQuery(sqlInstruction);
            sqlInstruction = $"DELETE FROM profile WHERE customerid={customerID} AND profileid={profileID};";
            NonQuery(sqlInstruction);
            UpdateMediaRating();
            UpdateSeriesRating();
        }

        public bool UpdateProfileWatchList(int customerID, int profileID, int mediaID, int progressPercentage)
        {
            // find if the media is also an episode
            string sqlQuery = "SELECT seriesid FROM episode WHERE mediaid=" + mediaID + ";";
            int seriesID = GetScalarInteger(sqlQuery);

            sqlQuery = "SELECT * FROM watchlist WHERE customerid=" + customerID + " AND profileid=" + profileID + " AND mediaid=" + mediaID + ";";
            if (Query(sqlQuery))
            {
                // updating SeriesWatchList if this media is an episode in a series
                if (seriesID > 0)
                    UpdateProfileSeriesWatchList(customerID, profileID, seriesID, mediaID);

                // update latest watched movie
                string set = $"previousWatchMedia={mediaID}";
                string where = $"customerid={customerID} AND profileid={profileID}";
                UpdateTuple("profile", set, where);

                // make or update watchlist tuple
                if (privateTable.Rows.Count == 0)
                {
                    string schema = "watchlist(customerid,profileid,mediaid,progresstime,rating)";
                    string values = "(" + customerID + ", " + profileID + ", " + mediaID + ", " + progressPercentage + ", NULL)";
                    if (InsertInto(schema, values))
                        return true;
                }
                else
                {
                    set = "progresstime=" + progressPercentage;
                    where = "customerid=" + customerID + " AND profileid=" + profileID + " AND mediaid=" + mediaID;
                    if (UpdateTuple("watchlist", set, where))
                        return true;
                }
            }
            publicErrorMessage = privateErrorMessage;
            return false;
        }

        public bool UpdateProfileSeriesWatchList(int customerID, int profileID, int seriesID, int previousWatchMedia)
        {
            string sqlQuery = "SELECT * FROM serieswatchlist WHERE customerid=" + customerID + " AND profileid=" + profileID + " AND seriesid=" + seriesID + ";";
            if (Query(sqlQuery))
            {
                if (privateTable.Rows.Count == 0)
                {
                    string schema = "serieswatchlist(customerid,profileid,seriesid,previouswatchmedia,rating)";
                    string values = "(" + customerID + ", " + profileID + ", " + seriesID + ", " + previousWatchMedia + ", NULL)";
                    if (InsertInto(schema, values))
                        return true;
                }
                else
                {
                    string set = "previouswatchmedia=" + previousWatchMedia;
                    string where = "customerid=" + customerID + " AND profileid=" + profileID + " AND seriesid=" + seriesID;
                    if (UpdateTuple("serieswatchlist", set, where))
                        return true;
                }
            }
            publicErrorMessage = privateErrorMessage;
            return false;
        }

        public bool RateMedia(int customerID, int profileID, int mediaID, int rating)
        {
            string sqlQuery = "SELECT * FROM watchlist WHERE customerid=" + customerID + " AND profileid=" + profileID + " AND mediaid=" + mediaID + ";";
            if (Query(sqlQuery))
            {
                if (privateTable.Rows.Count == 1)
                {
                    string set = "rating=" + rating;
                    string where = "customerid=" + customerID + " AND profileid=" + profileID + " AND mediaid=" + mediaID;
                    if (UpdateTuple("watchlist", set, where))
                    {
                        UpdateMediaRating(mediaID);
                        return true;
                    }
                }
                else
                {
                    publicErrorMessage = "You need to watch the Media before you rate it!";
                    return false;
                }
            }
            publicErrorMessage = privateErrorMessage;
            return false;
        }

        public bool RateSeries(int customerID, int profileID, int seriesID, int rating)
        {
            string sqlQuery = "SELECT * FROM serieswatchlist WHERE customerid=" + customerID + " AND profileid=" + profileID + " AND seriesid=" + seriesID + ";";
            if (Query(sqlQuery))
            {
                if (privateTable.Rows.Count == 1)
                {
                    string set = "rating=" + rating;
                    string where = "customerid=" + customerID + " AND profileid=" + profileID + " AND seriesid=" + seriesID;
                    if (UpdateTuple("serieswatchlist", set, where))
                    {
                        UpdateSeriesRating(seriesID);
                        return true;
                    }
                }
                else
                {
                    publicErrorMessage = "You need to watch the Media before you rate it!";
                    return false;
                }
            }
            publicErrorMessage = privateErrorMessage;
            return false;
        }

        public int ProfileAge(int customerID, int profileID)
        {
            string sqlQuery = $"SELECT age FROM (SELECT profileType as profileTypeID FROM profile WHERE customerid={customerID} AND profileid={profileID}) AS temp NATURAL JOIN ProfileType;";
            return GetScalarInteger(sqlQuery);
        }

        public object[] GetProfile(int customerID, int ProfileID)
        {
            string query = $"SELECT username, profileType, previousWatchMedia FROM Profile WHERE customerID={customerID} AND profileID={ProfileID};";
            if (Query(query) && privateTable.Rows.Count == 1)
            {
                DataRow row = privateTable.Rows[0];
                object[] profile = row.ItemArray;
                return profile;
            }

            return null;
        }
        #endregion

        #region Subscription Plan
        public bool UpdateSubscriptionPlan(int customerID, int subscriptionType, DateTime startDate, string cardOwner, string ccn, DateTime expirationDate, string cardType)
        {
            // get next available id numbers
            int creditCardID = GetNextTableID("creditcard");
            if (creditCardID == -1)
            {
                publicErrorMessage = "Something is wrong with the CreditCard table!";
                return false;
            }
            int subscriptionID = GetNextTableID("subscriptionplan");
            if (subscriptionID == -1)
            {
                publicErrorMessage = "Something is wrong with the SubscriptionPlan table!";
                return false;
            }

            // make CreditCard tuple
            string schema = "creditcard(creditcardid,cardtype,ccn,expirationdate,cardowner)";
            string values = $"({creditCardID}, '{cardType}', '{ccn}', '{expirationDate.ToString("yyyy'-'MM'-'dd")}', '{cardOwner}')";
            if (!InsertInto(schema, values))
            {
                publicErrorMessage = privateErrorMessage;
                return false;
            }

            // make SubscriptionPlan tuple
            string monthlyFee = MonthlyFeeString(customerID, subscriptionType);
            schema = "subscriptionplan(subscriptionID,startDate,expirationDate,paymentAmount)";
            values = $"({subscriptionID}, '{startDate.ToString("yyyy'-'MM'-'dd")}', NULL, {monthlyFee})";
            if (!InsertInto(schema, values))
            {
                DeleteTuple("creditcard", creditCardID);
                publicErrorMessage = privateErrorMessage;
                return false;
            }

            // delete old subscription plan
            DeleteSubscriptionPlan(customerID);

            // make Diamonds
            InsertInto("CreditCardInSubscription(subscriptionid,creditcardid)", "(" + subscriptionID + "," + creditCardID + ")");
            InsertInto("SubscriptionInCustomer(subscriptionid,customerid)", "(" + subscriptionID + "," + customerID + ")");
            InsertInto("SubscriptionTypeInPlan(subscriptionid,subscriptiontypeid)", "(" + subscriptionID + "," + subscriptionType + ")");

            return true;
        }

        public bool CancelSubscriptionplan(int customerID)
        {
            DateTime expirationDate = DateTime.Now.AddDays(-1);
            int subscriptionID = GetScalarInteger($"SELECT subscriptionID FROM SubscriptionInCustomer WHERE customerID={customerID};");
            UpdateTuple("SubscriptionPlan", $"expirationdate='{expirationDate.ToString("yyyy'-'MM'-'dd")}'", $"subscriptionID={subscriptionID}");

            return true;
        }

        public void DeleteSubscriptionPlan(int customerID)
        {
            string select = "SELECT subscriptionid, creditcardid";
            if (GenerateTableSubPlan(select, customerID) && privateTable.Rows.Count == 1)
            {
                DataRow row = privateTable.Rows[0];
                int subscriptionID = (int)row.ItemArray[0];
                int creditCardID = (int)row.ItemArray[1];
                DeleteDiamondTuples("subscriptiontypeinplan", "subscriptionid", subscriptionID);
                DeleteDiamondTuples("creditcardinsubscription", "subscriptionid", subscriptionID);
                DeleteDiamondTuples("subscriptionincustomer", "subscriptionid", subscriptionID);
                DeleteTuple("subscriptionplan", subscriptionID);
                DeleteTuple("creditcard", creditCardID);
            }
        }

        public object[] GetSubscriptionPlan(int customerID)
        {
            string select = "SELECT cardtype, ccn, expirationdate, cardowner, startdate, expirationdate2, paymentamount, subscriptiontypeid";
            if (GenerateTableSubPlan(select, customerID) && privateTable.Rows.Count == 1)
            {
                DataRow row = privateTable.Rows[0];
                object[] subscriptionPlanInfo = row.ItemArray;
                if (subscriptionPlanInfo[5].GetType() == typeof(DateTime))
                    subscriptionPlanInfo[5] = ((DateTime)subscriptionPlanInfo[5]).ToString("yyyy'-'MM'-'dd");
                else
                    subscriptionPlanInfo[5] = "Ongoing";
                return subscriptionPlanInfo;
            }

            return null;
        }

        private bool GenerateTableSubPlan(string select, int customerID)
        {
            string sqlQuery = select + " FROM(SELECT * FROM subscriptionincustomer WHERE customerid = " +
                customerID +
                ") AS temp NATURAL JOIN(SELECT subscriptionid, startdate, expirationdate AS expirationdate2, paymentamount FROM subscriptionplan) AS temp2 NATURAL JOIN creditcardinsubscription NATURAL JOIN subscriptiontypeinplan NATURAL JOIN creditcard NATURAL JOIN subscriptiontype;";
            return Query(sqlQuery);
        }

        public string MonthlyFeeString(int customerID, int subscriptionType)
        {
            return "49.99";
        }
        #endregion

        #region Media
        public bool MakeMedia(string title, string year, string country, string length, string age)
        {
            int mediaID = GetNextTableID("media");
            string schema = $"media(mediaid,title,year,country,length,pg_age,rating)";
            string values = $"({mediaID},'{title}',{year},'{country}',{length},{age},NULL)";
            if (!InsertInto(schema, values))
            {
                publicErrorMessage = privateErrorMessage;
                return false;
            }
            return true;
        }

        public bool UpdateMedia(int mediaID, string title, string year, string country, string length, string age)
        {
            StringBuilder set = new StringBuilder();
            bool firstSet = true;
            if (title != null && title != string.Empty)
            {
                set.Append($"title='{title}'");
                firstSet = false;
            }
            if (year != null && year != string.Empty)
            {
                if (!firstSet)
                {
                    set.Append(", ");
                    firstSet = false;
                }
                set.Append("year=" + year);
            }
            if (country != null && country != string.Empty)
            {
                if (!firstSet)
                {
                    set.Append(", ");
                    firstSet = false;
                }
                set.Append($"country='{country}'");
            }
            if (length != null && length != string.Empty)
            {
                if (!firstSet)
                {
                    set.Append(", ");
                    firstSet = false;
                }
                set.Append("length=" + length);
            }
            if (age != null && age != string.Empty)
            {
                if (!firstSet)
                {
                    set.Append(", ");
                    firstSet = false;
                }
                set.Append("pg_age=" + age);
            }

            string where = $"mediaid={mediaID}";
            if (!UpdateTuple("media", set.ToString(), where))
            {
                publicErrorMessage = privateErrorMessage;
                return false;
            }
            return true;
        }

        public void DeleteMedia(int mediaID)
        {
            DeleteDiamondTuples("Sequel", "sequel", mediaID);
            DeleteDiamondTuples("Sequel", "prequel", mediaID);
            DeleteDiamondTuples("Remake", "remake", mediaID);
            DeleteDiamondTuples("Remake", "original", mediaID);
            DeleteDiamondTuples("ActorsInMedia", "mediaid", mediaID);
            DeleteDiamondTuples("DirectorsInMedia", "mediaid", mediaID);
            DeleteDiamondTuples("GenreInMedia", "mediaid", mediaID);
            DeleteDiamondTuples("Episode", "mediaid", mediaID);
            DeleteDiamondTuples("WatchList", "mediaid", mediaID);
            string sqlInstruction = $"UPDATE profile SET previousWatchMedia=NULL WHERE previousWatchMedia={mediaID};";
            NonQuery(sqlInstruction);
            DeleteTuple("media", mediaID);
        }

        public bool MediaAddADG(int mediaID, int adgID, int choice)
        {
            string schema = string.Empty;
            string values = string.Empty;
            if (choice == 1)
            {
                schema = "ActorsInMedia(mediaID,actorID)";
                values = $"({mediaID},{adgID})";
            }
            else if (choice == 2)
            {
                schema = "DirectorsInMedia(mediaID,directorID)";
                values = $"({mediaID},{adgID})";
            }
            else if (choice == 3)
            {
                schema = "GenreInMedia(mediaID,genreID)";
                values = $"({mediaID},{adgID})";
            }

            if (!InsertInto(schema, values))
            {
                publicErrorMessage = privateErrorMessage;
                return false;
            }
            return true;
        }

        public void MediaRemoveADG(int mediaID, int adgID, int choice)
        {
            string diamondName = string.Empty;
            string idName = string.Empty;
            if (choice == 1)
            {
                diamondName = "ActorsInMedia";
                idName = "actorID";
            }
            else if (choice == 2)
            {
                diamondName = "DirectorsInMedia";
                idName = "directorID";
            }
            else if (choice == 3)
            {
                diamondName = "GenreInMedia";
                idName = "genreID";
            }

            DeleteDiamondTuples(diamondName, idName, adgID);
        }

        public bool MediaSequel(int prequel, int sequel)
        {
            string schema = "Sequel(prequel,sequel)";
            string values = $"({prequel},{sequel})";
            if (!InsertInto(schema, values))
            {
                publicErrorMessage = privateErrorMessage;
                return false;
            }
            return true;
        }

        public bool MediaRemake(int remake, int original)
        {
            string schema = "Remake(remake ,original )";
            string values = $"({remake},{original })";
            if (!InsertInto(schema, values))
            {
                publicErrorMessage = privateErrorMessage;
                return false;
            }
            return true;
        }

        public int GetMediaLength(int mediaID)
        {
            string sqlQuery = $"SELECT length FROM media WHERE mediaid={mediaID};";
            return GetScalarInteger(sqlQuery);
        }

        public string GetMediaTitle(int mediaID)
        {
            string sqlQuery = $"SELECT title FROM media WHERE mediaid={mediaID};";
            return GetScalarString(sqlQuery);
        }
        #endregion

        #region Series & Episodes
        public bool MakeSeries(string seriesTitle, string year)
        {
            int seriesID = GetNextTableID("series");
            string schema = "series(seriesid,seriesTitle,year,rating)";
            string values = $"({seriesID},'{seriesTitle}',{year},NULL)";
            if (!InsertInto(schema, values))
            {
                publicErrorMessage = privateErrorMessage;
                return false;
            }
            return true;
        }

        public void DeleteSeries(int seriesID)
        {
            DeleteDiamondTuples("episode", "seriesid", seriesID);
            DeleteDiamondTuples("SeriesWatchList", "seriesid", seriesID);
            DeleteTuple("series", seriesID);
        }

        public bool MakeEpisode(int seriesID, string seasonNumber, string episodeNumber, string mediaID)
        {
            string schema = "episode(mediaID,seriesID,seasonNumber,episodeNumber)";
            string values = $"({mediaID},{seriesID},{seasonNumber},{episodeNumber})";
            if (!InsertInto(schema, values))
            {
                publicErrorMessage = privateErrorMessage;
                return false;
            }
            return true;
        }

        public void DeleteEpisode(int mediaID)
        {
            DeleteDiamondTuples("episode", "mediaid", mediaID);
        }
        #endregion

        #region Actor & Director & Genre
        public bool MakeActorOrDirector(string firstName, string lastName, string country, string dateOfBirth, int choice)
        {
            int nameID = GetNextTableID("name");
            string schema = "name(nameid,firstname,lastname)";
            string values = $"({nameID},'{firstName}','{lastName}')";
            if (!InsertInto(schema, values))
            {
                publicErrorMessage = privateErrorMessage;
                return false;
            }

            int idNumber = 0;
            if (choice == 1)
            {
                idNumber = GetNextTableID("actor");
                schema = $"actor(actorID,country,dateOfBirth)";
            }
            else if (choice == 2)
            {
                idNumber = GetNextTableID("director");
                schema = $"director(directorID,country,dateOfBirth)";
            }
            values = $"({idNumber},'{country}','{dateOfBirth}')";

            if (!InsertInto(schema, values))
            {
                publicErrorMessage = privateErrorMessage;
                DeleteTuple("name", nameID);
                return false;
            }

            if (choice == 1)
                schema = "ActorName(nameID,actorID)";
            else if (choice == 2)
                schema = "DirectorName(nameID,directorID)";
            values = $"({nameID},{idNumber})";
            InsertInto(schema, values);

            return true;
        }

        public bool UpdateActorOrDirector(int idNumber, string firstName, string lastName, string country, string dateOfBirth, int choice)
        {
            string tableName = string.Empty;
            string idName = string.Empty;
            string sqlQuery = string.Empty;
            if (choice == 1)
            {
                sqlQuery = $"SELECT nameid FROM ActorName WHERE actorid={idNumber};";
                tableName = "actor";
                idName = "actorid";
            }
            else if (choice == 2)
            {
                sqlQuery = $"SELECT nameid FROM DirectorName WHERE directorid={idNumber};";
                tableName = "director";
                idName = "directorid";
            }
            int nameID = GetScalarInteger(sqlQuery);

            string set = $"firstname='{firstName}', lastname='{lastName}'";
            string where = "nameid=" + nameID;
            if (!UpdateTuple("name", set, where))
            {
                publicErrorMessage = privateErrorMessage;
                return false;
            }

            set = $"country='{country}', dateofbirth='{dateOfBirth}'";
            where = $"{idName}={idNumber}";
            if (!UpdateTuple(tableName, set, where))
            {
                publicErrorMessage = privateErrorMessage;
                return false;
            }

            return true;
        }

        public void DeleteActorOrDirector(int idNumber, int choice)
        {
            if (choice == 1)
            {
                string sqlQuery = $"SELECT nameid FROM ActorName WHERE actorid={idNumber};";
                int nameID = GetScalarInteger(sqlQuery);
                DeleteDiamondTuples("ActorName", "actorid", idNumber);
                DeleteDiamondTuples("ActorsInMedia", "actorid", idNumber);
                DeleteTuple("name", nameID);
                DeleteTuple("actor", idNumber);
            }
            else if (choice == 2)
            {
                string sqlQuery = $"SELECT nameid FROM DirectorName WHERE directorid={idNumber};";
                int nameID = GetScalarInteger(sqlQuery);
                DeleteDiamondTuples("DirectorName", "directorid", idNumber);
                DeleteDiamondTuples("DirectorsInMedia", "directorid", idNumber);
                DeleteTuple("name", nameID);
                DeleteTuple("director", idNumber);
            }
        }

        public bool MakeGenre(string genreName)
        {
            int genreID = GetNextTableID("genre");
            if (!InsertInto("genre(genreid,genreType)", $"({genreID},'{genreName}')"))
            {
                publicErrorMessage = privateErrorMessage;
                return false;
            }
            return true;
        }

        public bool UpdateGenre(int genreID, string genreName)
        {
            string set = $"genreType='{genreName}'";
            string where = $"genreid={genreID}";
            if (!UpdateTuple("genre", set, where))
            {
                publicErrorMessage = privateErrorMessage;
                return false;
            }
            return true;
        }

        public void DeleteGenre(int genreID)
        {
            DeleteDiamondTuples("GenreInMedia", "genreid", genreID);
            DeleteTuple("genre", genreID);
        }
        #endregion

        #region Supporting the server
        public void UpdateMediaRating()
        {
            string sqlQuery = "SELECT mediaid FROM media ORDER BY mediaid;";
            Query(sqlQuery);
            foreach (DataRow row in privateTable.Rows)
                UpdateMediaRating((int)row[0]);
        }

        public string UpdateMediaRating(int mediaID)
        {
            string sqlQuery = "SELECT CAST(AVG(CAST(rating AS DECIMAL)) AS DECIMAL(3,1)) AS rating FROM watchlist WHERE mediaid = " + mediaID + " AND rating IS NOT NULL;";
            string rating = GetScalarString(sqlQuery);
            if (rating.Length > 0)
            {
                string sqlInstruction = "UPDATE media SET rating = " + rating + " WHERE mediaid = " + mediaID + ";";
                NonQuery(sqlInstruction);
            }
            return rating;
        }

        public void UpdateSeriesRating()
        {
            string sqlQuery = "SELECT seriesid FROM series ORDER BY seriesid;";
            Query(sqlQuery);
            foreach (DataRow row in privateTable.Rows)
                UpdateSeriesRating((int)row[0]);
        }

        public string UpdateSeriesRating(int seriesID)
        {
            string sqlQuery = "SELECT CAST(AVG(CAST(rating AS DECIMAL)) AS DECIMAL(3,1)) AS rating FROM serieswatchlist WHERE seriesid = " + seriesID + " AND rating IS NOT NULL;";
            string rating = GetScalarString(sqlQuery);
            if (rating.Length > 0)
            {
                string sqlInstruction = "UPDATE series SET rating = " + rating + " WHERE seriesid = " + seriesID + ";";
                NonQuery(sqlInstruction);
            }
            return rating;
        }
        #endregion

        #region Tables
        public void TableOfProfiles(int customerID)
        {
            string sqlQuery = $"SELECT profileid, username, profiletype FROM (SELECT profileid,username,profiletype AS profiletypeid FROM profile WHERE customerid={customerID}) AS temp NATURAL JOIN profiletype;";
            Query(sqlQuery);
            publicTable = privateTable;
        }

        public void TableOfWatchList(int customerID, int profileID)
        {
            string sqlQuery = $"SELECT title, year, your_rating, length, progresstime AS progress_percentage,mediaid FROM (SELECT mediaid,rating AS your_rating,progresstime FROM watchlist WHERE customerid={customerID} AND profileid={profileID}) as temp NATURAL JOIN media;";
            Query(sqlQuery);
            publicTable = privateTable;
        }

        public void TableOfSeriesWatchList(int customerID, int profileID)
        {
            string sqlQuery = $"SELECT seriestitle, year, your_rating, title AS previous_watched_episode, seriesid FROM (SELECT seriesid,rating AS your_rating,previousWatchEpisode AS mediaid FROM serieswatchlist WHERE customerid={customerID} AND profileid={profileID}) as temp NATURAL JOIN (SELECT mediaid, title FROM media) AS temp2 NATURAL JOIN series;";
            Query(sqlQuery);
            publicTable = privateTable;
        }

        public bool TableOfMovies(string title, string year, string country, string length, string rating, string age, string releaseDate)
        {
            StringBuilder sqlQuery = new StringBuilder("SELECT * FROM media");
            string where = TableOfMovies_ConditionString(title, year, country, length, rating);
            sqlQuery.Append(where);
            bool firstCondition = (where == string.Empty);
            if (age != null && age != string.Empty)
            {
                if (firstCondition)
                {
                    sqlQuery.Append(" WHERE ");
                    firstCondition = false;
                }
                else
                    sqlQuery.Append(" AND ");
                sqlQuery.Append("pg_age<=" + age);
            }
            if (releaseDate != null && releaseDate != string.Empty)
            {
                if (firstCondition)
                {
                    sqlQuery.Append(" WHERE ");
                    firstCondition = false;
                }
                else
                    sqlQuery.Append(" AND ");
                sqlQuery.Append("release_Date>='" + releaseDate + "'");
            }

            sqlQuery.Append(" ORDER BY mediaid;");

            if (!Query(sqlQuery.ToString()))
            {
                publicErrorMessage = privateErrorMessage;
                return false;
            }

            publicTable = privateTable;
            return true;
        }

        public bool TableOfMovies(int customerID, int profileID, string director, string actor, string year, string title, string genre, string country, string length, string rating)
        {
            int age = ProfileAge(customerID, profileID);
            StringBuilder sqlQuery = new StringBuilder($"SELECT mediaid, title, year, country, length, rating FROM movies NATURAL JOIN (SELECT * FROM media WHERE pg_age<={age}) AS temp1");

            if (actor != null && actor != string.Empty)
            {
                string[] names = NameSplitter(actor);
                if (names == null)
                {
                    publicErrorMessage = "Invalid actor name!";
                    return false;
                }
                sqlQuery.Append($" NATURAL JOIN actorsinmedia NATURAL JOIN (SELECT actorid FROM actorswithnames WHERE actor_firstname='{names[0]}' AND actor_lastname='{names[1]}') AS temp2");
            }

            if (director != null && director != string.Empty)
            {
                string[] names = NameSplitter(director);
                if (names == null)
                {
                    publicErrorMessage = "Invalid director name!";
                    return false;
                }
                sqlQuery.Append($" NATURAL JOIN directorsinmedia NATURAL JOIN (SELECT directorid FROM directorswithnames WHERE director_firstname='{names[0]}' AND director_lastname='{names[1]}') AS temp3");
            }

            if (genre != null && genre != string.Empty)
                sqlQuery.Append($" NATURAL JOIN genreinmedia NATURAL JOIN (SELECT genreid FROM genre WHERE genretype='{genre}') as temp4");

            sqlQuery.Append(TableOfMovies_ConditionString(title, year, country, length, rating) + " ORDER BY mediaid;");

            if (!Query(sqlQuery.ToString()))
            {
                publicErrorMessage = privateErrorMessage;
                return false;
            }

            publicTable = privateTable;
            return true;
        }

        private string TableOfMovies_ConditionString(string title, string year, string country, string length, string rating)
        {
            StringBuilder sqlQuery = new StringBuilder();
            bool firstCondition = true;
            if (title != null && title != string.Empty)
            {
                firstCondition = false;
                sqlQuery.Append(" WHERE title LIKE '%" + title + "%'");
            }
            if (year != null && year != string.Empty)
            {
                if (firstCondition)
                {
                    sqlQuery.Append(" WHERE ");
                    firstCondition = false;
                }
                else
                    sqlQuery.Append(" AND ");
                sqlQuery.Append("year=" + year);
            }
            if (country != null && country != string.Empty)
            {
                if (firstCondition)
                {
                    sqlQuery.Append(" WHERE ");
                    firstCondition = false;
                }
                else
                    sqlQuery.Append(" AND ");
                sqlQuery.Append("country='" + country + "'");
            }
            if (length != null && length != string.Empty)
            {
                if (firstCondition)
                {
                    sqlQuery.Append(" WHERE ");
                    firstCondition = false;
                }
                else
                    sqlQuery.Append(" AND ");
                sqlQuery.Append("length<=" + length);
            }
            if (rating != null && rating != string.Empty)
            {
                if (firstCondition)
                {
                    sqlQuery.Append(" WHERE ");
                    firstCondition = false;
                }
                else
                    sqlQuery.Append(" AND ");
                sqlQuery.Append("rating>=" + rating);
            }
            return sqlQuery.ToString();
        }

        public bool TableOfSeries(string title, string year)
        {
            StringBuilder sqlQuery = new StringBuilder("SELECT seriesid, seriestitle, year, rating FROM series");
            bool firstCondition = true;
            if (title != null && title != string.Empty)
            {
                firstCondition = false;
                sqlQuery.Append(" WHERE seriestitle LIKE '%" + title + "%'");
            }
            if (year != null && year != string.Empty)
            {
                if (firstCondition)
                    sqlQuery.Append(" WHERE ");
                else
                    sqlQuery.Append(" AND ");
                sqlQuery.Append("year=" + year);
            }

            sqlQuery.Append(" ORDER BY seriesid;");

            if (!Query(sqlQuery.ToString()))
            {
                publicErrorMessage = privateErrorMessage;
                return false;
            }

            publicTable = privateTable;
            return true;
        }

        public void TableOfEpisodes(int seriesID)
        {
            string sqlQuery = $"SELECT mediaid, title, seasonnumber, episodenumber FROM (SELECT * FROM episode WHERE seriesid={seriesID}) AS temp NATURAL JOIN media ORDER BY seasonnumber, episodenumber;";
            Query(sqlQuery);
            publicTable = privateTable;
        }

        public void TableOfActorsInMedia(int mediaID)
        {
            string sqlQuery = $"SELECT actorid, firstname, lastname, dateofbirth, country FROM (SELECT * FROM ActorsInMedia WHERE mediaid={mediaID}) AS temp NATURAL JOIN actor NATURAL JOIN ActorName NATURAL JOIN name ORDER BY lastname;";
            Query(sqlQuery);
            publicTable = privateTable;
        }

        public void TableOfDirectorsInMedia(int mediaID)
        {
            string sqlQuery = $"SELECT directorid, firstname, lastname, dateofbirth, country FROM (SELECT * FROM DirectorsInMedia WHERE mediaid={mediaID}) AS temp NATURAL JOIN director NATURAL JOIN directorName NATURAL JOIN name ORDER BY lastname;";
            Query(sqlQuery);
            publicTable = privateTable;
        }

        public void TableOfGenresInMedia(int mediaID)
        {
            string sqlQuery = $"SELECT genreid, genreType FROM GenreInMedia NATURAL JOIN genre WHERE mediaid={mediaID};";
            Query(sqlQuery);
            publicTable = privateTable;
        }

        public void TableOfActors()
        {
            string sqlQuery = "SELECT actorid, firstname, lastname, dateofbirth, country FROM actor NATURAL JOIN actorname NATURAL JOIN name ORDER BY actorid;";
            Query(sqlQuery);
            publicTable = privateTable;
        }

        public void TableOfDirectors()
        {
            string sqlQuery = "SELECT directorid, firstname, lastname, dateofbirth, country FROM director NATURAL JOIN directorname NATURAL JOIN name ORDER BY directorid;";
            Query(sqlQuery);
            publicTable = privateTable;
        }

        public void TableOfGenres()
        {
            string sqlQuery = "SELECT genreid, genretype FROM genre ORDER BY genreid;";
            Query(sqlQuery);
            publicTable = privateTable;
        }

        public bool TableOfCustomers(string firstName, string lastName, string email)
        {
            StringBuilder sqlQuery = new StringBuilder("SELECT customerid, firstname, lastname, email, discountpercentage FROM customer NATURAL JOIN CustomerUser NATURAL JOIN users NATURAL JOIN username NATURAL JOIN name NATURAL JOIN logininuser NATURAL JOIN login");
            bool firstCondition = true;
            if (firstName != null && firstName != string.Empty)
            {
                sqlQuery.Append($" WHERE firstName LIKE '%{firstName}%'");
                firstCondition = false;
            }
            if (lastName != null && lastName != string.Empty)
            {
                if (firstCondition)
                {
                    sqlQuery.Append(" WHERE ");
                    firstCondition = false;
                }
                else
                    sqlQuery.Append(" AND ");
                sqlQuery.Append($"lastName LIKE '%{lastName}%'");
            }
            if (email != null && email != string.Empty)
            {
                if (firstCondition)
                    sqlQuery.Append(" WHERE ");
                else
                    sqlQuery.Append(" AND ");
                sqlQuery.Append($"email LIKE '%{email}%'");
            }
            sqlQuery.Append(" ORDER BY customerid;");

            if (!Query(sqlQuery.ToString()))
            {
                publicErrorMessage = privateErrorMessage;
                return false;
            }

            publicTable = privateTable;
            return true;
        }
        #endregion

        #region Login
        public int Login(string email, string password)
        {
            string query = "SELECT userID FROM (SELECT loginID FROM Login WHERE email='" + email + "' AND " + "password='" + password + "') AS temp NATURAL JOIN LoginInUser;";
            Debug.WriteLine(query);
            int user = GetScalarInteger(query);
            if (user < 0)
                publicErrorMessage = privateErrorMessage;

            return user;
        }

        public int GetCustomer(int user)
        {
            string query = "SELECT customerID FROM CustomerUser WHERE userID=" + user + ";";
            int customer = GetScalarInteger(query);
            if (customer < 0)
                publicErrorMessage = privateErrorMessage;
            return customer;

        }

        public int getAdministrator(int user)
        {
            string query = "SELECT adminID FROM AdministratorUser WHERE userID=" + user + ";";
            int administrator = GetScalarInteger(query);
            if (administrator < 0)
                publicErrorMessage = privateErrorMessage;

            return administrator;

        }
        #endregion

        public object[] GetAdminInfo(int adminID)
        {
            string sqlQuery = $"SELECT firstname, lastname, positionid FROM (SELECT * FROM administrator WHERE adminid={adminID}) AS temp NATURAL JOIN AdministratorUser NATURAL JOIN users NATURAL JOIN username NATURAL JOIN name;";
            Query(sqlQuery);
            DataRow row = privateTable.Rows[0];
            string name = row[0].ToString() + " " + row[1].ToString();
            object[] adminInfo = { name, row[2] };
            return adminInfo;
        }

        public void close()
        {
            con.Close();
        }
    }
}
