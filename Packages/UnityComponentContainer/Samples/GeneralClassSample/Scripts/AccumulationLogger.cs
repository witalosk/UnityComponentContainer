using System.Collections.Generic;
using UnityEngine;
namespace ComponentContainer.Samples.GeneralClassSample
{
    public class AccumulationLogger : ILogger
    {
        private readonly List<string> _logs = new();

        public void Log(string text)
        {
            _logs.Add(text);
            Debug.Log(string.Join(", ", _logs));
        }
    }
}
