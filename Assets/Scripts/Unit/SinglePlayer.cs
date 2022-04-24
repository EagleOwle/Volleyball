using UnityEngine;


[RequireComponent(typeof(UnitMotion))]
public class SinglePlayer : Unit
{
    private UnitMotion unitMotion;

    private void Awake()
    {
        InputHandler.Instance.ActionSetSwipeDirection += OnSwipe;
        unitMotion = GetComponent<UnitMotion>();
    }

    private void OnSwipe(Vector3 direction)
    {
        unitMotion.MoveDirection = direction.normalized;
    }

}
