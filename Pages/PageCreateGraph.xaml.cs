using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Security_network_module.CreateDataForGraph;
using Security_network_module.DataBase;
using Security_network_module.DrawObjectCanvas;
using Security_network_module.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using Security_network_module.GetCurrentStateSystem;
using System.Windows.Media;
using Security_network_module.Crossing;
using System.ComponentModel;
using System.Threading;

namespace Security_network_module.Pages
{
    /// <summary>
    /// Логика взаимодействия для PageCreateGraph.xaml
    /// </summary>
    public partial class PageCreateGraph : UserControl
    {
        public static string NameCountConnectClients = "КОП";
        public static string NameCountSameAdrress = "КПОА";
        ModelTabelParameter item_param1;
        ModelTabelParameter item_param2;
        BackgroundWorker get_currentstateserver;
        List<List<int>> List_CountConnectClients = new List<List<int>>()
        {
            new List<int>() { 17, 19, 30, 102, 70, 258, 225, 294, 181, 205, 170, 253, 281, 164, 208, 247, 125, 266, 273, 285, 230, 141, 79, 205, 113, 175, 144, 168, 169, 288, 234, 234, 180, 266, 195, 193, 208, 279, 283, 161, 161, 81, 74, 41, 51, 158, 26, 235, 198, 178, 167, 114, 253, 276, 160, 289, 163, 174, 174, 174 },
            new List<int>() { 107, 109, 130, 102, 170, 258, 225, 294, 181, 205, 170, 253, 281, 164, 208, 347, 325, 366, 373, 385, 330, 241, 279, 205, 413, 675, 644, 668, 669, 688, 634, 634, 680, 466, 495, 493, 208, 279, 283, 161, 161, 81, 74, 41, 51, 158, 26, 235, 198, 178, 167, 114, 253, 276, 160, 289, 163, 174, 174, 174 },
            new List<int>()  { 17, 19, 30, 52, 30, 25, 22, 29, 18, 25, 17, 25, 28, 16, 20, 24, 12, 26, 27, 28, 23, 14, 79, 80, 90, 75, 44, 68, 69, 88, 73, 94, 98, 96, 109, 109, 108, 107, 102, 101, 101, 81, 94, 91, 91, 158, 126, 135, 128, 128, 127, 124, 123, 126, 123, 129, 123, 124, 124, 134 },
            new List<int>()  { 47, 39, 40, 42, 40, 45, 42, 49, 48, 45, 47, 45, 48, 66, 60, 64, 62, 66, 67, 68, 63, 64, 69, 60, 60, 65, 64, 68, 69, 28, 23, 24, 26, 26, 32, 32, 32, 32, 32, 31, 31, 31, 31, 31, 31, 26, 26, 25, 28, 28, 27, 24, 23, 26, 23, 29, 23, 24, 24, 24 },
            new List<int>()  { 7, 9, 9, 9, 5, 5, 5, 9, 8, 5, 7, 5, 8, 6, 5, 4, 6, 6, 7, 8, 6, 4, 9, 6, 6, 5, 6, 6, 6, 8, 7, 7, 6, 6, 7, 7, 7, 7, 5, 7, 7, 7, 6, 6, 6, 6, 6, 5, 8, 8, 9, 9, 9, 6, 7, 3, 3, 4, 4, 4 },
            new List<int>() { 777, 779, 779, 779, 775, 775, 775, 779, 778, 775, 777, 775, 788, 786, 785, 784, 786, 786, 787, 788, 786, 784, 789, 786, 786, 795, 796, 796, 796, 798, 797, 797, 846, 846, 847, 847, 847, 847, 845, 847, 847, 847, 846, 846, 846, 846, 846, 845, 848, 848, 849, 849, 849, 856, 857, 853, 853, 884, 884, 884 },
            new List<int>() { 777, 779, 779, 779, 775, 775, 775, 779, 778, 775, 777, 775, 788, 786, 785, 784, 786, 786, 787, 788, 786, 784, 789, 786, 786, 795, 796, 796, 796, 798, 797, 797, 746, 746, 747, 747, 747, 747, 745, 747, 747, 747, 746, 746, 746, 746, 746, 745, 748, 748, 749, 749, 749, 756, 757, 753, 753, 784, 784, 784 },
            new List<int>() { 513, 513, 513, 513, 513, 513, 512, 513, 513, 513, 513, 513, 512, 512, 512, 512, 512, 512, 512, 512, 513, 509, 510, 509, 509, 511, 510, 510, 510, 512, 513, 512, 512, 512, 515, 515, 513, 514, 514, 514, 513, 513, 513, 513, 513, 511, 511, 511, 511, 511, 512, 512, 512, 512, 512, 512, 512, 512, 512, 512 },
            new List<int>() { 213, 213, 213, 213, 213, 213, 212, 213, 113, 113, 113, 113, 182, 172, 172, 172, 172, 172, 172, 172, 213, 209, 110, 109, 109, 111, 110, 110, 110, 112, 113, 112, 112, 112, 115, 115, 113, 114, 114, 114, 213, 213, 213, 213, 213, 211, 211, 211, 211, 221, 222, 222, 222, 222, 222, 212, 212, 212, 212, 212 },
            new List<int>()  { 17, 19, 30, 52, 30, 25, 22, 29, 18, 25, 17, 25, 28, 16, 20, 24, 12, 26, 27, 28, 23, 14, 79, 80, 90, 75, 44, 68, 69, 88, 73, 94, 98, 96, 109, 109, 108, 107, 102, 101, 101, 81, 94, 91, 91, 158, 126, 135, 128, 128, 127, 124, 123, 126, 123, 129, 123, 124, 124, 134 },
            new List<int>()  { 47, 39, 40, 42, 40, 45, 42, 49, 48, 45, 47, 45, 48, 66, 60, 64, 62, 66, 67, 68, 63, 64, 69, 60, 60, 65, 64, 68, 69, 28, 23, 24, 26, 26, 32, 32, 32, 32, 32, 31, 31, 31, 31, 31, 31, 26, 26, 25, 28, 28, 27, 24, 23, 26, 23, 29, 23, 24, 24, 24 }
        };

