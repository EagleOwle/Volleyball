using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Ball : MonoBehaviour
{
    [SerializeField] private PhysicMaterial ballPhysic, defaultPhysic;
    [SerializeField] private new Collider collider;
    [SerializeField] private AudioClip hitClip;
    [SerializeField] protected LayerMask unitLayer = 0;
    [SerializeField] protected LayerMask groundLayer = 0;
    [SerializeField] private TrajectoryRender trajectoryRender;
    private new Rigidbody rigidbody;

    public PlayerType currentPlayerSide;
    public int playerHitCount;

    public Action<PlayerType, int> ActionUnitHit;

    private void Start()
    {
        trajectoryRender.ShowTrajectory(transform.position, rigidbody.velocity);
    }

    private void OnEnable()
    {
        rigidbody = GetComponent<Rigidbody>();
        trajectoryRender = GameObject.FindObjectOfType<TrajectoryRender>();
        StateMachine.actionChangeState += PauseGame;

        currentPlayerSide = PlayerType.None;
        playerHitCount = 0;
    }

    private void OnDisable()
    {
        if (Game.Instance == null) return;
        StateMachine.actionChangeState -= PauseGame;
    }

    private void FixedUpdate()
    {
        Vector3 currentVelosity = rigidbody.velocity;

        if (Preference.Singletone.maxMagnetude > 0)
        {
            currentVelosity = Vector3.ClampMagnitude(currentVelosity, Preference.Singletone.maxMagnetude);

            if (StateMachine.currentState is GameState)
            {
                trajectoryRender.ShowTrajectory(transform.position, rigidbody.velocity);
            }
            else
            {
                trajectoryRender.Hide();
            }

            rigidbody.velocity = currentVelosity;
        }

        if (StateMachine.currentState is GameState)
        {
            ChangeSide();

            if (transform.position.y < 0)
            {
                Game.Instance.OnRoundFall(currentPlayerSide);
            }
        }
    }

    private void ChangeSide()
    {
        if (transform.position.x < 0 && currentPlayerSide != PlayerType.Local)
        {
            currentPlayerSide = PlayerType.Local;
            playerHitCount = 0;

            ActionUnitHit?.Invoke(currentPlayerSide, playerHitCount);
        }

        if (transform.position.x > 0 && currentPlayerSide != PlayerType.Rival)
        {
            currentPlayerSide = PlayerType.Rival;
            playerHitCount = 0;

            ActionUnitHit?.Invoke(currentPlayerSide, playerHitCount);
        }

        if (transform.position.x == 0 && currentPlayerSide != PlayerType.None)
        {
            currentPlayerSide = PlayerType.None;
            playerHitCount = 0;

            ActionUnitHit?.Invoke(currentPlayerSide, playerHitCount);
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

    private void OnCollisionEnter(Collision collision)
    {
        if (StateMachine.currentState is GameState)
        {
            Vector3 dir = collision.contacts[0].point - transform.position;
            dir = -dir.normalized;
            rigidbody.AddForce(dir * Preference.Singletone.pushForce);

            AudioController.Instance.PlayClip(hitClip);

            if ((1 << collision.collider.gameObject.layer & unitLayer) != 0)
            {
                Unit unit = collision.collider.GetComponentInParent<Unit>();

                HitUnit();

                ActionUnitHit?.Invoke(currentPlayerSide, playerHitCount);
            }

            if ((1 << collision.collider.gameObject.layer & groundLayer) != 0)
            {
                ActionUnitHit?.Invoke(PlayerType.None, 0);
                Game.Instance.OnRoundFall(currentPlayerSide);
            }
        }
    }

    private void HitUnit()
    {
        playerHitCount++;

        if (playerHitCount > 3)
        {
            ActionUnitHit?.Invoke(PlayerType.None, 0);
            Game.Instance.OnRoundFall(currentPlayerSide);
        }
    }
}
