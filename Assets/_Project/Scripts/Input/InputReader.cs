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
        public event UnityAction Attack = delegate { };
        
        public event UnityAction<RaycastHit> Click = delegate { };
        public event UnityAction SecondaryAction = delegate { };
        
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

        public void OnAttack(CallbackContext context)
        {
            if (context.started)
            {
                // Attack.Invoke();
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

        public void OnSecondaryAction(CallbackContext context)
        {
            if (context.started) { SecondaryAction.Invoke(); }
        }

        public void OnInteract(CallbackContext context)
        {
            // noop
        }

        public void OnCrouch(CallbackContext context)
        {
            // noop
        }

        public void OnJump(CallbackContext context)
        {
            // noop
        }

        public void OnPrevious(CallbackContext context)
        {
            // noop
        }

        public void OnNext(CallbackContext context)
        {
            // noop
        }

        public void OnSprint(CallbackContext context)
        {
            // noop
        }
    }
}
