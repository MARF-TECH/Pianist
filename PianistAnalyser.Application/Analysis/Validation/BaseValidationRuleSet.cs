using System.Linq;
using System.Collections.Generic;

namespace PianistAnalyser.Application.Analysis.Validation
{
    public class BaseValidationRuleSet<T> where T : class
    {
        protected List<ValidationRule<T>> RuleList;

        public BaseValidationRuleSet()
        {
            RuleList = new List<ValidationRule<T>>();
        }

        public IEnumerable<ValidationResult> Run()
        {
            return RuleList.Select(rule =>
            {
                if (rule.RunRuleDelegate())
                    return new ValidationResult() { Number = rule.Number, Measures = rule.Measures, IsValid = false, ErrorCode = rule.ErrorCode };
                return null;
            }).Where(result => result != null).ToList();
        }

    }
}
