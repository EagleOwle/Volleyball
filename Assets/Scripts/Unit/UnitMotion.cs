using System;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class UnitMotion : MonoBehaviour
{
    [SerializeField] private LayerMask _checkGroundMask;

    private Rigidbody _rigidbody;
    
    public bool OnGround => _onGround;
    private bool _onGround = false;

    private float velocityX;
    private float velocityY;
    private float moveYTarget;
    private float acceleration = 1;

    public Vector3 moveDirection { get; set; }

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }

    private void Start()
    {
        _onGround = false;

        switch (Game.Instance.scene.difficultEnum)
        {
            case GameDifficult.Easy:
                acceleration = 1;
                break;
            case GameDifficult.Normal:
                acceleration = 1.5f;
                break;
            case GameDifficult.Hard:
                acceleration = 2;
                break;
        }

    }

    private void FixedUpdate()
    {
        if ((StateMachine.currentState is GameState) is false)
        {
            moveDirection = Vector3.zero;
        }

        velocityY = Jump(moveDirection.y);
        velocityX = moveDirection.x * Preference.Singleton.moveSpeed * Time.deltaTime;

        if (_onGround == false)
        {
            velocityX *= acceleration;
        }

        _rigidbody.velocity = new Vector3(velocityX, velocityY, _rigidbody.velocity.z);

    }

    private float Jump(float inputJump)
    {
        velocityY = _rigidbody.velocity.y;
        moveYTarget = Physics.gravity.y;

        if (_onGround)
        {
            if (inputJump > 0)
            {
               velocityY = Preference.Singleton.jumpForce * Time.deltaTime;
            }
        }

        return  Mathf.MoveTowards(velocityY, moveYTarget, Preference.Singleton.downSpeed * Time.deltaTime);
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.collider.gameObject.layer == LayerMask.NameToLayer("Ground"))
        {
            _onGround = false;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.gameObject.layer == LayerMask.NameToLayer("Ground"))
        {
            _onGround = true;
        }
    }

}
