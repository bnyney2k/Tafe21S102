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
    //Tafe21S102(Contact_info) - Matt Gerlach

    public sealed partial class ContactInfo : Page
    {
        SQLiteConnection conn; // adding an SQLite connection
        string path = Path.Combine(Windows.Storage.ApplicationData.Current.LocalFolder.Path, "Findata.sqlite");

        public ContactInfo()
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
            conn.CreateTable<ContactDetail>();

            /// Refresh Data
            var query = conn.Table<ContactDetail>();
            ContactListView.ItemsSource = query.ToList();
        }

        //Clears fields
        private async void ClearFileds_Click(object sender, RoutedEventArgs e)
        {
            FirstNameText.Text = string.Empty;
            LastNameText.Text = string.Empty;
            CompanyName.Text = string.Empty;
            MobilePhone.Text = string.Empty;

            MessageDialog ClearDialog = new MessageDialog("Cleared", "information");
            await ClearDialog.ShowAsync();
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            Results();
        }

        //Add Button -  Add data
        private async void AppBarButton_Click_1(object sender, RoutedEventArgs e)
        {
            try
            {
                if (FirstNameText.Text.ToString() == "")
                {
                    MessageDialog dialog = new MessageDialog("No First Name entered", "Oops..!");
                    await dialog.ShowAsync();
                }
                else if(LastNameText.Text.ToString() == "")
                {
                    MessageDialog dialog = new MessageDialog("No Last Name entered", "Oops..!");
                    await dialog.ShowAsync();
                }
                else
                {
                    conn.Insert(new ContactDetail()
                    {
                        FirstName = FirstNameText.Text,
                        LastName = LastNameText.Text,
                        CompanyName = CompanyName.Text,
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
        //Update Button - updates Data
        private async void AppBarButton_Click_2(object sender, RoutedEventArgs e)
        {
            MessageDialog md;

            ContactDetail contactDetail = (ContactDetail)ContactListView.SelectedItem;

            contactDetail.FirstName = FirstNameText.Text;
            contactDetail.LastName = LastNameText.Text;
            contactDetail.CompanyName = CompanyName.Text;
            contactDetail.MobilePhone = MobilePhone.Text;


            if (conn.Update(contactDetail) > 0)
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


        //Delete Button - Deletes Data
        private async void AppBarButton_Click_3(object sender, RoutedEventArgs e)
        {
            try
            {
                string InfoSelection = ((ContactDetail)ContactListView.SelectedItem).FirstName;
                if (InfoSelection == "")
                {
                    MessageDialog dialog = new MessageDialog("Not selected the Item", "Oops..!");
                    await dialog.ShowAsync();
                }
                else
                {
                    conn.CreateTable<ContactDetail>();
                    var query1 = conn.Table<ContactDetail>();
                    var query3 = conn.Query<ContactDetail>("DELETE FROM ContactDetail WHERE FirstName ='" + InfoSelection + "'");
                    ContactListView.ItemsSource = query1.ToList();
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
