using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    [SerializeField] private MeshCollider meshCollider;
    [SerializeField] private SphereCollider sphereCollider;
    [SerializeField] private new Rigidbody rigidbody;
    [SerializeField] private AudioClip hitClip;
    [SerializeField] protected LayerMask unitLayer = 0;
    [SerializeField] protected LayerMask groundLayer = 0;

    private const float maxAngularVelocity = 80;

    public PlayerType currentPlayerSide;
    public int playerHitCount;

    private Preference.Ball config;
    private MatchPreference matchPreference;

    private TrajectoryCalculate trajectory;

    public void Initialise(int ballIndex, MatchPreference matchPreference)
    {
        config = Preference.Singleton.balls[ballIndex];
        this.matchPreference = matchPreference;
        trajectory = new TrajectoryCalculate();
        rigidbody.isKinematic = true;
        sphereCollider.enabled = false;
        meshCollider.enabled = true;
    }

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

        if (config.maxMagnetude > 0)
        {
            currentVelosity = Vector3.ClampMagnitude(currentVelosity, config.maxMagnetude);
            rigidbody.velocity = currentVelosity;
        }

        if (config.maxAngularMagnitude > 0)
        {
            currentAngularVelosity = Vector3.ClampMagnitude(currentAngularVelosity, config.maxAngularMagnitude);
            rigidbody.angularVelocity = currentAngularVelosity;
        }

        if (StateMachine.currentState is GameState)
        {
            ChangeSide();

            if (transform.position.y < 0)
            {
                Game.Instance.OnRoundFall(currentPlayerSide);
            }

            trajectory.Calculate(transform.position, rigidbody.velocity, groundLayer);
        }
        else
        {
            trajectory.Clear();
        }
    }

    private void ChangeSide()
    {
        if (transform.position.x < 0 && currentPlayerSide != PlayerType.Local)
        {
            currentPlayerSide = PlayerType.Local;
            playerHitCount = matchPreference.BallHits;

            Game.Instance.UnitHitBall(currentPlayerSide, playerHitCount);
        }

        if (transform.position.x > 0 && currentPlayerSide != PlayerType.Rival)
        {
            currentPlayerSide = PlayerType.Rival;
            playerHitCount = matchPreference.BallHits;

            Game.Instance.UnitHitBall(currentPlayerSide, playerHitCount);
        }

        if (transform.position.x == 0 && currentPlayerSide != PlayerType.None)
        {
            currentPlayerSide = PlayerType.None;
            playerHitCount = matchPreference.BallHits;

            Game.Instance.UnitHitBall(currentPlayerSide, playerHitCount);
        }
    }

    private void OnChangeGameState(State next, State last)
    {
        //Debug.LogError(" OnChangeGameState: " + state);
        if (next is GameState)
        {
            if (rigidbody == null)
            {
                Debug.LogError("rigidbody is null ");
            }

            rigidbody.isKinematic = false;
            sphereCollider.enabled = true;
            meshCollider.enabled = false;
        }

        if (next is PauseState || next is TimerState)
        {
            rigidbody.isKinematic = true;
        }

        if (next is FallState)
        {
            sphereCollider.enabled = false;
            meshCollider.enabled = true;
            currentPlayerSide = PlayerType.None;
            playerHitCount = matchPreference.BallHits;
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
        Game.Instance.UnitHitBall(PlayerType.None, 0);
        Game.Instance.OnRoundFall(currentPlayerSide);
    }

    private void HitUnit()
    {
        playerHitCount--;

        if (playerHitCount < 0)
        {
            Game.Instance.OnRoundFall(currentPlayerSide);
            playerHitCount = 0;
        }

        Game.Instance.UnitHitBall(currentPlayerSide, playerHitCount);
    }

    public RaycastHit TrajectoryHit
    {
        get
        {
            return trajectory.Hit;
        }
    }

    public List<Vector3> TrajectoryPoints
    {
        get
        {
            return trajectory.Points;
        }
    }

}
