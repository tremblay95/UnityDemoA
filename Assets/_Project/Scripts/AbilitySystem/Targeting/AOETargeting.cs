using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityUtils;
using Object = UnityEngine.Object;

namespace UnityDemoA
{
    [Serializable]
    public class AOETargeting : TargetingStrategy
    {
        [SerializeField] private GameObject _groundMarkerPrefab;
        [SerializeField] private float _aoeRadius = 5f;
        [SerializeField] private LayerMask _groundLayerMask = 1;
        [SerializeField] private float _groundOffset = 0.1f;

        private GameObject _groundMarkerInstance;

        protected override bool Begin()
        {
            if (!_groundMarkerPrefab || !_targetingManager.Input || !_targetingManager.Camera) return false;

            _groundMarkerInstance = Object.Instantiate(_groundMarkerPrefab, Vector3.zero.Add(y: _groundOffset), Quaternion.identity);
            _targetingManager.TargetingEnded += End;

            return true;
        }

        public override IEnumerable<ITargetable> Update()
        {
            if (!_groundMarkerInstance) { return null; }

            var groundHitPosition = GetWorldMarkerPosition();
            _groundMarkerInstance.transform.position = groundHitPosition.Add(y: _groundOffset);

            return Physics.OverlapSphere(groundHitPosition, _aoeRadius)
                .Select(c => c.GetComponentInParent<ITargetable>())
                .Where(t => t != null)
                .Distinct();
        }

        private Vector3 GetWorldMarkerPosition() // Todo: handle gamepad input
        {
            if (!_targetingManager.Camera) { return Vector3.zero; }

            var ray = _targetingManager.Camera.ScreenPointToRay(Mouse.current.position.ReadValue());
            return Physics.Raycast(ray, out var hit, 100f, _groundLayerMask) ? hit.point : Vector3.zero;
        }

        public override void End()
        {
            if (_groundMarkerInstance)
            {
                Object.Destroy(_groundMarkerInstance);
            }
        }
    }
}
