using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Windows;

namespace WPFApp
{
    public partial class MainWindow : Window
    {
        public List<Adat> adatok = new List<Adat>();
        public static string connectionString = "Server=localhost;Database=aru;User=root;Password=;SSLmode=NONE";

        public MainWindow()
        {
            InitializeComponent();
            DatabaseConnection(connectionString);
            AdatokBeolvasasa("customers");
            dtg_eredmenyek.ItemsSource = adatok;
        }

        private void DatabaseConnection(string connectionString)
        {
            try
            {
                using (MySqlConnection con = new MySqlConnection(connectionString))
                {
                    con.Open();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void AdatokBeolvasasa(string tableName)
        {
            string sqlCommand = $"SELECT * FROM {tableName}";

            try
            {
                using (MySqlConnection mySqlConnection = new MySqlConnection(connectionString))
                {
                    mySqlConnection.Open();

                    if (tableName == "customers")
                    {
                        adatok.Clear();
                        using (MySqlCommand mySqlCommand = new MySqlCommand(sqlCommand, mySqlConnection))
                        using (MySqlDataReader myReader = mySqlCommand.ExecuteReader())
                        {
                            while (myReader.Read())
                            {
                                Adat egyAdat = new Adat(myReader.GetString(0), myReader.GetString(1));
                                adatok.Add(egyAdat);
                            }
                        }
                    }

                    mySqlConnection.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btn_modosit_Click(object sender, RoutedEventArgs e)
        {
            string aruatvevo = txb_aruatvevo.Text;
            string szemid = txb_szemid.Text;

            string sqlCommand = $"UPDATE `customers` SET `Árúátvevő`=@aruatvevo WHERE SzemID=@szemid";
            try
            {
                using (MySqlConnection mySqlConnection = new MySqlConnection(connectionString))
                {
                    mySqlConnection.Open();

                    using (MySqlCommand mySqlCommand = new MySqlCommand(sqlCommand, mySqlConnection))
                    {
                        mySqlCommand.Parameters.AddWithValue("@aruatvevo", aruatvevo);
                        mySqlCommand.Parameters.AddWithValue("@szemid", szemid);

                        mySqlCommand.ExecuteNonQuery();
                    }

                    mySqlConnection.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            MessageBox.Show("Siker");

        }

        private void btn_torol_Click(object sender, RoutedEventArgs e)
        {
            string szemid = txb_szemid.Text;

            string sqlCommand = $"DELETE FROM `customers` WHERE SzemID=@szemid";
            try
            {
                using (MySqlConnection mySqlConnection = new MySqlConnection(connectionString))
                {
                    mySqlConnection.Open();

                    using (MySqlCommand mySqlCommand = new MySqlCommand(sqlCommand, mySqlConnection))
                    {
                        mySqlCommand.Parameters.AddWithValue("@szemid", szemid);

                        mySqlCommand.ExecuteNonQuery();
                    }

                    mySqlConnection.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            MessageBox.Show("Siker");

        }

        private void btn_uj_Click(object sender, RoutedEventArgs e)
        {
            string aruatvevo = txb_aruatvevo.Text;
            string szemid = txb_szemid.Text;

            string sqlCommand = "INSERT INTO `customers` (`SzemID`, `Árúátvevő`) VALUES (@szemid, @aruatvevo)";
            try
            {
                using (MySqlConnection mySqlConnection = new MySqlConnection(connectionString))
                {
                    mySqlConnection.Open();

                    using (MySqlCommand mySqlCommand = new MySqlCommand(sqlCommand, mySqlConnection))
                    {
                        mySqlCommand.Parameters.AddWithValue("@szemid", szemid);
                        mySqlCommand.Parameters.AddWithValue("@aruatvevo", aruatvevo);

                        mySqlCommand.ExecuteNonQuery();
                    }

                    mySqlConnection.Close();
                }
                MessageBox.Show("Siker");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

    }
}