        List<List<int>> List_CountSameAddress = new List<List<int>>()
        {
            new List<int>() { 82, 89, 95, 92, 96, 88, 86, 81, 99, 88, 99, 85, 524, 539, 543, 518, 542, 551, 540, 541, 554, 540, 537, 554, 543, 532, 564, 511, 563, 539, 536, 512, 562, 559, 541, 519, 559, 527, 549, 514, 841, 860, 857, 857, 850, 845, 849, 844, 845, 847, 845, 845, 843, 846, 847, 858, 867, 867, 856, 840 },
            new List<int>() { 82, 89, 95, 92, 96, 88, 86, 81, 99, 88, 99, 85, 524, 539, 543, 518, 542, 551, 540, 541, 554, 540, 537, 554, 543, 532, 564, 511, 563, 539, 536, 512, 562, 559, 541, 519, 559, 527, 549, 514, 541, 660, 657, 657, 650, 665, 659, 644, 645, 667, 645, 645, 563, 546, 547, 558, 367, 367, 356, 540 },
            new List<int>() { 82, 89, 95, 92, 96, 88, 86, 81, 99, 88, 99, 85, 82, 89, 95, 92, 96, 88, 86, 81, 99, 88, 99, 85, 84, 82, 89, 95, 92, 96, 88, 86, 81, 99, 88, 99, 85, 82, 89, 95, 92, 96, 98, 96, 91, 99, 98, 99, 95, 98, 99, 98, 100, 100, 100, 100, 102, 103, 104, 102},
            new List<int>() { 182, 189, 195, 192, 196, 288, 286, 181, 199, 188, 199, 185, 124, 139, 143, 118, 142, 151, 140, 141, 154, 140, 137, 254, 143, 132, 164, 111, 163, 139, 136, 172, 162, 159, 141, 119, 259, 127, 149, 114, 141, 160, 157, 157, 150, 165, 159, 144, 145, 167, 145, 145, 163, 146, 147, 158, 167, 167, 156, 140 },
            new List<int>() { 382, 389, 395, 392, 396, 388, 386, 381, 399, 388, 399, 385, 324, 339, 343, 318, 342, 351, 340, 341, 354, 340, 337, 354, 343, 332, 364, 331, 363, 339, 336, 372, 362, 359, 341, 319, 359, 327, 349, 314, 341, 360, 357, 357, 350, 365, 359, 344, 345, 367, 345, 345, 363, 346, 347, 358, 367, 367, 356, 340 },
            new List<int>()  { 47, 39, 40, 42, 40, 45, 42, 49, 48, 45, 47, 45, 48, 66, 60, 64, 62, 66, 67, 68, 63, 64, 69, 60, 60, 65, 64, 68, 69, 28, 23, 24, 26, 26, 32, 32, 32, 32, 32, 31, 31, 31, 31, 31, 31, 26, 26, 25, 28, 28, 27, 24, 23, 26, 23, 29, 23, 24, 24, 24 },
            new List<int>()  { 7, 9, 9, 9, 5, 5, 5, 9, 8, 5, 7, 5, 8, 6, 5, 4, 6, 6, 7, 8, 6, 4, 9, 6, 6, 5, 6, 6, 6, 8, 7, 7, 6, 6, 7, 7, 7, 7, 5, 7, 7, 7, 6, 6, 6, 6, 6, 5, 8, 8, 9, 9, 9, 6, 7, 3, 3, 4, 4, 4 },
            new List<int>() { 777, 779, 779, 779, 775, 775, 775, 779, 778, 775, 777, 775, 788, 786, 785, 784, 786, 786, 787, 788, 786, 784, 789, 786, 786, 795, 796, 796, 796, 798, 797, 797, 846, 846, 847, 847, 847, 847, 845, 847, 847, 847, 846, 846, 846, 846, 846, 845, 848, 848, 849, 849, 849, 856, 857, 853, 853, 884, 884, 884 },
            new List<int>() { 777, 779, 779, 779, 775, 775, 775, 779, 778, 775, 777, 775, 788, 786, 785, 784, 786, 786, 787, 788, 786, 784, 789, 786, 786, 795, 796, 796, 796, 798, 797, 797, 746, 746, 747, 747, 747, 747, 745, 747, 747, 747, 746, 746, 746, 746, 746, 745, 748, 748, 749, 749, 749, 756, 757, 753, 753, 784, 784, 784 },
            new List<int>() { 513, 513, 513, 513, 513, 513, 512, 513, 513, 513, 513, 513, 512, 512, 512, 512, 512, 512, 512, 512, 513, 509, 510, 509, 509, 511, 510, 510, 510, 512, 513, 512, 512, 512, 515, 515, 513, 514, 514, 514, 513, 513, 513, 513, 513, 511, 511, 511, 511, 511, 512, 512, 512, 512, 512, 512, 512, 512, 512, 512 },
            new List<int>() { 213, 213, 213, 213, 213, 213, 212, 213, 113, 113, 113, 113, 182, 172, 172, 172, 172, 172, 172, 172, 213, 209, 110, 109, 109, 111, 110, 110, 110, 112, 113, 112, 112, 112, 115, 115, 113, 114, 114, 114, 213, 213, 213, 213, 213, 211, 211, 211, 211, 221, 222, 222, 222, 222, 222, 212, 212, 212, 212, 212 }
        };
        public static double koef = 0.8;

