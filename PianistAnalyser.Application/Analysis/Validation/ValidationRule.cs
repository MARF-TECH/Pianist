using System;

using PianistAnalyser.Domain.Entities;

using static PianistAnalyser.Application.Analysis.Validation.ValidationErrorMessages;

namespace PianistAnalyser.Application.Analysis.Validation
{
    public class ValidationRule<T> where T : class
    {
        public int Number { get; set; }

        public Measure[] Measures { get; set; }

        public ValidationError ErrorCode { get; set; }

        public Func<T, bool> RuleDelegate { get; set; }

        public T ObjectTovalidate { get; set; }

        public ValidationRule(T @object, Func<T, bool> @delegate)
        {
            this.ObjectTovalidate = @object;
            this.RuleDelegate = @delegate;
        }

        public bool RunRuleDelegate()
        {
            return RuleDelegate(ObjectTovalidate);
        }
    }
}
