
using PianistAnalyser.Domain.Entities;
using PianistAnalyser.Domain.Exceptions;
using PianistAnalyser.Application.Analysis;

namespace PianistAnalyser.Application
{
    public class PartitionAnalyser
    {
        public static PartitionReport Analyse(Partition partition)
        {
            if (partition?.Length == 0) throw new EmptyPartionException();

            return new PartitionReportBuilder(partition).Build();
        }
    }
}