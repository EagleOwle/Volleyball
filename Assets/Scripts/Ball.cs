using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Ball : MonoBehaviour
{
    private Rigidbody _rigidbody;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
       
    }

    private void OnEnable()
    {
        Game.Instance.eventChangeStatus.AddListener(PauseGame);
    }

    private void OnDisable()
    {
        Game.Instance.eventChangeStatus.RemoveListener(PauseGame);
    }

    private void FixedUpdate()
    {
        //if(Game.Instance.status == Game.Status.pause)
        //{
        //    _rigidbody.velocity = Vector3.zero;
        //    return;
        //}

        Vector3 currentVelosity = _rigidbody.velocity;
        currentVelosity = Vector3.ClampMagnitude(currentVelosity, 15);
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
                break;
            case Game.Status.pause:
                _rigidbody.isKinematic = true;
                break;
        }
    }

}
