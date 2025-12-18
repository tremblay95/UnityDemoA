using System;
using System.Collections.Generic;
using System.Linq;
using KBCore.Refs;
using UnityEngine;

namespace UnityDemoA
{
    public class TargetingManager : ValidatedMonoBehaviour
    {
        public event Action<TargetData> TargetsConfirmed;
        public event Action TargetingCancelled;
        public event Action TargetingEnded;

        [SerializeField] InputReader _input;
        [SerializeField, Child] Camera _camera;

        private readonly HashSet<ITargetable> _targets = new();

        public bool IsTargeting => _targetingStrategy != null;

        public InputReader Input => _input;
        public Camera Camera => _camera;

        private TargetingStrategy _targetingStrategy;

        public bool BeginTargeting(TargetingStrategy targetingStrategy)
        {
            if (IsTargeting) { return false; }
            // Todo: request TargetingInputMode
            _input.Activate += OnConfirm;
            _input.Cancel += OnCancel;

            _targetingStrategy = targetingStrategy;
            if (!_targetingStrategy.Begin(this))
            {
                OnCancel();
                return false;
            }

            return true;
        }

        private void Update()
        {
            if (IsTargeting)
            {
                UpdateTargets(_targetingStrategy.Update().ToHashSet());
            }
        }

        private void UpdateTargets(HashSet<ITargetable> newTargets)
        {
            foreach (var target in _targets)
            {
                if (!newTargets.Contains(target))
                {
                    target.Unhighlight();
                }
            }

            foreach (var target in newTargets)
            {
                if (!_targets.Contains(target))
                {
                    target.Highlight();
                }
            }

            _targets.Clear();
            _targets.UnionWith(newTargets);
        }

        private void OnConfirm()
        {
            if (IsTargeting)
            {
                TargetsConfirmed?.Invoke(new TargetData { targets = _targets.Select(t => t.Transform).ToList() });
                TargetingEnded?.Invoke();
                Reset();
            }
        }

        private void OnCancel()
        {
            if (IsTargeting)
            {
                TargetingCancelled?.Invoke();
                TargetingEnded?.Invoke();
                Reset();
            }
        }

        private void Reset()
        {
            foreach (var target in _targets)
            {
                target.Unhighlight();
            }

            _targets.Clear();

            _targetingStrategy = null;

            TargetsConfirmed = null;
            TargetingCancelled = null;
            TargetingEnded = null;

            _input.Activate -= OnConfirm;
            _input.Cancel -= OnCancel;

            // Todo: release TargetingInputMode
        }
    }
}
