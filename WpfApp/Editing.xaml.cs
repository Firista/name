using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using WpfApp.BDClasses;

namespace WpfApp
{
    /// <summary>
    /// Interaction logic for Editing.xaml
    /// </summary>
    public partial class Editing : Window
    {
        public int id { get; set; }

        public Editing(int id)
        {
            this.id = id;
            InitializeComponent();
        }

        private void showError()
        {
            MessageBox.Show("Poadana Wartosc jest nie wlasciwa!", "Test", MessageBoxButton.OK);
        }

        private string GetValueFromName()
        {
            string value;
            value = Name.Text;
            if (value is string)
            {

                return value;
            }
            else
            {
                showError();
                return "";
            }

        }

        private string GetValueFromSurname()
        {
            string value;
            value = Surname.Text;
            if (value is string)
            {

                return value;
            }
            else
            {
                showError();
                return "";
            }
        }

        private int GetValueFromAge()
        {
            int value;
            if (Int32.TryParse(Age.Text, out value))
            {

                return value;
            }
            else
            {
                showError();
                return 0;
            }
        }

        private bool CheckInputs()
        {
            bool name = GetValueFromName() != "";
            bool surname = GetValueFromSurname() != "";
            bool age = GetValueFromAge() > 0;

            return name && surname && age;
        }

        private void EditButton(object sender, RoutedEventArgs e)
        {
            if (CheckInputs() == true)
            {
                Employee employee = new Employee(this.GetValueFromName(), this.GetValueFromSurname(), this.GetValueFromAge());

                string connectionString;
                SqlConnection con;

                connectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=|DataDirectory|\Database1.mdf;Integrated Security=True";

                con = new SqlConnection(connectionString);
                con.Open();

                string sql = "Update Employee set Name = '" + employee.name;
                sql += "', Surname = '" + employee.surname;
                sql += "', Age = '" + employee.age.ToString();
                sql += "' where ID =" + id.ToString();

                SqlCommand cmd = new SqlCommand(sql, con);

                SqlDataAdapter adapter = new SqlDataAdapter();

                adapter.UpdateCommand = new SqlCommand(sql, con);
                adapter.UpdateCommand.ExecuteNonQuery();

                cmd.Dispose();
                con.Close();

                ExitIt();
            }

        }

        public void ExitIt()
        {
            this.Close();
        }

        private void ExitButton(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
