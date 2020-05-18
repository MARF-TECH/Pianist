using System.IO;
using System.Linq;

using PianistAnalyser.Domain.Entities;
using PianistAnalyser.Domain.Exceptions;
using PianistAnalyser.Application.services;

using static PianistAnalyser.Domain.NoteFactory;

namespace PianistAnalyser.Desktop
{
    public class PartitionCsvFileReader : IPartitionReader
    {
        private const char _noteSeparator = ';';

        public string Filename { get; set; }

        public Partition Read()
        {
            var notes = File.ReadAllLines(Filename).TakeWhile(t => t != null);
            if (notes.Count() == 0) throw new EmptyPartionException();
            return GeneratePartition(notes, _noteSeparator);
        }
    }
}
