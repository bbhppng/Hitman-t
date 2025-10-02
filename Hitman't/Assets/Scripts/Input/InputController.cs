using UnityEngine;

public abstract class InputController : ScriptableObject
{
    public abstract Vector2 RetrieveMoveInput();
    public abstract bool RetrieveJumpInput();
    public abstract bool RetrieveWeaponInput();
}
