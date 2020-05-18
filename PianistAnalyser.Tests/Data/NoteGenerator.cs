using System.Collections.Generic;

using PianistAnalyser.Domain.Entities;
using PianistAnalyser.Domain.Enums;

using static PianistAnalyser.Domain.NoteFactory;

namespace PianistAnalyser.Tests.Data
{
    public class NoteGenerator
    {
        public static Note A = new Note(NoteValue.A);
        public static Note B = new Note(NoteValue.B);
        public static Note C = new Note(NoteValue.C);
        public static Note D = new Note(NoteValue.C);
        public static Note E = new Note(NoteValue.E);
        public static Note F = new Note(NoteValue.F);
        public static Note G = new Note(NoteValue.G);

        public static IEnumerable<object[]> GetNextNoteData()
        {
            // - STEP = 1
            yield return new object[] { A, B };
            yield return new object[] { E, F };

            // - STEP > 1
            yield return new object[] { A, C, 2 };
            yield return new object[] { A, E, 4 };
        }

        public static IEnumerable<object[]> GetPrevNoteData()
        {
            yield return new object[] { C, G, -3 };
            yield return new object[] { E, A, -4 };
        }

        public static IEnumerable<object[]> GetRotationNoteData()
        {
            yield return new object[] { C, G, 11 };
            yield return new object[] { G, B, -12 };
        }

        public static IEnumerable<object[]> GetConsecutiveNoteData()
        {
            yield return new object[] { C, G, false };
            yield return new object[] { B, C, true };
        }

        public static Partition TwoAccordsTwoNotesOneSilencePartition() =>
            GeneratePartition(
                new List<string> {
                    "A;B;D;G",
                    "A;B;D",
                    "A;C;G",        
                    ";",
                    "G;A;C"
                }, ';');

        public static IEnumerable<object[]> GetTwoAccordsTwoNotesOneSilencePartitionData()
        {
            var partition = TwoAccordsTwoNotesOneSilencePartition();

            yield return new object[] { partition[0], MeasureType.NOTES };
            yield return new object[] { partition[1], MeasureType.ACCORD };
            yield return new object[] { partition[2], MeasureType.NOTES };
            yield return new object[] { partition[3], MeasureType.SILENCE };
            yield return new object[] { partition[4], MeasureType.ACCORD };
        }

    }
}
