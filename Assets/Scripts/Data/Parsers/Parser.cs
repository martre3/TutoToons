using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TutoToons
{
    public abstract class Parser
    {
        protected static bool Validate<T, F>(List<ValidationRule<T, F>> rules, T t, F f)
        {
            foreach (var rule in rules)
            {
                if (rule.Rule(t, f) == false)
                {
                    Debug.LogError(rule.Message);

                    return false;
                }
            }

            return true;
        }
    }
}