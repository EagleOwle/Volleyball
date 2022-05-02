using System;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class UnitMotion : MonoBehaviour
{
    [SerializeField] private LayerMask _checkGroundMask;

    private Rigidbody _rigidbody;
    private bool _onGround = false;
    private float velocityX;
    private float velocityY;
    private float moveYTarget;

    private Vector3 moveDirection;
    public Vector3 MoveDirection
    {
        set
        {
            moveDirection = value;
        }
    }

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }

    private void Start()
    {
        _onGround = false;
    }

    private void FixedUpdate()
    {
        if ((StateMachine.currentState is GameState) is false)
        {
            moveDirection = Vector3.zero;
        }

        velocityY = Jump(moveDirection.y);

        if (_onGround)
        {
            velocityX = moveDirection.x * Preference.Singleton.moveSpeed * Time.deltaTime;
        }
        else
        {
            velocityX = moveDirection.x * (Preference.Singleton.moveSpeed * 1.5f) * Time.deltaTime;
        }

        _rigidbody.velocity = new Vector3(velocityX, velocityY, _rigidbody.velocity.z);

    }

    private float Jump(float inputJump)
    {
        float velocityY = _rigidbody.velocity.y;
        float moveYTarget = Physics.gravity.y;

        if (_onGround)
        {
            if (inputJump > 0)
            {
               velocityY = Preference.Singleton.jumpForce * Time.deltaTime;
            }

            moveYTarget = 0;
        }

        return Mathf.MoveTowards(velocityY, moveYTarget, Preference.Singleton.downSpeed * Time.deltaTime);
    }

    private void OnCollisionStay(Collision collision)
    {
        if (collision.collider.gameObject.layer == LayerMask.NameToLayer("Ground"))
        {
            _onGround = true;
        }
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

    private void OnDrawGizmos()
    {
        if (_rigidbody == null) return;

        Gizmos.color = Color.red;

        Vector3 startPosition = transform.position;

        Vector3 endPosition = startPosition + _rigidbody.velocity;

        Gizmos.DrawLine(startPosition, endPosition);

    }

}
