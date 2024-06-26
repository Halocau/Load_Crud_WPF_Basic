using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using Review1q1.BusinessObjects;

namespace Review1q1
{
    public partial class MainWindow : Window
    {
        public ObservableCollection<Employee> Employees { get; set; }

        public MainWindow()
        {
            InitializeComponent();
            LoadDataFromDatabase();
        }

        private void LoadDataFromDatabase()
        {
            using (var context = new Bonus2Context())
            {
                var employees = context.Employees.ToList();
                Employees = new ObservableCollection<Employee>(employees);
                EmployeeDataGrid.ItemsSource = Employees;
            }
        }

        private void BtnReset_Click(object sender, RoutedEventArgs e)
        {
            txtId.Clear();
            txtName.Clear();
            rbtnMale.IsChecked = true;
            dpDateOfBirth.SelectedDate = null;
            txtPhone.Clear();
            txtIDNumber.Clear();
            EmployeeDataGrid.SelectedIndex = -1;
        }

        private void BtnAdd_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(txtName.Text) || dpDateOfBirth.SelectedDate == null || string.IsNullOrEmpty(txtPhone.Text) || string.IsNullOrEmpty(txtIDNumber.Text))
            {
                MessageBox.Show("Please fill in all fields.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            var newEmployee = new Employee
            {
                EmployeeName = txtName.Text,
                Gender = rbtnMale.IsChecked == true ? "Male" : "Female",
                DateOfBirth = DateOnly.FromDateTime(dpDateOfBirth.SelectedDate.Value), // Chuyển đổi DateTime thành DateOnly
                Phone = txtPhone.Text,
                Idnumber = txtIDNumber.Text
            };

            using (var context = new Bonus2Context())
            {
                context.Employees.Add(newEmployee);
                context.SaveChanges();
            }

            Employees.Add(newEmployee);
            BtnReset_Click(sender, e);
        }

        private void BtnEdit_Click(object sender, RoutedEventArgs e)
        {
            if (EmployeeDataGrid.SelectedItem == null)
            {
                MessageBox.Show("Please select an employee to edit.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            var selectedEmployee = EmployeeDataGrid.SelectedItem as Employee;
            selectedEmployee.EmployeeName = txtName.Text;
            selectedEmployee.Gender = rbtnMale.IsChecked == true ? "Male" : "Female";
            selectedEmployee.DateOfBirth = DateOnly.FromDateTime(dpDateOfBirth.SelectedDate.Value); // Chuyển đổi DateTime thành DateOnly
            selectedEmployee.Phone = txtPhone.Text;
            selectedEmployee.Idnumber = txtIDNumber.Text;

            using (var context = new Bonus2Context())
            {
                context.Employees.Update(selectedEmployee);
                context.SaveChanges();
            }

            EmployeeDataGrid.Items.Refresh();
            BtnReset_Click(sender, e);
        }

        private void EmployeeDataGrid_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            if (EmployeeDataGrid.SelectedItem is Employee selectedEmployee)
            {
                txtId.Text = selectedEmployee.EmployeeId.ToString();
                txtName.Text = selectedEmployee.EmployeeName;
                rbtnMale.IsChecked = selectedEmployee.Gender == "Male";
                rbtnFemale.IsChecked = selectedEmployee.Gender == "Female";
                dpDateOfBirth.SelectedDate = selectedEmployee.DateOfBirth?.ToDateTime(TimeOnly.MinValue); // Chuyển đổi DateOnly thành DateTime
                txtPhone.Text = selectedEmployee.Phone;
                txtIDNumber.Text = selectedEmployee.Idnumber;
            }
        }

        private void BtnDelete_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (EmployeeDataGrid.SelectedItem == null)
                {
                    MessageBox.Show("Please select an employee to delete.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }

                var selectedEmployee = EmployeeDataGrid.SelectedItem as Employee;

                using (var context = new Bonus2Context())
                {
                    context.Employees.Remove(selectedEmployee);
                    context.SaveChanges();
                }

                Employees.Remove(selectedEmployee);
                BtnReset_Click(sender, e);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}