        private SQLite.SQLiteConnection connection;
        private DataBaseHelp db;
        
        private List<ModelTabelParameter> itemsParameterCountConnectClients;
        private List<ModelTabelParameter> itemsParameterCountSameAdrress;
        private List<double> IntervalForCountConnectClients;
        private List<double> IntervalForCountSameAdrress;

        public PageCreateGraph()
        {
            InitializeComponent();

            connection = new DataBaseConnection().SQLiteConnection();
            db = new DataBaseHelp(connection);
        }

        private void Param1_cb_DropDownOpened(object sender, EventArgs e)
        {
            itemsParameterCountConnectClients = db.GetItems<ModelTabelParameter>().Where(w => w.Name_parameter.Contains(NameCountConnectClients)).ToList();
            GetNameParameter(itemsParameterCountConnectClients, param1_cb);
        }

        private void Param2_cb_DropDownOpened(object sender, EventArgs e)
        {
            itemsParameterCountSameAdrress = db.GetItems<ModelTabelParameter>().Where(w => w.Name_parameter.Contains(NameCountSameAdrress)).ToList();
            GetNameParameter(itemsParameterCountSameAdrress, param2_cb);
        }

        private void GetNameParameter(IEnumerable<ModelTabelParameter> list, ComboBox comboBox)
        {
            var items = new List<string>();
            foreach (var item in list)
            {
                items.Add(item.Name_parameter + " " + db.GetItem<ModelGaps>(item.Id_gaps).Array_gaps);
            }
            comboBox.ItemsSource = items;
        }

