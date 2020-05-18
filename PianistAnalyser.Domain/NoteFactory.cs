using PianistAnalyser.Domain.Entities;
using PianistAnalyser.Domain.Extensions;
using System;
using System.Linq;
using System.Collections.Generic;
using PianistAnalyser.Domain.Enums;

namespace PianistAnalyser.Domain
{
    public static class NoteFactory
    {
        public static IReadOnlyList<string> PossibleNotes =
            Enum.GetValues(typeof(NoteValue))
            .Cast<NoteValue>()
            .Where(n => n != NoteValue.R)
            .Select(v => v.ToString())
            .ToList();

        public static int NotePosition(string note)
        {
            return PossibleNotes.IndexOf(note);
        }

        //- si deux accords se suivent, ils doivent être au maximum espacés d'une note en hauteur
        //  Exemples : - un accord de B peut être suivi par un accord de A, B ou C
        //             - un accord de G peut être suivi par un accord de F, G ou A
        public static bool AreConsecutiveNotes(Note current, Note next, int step = 1)
        {
            return next.Value == current.Value || current.IsNextTo(next, step) || next.IsNextTo(current, step);
        }

        public static bool IsNextTo(this Note current, Note next, int step = 1)
        {
            if (step == 0 || next == null || next.Value == NoteValue.R || current == null || current.Value == NoteValue.R) return false;

            step %= PossibleNotes.Count;
            var distance = current.Value - next.Value;

            if (step < 0) { if (distance > 0) distance -= PossibleNotes.Count; }
            else if (distance < 0) distance = ((int)next.Value - (PossibleNotes.Count - 1)) + ((int)current.Value + 1);

            return distance == step;
        }

        public static Partition GeneratePartition(IEnumerable<string> notes, char noteseparator)
        {
            return new Partition(
                notes.Select(m => m.Split(noteseparator).Select(n => new Note(n)).ToArray())
                     .Select(ns => new Measure(ns)).ToArray());
        }

    }
}
