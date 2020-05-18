using System.Collections.Generic;

using PianistAnalyser.Domain.Entities;
using PianistAnalyser.Application.Analysis.Validation;

namespace PianistAnalyser.Application.Analysis
{
    public class PartitionReport
    {
        private readonly Partition _partition;

        private readonly List<ValidationResult> _reportList = new List<ValidationResult>();

        public IReadOnlyCollection<ValidationResult> ReportList => _reportList.AsReadOnly();

        public int NotesCount => _partition.NotesCount;

        public int AccordsCount => _partition.AccordsCount;

        public int SilencesCount => _partition.SilencesCount;


        public PartitionReport(Partition partition)
        {
            _partition = partition;
        }

        public void Add(IEnumerable<ValidationResult> report)
        {
            _reportList.AddRange(report);
        }

    }
}