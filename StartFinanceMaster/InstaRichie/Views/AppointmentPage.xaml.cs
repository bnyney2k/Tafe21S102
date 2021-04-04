// Program: Start Finance
// Author: Zheru Chen
// Date: 03 April 2021
// Responsbility Part: Appointment 

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
    public sealed partial class AppointmentPage : Page
    {
        SQLiteConnection conn; // adding an SQLite connection
        string path = Path.Combine(Windows.Storage.ApplicationData.Current.LocalFolder.Path, "Findata.sqlite");

        public AppointmentPage()
        {
            this.InitializeComponent();
            NavigationCacheMode = Windows.UI.Xaml.Navigation.NavigationCacheMode.Enabled;
            /// Initializing a database
            conn = new SQLite.Net.SQLiteConnection(new SQLite.Net.Platform.WinRT.SQLitePlatformWinRT(), path);
            // Creating table
            conn.CreateTable<Appointments>();
            // gets current date and time
            //AddStartTime.Time = TimePickerSelectedValueChangedEventArgs;
        }



        private void Update_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Delete_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Add_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Pivot_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            int num = AppointmentTypeContent.SelectedIndex;
            if (num == 0)
            {
                AddFooter.Visibility = Visibility.Visible;
                DeleteFooter.Visibility = Visibility.Collapsed;
                UpdateFooter.Visibility = Visibility.Collapsed;
            }
            else if (num == 1)
            {
                AddFooter.Visibility = Visibility.Collapsed;
                DeleteFooter.Visibility = Visibility.Visible;
                UpdateFooter.Visibility = Visibility.Collapsed;
            }
            else
            {
                AddFooter.Visibility = Visibility.Collapsed;
                DeleteFooter.Visibility = Visibility.Collapsed;
                UpdateFooter.Visibility = Visibility.Visible;
            }
        }
    }
}
