using BenchmarkDotNet.Columns;
using BenchmarkDotNet.Configs;
using BenchmarkDotNet.Engines;
using BenchmarkDotNet.Jobs;
using BenchmarkDotNet.Reports;
using BenchmarkDotNet.Toolchains.InProcess.NoEmit;
using Perfolizer.Horology;


namespace EFDapperBenchmark.Configurations
{
    public class BenchmarkConfig : ManualConfig
    {
        public BenchmarkConfig()
        {
            AddJob(Job.MediumRun
                .WithStrategy(RunStrategy.Throughput)
                .WithToolchain(InProcessNoEmitToolchain.Instance)
                .WithIterationTime(TimeInterval.FromMilliseconds(250))
            );

            AddColumn(StatisticColumn.Mean);
            AddColumn(StatisticColumn.Min);
            AddColumn(StatisticColumn.Max);
            AddColumn(StatisticColumn.OperationsPerSecond);
            WithSummaryStyle(SummaryStyle.Default.WithTimeUnit(Perfolizer.Horology.TimeUnit.Millisecond));
        }
    }
}
