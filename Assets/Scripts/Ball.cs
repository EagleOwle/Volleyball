using System;
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
    [SerializeField] protected LayerMask unitLayer = 0;
    [SerializeField] protected LayerMask groundLayer = 0;
    [SerializeField] private TrajectoryRender trajectoryRender;
    private new Rigidbody rigidbody;

    private GameObject lastUnit;
    private int hitCount;
    public int HitCount
    {
        set
        {
            hitCount = value;
            ActionHitCount?.Invoke(hitCount);
        }

        get
        {
            return hitCount;
        }
    }

    public Action<int> ActionHitCount;

    private void Start()
    {
        trajectoryRender.ShowTrajectory(transform.position, rigidbody.velocity);
    }

    private void OnEnable()
    {
        rigidbody = GetComponent<Rigidbody>();
        trajectoryRender = GameObject.FindObjectOfType<TrajectoryRender>();
        StateMachine.actionChangeState += PauseGame;
        HitCount = 0;
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
            trajectoryRender.ShowTrajectory(transform.position, rigidbody.velocity);
            rigidbody.velocity = currentVelosity;
        }

        if (transform.position.y < 0)
        {
            Game.Instance.OnRoundFall.Invoke();
        }
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

    private void HitUnit(GameObject obj)
    {
        if (obj == lastUnit)
        {
            HitCount++;

            if(hitCount > 3 )
            {
                Game.Instance.OnRoundFall.Invoke();
            }
        }
        else
        {
            HitCount = 1;
            lastUnit = obj;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        Vector3 dir = collision.contacts[0].point - transform.position;
        dir = -dir.normalized;
        rigidbody.AddForce(dir * pushForce);

        AudioController.Instance.PlayClip(hitClip);

        //Debug.LogError("Hit " + collision.collider.gameObject.layer + "/ " + collision.collider.name);

        if ((1 << collision.collider.gameObject.layer & unitLayer) != 0)
        {
            HitUnit(collision.gameObject);
            return;
        }

        if ((1 << collision.collider.gameObject.layer & groundLayer) != 0)
        {
            Game.Instance.OnRoundFall.Invoke();
        }
    }


}
