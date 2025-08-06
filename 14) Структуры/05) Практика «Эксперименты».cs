//всем спасибо всем пака я в тильт
using System;
using System.Collections.Generic;
using System.Diagnostics;
namespace StructBenchmarking
{
    public class Experiments
    {
        public static ChartData BuildChartDataForArrayCreation(
            IBenchmark benchmarkInstance, int repetitionsCount)
        {
            return BuildChartsDataForClasses(benchmarkInstance, repetitionsCount, false, "Create array");
        }

        public static ChartData BuildChartDataForMethodCall(
            IBenchmark benchmarkInstance, int repetitionsCount)
        {
            return BuildChartsDataForClasses(benchmarkInstance, repetitionsCount, true, "Call method with argument");
        }

		public static ChartData BuildChartsDataForClasses(IBenchmark benchmarkInstance,
														  int repetitionsCount,
														  bool isMethodCallerFlag,
														  string chartTitle)
        {
            var classExecutionTimes = new List<ExperimentResult>();
            var structExecutionTimes = new List<ExperimentResult>();

            foreach (var fieldCount in Constants.FieldCounts)
            {
                var comparableMethods = new CompariableMethods(fieldCount, isMethodCallerFlag);
				classExecutionTimes.Add(new ExperimentResult(fieldCount,
benchmarkInstance.MeasureDurationInMs(comparableMethods.Classes,
repetitionsCount)));
				structExecutionTimes.Add(new ExperimentResult(fieldCount,
benchmarkInstance.MeasureDurationInMs
(comparableMethods.Structures,
repetitionsCount)));
            }
            return new ChartData
            {
                Title = chartTitle,
                ClassPoints = classExecutionTimes,
                StructPoints = structExecutionTimes,
            };
        }
    }

    public class CompariableMethods
    {
        public ITask Classes { get; set; }
        public ITask Structures { get; set; }

        public CompariableMethods(int size, bool isMethodCaller)
        {
            if (isMethodCaller)
            {
                Classes = new MethodCallWithClassArgumentTask(size);
                Structures = new MethodCallWithStructArgumentTask(size);
            }
            else
            {
                Classes = new ClassArrayCreationTask(size);
                Structures = new StructArrayCreationTask(size);
            }
        }
    }
}