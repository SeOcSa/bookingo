using bookingo.Classes;
using MahApps.Metro.Controls;
using MahApps.Metro.Controls.Dialogs;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace bookingo
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : MetroWindow
    {
        #region Cons
        String ConnectionString = @"Server=.\SQLEXPRESS;Database=bookingo;Trusted_Connection=Yes;";
        String EmptyString = String.Empty;
        String content = "Password requirements:\nminimum 6 chars length;\n" +
               "1 special char;\n" +
               "1 digit;\n" +
               "1 uppercase.\n" +
               "Letter is mandatory in the password.";
        #endregion

        User activeUser;
        List<User> allUsers;
        public MainWindow()
        {
            InitializeComponent();
            this.DataContext = content;
            
        }

        private void getAllUser()
        {
            String command = "SELECT * FROM [bookingo].[dbo].[User]";
            DatabaseConnection dbConnection = new DatabaseConnection(ConnectionString, command);
            allUsers = dbConnection.getAllUser();
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            windowsView.SelectedIndex = 0;
            clearSelection();
        }
        private void back_Click(object sender, RoutedEventArgs e)
        {
            windowsView.SelectedIndex = 1;
        }
        private void loginButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                String command = "SELECT [UserName], [JobTitle] FROM [bookingo].[dbo].[User] WHERE [UserName]='" +
                       userTxt.Text.ToString() + "'" + " AND [Password]=" + "'" + passwordTxt.Password + "'";
                try
                {
                    DatabaseConnection dbConnection = new DatabaseConnection(ConnectionString, command);
                    activeUser = dbConnection.getLoginUser();

                    if (dbConnection.getStatus() == true)
                    {
                        windowsView.SelectedIndex = 2;
                        command = "SELECT * FROM [bookingo].[dbo].[Rooms]";
                        dbConnection = new DatabaseConnection(ConnectionString, command);
                        insertRooms(dbConnection.getRooms().allRooms());
                    }
                    else
                    {
                        this.ShowMessageAsync("Login error", "Invalid password or username.\nPlease Try again!");
                    }
                }
                catch(Exception exce)
                {
                    this.ShowMessageAsync("Database command  error: \n", exce.Message, MessageDialogStyle.Affirmative);
                }

            }
            catch (Exception exce)
            {
                this.ShowMessageAsync("Database connection error: \n", exce.Message,MessageDialogStyle.Affirmative);
            }


            // this.listBox.Items.Add(new Room("Sala: A22", "Capacitatea: 20", "Etaj: 3", "Accesorii: projector, TV", "/imgSource/Meeting_Room.png", false));
        }
        private void insertRooms(List<Room> rooms)
        {
            foreach(var room in rooms)
            {
                this.listBox.Items.Add(room);
            }
        }
        private void addBut_Click(object sender, RoutedEventArgs e)
        {
            windowsView.SelectedIndex = 3;
            
        }
        private void access_Click(object sender, RoutedEventArgs e)
        {
            String command = "SELECT [UserName], [JobTitle] FROM [bookingo].[dbo].[User] WHERE [UserName]='" +
                      userName.Text.ToString() + "'";

            try
            {
                DataValidation dataV = new DataValidation(ConnectionString, command);
                dataV.testUserName(userName.Text);
                dataV.testPassword(userPassword.Password);
                dataV.confirmPassword(userPassword.Password, confirmPassword.Password);
                dataV.testJobTitle(userRang.SelectedIndex.ToString());
                String addValue = userRang.SelectedValue.ToString() ;

                command = "INSERT INTO [bookingo].[dbo].[User] VALUES('" +
                      userName.Text.ToString() + "',"
                      + "'" + userPassword.Password + "',"
                      + "'" + addValue + "')";

                DatabaseConnection dbConnection = new DatabaseConnection(ConnectionString, command);
                dbConnection.queryCommandType();

                this.ShowMessageAsync("Succes", "User has been registered.");
                clearSelection();
            }
            catch (Exception ex)
            {
                this.ShowMessageAsync("Error", ex.Message);
            }
        }
        public void clearSelection()
        {
            userName.Clear();
            userName.BorderBrush = Brushes.White;

            userPassword.Clear();
            userPassword.BorderBrush = Brushes.White;

            confirmPassword.Clear();
            confirmPassword.BorderBrush = Brushes.White;

            userRang.SelectedIndex = -1;
        }
        private void userName_TextChanged(object sender, TextChangedEventArgs e)
        {
            TextBox getSender = (TextBox)sender;
            if(getSender.Text.Length<3)
            {
                getSender.BorderBrush = Brushes.Red;
            }
            else
            {
                getSender.BorderBrush = Brushes.Green;
            }
        }
        private void confirmPassword_PasswordChanged(object sender, RoutedEventArgs e)
        {
            if (!userPassword.Password.Equals(confirmPassword.Password))
            {
                confirmPassword.BorderBrush = Brushes.Red;
            }
            else
            {
                if (!(userPassword.Password.Equals(String.Empty)) && !(confirmPassword.Password.Equals(String.Empty)))
                {
                    confirmPassword.BorderBrush = Brushes.Green;
                    userPassword.BorderBrush = Brushes.Green;
                }
            }
        }
        private void listBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                HistoryListBox.Items.Clear();
                String roomName = ((Room)(((ListBox)sender).SelectedItem)).Name;
                String[] splits = roomName.Split(' ');
                String bookCommand = "SELECT * FROM [bookingo].[dbo].[Booking] WHERE [RoomName]='" + splits[2] + "'";
                getAllUser();
                DatabaseConnection dbConnection = new DatabaseConnection(ConnectionString, bookCommand);
                dbConnection.setAllUser(allUsers);
                insertBookings(dbConnection.getBooking());
            }
            catch (Exception ex)
            {
                this.ShowMessageAsync("Error", ex.Message);
            }
        }
        private void insertBookings(List<Booking> booking)
        {
            foreach(var book in booking)
            {
                this.HistoryListBox.Items.Add(book);
            }
        }
        private async void addBooking_Click(object sender, RoutedEventArgs e)
        {
            if(listBox.SelectedIndex!=-1)
            {
                Room room = (Room)listBox.SelectedItem;
                String roomName= room.Name.Split(' ')[2];
                String userName = activeUser.Name; //TO DO: getUser name from login user

                DateTime startT = startTime.SelectedDate.Value;
                String sqlStartDateTime = startT.ToString("yyyy-MM-dd HH:mm:ss.fff");

                DateTime endT = endTime.SelectedDate.Value;
                String sqlEndDateTime = endT.ToString("yyyy-MM-dd HH:mm:ss.fff");

                DateTime sqlstartTime = Convert.ToDateTime(sqlStartDateTime);
                DateTime sqlEndTime = Convert.ToDateTime(sqlEndDateTime);

                try
                {
                    String bookCommand = "SELECT * FROM [bookingo].[dbo].[Booking] WHERE [RoomName]='" + roomName + "'";

                    DataValidation datV = new DataValidation(ConnectionString, bookCommand);

                    datV.selectedDateTime(startT, endT);

                    datV.testFreePosition(sqlstartTime, sqlEndTime);

                    MessageDialogResult result = await this.ShowMessageAsync("Question", "Are you sure to booking room: " + roomName +
                    " from date: " + startT.ToString() + " to date: " + endT.ToString(),
                    MessageDialogStyle.AffirmativeAndNegative);


                    if (result == MessageDialogResult.Affirmative)
                    {
                        try
                        {
                            String command = "INSERT INTO [bookingo].[dbo].[Booking] VALUES (" + "'" +
                                userName + "', " + "'" +
                                roomName + "', " + "'" +
                                sqlStartDateTime + "'," + "'" +
                                sqlEndDateTime + "')";

                            DatabaseConnection dbConnection = new DatabaseConnection(ConnectionString, command);
                            dbConnection.queryCommandType();

                            await this.ShowMessageAsync("Success", "Booking completed.");
                        }
                        catch (Exception ex)
                        {
                            await this.ShowMessageAsync("Error", ex.Message);
                        }
                    }
                }
                catch (Exception ex)
                {
                    await this.ShowMessageAsync("Error", ex.Message);
                }
            }
            else
            {
               await this.ShowMessageAsync("Information", "Select a room from the list and try again");
            }
        }
        private async void updateButt_Click(object sender, RoutedEventArgs e)
        {
            if (activeUser.JobTitle == "CEO")
            {
                String commad = "Update []";
                DatabaseConnection dbConnection = new DatabaseConnection(ConnectionString, commad);
            }
            else if (((Booking)HistoryListBox.SelectedItem).User.Name == activeUser.Name)
            {
                String commad = "Update []";
                DatabaseConnection dbConnection = new DatabaseConnection(ConnectionString, commad);
            }
            else
                await this.ShowMessageAsync("Error","No access permision.");
        }
        private async void removeButt_Click(object sender, RoutedEventArgs e)
        {
            Booking book = ((Booking)HistoryListBox.SelectedItem);
            String commad = "DELETE FROM [bookingo].[dbo].[User] WHERE" +
                    "[UserName]='" + activeUser.Name + "' AND " +
                    "[RoomName]='" + book._RoomName + "' AND " +
                    "[StartTime]=" + book.StartDate + "AND" +
                    "[EndTime]=" + book.EndDate;

            if (activeUser.JobTitle == "CEO")
            {
                DatabaseConnection dbConnection = new DatabaseConnection(ConnectionString, commad);
                dbConnection.queryCommandType();
            }
            else if (book.User.Name == activeUser.Name)
            {
                DatabaseConnection dbConnection = new DatabaseConnection(ConnectionString, commad);
                dbConnection.queryCommandType();
            }
            else
                await this.ShowMessageAsync("Error", "No access permision.");
        }
    }
}
