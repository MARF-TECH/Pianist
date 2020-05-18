using System;
using System.Linq;

using PianistAnalyser.Domain.Enums;

namespace PianistAnalyser.Domain.Entities
{
    public class Partition
    {

        private readonly Measure[] _measure;

        public int Length => _measure.Length;
        public Measure First => _measure[0];
        public Measure Last => _measure[Length - 1];

        public int NotesCount => _measure.Where(m => m.Type == MeasureType.NOTES).Count();
        public int AccordsCount => _measure.Where(m => m.Type == MeasureType.ACCORD).Count();
        public int SilencesCount => _measure.Where(m => m.Type == MeasureType.SILENCE).Count();

        public Partition(Measure[] notes)
        {
            this._measure = notes;
        }

        public Measure this[int index]
        {
            get
            {
                if (index < 0 && index >= _measure.Length) throw new IndexOutOfRangeException("Notes out of range");
                return _measure[index];
            }
            set
            {
                if (index < 0 || index >= _measure.Length) throw new IndexOutOfRangeException("Notes out of range");
                _measure[index] = value;
            }
        }

    }
}
