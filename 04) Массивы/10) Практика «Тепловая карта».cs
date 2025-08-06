using System;

namespace Names
{
    internal static class HeatmapTask
    {
        public static HeatmapData GetBirthsPerDateHeatmap(NameData[] Names)
        {
            var (Days, Months) = InitializeDaysAndMonths();

            var heatmap = new double[Days.Length, Months.Length];
            foreach (var Name in Names)
            {
                if (Name.BirthDate.Day != 1)
                    heatmap[Name.BirthDate.Day - 2, Name.BirthDate.Month - 1]++;
            }

            return new HeatmapData("Пример карты интенсивностей", heatmap, Days, Months);
        }

        private static (string[] Days, string[] Months) InitializeDaysAndMonths()
        {
            var Days = InitializeArray(30, 2);
            var Months = InitializeArray(12, 1);
            return (Days, Months);
        }

        private static string[] InitializeArray(int length, int startValue)
        {
            var array = new string[length];
            for (int i = 0; i < length; i++)
                array[i] = (i + startValue).ToString();
            return array;
        }
    }
}