using System.Collections.Generic;
using KBCore.Refs;
using UnityEngine;

namespace UnityDemoA
{
    public class TargetingManager : ValidatedMonoBehaviour
    {
        [SerializeField] InputReader _input;
        [SerializeField, Child] Camera _camera;
        
        List<Transform> _targets = new();
        
        public InputReader Input => _input;
        public Camera Camera => _camera;
        public IReadOnlyList<Transform> Targets => _targets;
        public bool Completed { get; private set; }
        public bool Cancelled { get; private set; }
        
        
        private TargetingStrategy _currentTargetingStrategy;

        private void Update()
        {
            if (_currentTargetingStrategy is { IsTargeting: true })
            {
                _currentTargetingStrategy.Update();
            }
        }

        public void SetCurrentStrategy(TargetingStrategy targetingStrategy) => _currentTargetingStrategy = targetingStrategy;

        public void ClearCurrentStrategy()
        {
            _currentTargetingStrategy = null;
        }

        public void CompleteTargeting(List<Transform> targets)
        {
            Cancelled = false;
            Completed = true;
            _targets = targets;
        }

        public void ClearTargets() => _targets.Clear();

        public void CancelTargeting()
        {
            Completed = false;
            Cancelled = true;
            ClearTargets();
        }

        public void Reset() => Completed = Cancelled = false;
    }
}