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
    /// Interaction logic for Visualizer.xaml
    /// </summary>
    public partial class Visualizer : Window
    {
        private List<Employee> employeeList;

        public Visualizer()
        {
            InitializeComponent();
        }

        public Visualizer(List<Employee> employeeList)
        {
            InitializeComponent();

            this.employeeList = employeeList;

            foreach (var employee in employeeList)
            {
                var itemLabel = new Label();
                var itemButtonEdit = new Button();
                var itemButtonDelete = new Button();
                itemLabel.HorizontalAlignment = HorizontalAlignment.Center;
                itemLabel.Content = $"{employee.name}  {employee.surname}  {employee.age}";
                ItemListStackPanel.Children.Add(itemLabel);
                itemButtonEdit.HorizontalAlignment = HorizontalAlignment.Center;
                itemButtonEdit.Content = "Edytuj";
                itemButtonEdit.Command = Edit(employee.id);
                ItemListStackPanel.Children.Add(itemButtonEdit);
                itemButtonDelete.HorizontalAlignment = HorizontalAlignment.Center;
                itemButtonDelete.Content = "Usuń";
                itemButtonDelete.Command = Delete(employee.id);
                ItemListStackPanel.Children.Add(itemButtonDelete);
            }
        }

        private ICommand Edit(int id)
        {
            string connectionString;
            SqlConnection con;

            connectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=|DataDirectory|\Database1.mdf;Integrated Security=True";

            con = new SqlConnection(connectionString);
            con.Open();

            string sql = "select * from Employee where ID = " + id.ToString();

            SqlCommand cmd = new SqlCommand(sql, con);

            SqlDataAdapter adapter = new SqlDataAdapter();

            adapter.UpdateCommand = new SqlCommand(sql, con);
            adapter.UpdateCommand.ExecuteNonQuery();

            cmd.Dispose();
            con.Close();

            employeeList = employeeList.Select(x => x).OrderBy(x => x.surname).ToList<Employee>();

            throw new NotImplementedException();
        }

        private ICommand Delete(int id)
        {
            string connectionString;
            SqlConnection con;

            connectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=|DataDirectory|\Database1.mdf;Integrated Security=True";

            con = new SqlConnection(connectionString);
            con.Open();

            string sql = "select * from Employee where ID = " + id.ToString();

            SqlCommand cmd = new SqlCommand(sql, con);

            SqlDataAdapter adapter = new SqlDataAdapter();

            adapter.DeleteCommand = new SqlCommand(sql, con);
            adapter.DeleteCommand.ExecuteNonQuery();

            cmd.Dispose();
            con.Close();

            throw new NotImplementedException();
        }

    }
}
