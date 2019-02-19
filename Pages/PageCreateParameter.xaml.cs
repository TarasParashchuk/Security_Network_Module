using Newtonsoft.Json;
using Security_network_module.CreateDataForGraph;
using Security_network_module.DataBase;
using Security_network_module.HelpFunction;
using Security_network_module.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace Security_network_module.Pages
{
    public partial class PageCreateParameter : UserControl
    {
        private SQLite.SQLiteConnection connection;
        private List<ModelGaps> list_settings_sensors;
        private ModelGaps item;
        private List<int> array_gaps;
        private DataBaseHelp db;
        private TextBox txtBox;

        public PageCreateParameter()
        {
            InitializeComponent();
            connection = new DataBaseConnection().SQLiteConnection();
            db = new DataBaseHelp(connection);
        }

        private void cb_DropDownOpened(object sender, EventArgs e)
        {
            var list_gaps = new List<string>();
            list_settings_sensors = db.GetItems<ModelGaps>();
            foreach (var item in list_settings_sensors)
                list_gaps.Add(item.Name_gaps + " " + item.Array_gaps);
            ComboBoxGaps.ItemsSource = list_gaps;
        }

        private void cb_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            item = list_settings_sensors.ElementAt(ComboBoxGaps.SelectedIndex);
            array_gaps = JsonConvert.DeserializeObject<List<int>>(item.Array_gaps);

            ChangeCountTextBox.Remove(GroupBoxExpert, "txtBoxExpertAssessments");
            ChangeCountTextBox.ExpertCreate(GroupBoxExpert, array_gaps.Count - 1);

            ChangeCountTextBox.Remove(GroupBoxFuzzyNumbers, "txtBoxNameFuzzyNumbers");
            ChangeCountTextBox.Gap(GroupBoxFuzzyNumbers, 1, array_gaps.Count - 1, "txtBoxNameFuzzyNumbers", 0, true);
        }

        private void Save_Click(object sender, RoutedEventArgs e)
        {
            if (ComboBoxGaps.Text == string.Empty)
                MessageBox.Show("Заповніть поля!", "Network Security Module", MessageBoxButton.OK, MessageBoxImage.Information);
            else
            {
                var count_const = array_gaps.Count - 1;

                var item_network_parameter = new ModelTabelNetworkParameter();
                var ListExpertAssessments = new List<NetworkParameter>();
                var ListAssessments = new List<double>();

                db.SaveItem(new ModelTabelParameter() { Name_parameter = ComboBoxParameter.Text, Id_gaps = item.Id}, false);

                for (var i = 1; i <= count_const * count_const; i++)
                {
                    txtBox = (TextBox)GroupBoxExpert.FindName("txtBoxExpertAssessments" + i);
                    ListAssessments.Add(Convert.ToDouble(txtBox.Text));

                    if (i % count_const == 0)
                    {
                        txtBox = (TextBox)GroupBoxExpert.FindName("txtBoxNameFuzzyNumbers" + (i / count_const));
                        ListExpertAssessments.Add(new NetworkParameter { Name = txtBox.Text, Array_coordinates = ListAssessments });
                        ListAssessments = new List<double>();
                    }
                }

                ListExpertAssessments = ConstantCoordinates.GetConstantCoordinates(ListExpertAssessments, array_gaps);

                var Id_parameter = db.GetItems<ModelTabelParameter>().Last().Id;

                foreach (var i in ListExpertAssessments)
                { 
                    item_network_parameter.Array_coordinates = JsonConvert.SerializeObject(i);
                    item_network_parameter.Id_Parameter = Id_parameter;
                    db.SaveItem(item_network_parameter, false);
                }
            }
        }
    }
}
