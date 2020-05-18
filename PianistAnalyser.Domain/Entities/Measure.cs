using System;
using System.Linq;
using PianistAnalyser.Domain.Enums;

using static PianistAnalyser.Domain.NoteFactory;

namespace PianistAnalyser.Domain.Entities
{
    public class Measure
    {
        private readonly Note[] _notes;

        public MeasureType Type { get; private set; }

        public int Length => _notes.Length;

        public override string ToString() => string.Join(";", _notes.Select(n => n.StringValue));

        public Measure(Note[] notes)
        {
            this._notes = notes;
            this.Type = GetNotesType();
        }

        public Note this[int index]
        {
            get
            {
                if (index < 0 && index >= _notes.Length) throw new IndexOutOfRangeException("Note out of range");
                return _notes[index];
            }
            set
            {
                if (index < 0 || index >= _notes.Length) throw new IndexOutOfRangeException("Note out of range");
                _notes[index] = value;
            }
        }

        private MeasureType GetNotesType()
        {
            if (_notes.All(n => n.Value == NoteValue.R))
            {
                return MeasureType.SILENCE;
            }
            else
            {
                // -Lorsque très exactement 3 notes sont jouées sur une même mesure
                if (_notes.Length == 3)
                {
                    // - la première et la deuxième note se suivent
                    var _fstNote = _notes[0];
                    var _sndNote = _notes[1];
                    if (_sndNote.IsNextTo(_fstNote))
                    {
                        // - la deuxième et la troisième note ont une différence de deux Notes
                        var _thirdNote = _notes[2];
                        if (_thirdNote.IsNextTo(_sndNote, 2))
                        {
                            return MeasureType.ACCORD;
                        }
                    }
                }
            }
            return MeasureType.NOTES;
        }

    }
}
