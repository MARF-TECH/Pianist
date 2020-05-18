using System;

using Xunit;
using FluentAssertions;

using PianistAnalyser.Tests.Data;
using PianistAnalyser.Application;
using PianistAnalyser.Domain.Enums;
using PianistAnalyser.Domain.Entities;
using PianistAnalyser.Domain.Exceptions;

using static PianistAnalyser.Application.Analysis.Validation.ValidationErrorMessages;

namespace PianistAnalyser.Tests.Unit
{
    public class PartitionTests
    {
        [Fact]
        public void Empty_Should_Throw_Exception()
        {
            Action action = () =>
            {
                var emptypartition = new Partition(new Measure[] { });
                PartitionAnalyser.Analyse(emptypartition);
            };
            action.Should()
                  .Throw<EmptyPartionException>()
                  .WithMessage("Your Partition is either empty or contains errors");
        }

        [Theory]
        [MemberData(nameof(NoteGenerator.GetTwoAccordsTwoNotesOneSilencePartitionData), MemberType = typeof(NoteGenerator))]
        public void Notes_Should_Have_Correct_Types(Measure notes, MeasureType type)
        {
            notes.Type.Should().Be(type);
        }

        [Fact]
        public void Should_Have_Exact_Number_Of_Notes_By_Type()
        {
            var partition = NoteGenerator.TwoAccordsTwoNotesOneSilencePartition();

            partition.Length.Should().Equals(5);
            partition.AccordsCount.Should().Equals(2);
            partition.NotesCount.Should().Equals(2);
            partition.SilencesCount.Should().Equals(1);
        }

        [Fact]
        public void ShouldNot_End_With_Silence_Or_Accord()
        {
            var partition = NoteGenerator.TwoAccordsTwoNotesOneSilencePartition();
            var report = PartitionAnalyser.Analyse(partition);

            report.ReportList.Should().NotBeEmpty();
            report.ReportList.Should().Contain(
                r => r.ErrorCode == ValidationError.HARMONY_LAST_MEASUREMENT_TYPE_ERROR
            );
        }

    }
}
