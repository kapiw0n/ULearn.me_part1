using System;
using System.Diagnostics;
using System.Text;
using NUnit.Framework;

namespace StructBenchmarking
{
    public class Benchmark : IBenchmark
    {
        public double MeasureDurationInMs(ITask taskToMeasure, int numberOfRepetitions)
        {
            GC.Collect();
            GC.WaitForPendingFinalizers();
            taskToMeasure.Run();

            var stopwatch = new Stopwatch();
            stopwatch.Start();
            for (int i = 0; i < numberOfRepetitions; i++)
            {
                taskToMeasure.Run();
            }
            stopwatch.Stop();
            return stopwatch.Elapsed.TotalMilliseconds / numberOfRepetitions;
        }
    }

    [TestFixture]
    public class RealBenchmarkUsageSample
    {
        [Test]
        public void StringConstructorFasterThanStringBuilder()
        {
            var benchmark = new Benchmark();

            double stringBuilderDuration = benchmark.MeasureDurationInMs(new StringBuilderTask(), 10000);
            double stringConstructorDuration = benchmark.MeasureDurationInMs(new StringConstructorTask(), 10000);

            Assert.Less(stringConstructorDuration, stringBuilderDuration);
        }
    }

    public class StringBuilderTask : ITask
    {
        public void Run()
        {
            var stringBuilder = new StringBuilder();
            for (int i = 0; i < 10000; i++)
            {
                stringBuilder.Append("a");
            }
            var resultString = stringBuilder.ToString();
        }
    }

    public class StringConstructorTask : ITask
    {
        public void Run()
        {
            var resultString = new string('a', 10000);
        }
    }
}