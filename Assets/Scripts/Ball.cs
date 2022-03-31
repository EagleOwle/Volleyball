using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Ball : MonoBehaviour
{
    private Rigidbody _rigidbody;
    [SerializeField] private float _pushForce = 25f;
    [SerializeField] private float _maxMagnetude = 5;
    [SerializeField] private PhysicMaterial _ballPhysic, _defaultPhysic;
    [SerializeField] private Collider _collider;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
       
    }

    private void OnEnable()
    {
        Game.Instance.OnChangeMenu += PauseGame;
    }

    private void OnDisable()
    {
        if (Game.Instance == null) return;
        Game.Instance.OnChangeMenu -= PauseGame;
    }

    private void FixedUpdate()
    {
        Vector3 currentVelosity = _rigidbody.velocity;
        currentVelosity = Vector3.ClampMagnitude(currentVelosity, _maxMagnetude);
        _rigidbody.velocity = currentVelosity;
    }

    private void OnDrawGizmos()
    {
        if (_rigidbody == null) return;

        Gizmos.color = Color.yellow;

        Vector3 startPosition = transform.position;

        Vector3 endPosition = startPosition + _rigidbody.velocity;

        Gizmos.DrawLine(startPosition, endPosition);

    }

    private void PauseGame(Game.Status status)
    {
        switch (status)
        {
            case Game.Status.game:
                _rigidbody.isKinematic = false;
                _collider.material = _ballPhysic;
                break;
            case Game.Status.pause:
                _rigidbody.isKinematic = true;
                break;
            case Game.Status.fall:
                _collider.material = _defaultPhysic;
                break;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        Vector3 dir = collision.contacts[0].point - transform.position;
        dir = -dir.normalized;
        _rigidbody.AddForce(dir * _pushForce);

        if (collision.collider.gameObject.layer == LayerMask.NameToLayer("Ground"))
        {
           // Game.Instance.OnRoundFall.Invoke();
        }
    }


}
