using KBCore.Refs;
using UnityEngine;
using UnityEngine.InputSystem;

namespace UnityDemoA
{
    public class Targetable : ValidatedMonoBehaviour, ITargetable
    {
        [SerializeField, Child] 
        private Outline _outline;

        public Transform Transform => transform;

        private void Awake() => _outline.enabled = false;

        public void Highlight() => _outline.enabled = true;
        public void Unhighlight() => _outline.enabled = false;

        // for debugging
        private void Update()
        {
            if (Keyboard.current.oKey.wasPressedThisFrame) _outline.enabled = !_outline.enabled;
        }
    }
}
