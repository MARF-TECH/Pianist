using System;
using System.IO;
using System.Reflection;

using Xunit;
using FluentAssertions;
using PianistAnalyser.Application;
using PianistAnalyser.Domain.Exceptions;

using static PianistAnalyser.Application.Analysis.Validation.ValidationErrorMessages;
using PianistAnalyser.Desktop;

namespace PianistAnalyser.Tests.E2E
{
    public class PartitionTests
    {
        [Fact]
        public void Empty_File_Should_Throw_Exception()
        {
            Action action = () =>
            {
                string file = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), "5.csv");
                var partition = new PartitionCsvFileReader { Filename = file }.Read();
            };
            action.Should()
                  .Throw<EmptyPartionException>()
                  .WithMessage("Your Partition is either empty or contains errors");

        }

        [Fact]
        public void Should_Analyse_Partition_From_CsvFile()
        {
            string file = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), "3.csv");
            var partition = new PartitionCsvFileReader { Filename = file }.Read();
            var report = PartitionAnalyser.Analyse(partition);

            //  ------------------------------
            //      Partion Anaysis Report
            //  ------------------------------
            //  Number of Measures:            18
            //  Number of accords:             10
            //  Number of silences:            3
            //  Number of measures with notes: 5

            //  Your Partion is not in Harmony..

            //  According to this set of rules anyway:
            //  notes[A; B; D] n°1 of type ACCORD of A ==> next notes[;] of type SILENCE ==> next notes cannot be of type SILENCE
            //  notes[B; C; E] n°3 of type ACCORD of B ==> next notes[D; E; G] of type ACCORD of D ==> notes are separated by more than one high note
            //  notes[G; A; C] n°7 of type ACCORD of G ==> next notes[;] of type SILENCE ==> next notes cannot be of type SILENCE
            //  notes[;] n°8 of type SILENCE ==> next notes[G; A; G]  ==> next notes have to be of type ACCORD}
            //  notes[;] n°18 of type SILENCE   ==> partition cannot end with SILENCE note

            report.ReportList.Should().HaveCount(5);
            report.ReportList.Should().Contain(
               r => r.ErrorCode == ValidationError.HARMONY_NEXT_MEASUREMENT_TYPE_ACCORD_ERROR
            );

        }

    }
}
