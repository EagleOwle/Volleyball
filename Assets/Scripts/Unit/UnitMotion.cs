using System;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class UnitMotion : MonoBehaviour
{
    //[SerializeField] private float _moveSpeed = 250;
    //[SerializeField] private float _jumpForce = 250;

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
        velocityY = Jump();

        if (_onGround)
        {
            velocityX = moveDirection.x * Preference.Singletone.moveSpeed * Time.deltaTime;
        }
        else
        {
            velocityX = moveDirection.x * (Preference.Singletone.moveSpeed * 1.5f) * Time.deltaTime;
        }

        _rigidbody.velocity = new Vector3(velocityX, velocityY, _rigidbody.velocity.z);

    }

    private float Jump()
    {
        float velocityY = _rigidbody.velocity.y;
        float moveYTarget = Physics.gravity.y;

        if (_onGround)
        {
            if (moveDirection.y > 0)
            {
               velocityY = Preference.Singletone.jumpForce * Time.deltaTime;
            }

            moveYTarget = 0;
        }

        return Mathf.MoveTowards(velocityY, moveYTarget, Preference.Singletone.downSpeed * Time.deltaTime);
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

    private bool CheckGround()
    {
        bool onGround = false;
        float distance = 0.3f;
        Vector3 startRayPosition = transform.position;
        Vector3 direction = Vector3.down;

        Physics.Raycast(startRayPosition, direction, out RaycastHit hitInfo, distance, _checkGroundMask);

        if (hitInfo.collider)
        {
            Debug.DrawLine(startRayPosition, hitInfo.point, Color.red);
            onGround = true;
        }
        else
        {
            Debug.DrawRay(startRayPosition, direction * distance, Color.green);
           
        }

        return onGround;
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
