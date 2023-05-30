using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TutoToons
{
    public class ValidationRule<T, F>
    {
        public Func<T, F, bool> Rule { get; }
        public string Message { get; }

        public ValidationRule(Func<T, F, bool> rule, string message)
        {
            Rule = rule;
            Message = message;
        }
    }
}
