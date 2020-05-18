using System;
using System.Text;
using System.Collections.Generic;

using PianistAnalyser.Application;
using PianistAnalyser.Domain.Enums;
using PianistAnalyser.Domain.Entities;
using PianistAnalyser.Application.services;
using PianistAnalyser.Application.Analysis;

using static PianistAnalyser.Application.Analysis.Validation.ValidationErrorMessages;

namespace PianistAnalyser.Desktop
{
    public class ConsoleApplication
    {
        private readonly IPartitionReader _reader;

        public ConsoleApplication(IPartitionReader reader)
        {
            this._reader = reader;
        }

        public void Run(string filename)
        {
            // - partition 
            this._reader.Filename = filename;
            var partition = this._reader.Read();

            Console.WriteLine("------------------------------");
            Console.WriteLine("   Partion Anaysis Report");
            Console.WriteLine("------------------------------");

            // - analysis result
            var report = PartitionAnalyser.Analyse(partition);
            Console.WriteLine($"Number of Measures:             {partition.Length}");
            DisplayPartitionAnalysisReport(report);
            DisplayPartitionHarmonyReport(report);
        }

        private void DisplayPartitionAnalysisReport(PartitionReport report)
        {
            var mr = new StringBuilder()
                .AppendLine($"Number of accords:              {report.AccordsCount}")
                .AppendLine($"Number of silences:             {report.SilencesCount}")
                .AppendLine($"Number of measures with notes:  {report.NotesCount}");
            Console.WriteLine(mr.ToString());
        }

        private void DisplayPartitionHarmonyReport(PartitionReport report)
        {
            var hr = new StringBuilder();

            if (report.ReportList.Count == 0)
            {
                hr.AppendLine($"Your Partion is in Perfect Harmony");
            }
            else
            {
                hr.AppendLine($"Your Partion is not in Harmony.. according to this set of rules:");
                foreach (var detail in report.ReportList)
                {
                    var notes = detail.Measures[0];
                    var message = hr.Append($"notes [{notes}] ").Append($"n°{detail.Number} ");

                    var nextnotes = "";
                    if (detail.Measures.Length == 2)
                    {
                        notes = detail.Measures[1];
                        nextnotes = $"==> next notes [{notes}] {GetNotesTypeMessage(notes)}";
                    }
                    var error = ProcessMessage(HarmonyError.HarmonyErrorMessage(detail.ErrorCode), ("{{M-TYPE}}", notes.Type.ToString()));

                    hr.Append($"{GetNotesTypeMessage(detail.Measures[0])} ").AppendLine($"{nextnotes} ==> {error}");
                }
            }
            Console.WriteLine(hr.ToString());
        }

        private string GetNotesTypeMessage(Measure notes) =>
            notes.Type == MeasureType.ACCORD || notes.Type == MeasureType.SILENCE ?
                 $"of type {notes.Type}" + (notes.Type == MeasureType.ACCORD ? $" of {notes[0]} " : " ")
                 : "";
    }
}
