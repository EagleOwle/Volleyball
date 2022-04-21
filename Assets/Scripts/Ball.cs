using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Ball : MonoBehaviour
{
    [SerializeField] private float pushForce = 25f;
    [SerializeField] private float maxMagnetude = 5;
    [SerializeField] private PhysicMaterial ballPhysic, defaultPhysic;
    [SerializeField] private new Collider collider;
    private new Rigidbody rigidbody;

    private void Awake()
    {
        rigidbody = GetComponent<Rigidbody>();
    }

    private void OnEnable()
    {
        StateMachine.actionChangeState += PauseGame;
    }

    private void OnDisable()
    {
        if (Game.Instance == null) return;
        StateMachine.actionChangeState -= PauseGame;
    }

    private void FixedUpdate()
    {
        Vector3 currentVelosity = rigidbody.velocity;
        currentVelosity = Vector3.ClampMagnitude(currentVelosity, maxMagnetude);
        rigidbody.velocity = currentVelosity;
    }

    private void OnDrawGizmos()
    {
        if (rigidbody == null) return;

        Gizmos.color = Color.yellow;
        Vector3 startPosition = transform.position;
        Vector3 endPosition = startPosition + rigidbody.velocity;
        Gizmos.DrawLine(startPosition, endPosition);

    }

    private void PauseGame(State state)
    {
        if(state is GameState)
        {
            rigidbody.isKinematic = false;
            collider.material = ballPhysic;
        }

        if (state is PauseState)
        {
            rigidbody.isKinematic = true;
        }

        if (state is FallState)
        {
            collider.material = defaultPhysic; ;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        Vector3 dir = collision.contacts[0].point - transform.position;
        dir = -dir.normalized;
        rigidbody.AddForce(dir * pushForce);

        if (collision.collider.gameObject.layer == LayerMask.NameToLayer("Ground"))
        {
           // Game.Instance.OnRoundFall.Invoke();
        }
    }


}
