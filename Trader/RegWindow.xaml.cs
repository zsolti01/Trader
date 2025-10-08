using System;
using System.Collections.Generic;
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

namespace Trader
{
    /// <summary>
    /// Interaction logic for RegWindow.xaml
    /// </summary>
    public partial class RegWindow : Window
    {
        private readonly DatabaseStatements db = new DatabaseStatements();

        public RegWindow()
        {
            InitializeComponent();
        }

        private void regButton_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = new MainWindow();
            mainWindow.Show();
            this.Close();

            if (passwordTextBox.Password == passwordAgainTextBox.Password)
            {
                var user = new
                {
                    UserName = userNameTextBox.Text,
                    FullName = fullNameTextBox.Text,
                    UserPassword = passwordTextBox.Password,
                    Salt = "",
                    Email = emailTextBox.Text
                };
                MessageBox.Show(db.AddNewUser(user).ToString());
            }
            else
            {
                MessageBox.Show("Eltérő jelszavak!");
            }
        }

        private void exit_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void toLogIn_Click(object sender, RoutedEventArgs e)
        {
            LogWindow logWindow = new LogWindow();
            logWindow.Show();
            this.Close();
        }
    }
}
