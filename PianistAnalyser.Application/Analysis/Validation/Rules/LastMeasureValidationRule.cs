using System.Linq;

using PianistAnalyser.Domain.Enums;
using PianistAnalyser.Domain.Entities;

using static PianistAnalyser.Application.Analysis.Validation.ValidationErrorMessages;

namespace PianistAnalyser.Application.Analysis.Validation.Rules
{
    public class LastMeasureValidationRule : BaseValidationRuleSet<Measure[]>
    {
        // - la dernière mesure de la partition doit comporter une ou plusieurs notes, sans accord     
        public LastMeasureValidationRule(int n, params Measure[] measures)
        {
            RuleList.Add(new ValidationRule<Measure[]>(measures, m => m.Last().Type == MeasureType.ACCORD || m.Last().Type == MeasureType.SILENCE)
            {
                Number = n,
                Measures = measures,
                ErrorCode = ValidationError.HARMONY_LAST_MEASUREMENT_TYPE_ERROR,
            });
        }
    }
}
