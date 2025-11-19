using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityUtils;

namespace UnityDemoA
{
    [Serializable]
    public class MeleeTargeting : TargetingStrategy
    {
        [SerializeField] private float _range = 2f;
        [SerializeField] private float _sweepAngle = 45f;
        [SerializeField] private float _verticalOffset = 0.5f;
        [SerializeField] private LayerMask _layerMask = 1;

        protected override void Start()
        {
            _isTargeting = true;
            _targetingManager.SetCurrentStrategy(this);
            
            if (_targetingManager.Input != null)
            {
                _targetingManager.Input.Attack += OnAttack;
                _targetingManager.Input.SecondaryAction += _targetingManager.CancelTargeting;
                _targetingManager.Input.SecondaryAction += Cancel;
            }
        }

        public override void Update()
        {
            // Todo: Highlight targets in range
        }

        private void OnAttack()
        {
            if (!_isTargeting) { return; }

            _targetingManager.CompleteTargeting(GetTargetsInAttackRange());
            Cancel();
        }

        private List<Transform> GetTargetsInAttackRange()
        {
            var potentialTargets = Physics.OverlapSphere(_targetingManager.transform.position.Add(y: _verticalOffset), _range, _layerMask)
                .Select(c => c.transform).ToList();

            for (int i = potentialTargets.Count - 1; i >= 0; i--)
            {
                var directionToTarget = potentialTargets[i].transform.position - _targetingManager.transform.position;
                var angleToTarget = Vector3.Angle(directionToTarget, _targetingManager.transform.forward);

                if (angleToTarget > _sweepAngle || potentialTargets[i].CompareTag("Player"))
                {
                    potentialTargets.RemoveAt(i);
                }
            }

            return potentialTargets.ConvertAll(c => c.transform);
        }

        public override void Cancel()
        {
            _isTargeting = false;
            _targetingManager.ClearCurrentStrategy();
            
            if (_targetingManager.Input != null)
            {
                _targetingManager.Input.Attack += OnAttack;
                _targetingManager.Input.SecondaryAction -= _targetingManager.CancelTargeting;
                _targetingManager.Input.SecondaryAction -= Cancel;
            }
        }
    }
}