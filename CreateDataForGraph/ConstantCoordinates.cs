using Security_network_module.Model;
using System;
using System.Collections.Generic;

namespace Security_network_module.CreateDataForGraph
{
    class ConstantCoordinates
    {
        //отримання з оцінок експертів еталоних станів системи
        public static List<NetworkParameter> GetConstantCoordinates(List<NetworkParameter> ListExpertRatings, List<int> array_gaps)
        {
            var list = new List<NetworkParameter>();
            var array_koef = new List<double>();
            var cm = 0.0;

            for (var i = 1; i < array_gaps.Count; i++)
                array_koef.Add(Math.Round(Convert.ToDouble(array_gaps[i]) / array_gaps[array_gaps.Count - 1], 3));

            for (var i = 0; i < ListExpertRatings.Count; i++)
            {
                cm = ListExpertRatings[i].Array_coordinates[i];
                for (var j = 0; j < ListExpertRatings[i].Array_coordinates.Count; j++)
                {
                    ListExpertRatings[i].Array_coordinates[j] = Math.Round(ListExpertRatings[i].Array_coordinates[j] / cm, 2);
                }
            }

            for (var i = 0; i < ListExpertRatings.Count; i++)
            {
                list.Add(new NetworkParameter { Name = ListExpertRatings[i].Name, Array_coordinates = new List<double>() });
                for (var j = 0; j < ListExpertRatings[i].Array_coordinates.Count; j++)
                {
                    list[i].Array_coordinates.Add(ListExpertRatings[i].Array_coordinates[j]);
                    list[i].Array_coordinates.Add(array_koef[j]);
                }
            }

            for (var i = 0; i < list.Count; i++)
            {
                list[i].Array_coordinates = GetConvertNetworkParameters(list[i].Array_coordinates, list[i].Array_coordinates.Count);
            }
            return list;
        }

        //розширення на нульові елементи НЧ
        public static List<double> GetConvertNetworkParameters(List<double> item, int length)
        {
            for (var j = 0; j < length; j = j + 2)
            {
                if (item[0] != 0)
                {
                    item.Insert(j, item[j + 1]);
                    item.Insert(j, 0);
                    length += 2;
                }
                else
                {
                    if (item[length - 2] != 0)
                    {
                        item.Add(0);
                        item.Add(item[length - 1]);
                        length += 2;
                    }
                    else
                    {
                        while (item[0] == 0)
                        {
                            if (item[2] == 0)
                            {
                                item.RemoveAt(0);
                                item.RemoveAt(0);
                                length -= 2;
                            }
                            else break;
                        }

                        while (item[j] == 0 && j != length - 2)
                        {
                            if (item[j + 2] == 0)
                            {
                                item.RemoveAt(j + 2);
                                item.RemoveAt(j + 2);
                                length -= 2;
                            }
                            else break;
                        }
                    }
                }
            }
            return item;
        }
    }
}
