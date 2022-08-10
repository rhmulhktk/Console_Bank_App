using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Console_Bank_App
{
    internal class Program
    {
        static void Main(string[] args)
        {
            //create instanace of database connection
            SqlConnection conn = new SqlConnection(DbConnection.ConnectionString());
            int option;

            do
            {
                MainMessage.Message();
                Console.WriteLine(" 1. For Create Account");
                Console.WriteLine(" 2. For LogIn Account");
                Console.WriteLine(" 0. For Exit Program");
                Console.WriteLine("");
                Console.Write("    Enter Your Operation .");
                option = Convert.ToInt16(Console.ReadLine());


                try
                {

                    switch (option)
                    {
                        // For Create Account
                        case 1:
                            {
                                //Generate a random number
                                Random random = new Random();
                                //Any random integer
                                double AccountNumber = random.Next(1000, 100000);
                                
                                Console.WriteLine();
                                Console.WriteLine("*****************************************************************");
                                Console.Write(" Enter Your CNIC Number : ");
                                string CNICNumber = Convert.ToString(Console.ReadLine());
                                Console.Write(" Enter Account Password : ");
                                string AccountPassword = Convert.ToString(Console.ReadLine());
                                Console.WriteLine("");

                                try
                                {
                                    UserInformation user = new UserInformation();

                                    user.CreateAccount(CNICNumber, AccountNumber, AccountPassword);


                                }
                                catch (Exception)
                                {

                                    throw;
                                }
                                break;
                            }

                        // For LogIn in Account
                        case 2:
                            {
                                UserInformation use = new UserInformation();
                                use.Login();
                                break;
                            }

                        //For Exit Program
                        case 0:
                            {
                                Environment.Exit(0);
                                break;
                            }

                        default:
                            {
                                Console.WriteLine("     No match found");
                                Console.WriteLine();
                                break;
                            }
                    }
                    conn.Close();
                }

                catch (Exception e)
                {
                    Console.WriteLine();
                    Console.WriteLine("Error: " + e.Message);
                    Console.WriteLine();
                }
            }
            while (option != Environment.ExitCode);

            Console.Read();
        }
    }
}
