using System;
using PianistAnalyser.Domain.Enums;
using PianistAnalyser.Domain.Exceptions;

namespace PianistAnalyser.Domain.Entities
{
    public class Note
    {
        public NoteValue Value { get; private set; }

        public string StringValue { get; private set; }

        public override string ToString() => StringValue;

        public Note(string note)
        {
            note = note.Trim();
            this.StringValue = note;
            if (string.IsNullOrEmpty(note) || note.Equals(";") || note.Equals("R"))
            {
                Value = NoteValue.R;
            }
            else
            {
                try
                {
                    Value = (NoteValue)Enum.Parse(typeof(NoteValue), note);
                }
                catch (Exception)
                {
                    throw new NotSupportedNoteException();
                }
            }
        }

        public Note(NoteValue note)
        {
            this.Value = note;
            this.StringValue = note.ToString();
        }
    }
}
