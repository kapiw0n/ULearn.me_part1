using System;
using System.Linq;

namespace Names
{
  internal static class HistogramTask
  {
    public static HistogramData GetBirthsPerDayHistogram(NameData[] names, string name)
    {
        var daysCount = 31;
        var days = Enumerable.Range(1, daysCount).Select(x => x.ToString()).ToArray();
        var counts = new double[daysCount];
        foreach (var Person in names)
        {
            if (Person.Name == name && Person.BirthDate.Day != 1)
                counts[Person.BirthDate.Day - 1]++;
        }
        return new HistogramData($"Рождаемость людей с именем '{name}'", days, counts);
    }
  }
}
