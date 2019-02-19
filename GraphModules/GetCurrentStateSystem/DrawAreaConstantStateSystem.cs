using Security_network_module.DrawObjectCanvas;
using Security_network_module.Model;
using System;
using System.Collections.Generic;
using System.Windows.Controls;
using System.Windows.Media;

namespace Security_network_module.GetCurrentStateSystem
{
    class DrawAreaConstantStateSystem
    {
        public static double koef = 0.8;
        public static string[] Name_Oblast_ST = { "Н", "БНВ", "БВН", "В", "П" };

        private double height;
        private double width;
        private Canvas CanvasGraph;
        private Main_figures shapes;
        private List<string> ListNameConnectClients;
        private List<string> ListNameSameAdrress;
        private List<double> PointsConstantCoordinatesCC;
        private List<double> PointsConstantCoordinatesSA;

        public DrawAreaConstantStateSystem(Canvas CanvasGraph, List<string> ListNameConnectClients, List<string> ListNameSameAdrress, List<NetworkParameter> list_point_ConnectClients, List<NetworkParameter> list_point_SameAdrress)
        {
            this.CanvasGraph = CanvasGraph;
            this.ListNameConnectClients = ListNameConnectClients;
            this.ListNameSameAdrress = ListNameSameAdrress;
            PointsConstantCoordinatesCC = DrawAreaCurrentStateSystem.GetListIntersectionPoints(list_point_ConnectClients, "ConstantState");
            PointsConstantCoordinatesSA = DrawAreaCurrentStateSystem.GetListIntersectionPoints(list_point_SameAdrress, "ConstantState");

            height = 0.55 * CanvasGraph.Height;
            width = 0.5 * CanvasGraph.Width;
            shapes = new Main_figures(CanvasGraph);
        }

        public void Rect_Area()
        {
            List<Color> colors;
            for (var i = 0; i <= PointsConstantCoordinatesSA.Count / 2; i++)
            {
                colors = GetNameColorArea(i + 1, width, height);
                if (i == 0)
                    Draw_main_rect(colors, 1, 0, 0, 0);
                else
                if (i == PointsConstantCoordinatesSA.Count / 2)
                    Draw_main_rect(colors, 0, 0, 1, 0);
                else
                    Draw_main_rect(colors, 0, 1, 0, 2 * (i - 1));
            }
        }

        private void Draw_main_rect(List<Color> color, int k0, int k1, int k2, int j)
        {
            var cord = 0.0;

            if (k1 != 0)
                cord = PointsConstantCoordinatesSA[j + 3] - PointsConstantCoordinatesSA[j + 1];

            var length1 = PointsConstantCoordinatesCC.Count;
            var length2 = PointsConstantCoordinatesSA.Count;
            var y1 = height * (1 - k1 - k2) + k1 * PointsConstantCoordinatesSA[j + 1] + k2 * PointsConstantCoordinatesSA[length2 - 1];
            var y2 = k2 * (1.69 * height - PointsConstantCoordinatesSA[length2 - 1]) + k1 * cord + k0 * (PointsConstantCoordinatesSA[1] - height);

            shapes.Draw_Rect(width, y1, PointsConstantCoordinatesCC[0] - width, y2, color[0], 1);
            for (var i = 0; i < PointsConstantCoordinatesCC.Count - 3; i += 2)
            {
                shapes.Draw_Rect(PointsConstantCoordinatesCC[i], y1, PointsConstantCoordinatesCC[i + 2] - PointsConstantCoordinatesCC[i], y2, color[i / 2 + 1], 1);
            }
            shapes.Draw_Rect(PointsConstantCoordinatesCC[length1 - 2], y1, width + 0.69 * height - PointsConstantCoordinatesCC[length1 - 2], y2, color[color.Count - 1], 1);
        }

        private List<Color> GetNameColorArea(int select_index, double l, double h)
        {
            var color = new List<Color>();

            var x_Text = l + koef * h * 1.14 - 20;
            var x_Rect = l + koef * h * 1.14 - 65;
            var size_text = Convert.ToInt32(koef * h / 21);

            for (var i = 1; i < ListNameConnectClients.Count; i++)
            {
                var value = Namegraph(ListNameSameAdrress[select_index]) * 10 + Namegraph(ListNameConnectClients[i]);
                switch (value)
                {
                    case 55: case 45: case 54:  color.Add(Colors.DimGray); shapes.Text(x_Text, 497, Name_Oblast_ST[4], size_text);      shapes.Draw_Rect(x_Rect, 490, 30, 30, Colors.DimGray, 1);   break;
                    case 44: case 53:           color.Add(Colors.Gray); shapes.Text(x_Text, 457, Name_Oblast_ST[3], size_text);         shapes.Draw_Rect(x_Rect, 450, 30, 30, Colors.Gray, 1);      break;
                    case 43: case 52: case 51:  color.Add(Colors.DarkGray); shapes.Text(x_Text, 417, Name_Oblast_ST[2], size_text);     shapes.Draw_Rect(x_Rect, 410, 30, 30, Colors.DarkGray, 1);  break;
                    case 42: case 34: case 35:  color.Add(Colors.Silver); shapes.Text(x_Text, 377, Name_Oblast_ST[1], size_text);       shapes.Draw_Rect(x_Rect, 370, 30, 30, Colors.Silver, 1);    break;
                    default:                    color.Add(Colors.Gainsboro); shapes.Text(x_Text, 337, Name_Oblast_ST[0], size_text);    shapes.Draw_Rect(x_Rect, 330, 30, 30, Colors.Gainsboro, 1); break;
                }
            }
            return color;
        }

        public int Namegraph(string gr_table_name)
        {
            switch (gr_table_name)
            {
                case "ОМ": return 1;
                case "М": return 2;
                case "С": return 3;
                case "Б": return 4;
                case "ОБ": return 5;
                default: return 0;
            }
        }
    }
}
