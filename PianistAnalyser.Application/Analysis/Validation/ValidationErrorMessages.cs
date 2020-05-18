using System.Linq;
using System.Collections.Generic;

namespace PianistAnalyser.Application.Analysis.Validation
{
    public static class ValidationErrorMessages
    {
        public enum ValidationError
        {
            HARMONY_NEXT_MEASUREMENT_TYPE_EXC_ERROR,
            HARMONY_NEXT_MEASUREMENT_TYPE_INC_ERROR,
            HARMONY_NEXT_MEASUREMENT_TYPE_ACCORD_ERROR,
            HARMONY_MEASUREMENT_HIGH_NOTE_SEP_ERROR,
            HARMONY_LAST_MEASUREMENT_TYPE_ERROR
        }

        public static class HarmonyError
        {
            private const string NEXT_MEASUREMENT_TYPE_EXC_ERROR_MSG = "next notes cannot be of type {{M-TYPE}}";
            private const string NEXT_MEASUREMENT_TYPE_INC_ERROR_MSG = "next notes have to be of type {{M-TYPE}}";
            private const string HARMONY_NEXT_MEASUREMENT_TYPE_ACCORD_ERROR_MSG = "next notes have to be of type ACCORD}";
            private const string MEASUREMENT_HIGH_NOTE_SEP_ERROR_MSG = "notes are separated by more than one high note";
            private const string LAST_MEASUREMENT_TYPE_ERROR_MSG = "partition cannot end with {{M-TYPE}} note";

            public static IReadOnlyDictionary<ValidationError, string> Messages => new Dictionary<ValidationError, string>
            {
                { ValidationError.HARMONY_NEXT_MEASUREMENT_TYPE_EXC_ERROR, NEXT_MEASUREMENT_TYPE_EXC_ERROR_MSG},
                { ValidationError.HARMONY_NEXT_MEASUREMENT_TYPE_INC_ERROR, NEXT_MEASUREMENT_TYPE_INC_ERROR_MSG},
                { ValidationError.HARMONY_NEXT_MEASUREMENT_TYPE_ACCORD_ERROR, HARMONY_NEXT_MEASUREMENT_TYPE_ACCORD_ERROR_MSG},
                { ValidationError.HARMONY_MEASUREMENT_HIGH_NOTE_SEP_ERROR, MEASUREMENT_HIGH_NOTE_SEP_ERROR_MSG},
                { ValidationError.HARMONY_LAST_MEASUREMENT_TYPE_ERROR, LAST_MEASUREMENT_TYPE_ERROR_MSG}
            };

            public static string HarmonyErrorMessage(ValidationError err)
            {
                return Messages.TryGetValue(err, out string message) ? message : $"Error: Harmony Violation {err}";
            }
        }

        public static string ProcessMessage(string msg, params (string key, string value)[] values)
        {
            return values.Aggregate(msg, (current, m) => current.Replace(m.key, m.value));
        }

    }
}