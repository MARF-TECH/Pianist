using PianistAnalyser.Domain.Entities;

namespace PianistAnalyser.Application.services
{
    public interface IPartitionReader
    {
        string Filename { get; set; }

        Partition Read();
    }
}
