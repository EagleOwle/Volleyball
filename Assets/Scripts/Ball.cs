using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    [SerializeField] private PhysicMaterial ballPhysic, defaultPhysic;
    [SerializeField] private new Collider collider;
    [SerializeField] private new Rigidbody rigidbody;
    [SerializeField] private AudioClip hitClip;
    [SerializeField] protected LayerMask unitLayer = 0;
    [SerializeField] protected LayerMask groundLayer = 0;

    private const float maxAngularVelocity = 80;

    public PlayerType currentPlayerSide;
    public int playerHitCount;
    public Action<PlayerType, int> actionUnitHit;

    private void OnEnable()
    {
        StateMachine.actionChangeState += OnChangeGameState;
        rigidbody.maxAngularVelocity = maxAngularVelocity;

    }

    private void OnDisable()
    {
        StateMachine.actionChangeState -= OnChangeGameState;
    }

    private void OnDestroy()
    {
        StateMachine.actionChangeState -= OnChangeGameState;
    }

    private void FixedUpdate()
    {
        Vector3 currentVelosity = rigidbody.velocity;
        Vector3 currentAngularVelosity = rigidbody.angularVelocity;

        if (Preference.Singleton.maxMagnetude > 0)
        {
            currentVelosity = Vector3.ClampMagnitude(currentVelosity, Preference.Singleton.maxMagnetude);
            rigidbody.velocity = currentVelosity;
        }

        if (Preference.Singleton.maxAngularMagnitude > 0)
        {
            currentAngularVelosity = Vector3.ClampMagnitude(currentAngularVelosity, Preference.Singleton.maxAngularMagnitude);
            rigidbody.angularVelocity = currentAngularVelosity;
        }

        if (StateMachine.currentState is GameState)
        {
            ChangeSide();

            if (transform.position.y < 0)
            {
                Game.Instance.OnRoundFall(currentPlayerSide);
            }

            if (Game.Instance.scene.difficultEnum != ScenePreference.GameDifficult.Hard)
            {
                TrajectoryRender.Instance.ShowTrajectory(transform.position, rigidbody.velocity);
            }
            else
            {
                TrajectoryRender.Instance.Hide();
            }
        }
        else
        {
            TrajectoryRender.Instance.Hide();
        }
    }

    private void ChangeSide()
    {
        if (transform.position.x < 0 && currentPlayerSide != PlayerType.Local)
        {
            currentPlayerSide = PlayerType.Local;
            playerHitCount = 0;

            actionUnitHit?.Invoke(currentPlayerSide, playerHitCount);
        }

        if (transform.position.x > 0 && currentPlayerSide != PlayerType.Rival)
        {
            currentPlayerSide = PlayerType.Rival;
            playerHitCount = 0;

            actionUnitHit?.Invoke(currentPlayerSide, playerHitCount);
        }

        if (transform.position.x == 0 && currentPlayerSide != PlayerType.None)
        {
            currentPlayerSide = PlayerType.None;
            playerHitCount = 0;

            actionUnitHit?.Invoke(currentPlayerSide, playerHitCount);
        }
    }

    private void OnChangeGameState(State state)
    {
        //Debug.LogError(" OnChangeGameState: " + state);
        if (state is GameState)
        {
            if (rigidbody == null)
            {
                Debug.LogError("rigidbody is null ");
            }

            rigidbody.isKinematic = false;
            collider.material = ballPhysic;
        }

        if (state is PauseState || state is TimerState)
        {
            rigidbody.isKinematic = true;
        }

        if (state is FallState)
        {
            collider.material = defaultPhysic;
            currentPlayerSide = PlayerType.None;
            playerHitCount = 0;
            transform.rotation = Quaternion.identity;
        }
    }


    private void OnCollisionEnter(Collision collision)
    {
        if (StateMachine.currentState is GameState)
        {
            AudioController.Instance.PlayClip(hitClip, true);

            if ((1 << collision.collider.gameObject.layer & unitLayer) != 0)
            {
                HitUnit();
            }

            if ((1 << collision.collider.gameObject.layer & groundLayer) != 0)
            {
                HitEnvironment();
            }
        }
    }

    private void HitEnvironment()
    {
        actionUnitHit?.Invoke(PlayerType.None, 0);
        Game.Instance.OnRoundFall(currentPlayerSide);
    }

    private void HitUnit()
    {
        playerHitCount++;

        if (playerHitCount > 3)
        {
            actionUnitHit?.Invoke(PlayerType.None, 0);
            Game.Instance.OnRoundFall(currentPlayerSide);
        }
        else
        {
            actionUnitHit?.Invoke(currentPlayerSide, playerHitCount);
        }
    }
}
