
using PianistAnalyser.Domain.Enums;
using PianistAnalyser.Domain.Entities;

using static PianistAnalyser.Domain.NoteFactory;
using static PianistAnalyser.Application.Analysis.Validation.ValidationErrorMessages;

namespace PianistAnalyser.Application.Analysis.Validation.Rules
{
    public class MeasuresValidationRule : BaseValidationRuleSet<Measure[]>
    {
        public MeasuresValidationRule(int n, params Measure[] measures)
        {
            //  - un silence doit être suivi d'un accord
            RuleList.Add(new ValidationRule<Measure[]>(measures,
                m => m[0].Type == MeasureType.SILENCE
                     && m[1].Type != MeasureType.ACCORD)
            {
                Number = n,
                Measures = measures,
                ErrorCode = ValidationError.HARMONY_NEXT_MEASUREMENT_TYPE_ACCORD_ERROR,
            });

            //  - un accord ne peut être suivi d'un silence
            RuleList.Add(new ValidationRule<Measure[]>(measures,
                m => m[0].Type == MeasureType.ACCORD
                     && m[1].Type == MeasureType.SILENCE)
            {
                Number = n,
                Measures = measures,
                ErrorCode = ValidationError.HARMONY_NEXT_MEASUREMENT_TYPE_EXC_ERROR,
            });

            // - si deux accords se suivent, ils doivent être au maximum espacés d'une note en hauteur
            //  Exemples:
            //  - un accord de B peut être suivi par un accord de A, B ou C
            //  - un accord de G peut être suivi par un accord de F, G ou A
            RuleList.Add(new ValidationRule<Measure[]>(measures,
                m => m[0].Type == MeasureType.ACCORD
                     && m[1].Type == MeasureType.ACCORD
                     && !AreConsecutiveNotes(measures[0][0], measures[1][0], step: 1))
            {
                Number = n,
                Measures = measures,
                ErrorCode = ValidationError.HARMONY_MEASUREMENT_HIGH_NOTE_SEP_ERROR,
            });

        }
    }
}
