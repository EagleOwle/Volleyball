﻿using UnityEngine;
using System.Collections;

public abstract class Unit : MonoBehaviour
{
    [SerializeField] protected UnitMotion unitMotion;
    [SerializeField] protected Transform meshTransform;

    [SerializeField] protected LayerMask ballLayer = 0;

    protected PlayerSide player;
    Vector3 forceDirection;

    public virtual void Initialise(PlayerSide player)
    {
        this.player = player;

        if (player == PlayerSide.Left)
        {
             meshTransform.eulerAngles = new Vector3(meshTransform.eulerAngles.x, 90, meshTransform.eulerAngles.z);
        }

        if (player == PlayerSide.Right)
        {
            meshTransform.eulerAngles = new Vector3(meshTransform.eulerAngles.x, -90, meshTransform.eulerAngles.z);
        }

        StateMachine.actionChangeState += OnChangeGameState;
        ClearValue();
    }

    private float PushForce()
    {
        switch (Game.Instance.scene.difficultEnum)
        {
            case GameDifficult.Easy:
                return Preference.Singleton.pushForce;// * 2;

            case GameDifficult.Normal:
                return Preference.Singleton.pushForce;

            case GameDifficult.Hard:
                return Preference.Singleton.pushForce;

            default:
                return Preference.Singleton.pushForce;
        }
    }

    private void OnChangeGameState(State next, State last)
    {
        if (next is GameState)
        {
            ClearValue();
        }
    }

    protected virtual void ClearValue()
    {
    }

    protected virtual void OnCollisionEnter(Collision collision)
    {
        if (StateMachine.currentState is GameState)
        {
            if ((1 << collision.gameObject.layer & ballLayer) != 0)
            {
                if (player == PlayerSide.Left)
                {
                    forceDirection = (Vector3.right + Vector3.up) * PushForce();
                }

                if (player == PlayerSide.Right)
                {
                    forceDirection = (Vector3.left + Vector3.up) * PushForce();
                }

                Rigidbody rigidbody = collision.collider.gameObject.GetComponentInParent<Rigidbody>();
                rigidbody.AddForce(forceDirection, ForceMode.Impulse);
            }
        }
    }

    protected virtual void OnDisable()
    {
        StateMachine.actionChangeState -= OnChangeGameState;
    }

    protected virtual void OnDestroy()
    {
        StateMachine.actionChangeState -= OnChangeGameState;
    }

}
