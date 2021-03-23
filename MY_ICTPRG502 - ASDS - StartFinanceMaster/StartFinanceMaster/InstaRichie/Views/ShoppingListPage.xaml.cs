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
    //Tafe21S102(Shopping List) - Jinghua Zhong

    public sealed partial class ShoppingListPage : Page
    {
        SQLiteConnection conn; // adding an SQLite connection
        string path = Path.Combine(Windows.Storage.ApplicationData.Current.LocalFolder.Path, "Findata.sqlite");

        public ShoppingListPage()
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
            conn.CreateTable<ShoppingList>();

            /// Refresh Data
            var query = conn.Table<ShoppingList>();
            ShoppingListView.ItemsSource = query.ToList();
        }

        //Clears fields
        private async void ClearFileds_Click(object sender, RoutedEventArgs e)
        {
            ShoppingItemIDText.Text = string.Empty;
            ShopNameText.Text = string.Empty;
            NameOfItemText.Text = string.Empty;
            ShoppingDateText.Text = string.Empty;
            
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
                if (ShopNameText.Text.ToString() == "")
                {
                    MessageDialog dialog = new MessageDialog("Please enter Shopping Item ID");
                    await dialog.ShowAsync();
                }
                else
                {
                    conn.Insert(new ShoppingList()
                    {
                        ShopName = ShopNameText.Text,
                        NameOfItem = NameOfItemText.Text,
                      
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
                    MessageDialog dialog = new MessageDialog("A Similar Shopping Item ID already exists, Try a different ID", "Oops..!");
                    await dialog.ShowAsync();
                }
            }
        }
        //Update Button - updates Data
        private async void AppBarButton_Click_2(object sender, RoutedEventArgs e)
        {
            MessageDialog md;


            ShoppingList shoppingList = (ShoppingList)ShoppingListView.SelectedItem;

            shoppingList.ShopName = ShopNameText.Text;
            shoppingList.NameOfItem = NameOfItemText.Text;


            if (conn.Update(shoppingList) > 0)
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
                string InfoSelection = ((ShoppingList)ShoppingListView.SelectedItem).ShopName;
                if (InfoSelection == "")
                {
                    MessageDialog dialog = new MessageDialog("Not selected the Item", "Oops..!");
                    await dialog.ShowAsync();
                }
                else
                {
                    conn.CreateTable<ShoppingList>();
                    var query1 = conn.Table<ShoppingList>();
                    var query3 = conn.Query<ShoppingList>("DELETE FROM Shopping List WHERE ShopName ='" + InfoSelection + "'");
                    ShoppingListView.ItemsSource = query1.ToList();
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