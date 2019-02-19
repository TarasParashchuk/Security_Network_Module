using System;
using System.Collections.Generic;

namespace Security_network_module.Crossing
{
    class Intersection_point
    {
        private double[] a = new double[2];
        private double[] c = new double[2];
        private double[] b = new double[2];
        private double y1, y2, y3, y4;
        private double x1, x2, x3, x4;
        private List<double> interval_p;
        private double lim_left_p, lim_right_p, lim;
        private bool flag;

        public Intersection_point(double x1, double y1, double x2, double y2, double x3, double y3, double x4, double y4, List<double> interval_p, bool flag)
        {
            this.y1 = y1;
            this.y2 = y2;
            this.y3 = y3;
            this.y4 = y4;
            this.x1 = x1;
            this.x2 = x2;
            this.x3 = x3;
            this.x4 = x4;
            this.flag = flag;
            this.interval_p = interval_p;

            if (interval_p != null)
            {
                lim_left_p = Math.Round(interval_p[0], 5);
                lim_right_p = Math.Round(interval_p[1], 5);
            }

            //Коефіцієнт a при x
            a[0] = y2 - y1;
            a[1] = y4 - y3;
            //Коефіцієнт b при y
            b[0] = x1 - x2;
            b[1] = x3 - x4;
            //Вільний коефіцієнт с
            c[0] = y1 * x2 - x1 * y2;
            c[1] = y3 * x4 - x3 * y4;
        }

        public List<double> Intersection_point_xy()
        {
            //Основний визначник
            var d = a[0] * b[1] - a[1] * b[0];

            if (d == 0)
                return null;
            else
            {
                // Знаходимо допоміжні визначники
                var d1 = c[0] * b[1] - c[1] * b[0];
                var d2 = a[0] * c[1] - a[1] * c[0];

                var x = -d1 / d;
                var y = -d2 / d + 1;

                if ((flag && Math.Max(x1, x3) <= x && x <= Math.Min(x2, x4)) || (!flag && Math.Max(y1, y3) <= y && y <= Math.Min(y2, y4)))
                {
                    if (interval_p != null && ((flag && lim_left_p <= x && lim_right_p >= x) || (!flag && lim_left_p <= y && lim_right_p >= y)))
                    {
                        return new List<double> { Math.Round(x, 2), Math.Round(y, 2) };
                    }
                    else if (interval_p == null)
                    {
                        return new List<double> { Math.Round(x, 2), Math.Round(y, 2) };
                    }
                    else return null;
                }
                else return null;
            }
        }
    }
}
