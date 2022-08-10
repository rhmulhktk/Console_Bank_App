using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Console_Bank_App
{

    internal class UserInformation
    {
        private string AccountNumber;
        private string AccountPassword;
        private double Balance;


        private string AccountType;
        private string AccountTitle;
        private string CNIC;
        private string Email;


        public void CreateAccount(string CNIC, Double AccountNumber, string Password)
        {
            this.CNIC = CNIC;
            this.AccountNumber = AccountNumber.ToString();
            this.AccountPassword = Password;
            CreateAcc();
        }


        //Database Create Account Action
        public void CreateAcc()
        {
            //create instanace of database connection
            SqlConnection conn = new SqlConnection(DbConnection.ConnectionString());
            conn.Open();

            string insQuery = "INSERT INTO Bank(AccNum, CNIC, Pass) VALUES('" + AccountNumber + "', '" + CNIC + "', '" + AccountPassword + "')";

            // sqlCommand Class Object
            SqlCommand insertCommand = new SqlCommand(insQuery, conn);

            Console.WriteLine("Your Account Has Been Created Your \n" +
                                    "Your CNIC Number is " + CNIC + "\n" +
                                    "Your Account Number is " + AccountNumber + "\n" +
                                    "Your Password is " + AccountPassword);
            Console.WriteLine();
            Console.WriteLine("*****************************************************************");
            Console.WriteLine("Commands executed! Total rows affected are " + insertCommand.ExecuteNonQuery() +
            " Data Inserted! Now press enter to move to the Previous section!");
            Console.ReadLine();
            Console.Clear();

        }

        public void Login()
        {
            //create instanace of database connection
            SqlConnection conn = new SqlConnection(DbConnection.ConnectionString());
            conn.Open();


            Console.WriteLine();
            Console.WriteLine("*****************************************************************");
            Console.Write(" Enter Account Number : ");
            string accountNumber = Convert.ToString(Console.ReadLine());
            Console.Write(" Enter Account Password : ");
            string accountPassword = Convert.ToString(Console.ReadLine());
            Console.WriteLine("");

            // Select * TableName Query
            string selQuery = "SELECT * FROM Bank WHERE AccNum ='" + accountNumber + "' or Pass ='" + accountPassword + "'";

            // sqlCommand Class Object
            SqlCommand command = new SqlCommand(selQuery, conn);

            using (SqlDataReader reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    // iterate your results here
                    string AccNum = reader["AccNum"].ToString();
                    string AccPass = reader["Pass"].ToString();
                    string AccTitle = reader["AccTitle"].ToString();

                    ////Console.WriteLine(String.Format(" AccNum: {0}\n AccTitle: {1}\n AccountType: {2}\n CNIC: {3}\n Email: {4}\n Pass: {5}\n Balance: {6}",
                    ////reader[0], reader[1], reader[2], reader[3], reader[4], reader[5], reader[6]));

                    if (accountNumber.Equals(AccNum) && accountPassword.Equals(AccPass))
                    {
                        Console.WriteLine("You have successfully logged in Most WellCome Mr. " + AccTitle + "!");

                        AccountNumber = AccNum;
                        AccountPassword = AccPass;
                        AccountTitle = AccTitle;

                        int suboption;
                        do
                        {
                            Console.WriteLine(" 1. For Account Details");
                            Console.WriteLine(" 2. For Account Updates");
                            Console.WriteLine(" 3. For Deposit Amount");
                            Console.WriteLine(" 4. For Withdraw Amount");
                            Console.WriteLine(" 5. For Balance Inquiry");
                            Console.WriteLine(" 0. For LogOut");
                            Console.WriteLine("");
                            Console.Write("    Enter Your Operation .");
                            suboption = Convert.ToInt16(Console.ReadLine());

                            switch (suboption)
                            {
                                case 1:
                                    {
                                        AccountDisplay();
                                        break;

                                    }

                                case 2:
                                    {
                                        //AccountDisplay();
                                        AccountUpdates();
                                        break;

                                    }

                                case 3:
                                    {

                                        Console.Write(" Enter Amount for Deposit : ");
                                        double amount = Convert.ToDouble(Console.ReadLine());
                                        SetDeposit(amount);
                                        break;

                                    }

                                case 4:
                                    {

                                        Console.Write(" Enter Amount for Withdraw : ");
                                        double amount = Convert.ToDouble(Console.ReadLine());
                                        SetWithdraw(amount);
                                        break;
                                    }

                                case 5:
                                    {
                                        
                                        Console.WriteLine(" Your Balance is " + GetNetBalance());
                                        //tr.GetNetBalance();
                                        break;
                                    }

                                case 0:
                                    {
                                        Console.Clear();
                                        MainMessage.Message();
                                        Login();
                                        break;
                                    }

                                default:
                                    {
                                        Console.WriteLine("     No match found");
                                        Console.WriteLine();
                                        break;
                                    }
                            }

                            Console.WriteLine("*****************************************************************");
                        }
                        while (suboption != 0);


                        //Console.WriteLine("Now press enter to move to the Previous section!");
                        //Console.ReadLine();
                        //Console.Clear();
                        //break;
                        //Console.ReadLine();

                        break;
                    }
                    else
                    {
                        Console.WriteLine("Your username or password is incorect, try again !!!");
                        Console.ReadLine();
                        break;

                    }
                }
            }
            Console.ReadLine();
            Console.Clear();

        }
        public void AccountUpdates()
        {
            //create instanace of database connection
            SqlConnection conn = new SqlConnection(DbConnection.ConnectionString());
            conn.Open();

            // Select * TableName Query
            string selQuery = "SELECT * FROM Bank WHERE AccNum ='" + AccountNumber + "' or Pass ='" + AccountPassword + "'";

            // sqlCommand Class Object
            SqlCommand command = new SqlCommand(selQuery, conn);

            Console.WriteLine();
            using (SqlDataReader reader = command.ExecuteReader())
            {
                while (reader.Read())
                {

                    Console.WriteLine(String.Format(" AccNum: {0}\n AccTitle: {1}\n AccountType: {2}\n CNIC: {3}\n Email: {4}\n Pass: {5}",
                    reader[0], reader[1], reader[2], reader[3], reader[4], reader[5]));
                    Console.WriteLine();
                }
            }

                // UPDATE command!
                Console.Write(" Enter Account Holder Name: ");
                string accounTitle = Convert.ToString(Console.ReadLine());
                Console.Write(" Enter Account Type: ");
                string accountType = Convert.ToString(Console.ReadLine());
                Console.Write(" Enter Your Email: ");
                string email = Convert.ToString(Console.ReadLine());

                SqlCommand UpdateCommand = new SqlCommand("UPDATE Bank SET AccTitle = '" + accounTitle + "', AccountType = '" + accounTitle + "', Email = '" + email + "' WHERE AccNum = '" + AccountNumber + "'", conn);

                // Updatecommand Command to keep change in database
                UpdateCommand.ExecuteNonQuery();


                // After Update showing Updates changes
                Console.WriteLine();

                Console.WriteLine("****UPDATED RECORD****");

                Console.WriteLine();

            using (SqlDataReader reader = command.ExecuteReader())
            {

                    while (reader.Read())
                    {
                        Console.WriteLine(String.Format(" AccNum: {0}\n AccTitle: {1}\n AccountType: {2}\n CNIC: {3}\n Email: {4}\n Pass: {5}",
                        reader[0], reader[1], reader[2], reader[3], reader[4], reader[5]));
                        Console.WriteLine();
                    }
                    Console.WriteLine();
            }

                Console.WriteLine("*****************************************************************");
            }

        public void AccountDisplay()
        {
            //create instanace of database connection
            SqlConnection conn = new SqlConnection(DbConnection.ConnectionString());
            conn.Open();

            // Select * TableName Query
            string selQuery = "SELECT * FROM Bank WHERE AccNum ='" + AccountNumber +"' or Pass ='" + AccountPassword + "'";

            // sqlCommand Class Object
            SqlCommand command = new SqlCommand(selQuery, conn);

            Console.WriteLine();
            using (SqlDataReader reader = command.ExecuteReader())
            {
                while (reader.Read())
                {

                    Console.WriteLine(String.Format(" AccNum: {0}\n AccTitle: {1}\n AccountType: {2}\n CNIC: {3}\n Email: {4}\n Pass: {5}\n Balance: {6}",
                    reader[0], reader[1], reader[2], reader[3], reader[4], reader[5], reader[6]));
                    Console.WriteLine();
                }
            }

        }




        public double GetNetBalance()
        {
            //create instanace of database connection
            SqlConnection conn = new SqlConnection(DbConnection.ConnectionString());
            conn.Open();

            // Select * TableName Query
            string selQuery = "SELECT * FROM Bank WHERE AccNum ='" + AccountNumber + "'";

            // sqlCommand Class Object
            SqlCommand command = new SqlCommand(selQuery, conn);

            Console.WriteLine();
            using (SqlDataReader reader = command.ExecuteReader())
            {
                while (reader.Read())
                {

                    string Bal = reader["Balance"].ToString();
                    //Console.WriteLine("Your Account Balance is "+ Balance);
                    Balance = Convert.ToDouble(Bal);
                }
            }

            Console.WriteLine();
            return Balance;
        }

        public void SetDeposit(double deposit)
        {
            //create instanace of database connection
            SqlConnection conn = new SqlConnection(DbConnection.ConnectionString());
            conn.Open();

            // Select * TableName Query
            string selQuery = "SELECT * FROM Bank WHERE AccNum ='" + AccountNumber + "'";

            // sqlCommand Class Object
            SqlCommand command = new SqlCommand(selQuery, conn);

            using (SqlDataReader reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    Balance = Convert.ToDouble(reader["Balance"]);
                }
            }

                    Balance = Balance + deposit;

                    // UPDATE command!
                    SqlCommand UpdateCommand = new SqlCommand("UPDATE Bank SET Balance = '" + Balance + "' WHERE AccNum = '" + AccountNumber + "'", conn);

                    // Updatecommand Command to keep change in database
                    UpdateCommand.ExecuteNonQuery();


            using (SqlDataReader reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    Balance = Convert.ToDouble(reader["Balance"]);
                }
            }
            Console.WriteLine();
                    Console.WriteLine("Your Amount Rs. " + deposit + " is Deposit Sucessfully" +
                        "Your Net Balance is " + Balance);

            Console.WriteLine();

        }

        public void SetWithdraw(double withdraw)
        {
            //create instanace of database connection
            SqlConnection conn = new SqlConnection(DbConnection.ConnectionString());
            conn.Open();

            // Select * TableName Query
            string selQuery = "SELECT * FROM Bank WHERE AccNum ='" + AccountNumber + "'";

            // sqlCommand Class Object
            SqlCommand command = new SqlCommand(selQuery, conn);

            using (SqlDataReader reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    Balance = Convert.ToDouble(reader["Balance"]);
                }
            }

            if (Balance < withdraw)
            {
                Console.WriteLine();
                Console.WriteLine("Insufficient Balance");
                Console.WriteLine();
            }
            else
            {
                if (withdraw >= 500)
                {
                    Balance = Balance - withdraw;

                    // UPDATE command!
                    SqlCommand UpdateCommand = new SqlCommand("UPDATE Bank SET Balance = '" + Balance + "' WHERE AccNum = '" + AccountNumber + "'", conn);

                    // Updatecommand Command to keep change in database
                    UpdateCommand.ExecuteNonQuery();


                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Balance = Convert.ToDouble(reader["Balance"]);
                        }
                    }
                    Console.WriteLine();
                    Console.WriteLine("Your Amount Rs. " + withdraw + " is Withdraw Sucessfully" +
                        "Your Net Balance is " + Balance);

                    Console.WriteLine();
                    
                }
                else
                {
                    Console.WriteLine();
                    Console.WriteLine("Withdraw Less then Rs. 500 is not possible and You are Withdraw Rs. "+ withdraw);
                    Console.WriteLine();
                 
                }
            }

        }
    }
}


