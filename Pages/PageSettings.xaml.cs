using Security_network_module.DataBase;
using Security_network_module.HelpFunction;
using Security_network_module.Model;
using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using Newtonsoft.Json;

namespace Security_network_module.Pages
{
    public partial class PageSettings : UserControl
    {
        private SQLite.SQLiteConnection connection;
        private DataBaseHelp db;
        private int count_txtbox = 3;

        public PageSettings()
        {
            InitializeComponent();
            connection = new DataBaseConnection().SQLiteConnection();
            db = new DataBaseHelp(connection);
            ListViewParameter.ItemsSource = db.GetItems<ModelGaps>();
            ChangeCountTextBox.Gap(GroupBoxCountClient, 1, 4, "txtBox", 7, true);
        }

        private void Save_Click(object sender, RoutedEventArgs e)
        {
            if (Name_gaps.Text == string.Empty)
                MessageBox.Show("Заповніть поля!", "Network Security Module", MessageBoxButton.OK, MessageBoxImage.Information);
            else
            {
                TextBox txtBox;

                var array_gaps = new List<int>();
                for (var i = 1; i <= count_txtbox + 1; i++)
                {
                    txtBox = (TextBox)GroupBoxCountClient.FindName("txtBox" + i);
                    array_gaps.Add(Convert.ToInt32(txtBox.Text));
                }

                var item_parameter = new ModelGaps()
                {
                    Name_gaps = Name_gaps.Text,
                    Array_gaps = JsonConvert.SerializeObject(array_gaps)
                };
                db.SaveItem(item_parameter, false);
                ListViewParameter.ItemsSource = db.GetItems<ModelGaps>();
            }
        }

        private void Add_Click(object sender, RoutedEventArgs e)
        {
            var current_count_textbox = Convert.ToInt32(numericUpDown.Value);
            if (count_txtbox < current_count_textbox)
            {
                ChangeCountTextBox.Gap(GroupBoxCountClient, count_txtbox + 2, current_count_textbox + 1, "txtBox", 7, true);
            }
            else
            {
                if (count_txtbox > current_count_textbox)
                {
                    ChangeCountTextBox.Gap(GroupBoxCountClient, current_count_textbox + 2, count_txtbox + 1, "txtBox", 7, false);
                }
                else return;
            }
            count_txtbox = current_count_textbox;
        }

        private void numericUpDown_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            if (e.NewValue < 3)
                numericUpDown.Value = 3;
            if (e.NewValue > 16)
                numericUpDown.Value = 15;
        }

        private void Save_Settings_Click(object sender, RoutedEventArgs e)
        {
            
        }

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            
        }
    }
}
