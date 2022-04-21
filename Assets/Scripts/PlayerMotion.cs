using System;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerMotion : MonoBehaviour
{
    [SerializeField] private float _maxSpeed = 3;
    [SerializeField] private float _minSpeed = 3;
    [SerializeField] private float _moveSpeed = 3;
    [SerializeField] private float _jumpSpeed = 5;
    [SerializeField] private float _jumpSence = 1;
    [SerializeField] private float _jumpForce = 500;
    [SerializeField] private LayerMask _checkGroundMask;

    private Rigidbody _rigidbody;
    private bool _onGround = false;
    private float moveX;
    private float moveY;
    private float moveYTarget;

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
        #region Jump

        if (_onGround)
        {
            moveYTarget = 0;

            if (InputHandler.Instance.SwipeDirection.y > _jumpSence)
            {
                moveY = _jumpForce;
            }
        }
        else
        {
            moveYTarget = Physics.gravity.y;
        }

        moveY = Mathf.MoveTowards(moveY, moveYTarget, _jumpSpeed * Time.deltaTime);

        #endregion

        #region Move

        moveX = 0;
        if (InputHandler.Instance.OnSwipe)
        {
            if (Mathf.Abs(InputHandler.Instance.SwipeDirection.x) > _minSpeed)
            {
                moveX = InputHandler.Instance.SwipeDirection.x;
            }
        }

        moveX = Mathf.Clamp(moveX, -_maxSpeed, _maxSpeed);
        moveX = moveX * _moveSpeed * Time.deltaTime;
        #endregion

        _rigidbody.velocity = new Vector3(moveX, moveY, _rigidbody.velocity.z);

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

        Gizmos.color = Color.yellow;

        Vector3 startPosition = transform.position;

        Vector3 endPosition = startPosition + _rigidbody.velocity;

        Gizmos.DrawLine(startPosition, endPosition);

    }

}
