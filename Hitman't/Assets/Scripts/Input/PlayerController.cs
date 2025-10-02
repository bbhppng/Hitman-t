using UnityEngine;
using UnityEngine.InputSystem;
//ts handles input
[CreateAssetMenu(fileName = "PlayerController", menuName = "InputController/PlayerController")]
public class PlayerController : InputController
{
    private PlayerInputActions _inputActions;
    private bool _isJumping, _isShooting;

    private void OnEnable() 
    {
        _inputActions = new PlayerInputActions(); //this creates new instance of the input actions
        _inputActions.Player.Enable(); //that enables player action map
        _inputActions.Player.Jump.started += JumpStarted; //that's how you subscribe to events
        _inputActions.Player.Jump.canceled += JumpCanceled;
        _inputActions.Player.Attack.started += AttackStarted;
        _inputActions.Player.Attack.canceled += AttackCanceled;
    }

    private void OnDisable()
    {
        _inputActions.Player.Disable();
        _inputActions.Player.Jump.started -= JumpStarted;
        _inputActions.Player.Jump.canceled -= JumpCanceled;
        _inputActions.Player.Attack.started -= AttackStarted;
        _inputActions.Player.Attack.canceled -= AttackCanceled;
        _inputActions = null;
    }

    private void JumpCanceled(InputAction.CallbackContext action)
    {
        _isJumping = false;
    }
    
    private void JumpStarted(InputAction.CallbackContext action)
    {
        _isJumping = true;
    }

    private void AttackCanceled(InputAction.CallbackContext action)
    {
        _isShooting = false;
    }
    
    private void AttackStarted(InputAction.CallbackContext action)
    {
        _isShooting = true;
    }

    
    public override Vector2 RetrieveMoveInput()
    {
        return _inputActions.Player.Move.ReadValue<Vector2>();
    }

    public override bool RetrieveJumpInput()
    {
        return _isJumping;
    }

    public override bool RetrieveWeaponInput()
    {
        return _isShooting;
    }
}
