using System;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerMotion : MonoBehaviour
{
    [SerializeField] private float _speed = 3;
    [SerializeField] private float _jumpSence = 1;
    [SerializeField] private float _jumpForce = 500;
    [SerializeField] private LayerMask _checkGroundMask;
    private Rigidbody _rigidbody;
    private float _targetX;
    private float _startX;
    private Vector3 _targetPosition;
    private bool _onGround = false;
    

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }

    private void OnEnable()
    {
        Game.Instance.eventChangeStatus.AddListener(PauseGame);
    }

    private void Start()
    {
        _onGround = false;
    }

    private void FixedUpdate()
    {
        //if (Game.Instance.status == Game.Status.pause)
        //{
        //    _rigidbody.velocity = Vector3.zero;
        //    return;
        //}

        if (InputHandler.Instance.OnSwipe == false)
        {
            _targetX = 0;
            _startX = _rigidbody.position.x;
        }
        else
        {
            _targetX = InputHandler.Instance.SwipeDirection.x;
        }

        if (_onGround)
        {
            if (InputHandler.Instance.SwipeDirection.y > _jumpSence)
            {
                _rigidbody.AddForce(Vector3.up * _jumpForce);
            }
        }

        //_targetPosition = new Vector3(_startX + _targetX, _rigidbody.position.y, _rigidbody.position.z);
        //_targetPosition = Vector3.MoveTowards(_rigidbody.position, _targetPosition, _speed * Time.deltaTime);
        //_rigidbody.MovePosition(_targetPosition);
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
        Game.Instance.eventChangeStatus.RemoveListener(PauseGame);
    }

    private void PauseGame(Game.Status status)
    {
        switch (status)
        {
            case Game.Status.game:
                _rigidbody.isKinematic = false;
                break;
            case Game.Status.pause:
                _rigidbody.isKinematic = true;
                break;
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
