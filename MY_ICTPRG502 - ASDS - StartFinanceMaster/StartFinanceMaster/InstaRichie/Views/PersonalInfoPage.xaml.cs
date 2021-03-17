using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using SQLite;
using StartFinance.Models;
using Windows.UI.Popups;
using SQLite.Net;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace StartFinance.Views
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class PersonalInfoPage : Page
    {
        SQLiteConnection conn; // adding an SQLite connection
        string path = Path.Combine(Windows.Storage.ApplicationData.Current.LocalFolder.Path, "Findata.sqlite");

        public PersonalInfoPage()
        {
            this.InitializeComponent();
            NavigationCacheMode = Windows.UI.Xaml.Navigation.NavigationCacheMode.Enabled;
            /// Initializing a database
            conn = new SQLite.Net.SQLiteConnection(new SQLite.Net.Platform.WinRT.SQLitePlatformWinRT(), path);
            Results();
        }

        public void Results()
        {
            // Creating table
            conn.CreateTable<PersonalInfo>();

            /// Refresh Data
            var query = conn.Table<PersonalInfo>();
            PersonalInfoListView.ItemsSource = query.ToList();
        }

       

        private async void ClearFileds_Click(object sender, RoutedEventArgs e)
        {
            FirstNameText.Text = string.Empty;
            LastNameText.Text = string.Empty;
            EmailText.Text = string.Empty;
            MobilePhone.Text = string.Empty;

            MessageDialog ClearDialog = new MessageDialog("Cleared", "information");
            await ClearDialog.ShowAsync();
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            Results();
        }

      
        //Add Button
        private async void AppBarButton_Click_1(object sender, RoutedEventArgs e)
        {
            try
            {
                if (FirstNameText.Text.ToString() == "")
                {
                    MessageDialog dialog = new MessageDialog("No First Name entered", "Oops..!");
                    await dialog.ShowAsync();
                }
                else
                {
                    conn.Insert(new PersonalInfo()
                    {
                        FirstName = FirstNameText.Text,
                        LastName = LastNameText.Text,
                        Email = EmailText.Text,
                        MobilePhone = MobilePhone.Text
                    });
                    Results();
                }
            }
            catch (Exception ex)
            {
                if (ex is FormatException)
                {
                    MessageDialog dialog = new MessageDialog("You forgot to enter the Value or entered an invalid data", "Oops..!");
                    await dialog.ShowAsync();
                }
                else if (ex is SQLiteException)
                {
                    MessageDialog dialog = new MessageDialog("A Similar Personal ID already exists, Try a different ID", "Oops..!");
                    await dialog.ShowAsync();
                }
            }
        }
        //Update Button
        private async void AppBarButton_Click_2(object sender, RoutedEventArgs e)
        {
            MessageDialog md;

            PersonalInfo personalInfo = (PersonalInfo)PersonalInfoListView.SelectedItem;

            personalInfo.FirstName = FirstNameText.Text;
            personalInfo.LastName = LastNameText.Text;
            personalInfo.Email = EmailText.Text;
            personalInfo.MobilePhone = MobilePhone.Text;


            if (conn.Update(personalInfo) > 0)
            {
                Results();
                md = new MessageDialog("Update Successful");
            }
            else
            {
                md = new MessageDialog("Error! Invalid data, Update failed");
            }

            await md.ShowAsync();
        }


            //Delete Button
            private async void AppBarButton_Click_3(object sender, RoutedEventArgs e)
        {
            try
            {
                string InfoSelection = ((PersonalInfo)PersonalInfoListView.SelectedItem).FirstName;
                if (InfoSelection == "")
                {
                    MessageDialog dialog = new MessageDialog("Not selected the Item", "Oops..!");
                    await dialog.ShowAsync();
                }
                else
                {
                    conn.CreateTable<PersonalInfo>();
                    var query1 = conn.Table<PersonalInfo>();
                    var query3 = conn.Query<PersonalInfo>("DELETE FROM PersonalInfo WHERE FirstName ='" + InfoSelection + "'");
                    PersonalInfoListView.ItemsSource = query1.ToList();
                }
            }
            catch (NullReferenceException)
            {
                MessageDialog dialog = new MessageDialog("Not selected the Item", "Oops..!");
                await dialog.ShowAsync();
            }

        }
    }
}
