using System;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerMotion : MonoBehaviour
{
    [SerializeField] private float _maxSpeed = 3;
    [SerializeField] private float _minSpeed = 3;
    [SerializeField] private float _moveSpeed = 3;
    [SerializeField] private float _maxJump = 5;
    [SerializeField] private float _jumpSence = 1;
    [SerializeField] private float _jumpForce = 500;
    [SerializeField] private LayerMask _checkGroundMask;

    private Rigidbody _rigidbody;
    private bool _onGround = false;
    private float clampX;
    private float clampY;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }

    private void OnEnable()
    {
       Game.Instance.OnChangeMenu += PauseGame;
    }

    private void Start()
    {
        _onGround = false;
    }

    private void FixedUpdate()
    {
        #region Jump
        clampY = _rigidbody.velocity.y;
        if (_onGround)
        {
            if (InputHandler.Instance.SwipeDirection.y > _jumpSence)
            {
                clampY = _jumpForce;
            }
        }
        clampY = Mathf.Clamp(clampY, Physics.gravity.y, _maxJump);
        #endregion

        #region Move

        clampX = 0;
        if (InputHandler.Instance.OnSwipe)
        {
            if (Mathf.Abs(InputHandler.Instance.SwipeDirection.x) > _minSpeed)
            {
                clampX = InputHandler.Instance.SwipeDirection.x;
            }
        }

        clampX = Mathf.Clamp(clampX, -_maxSpeed, _maxSpeed);
        clampX = clampX * _moveSpeed * Time.deltaTime;
        #endregion

        _rigidbody.velocity = new Vector3(clampX, clampY, _rigidbody.velocity.z);

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

    private void OnDisable()
    {
        if (Game.Instance == null) return;
        Game.Instance.OnChangeMenu -= PauseGame;
    }

    private void PauseGame(Game.Status status)
    {
        //switch (status)
        //{
        //    case Game.Status.game:
        //        _rigidbody.isKinematic = false;
        //        break;
        //    case Game.Status.pause:
        //        _rigidbody.isKinematic = true;
        //        break;
        //    case Game.Status.fall:
        //        _rigidbody.isKinematic = true;
        //        break;
        //}
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
