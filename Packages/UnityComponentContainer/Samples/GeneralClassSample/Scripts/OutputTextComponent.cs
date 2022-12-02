using System;
using UnityEngine;

namespace ComponentContainer.Samples.GeneralClassSample
{
    public class OutputTextComponent : MonoBehaviour
    {
        [SerializeField]
        private string _outputText;
        
        private ILogger _logger;
        
        [Inject]
        public void Construct(ILogger logger)
        {
            _logger = logger;
        }

        private void Start()
        {
            _logger.Log(_outputText);
        }
    }
}
