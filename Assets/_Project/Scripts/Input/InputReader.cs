using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;
using static PlayerInputActions;
using static UnityEngine.InputSystem.InputAction;

namespace UnityDemoA
{
    [CreateAssetMenu(fileName = "InputReader", menuName = "UnityDemoA/InputReader")]
    public class InputReader : ScriptableObject, IPlayerActions
    {
        public event UnityAction<Vector2> Move = delegate { };
        public event UnityAction<Vector2, bool> Look = delegate { };
        public event UnityAction Activate = delegate { };
        
        public event UnityAction<RaycastHit> Click = delegate { };
        public event UnityAction Cancel = delegate { };

        public event UnityAction<int> AbilitySelected = delegate { };
        
        private PlayerInputActions _inputActions;
        
        public Vector2 Direction => _inputActions.Player.Move.ReadValue<Vector2>();

        private void OnEnable()
        {
            if (_inputActions == null)
            {
                _inputActions = new PlayerInputActions();
                _inputActions.Player.SetCallbacks(this);
            }
        }
        
        public void EnableInputActions() => _inputActions.Enable();
        public void DisableInputActions() => _inputActions.Disable();

        public void OnMove(CallbackContext context)
        {
            Move.Invoke(context.ReadValue<Vector2>());
        }

        public void OnLook(CallbackContext context)
        {
            Look.Invoke(context.ReadValue<Vector2>(), IsDeviceMouse(context));
        }

        private bool IsDeviceMouse(CallbackContext context) => context.control.device.name == "Mouse";

        public void OnActivate(CallbackContext context)
        {
            if (context.started)
            {
                Activate.Invoke();
                if (IsDeviceMouse(context))
                {
                    var ray = Camera.main.ScreenPointToRay(Mouse.current.position.ReadValue());
                    if (Physics.Raycast(ray.origin, ray.direction, out var hit, 100f))
                    {
                        Click.Invoke(hit);
                    }
                }
            }
        }

        public void OnCancel(CallbackContext context)
        {
            if (context.started) { Cancel.Invoke(); }
        }

        public void OnAbility1(CallbackContext context) => AbilitySelected.Invoke(1);
        public void OnAbility2(CallbackContext context) => AbilitySelected.Invoke(2);
        public void OnAbility3(CallbackContext context) => AbilitySelected.Invoke(3);
        public void OnAbility4(CallbackContext context) => AbilitySelected.Invoke(4);
    }
}
