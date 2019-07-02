using System;
using System.Collections.Generic;
using System.Data;
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
using System.Windows.Navigation;
using System.Windows.Shapes;
using WpfApp.BDClasses;

namespace WpfApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void ShowAll(object sender, RoutedEventArgs e)
        {
            string connectionString;
            SqlConnection con;

            connectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=|DataDirectory|\Database1.mdf;Integrated Security=True";

            SqlDataAdapter sad = new SqlDataAdapter();

            con = new SqlConnection(connectionString);
            con.Open();

            SqlCommand cmd = new SqlCommand("select * from Employee", con);

            var employeeList = new List<Employee>();

            SqlDataReader reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                int id = (int)(reader["ID"]);
                string name = (reader["Name"]).ToString();
                string surname = (reader["Surname"]).ToString();
                int age = (int)(reader["Age"]);

                Employee employee = new Employee(id, name, surname, age);
                employeeList.Add(employee);
            }

            cmd.Dispose();
            con.Close();

            employeeList = employeeList.Select(x =>x).OrderBy(x => x.surname).ToList<Employee>();

            var visualizerWindow = new Visualizer(employeeList);
            visualizerWindow.ShowDialog();
        }

        private void Add(object sender, RoutedEventArgs e)
        {
            var addingWindow = new Adding();
            addingWindow.ShowDialog();
        }
    }
}
