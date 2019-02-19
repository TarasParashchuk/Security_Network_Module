using Security_network_module.DrawObjectCanvas;
using Security_network_module.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Controls;
using System.Windows.Media;

namespace Security_network_module.GetCurrentStateSystem
{
    class DrawAreaCurrentStateSystem
    {
        public static string[] Name_Oblast_ST = { "Н", "БНВ", "БВН", "В", "П" };

        private Canvas CanvasGraph;
        private Main_figures shapes;
        private List<string> ListNameConnectClients;
        private List<string> ListNameSameAdrress;
        private List<NetworkParameter> list_point1;
        private List<NetworkParameter> list_point2;
        private List<double> IntersectionPointsCurrentCoordinatesCountConnectClients;
        private List<double> IntersectionPointsCurrentCoordinatesCountSameAdrress;
        private List<double> IntersectionPointsConstantCoordinatesCountConnectClients;
        private List<double> IntersectionPointsConstantCoordinatesCountSameAdrress;

        public DrawAreaCurrentStateSystem(Canvas CanvasGraph, List<string> ListNameConnectClients, List<string> ListNameSameAdrress, List<NetworkParameter> list_point1, List<NetworkParameter> list_point2)
        {
            this.CanvasGraph = CanvasGraph;
            this.ListNameConnectClients = ListNameConnectClients;
            this.ListNameSameAdrress = ListNameSameAdrress;
            this.list_point1 = list_point1;
            this.list_point2 = list_point2;

            shapes = new Main_figures(CanvasGraph);
            GetIntersectionPoints();
        }

        public static List<double> GetListIntersectionPoints(List<NetworkParameter> list, string name)
        {
            return list.Where(w => w.Name == name).Select(s => s.Array_coordinates).First();
        }

        private void GetIntersectionPoints()
        {
            IntersectionPointsCurrentCoordinatesCountConnectClients = GetListIntersectionPoints(list_point1, "CurrentState");
            IntersectionPointsCurrentCoordinatesCountSameAdrress = GetListIntersectionPoints(list_point2, "CurrentState");

            IntersectionPointsConstantCoordinatesCountConnectClients = GetListIntersectionPoints(list_point1, "ConstantState");
            IntersectionPointsConstantCoordinatesCountSameAdrress = GetListIntersectionPoints(list_point2, "ConstantState"); ;
        }

        public void DrawLineArea()
        {
            var obj = new Draw_main_object(CanvasGraph);

            obj.Draw_main_line(IntersectionPointsConstantCoordinatesCountConnectClients, Colors.Black, true);
            obj.Draw_main_line(IntersectionPointsCurrentCoordinatesCountConnectClients, Colors.Red, true);
            obj.Draw_main_line(IntersectionPointsConstantCoordinatesCountSameAdrress, Colors.Black, false);
            obj.Draw_main_line(IntersectionPointsCurrentCoordinatesCountSameAdrress, Colors.Red, false);

            obj.Draw_main_point(IntersectionPointsCurrentCoordinatesCountConnectClients, IntersectionPointsCurrentCoordinatesCountSameAdrress, Colors.Red);
            obj.Draw_main_point(IntersectionPointsConstantCoordinatesCountConnectClients, IntersectionPointsConstantCoordinatesCountSameAdrress, Colors.Black);
        } 
        
        public string DrawCurrentStateSystem()
        {
            var x = ((IntersectionPointsCurrentCoordinatesCountConnectClients.Count - 1) / 2 < 4) ? IntersectionPointsCurrentCoordinatesCountConnectClients[2] - IntersectionPointsCurrentCoordinatesCountConnectClients[0] : 0;
            var y = ((IntersectionPointsCurrentCoordinatesCountSameAdrress.Count - 1) / 2 < 4) ? IntersectionPointsCurrentCoordinatesCountSameAdrress[3] - IntersectionPointsCurrentCoordinatesCountSameAdrress[1] : 0;

            var x0 = 0.0;
            var y0 = 0.0;
            var x1 = IntersectionPointsCurrentCoordinatesCountConnectClients[0];
            var x2 = IntersectionPointsCurrentCoordinatesCountConnectClients[2];
            var y1 = IntersectionPointsCurrentCoordinatesCountSameAdrress[1];
            var y2 = IntersectionPointsCurrentCoordinatesCountSameAdrress[3];

            if (x1 > x2 && y1 > y2)
            {
                x0 = x2;
                y0 = y2;
            }
            else
            {
                if (x1 > x2 && y1 < y2)
                {
                    x0 = x2;
                    y0 = y1;
                }
                else
                {
                    if (x1 < x2 && y1 < y2)
                    {
                        x0 = x1;
                        y0 = y1;
                    }
                    else
                    {
                        if (x1 < x2 && y1 > y2)
                        {
                            x0 = x1;
                            y0 = y2;
                        }
                    }
                } 
            }

            shapes.Draw_Rect(x0, y0, x, y, Colors.Red, 0.3);

            var middle_x = x0 + x / 2;
            var middle_y = y0 + y / 2;
            var name_gr1 = Find(IntersectionPointsConstantCoordinatesCountConnectClients, middle_x, 0);
            var name_gr2 = Find(IntersectionPointsConstantCoordinatesCountSameAdrress, middle_y, 1);
            return Namearea(Namegraph(ListNameSameAdrress[name_gr2 + 1]) * 10 + Namegraph(ListNameConnectClients[name_gr1 + 1])).ToString();
        }

        public int Find(List<double> arj, double x, int index)
        {
            var min = arj[index];
            for (var i = index; i < arj.Count; i += 2)
            {
                if ((Math.Abs(arj[i] - x)) < (Math.Abs(x - min)))
                    min = arj[i];
            }
            var j = arj.IndexOf(min);
            return (j + index) / 2;
        }

        public string Namearea(int str_name)
        {
            switch (str_name)
            {
                case 55: case 45: case 54: return Name_Oblast_ST[4];
                case 44: case 53: case 4: return Name_Oblast_ST[3];
                case 43: case 52: case 51: case 3: return Name_Oblast_ST[2];
                case 42: case 34: case 35: return Name_Oblast_ST[1];
                default: return Name_Oblast_ST[0];
            }
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
