using PianistAnalyser.Domain.Entities;

using static PianistAnalyser.Application.Analysis.Validation.ValidationErrorMessages;

namespace PianistAnalyser.Application.Analysis.Validation
{
    public class ValidationResult
    {
        public bool IsValid { get; set; }

        public int Number { get; set; }

        public Measure[] Measures { get; set; }

        public ValidationError ErrorCode { get; set; }
    }
}