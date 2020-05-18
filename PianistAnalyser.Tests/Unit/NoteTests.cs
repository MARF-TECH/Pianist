using Xunit;
using System;
using FluentAssertions;

using PianistAnalyser.Tests.Data;
using PianistAnalyser.Domain.Enums;
using PianistAnalyser.Domain.Entities;
using PianistAnalyser.Domain.Exceptions;

using static PianistAnalyser.Domain.NoteFactory;

namespace PianistAnalyser.Tests.Unit
{
    public class NoteTests
    {
        [Theory]
        [InlineData("do")]
        [InlineData("re")]
        [InlineData("me")]
        [InlineData("fa")]
        [InlineData("sol")]
        public void Playing_Wrong_Note_Should_Throw_Exception(string note)
        {
            Action action = () => { new Note(note); };

            action.Should().Throw<NotSupportedNoteException>();
        }

        [Theory]
        [InlineData(NoteValue.A)]
        public void Can_Create_Note_With_Valid_Value(NoteValue v)
        {
            var note = new Note(v);

            note.StringValue.Should().NotBeEmpty();
            PossibleNotes.Should().Contain(note.StringValue);
        }

        [Theory]
        [MemberData(nameof(NoteGenerator.GetNextNoteData), MemberType = typeof(NoteGenerator))]
        public void Should_Return_Correct_Next_Note(Note note, Note next, int step = 1)
        {
            next.IsNextTo(note, step).Should().BeTrue();
        }

        [Theory]
        [MemberData(nameof(NoteGenerator.GetPrevNoteData), MemberType = typeof(NoteGenerator))]
        public void Should_Return_Correct_Prev_Note(Note note, Note next, int step = -1)
        {
            next.IsNextTo(note, step).Should().BeTrue();
        }

        [Theory]
        [MemberData(nameof(NoteGenerator.GetRotationNoteData), MemberType = typeof(NoteGenerator))]
        public void Should_Return_Correct_Note_Full_List_Rotation(Note note, Note next, int step = 1)
        {
            next.IsNextTo(note, step).Should().BeTrue();
        }

        [Theory]
        [MemberData(nameof(NoteGenerator.GetConsecutiveNoteData), MemberType = typeof(NoteGenerator))]
        public void Should_Check_If_Two_Notes_Are_Consecutives(Note current, Note next, bool isConcsetutive)
        {
            AreConsecutiveNotes(current, next).Should().Be(isConcsetutive);
        }

    }
}
