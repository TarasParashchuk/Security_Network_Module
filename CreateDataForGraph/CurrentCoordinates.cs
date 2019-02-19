using Security_network_module.Model;
using System;
using System.Collections.Generic;

namespace Security_network_module.CreateDataForGraph
{
    class CurrentCoordinates
    {
        public static List<double> GetCurrentStateSystemCoordinatess(List<NetworkParameter> List_ConstantCoordinates, int[] Array_Sensors)
        {
            var current_size = 0;
            var index_delta = 0;

            for (var i = 0; i < Array_Sensors.Length; i++)
            {
                if (Array_Sensors[i] != 0)
                {
                    for (var j = 1; j < List_ConstantCoordinates[i - index_delta].Array_coordinates.Count; j = j + 2)
                        List_ConstantCoordinates[i - index_delta].Array_coordinates[j] *= Array_Sensors[i];
                }
                else
                {
                    List_ConstantCoordinates.RemoveAt(i - index_delta);
                    index_delta++;
                }
            }

            var ListCurrentStateSystemCoordinates = new List<double>();

            for (var i = 0; i < List_ConstantCoordinates.Count; i += 2)
            {
                if (List_ConstantCoordinates.Count != 1)
                {
                    if (i == 0)
                        ListCurrentStateSystemCoordinates = GetSumCurrentCoordinates(List_ConstantCoordinates[i].Array_coordinates, List_ConstantCoordinates[i + 1].Array_coordinates);
                    else
                        ListCurrentStateSystemCoordinates = GetSumCurrentCoordinates(ListCurrentStateSystemCoordinates, List_ConstantCoordinates[i].Array_coordinates);
                    while (current_size != ListCurrentStateSystemCoordinates.Count)
                    {
                        current_size = ListCurrentStateSystemCoordinates.Count;
                        ListCurrentStateSystemCoordinates = LinearApproximationLocalMaximum(ListCurrentStateSystemCoordinates);
                    }
                }
                else ListCurrentStateSystemCoordinates = List_ConstantCoordinates[0].Array_coordinates;

                
            }

            for (var i = 1; i < ListCurrentStateSystemCoordinates.Count; i += 2)
                ListCurrentStateSystemCoordinates[i] = Math.Round(ListCurrentStateSystemCoordinates[i] / 60, 4);

            return ConstantCoordinates.GetConvertNetworkParameters(ListCurrentStateSystemCoordinates, ListCurrentStateSystemCoordinates.Count);
        }

        //сумування двох НЧ
        private static List<double> GetSumCurrentCoordinates(List<double> list1, List<double> list2)
        {
            var List_Coordinates = new List<double>();
            var ListCurrentStateSystemCoordinates = new List<double>();
            double func1, func2;

            for (var i = 0; i < list1.Count; i += 2)
            {
                for (var j = 0; j < list2.Count; j += 2)
                {
                    func1 = list1[i];
                    func2 = list2[j];
                    if (func1 != 0 && func2 != 0)
                    {
                        if (func1 > func2)
                            List_Coordinates.Add(func2);
                        else List_Coordinates.Add(func1);

                        List_Coordinates.Add(list1[i + 1] + list2[j + 1]);
                    }
                }
            }
            return LinearApproximationLocalMaximum(List_Coordinates);
        }

        //лінійна апроксимація
        private static List<double> LinearApproximationLocalMaximum(List<double> List_Coordinates)
        {
            var ListCurrentStateSystemCoordinates = new List<double>();
            double func1, func2, func_current, max;

            var temp = 0.0;
            for (var i = 1; i < List_Coordinates.Count - 2; i += 2)
            {
                for (int j = i + 2; j < List_Coordinates.Count; j += 2)
                {
                    if (List_Coordinates[i] > List_Coordinates[j])
                    {
                        temp = List_Coordinates[i];
                        List_Coordinates[i] = List_Coordinates[j];
                        List_Coordinates[j] = temp;

                        temp = List_Coordinates[i - 1];
                        List_Coordinates[i - 1] = List_Coordinates[j - 1];
                        List_Coordinates[j - 1] = temp;
                    }
                }
            }
            max = List_Coordinates[List_Coordinates.Count - 1];
            for (var i = 2; i < List_Coordinates.Count - 2; i += 2)
            {
                func_current = List_Coordinates[i];
                func1 = List_Coordinates[i - 2];
                func2 = List_Coordinates[i + 2];
                if (i == 2)
                {
                    ListCurrentStateSystemCoordinates.Add(List_Coordinates[0]);
                    ListCurrentStateSystemCoordinates.Add(List_Coordinates[1]);
                }

                if ((func1 < func_current && func_current > func2) | (func1 == func_current && func_current > func2) |
                (func1 < func_current && func_current == func2) | (func1 == func_current && func_current < func2) & List_Coordinates[i + 1] < max |
                (func1 > func_current && func_current == func2) & List_Coordinates[i + 1] > max)
                {
                    ListCurrentStateSystemCoordinates.Add(List_Coordinates[i]);
                    ListCurrentStateSystemCoordinates.Add(List_Coordinates[i + 1]);
                }

                if (i == List_Coordinates.Count - 4)
                {
                    ListCurrentStateSystemCoordinates.Add(List_Coordinates[List_Coordinates.Count - 2]);
                    ListCurrentStateSystemCoordinates.Add(List_Coordinates[List_Coordinates.Count - 1]);
                }
            }
            return ListCurrentStateSystemCoordinates;
        }
    }
}
