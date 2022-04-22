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
    [SerializeField] private AudioClip hitClip;
    [SerializeField] private TrajectoryRender trajectoryRender;
    private new Rigidbody rigidbody;

    private void Awake()
    {
        rigidbody = GetComponent<Rigidbody>();
        trajectoryRender = GameObject.FindObjectOfType<TrajectoryRender>();
    }

    private void Start()
    {
        trajectoryRender.ShowTrajectory(transform.position, rigidbody.velocity);
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

        if (maxMagnetude > 0)
        {
            currentVelosity = Vector3.ClampMagnitude(currentVelosity, maxMagnetude);
            rigidbody.velocity = currentVelosity;
        }

        if (transform.position.y < 0)
        {
            Game.Instance.OnRoundFall.Invoke();
        }
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
            collider.material = defaultPhysic;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        Vector3 dir = collision.contacts[0].point - transform.position;
        dir = -dir.normalized;
        rigidbody.AddForce(dir * pushForce);

        AudioController.Instance.PlayClip(hitClip);

        if (collision.collider.gameObject.layer == LayerMask.NameToLayer("Ground"))
        {
           //Game.Instance.OnRoundFall.Invoke();
        }

        trajectoryRender.ShowTrajectory(transform.position, rigidbody.velocity);
    }


}
