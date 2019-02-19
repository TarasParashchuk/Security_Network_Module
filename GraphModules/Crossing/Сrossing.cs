using System;
using System.Collections.Generic;
using Security_network_module.Model;

namespace Security_network_module.Crossing
{
    class Сrossing
    {
        public List<NetworkParameter> IntersectionPoint(List<NetworkParameter> list, List<double> interval_p, bool flag)
        {
            var Crossing_points = new List<NetworkParameter>();
            Crossing_points.Add(new NetworkParameter() { Name = "CurrentState", Array_coordinates = GetListPoint(list, interval_p, flag, 0) });
            Crossing_points.Add(new NetworkParameter() { Name = "ConstantState", Array_coordinates = GetListPoint(list, null, flag, 1) });
            return Crossing_points;
        }

        private List<double> GetListPoint(List<NetworkParameter> list, List<double> interval_p, bool flag, int index)
        {
            var point_current_state = new List<double>();
            for (var i = 1; i < list.Count - index; i++)
            {
                var buf = GetPoint(list[i * index].Array_coordinates, list[i + index].Array_coordinates, interval_p, flag);
                if (buf.Count != 0 && index == 1)
                    point_current_state.AddRange(buf);
                else if (buf.Count != 0 && point_current_state.Count / 2  != 2 && index == 0)
                    point_current_state.AddRange(buf);
            }

            if(index == 0)
            {
                var xy = list[0].Array_coordinates;
                point_current_state.AddRange(new List<double> { Math.Round(xy[0], 4), Math.Round(xy[1], 4), Math.Round(xy[xy.Count - 2], 4), Math.Round(xy[xy.Count - 1], 4) });
            }

            return point_current_state;
        }

        private List<double> GetPoint(List<double> xy1, List<double> xy2, List<double> interval_p, bool flag)
        {
            var points = new List<double>();
            double x1, y1, x2, y2, x3, y3, x4, y4;

            for (var i = 0; i < xy1.Count - 2; i += 2)
            {
                x1 = xy1[i];
                y1 = xy1[i + 1];
                x2 = xy1[i + 2];
                y2 = xy1[i + 3];

                for (var j = 0; j < xy2.Count - 2; j += 2)
                {
                    x3 = xy2[j];
                    y3 = xy2[j + 1];
                    x4 = xy2[j + 2];
                    y4 = xy2[j + 3];

                    if ((flag && Math.Min(y1, y2) == Math.Min(y3, y4)) || (!flag && Math.Min(x1, x2) == Math.Min(x3, x4)))
                    {
                        var point = new Intersection_point(x1, y1, x2, y2, x3, y3, x4, y4, interval_p, flag).Intersection_point_xy();
                        if (point != null)
                        {
                            points.Add(point[0]);
                            points.Add(point[1]);
                            break;
                        }
                    }

                }
                if (points.Count != 0)
                    break;
            }
            return points;
        }
    }
}
