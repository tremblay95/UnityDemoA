using System;
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

        protected override void Start()
        {
            _isTargeting = true;
            
            _targetingManager.SetCurrentStrategy(this);

            if (_groundMarkerPrefab != null)
            {
                _groundMarkerInstance = Object.Instantiate(_groundMarkerPrefab, Vector3.zero.Add(y: _groundOffset), Quaternion.identity);
            }

            if (_targetingManager.Input != null)
            {
                _targetingManager.Input.Click += OnClick;
                _targetingManager.Input.SecondaryAction += _targetingManager.CancelTargeting;
                _targetingManager.Input.SecondaryAction += Cancel;
            }
        }
        
        public override void Update()
        {
            if (!_isTargeting || _groundMarkerInstance == null) { return; }
            
            _groundMarkerInstance.transform.position = GetWorldMarkerPosition().Add(y: _groundOffset);
        }

        private Vector3 GetWorldMarkerPosition() // Todo: handle gamepad input
        {
            if (_targetingManager.Camera == null) { return Vector3.zero; }
            
            var ray = _targetingManager.Camera.ScreenPointToRay(Mouse.current.position.ReadValue());
            return Physics.Raycast(ray, out var hit, 100f, _groundLayerMask) ? hit.point : Vector3.zero;
        }

        private void OnClick(RaycastHit hit)
        {
            if (!_isTargeting) { return; }

            var targets = Physics.OverlapSphere(hit.point, _aoeRadius).Select(c => c.transform).ToList();

            _targetingManager.CompleteTargeting(targets);
            Cancel();
        }
        
        public override void Cancel()
        {
            _isTargeting = false;
            _targetingManager.ClearCurrentStrategy();
            
            if (_groundMarkerInstance != null)
            {
                Object.Destroy(_groundMarkerInstance);
            }
            
            if (_targetingManager.Input != null)
            {
                _targetingManager.Input.Click -= OnClick;
                _targetingManager.Input.SecondaryAction -= _targetingManager.CancelTargeting;
                _targetingManager.Input.SecondaryAction -= Cancel;
            }
        }
    }
}