        private void cb1_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            item_param1 = itemsParameterCountConnectClients.ElementAt(param1_cb.SelectedIndex);
            
        }

        private void cb2_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            item_param2 = itemsParameterCountSameAdrress.ElementAt(param2_cb.SelectedIndex);
        }

        private List<NetworkParameter> ComboBoxSelectionChanged(ModelTabelParameter item, List<int> DataSensors, DataGrid dataGrid)
        {
            var ListNetworkParameter = db.GetItems<ModelTabelNetworkParameter>().Where(w => w.Id_Parameter == item.Id).Select(s => s.Array_coordinates).ToList();
            var ListConstantCoordinates = GetConstantCoordinatesNetwork(ListNetworkParameter, dataGrid);
            var array_gaps = JsonConvert.DeserializeObject<List<int>>(db.GetItem<ModelGaps>(item.Id_gaps).Array_gaps);

            var CurrentStateSystem = new NetworkParameter()
            {
                Name = "P-" + item.Name_parameter,
                Array_coordinates = CurrentCoordinates.GetCurrentStateSystemCoordinatess(GetConstantCoordinatesNetwork(ListNetworkParameter, dataGrid), GetArray(DataSensors, array_gaps))
            };

            ListConstantCoordinates.Insert(0, CurrentStateSystem);

            return ListConstantCoordinates;
        }

        private List<NetworkParameter> GetConstantCoordinatesNetwork(IEnumerable<string> ListNetworkParameter, DataGrid datagrid)
        {
            var ItemsSourceComboBox = new List<DataGridNetworkParameter>();
            var ListConstantCoordinates = new List<NetworkParameter>();
            foreach (var item in ListNetworkParameter)
            {
                var jObject = JObject.Parse(item);
                ListConstantCoordinates.Add(JsonConvert.DeserializeObject<NetworkParameter>(item));
                ItemsSourceComboBox.Add(new DataGridNetworkParameter() { Name = jObject.SelectToken("Name").ToString(), Array_coordinates = jObject.SelectToken("Array_coordinates").ToString().Replace(Environment.NewLine, "") });
            };

            datagrid.ItemsSource = ItemsSourceComboBox;
            return ListConstantCoordinates;
        }

        private int[] GetArray(List<int> list, List<int> array)
        {
            var array_sensors = new int[array.Count - 1];
            for (var i = 0; i < array.Count - 1; i++)
            {
                for (var j = 0; j < list.Count; j++)
                {
                    if (array[i] <= list[j] && array[i + 1] > list[j])
                        array_sensors[i] += 1;
                }
            }
            return array_sensors;
        }

        private List<NetworkParameter> ConvertListForCanvas(List<NetworkParameter> ListCoordinates, bool flag)
        {
            var interval_cord_p = 0.0;
            var pocket_array = 0.0;
            var gran_left = 0.0;
            var gran_right = 0.0;
            var h = CanvasGraph.Height * 0.5;
            var l = CanvasGraph.Width * 0.5;
            var array_max_graph = new List<double>();
            var List_Coordinates = new List<NetworkParameter>();
            List_Coordinates = ListCoordinates;

            for (var i = 0; i < List_Coordinates.Count; i++)
            {
                var array = List_Coordinates[i].Array_coordinates;
                for (var j = 0; j < array.Count; j += 2)
                {
                    if (i == 0 && array[j] == 1)
                    {
                        interval_cord_p = array[j + 1];
                    }
                    else if (array[j] == 1)
                        array_max_graph.Add(array[j + 1]);
                    if (flag)
                    {
                        pocket_array = array[j];
                        array[j] = Math.Round(l + h * 0.76 * array[j + 1], 5);
                        array[j + 1] = Math.Round(h * (0.9 - 0.76 * pocket_array), 5);
                    }
                    else
                    {
                        array[j] = Math.Round(l * 0.9 - h * 0.76 * array[j], 5);
                        array[j + 1] = Math.Round(h * (1.1 + 0.76 * array[j + 1]), 5);
                    }
                }
            }
            array_max_graph.Sort();
            for (var i = 0; i < array_max_graph.Count; i++)
            {
                if (i == 0 && array_max_graph[i] == interval_cord_p)
                {
                    gran_left = array_max_graph[i];
                    gran_right = array_max_graph[i + 1];
                    break;
                }
                else if (array_max_graph[i] == interval_cord_p && i != array_max_graph.Count - 1)
                {
                    gran_left = array_max_graph[i - 1];
                    gran_right = array_max_graph[i + 1];
                    break;
                }
                else if (array_max_graph[i] == interval_cord_p && i == array_max_graph.Count - 1)
                {
                    gran_left = array_max_graph[i - 1];
                    gran_right = array_max_graph[i];
                    break;
                }
                else if (interval_cord_p > array_max_graph[i])
                {
                    gran_left = array_max_graph[i];
                }
                else
                {
                    gran_right = array_max_graph[i];
                    break;
                }
            }

            if (flag)
            {
                IntervalForCountConnectClients = new List<double>();
                IntervalForCountConnectClients.Add(l + h * 0.76 * gran_left);
                IntervalForCountConnectClients.Add(l + h * 0.76 * gran_right);
            }
            else
            {
                IntervalForCountSameAdrress = new List<double>();
                IntervalForCountSameAdrress.Add(h * (1.1 + 0.76 * gran_left));
                IntervalForCountSameAdrress.Add(h * (1.1 + 0.76 * gran_right));
            }
            return List_Coordinates;
        }

        private void Graph_Build(List<NetworkParameter> list, bool flag)
        {
            var x_graph_map = 0.0;
            var y_graph_map = 0.0;
            var interval = 0.0;
            List<double> arr_leg;
            var shapes = new Main_figures(CanvasGraph);

            if (flag)
            {
                x_graph_map = 0.7 * CanvasGraph.Width / 2;
                y_graph_map = 20;
            }
            else
            {
                x_graph_map = 10;
                y_graph_map = 1.1 * CanvasGraph.Height / 2;
            }

            foreach (var item in list)
            {
                Color color;

                interval = interval + 20;

                //Вибір кольорів для графіків
                switch (item.Name)
                {
                    case "ОМ":  color = Colors.Blue;    break;
                    case "М":   color = Colors.Green;   break;
                    case "С":   color = Colors.Yellow;  break;
                    case "Б":   color = Colors.Orange;  break;
                    case "ОБ":  color = Colors.Red;     break;
                    default:    color = Colors.Navy;    break;
                }
                arr_leg = new List<double>() { x_graph_map, y_graph_map + interval, x_graph_map + koef * 60, y_graph_map + interval };
                //Відмальвуємо графік
                shapes.Draw_polyline(item.Array_coordinates, 2, color, false);

                //Підписуємо легенду графіка
                shapes.Text(x_graph_map + koef * 60 + 10, y_graph_map + interval - 10, item.Name, Convert.ToInt32(0.25 * (koef * CanvasGraph.Height) / 10.5));
                //Легенда графіка
                shapes.Draw_polyline(arr_leg, 3, color, false);
            }
        }
        
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            get_currentstateserver = new BackgroundWorker();

            get_currentstateserver.DoWork += Get_currentstateserver_DoWork;
            get_currentstateserver.ProgressChanged += Get_currentstateserver_ProgressChanged;
            get_currentstateserver.WorkerSupportsCancellation = true;
            get_currentstateserver.WorkerReportsProgress = true;

            get_currentstateserver.RunWorkerAsync();
        }

        private void Get_currentstateserver_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            BuildCurrentStateSystem(e.ProgressPercentage);
        }

        private void BuildCurrentStateSystem(int i)
        {
            CanvasGraph.Children.Clear();

            var ListConstantCoordinatesCountConnectClients = ConvertListForCanvas(ComboBoxSelectionChanged(item_param1, List_CountConnectClients[i], data_grid1), true);
            var ListConstantCoordinatesCountSameAdrress = ConvertListForCanvas(ComboBoxSelectionChanged(item_param2, List_CountSameAddress[i], data_grid2), false);
            var shapes = new Main_figures(CanvasGraph);
            var coordinate_axes = new Coordinate_axes(CanvasGraph);

            coordinate_axes.Main_coordinate_axes();

            Graph_Build(ListConstantCoordinatesCountConnectClients, true);
            Graph_Build(ListConstantCoordinatesCountSameAdrress, false);

            var Сrossing = new Сrossing();
            var list_pointCountConnectClients = Сrossing.IntersectionPoint(ListConstantCoordinatesCountConnectClients, IntervalForCountConnectClients, true);
            var list_pointCountSameAdrress = Сrossing.IntersectionPoint(ListConstantCoordinatesCountSameAdrress, IntervalForCountSameAdrress, false);

            var ListNameConnectClients = ListConstantCoordinatesCountConnectClients.Select(s => s.Name).ToList();
            var ListNameSameAdrress = ListConstantCoordinatesCountSameAdrress.Select(s => s.Name).ToList();

            var DrawAreaStateSystem = new DrawAreaConstantStateSystem(CanvasGraph, ListNameConnectClients, ListNameSameAdrress, list_pointCountConnectClients, list_pointCountSameAdrress);
            DrawAreaStateSystem.Rect_Area();

            var DrawAreaCurrentStateSystem = new DrawAreaCurrentStateSystem(CanvasGraph, ListNameConnectClients, ListNameSameAdrress, list_pointCountConnectClients, list_pointCountSameAdrress);
            DrawAreaCurrentStateSystem.DrawLineArea();
            danger_level.Text = DrawAreaCurrentStateSystem.DrawCurrentStateSystem();
        }

        private void Get_currentstateserver_DoWork(object sender, DoWorkEventArgs e)
        {
            BackgroundWorker worker = sender as BackgroundWorker;
            var i = 0;
            while(i != List_CountConnectClients.Count)
            {
                
                if (worker.CancellationPending)
                {
                    e.Cancel = true;
                    return;
                }
                worker.ReportProgress(i);
                Thread.Sleep(5000);
                i++;

            }
        }

        private void Print_Click(object sender, RoutedEventArgs e)
        {
            get_currentstateserver.CancelAsync();
        }
    }
}
