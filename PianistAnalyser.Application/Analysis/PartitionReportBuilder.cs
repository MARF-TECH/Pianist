
using PianistAnalyser.Domain.Entities;
using PianistAnalyser.Application.Analysis.Validation.Rules;

namespace PianistAnalyser.Application.Analysis
{
    public class PartitionReportBuilder
    {
        private readonly Partition _partition;

        private readonly PartitionReport _report;

        public PartitionReportBuilder(Partition partition)
        {
            _partition = partition;
            _report = new PartitionReport(partition);
        }

        public PartitionReport Build()
        {
            for (int i = 0; i < _partition.Length - 2; i++)
            {
                _report.Add(new MeasuresValidationRule(i + 1, _partition[i], _partition[i + 1]).Run());
            }
            _report.Add(new LastMeasureValidationRule(_partition.Length, _partition.Last).Run());
            return _report;
        }

    }
}