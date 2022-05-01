using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class UIGamePreference : MonoBehaviour
{
    public UnityEvent<float> actionReadMusicValue;
    public UnityEvent<float> actionReadSfxValue;
    public UnityEvent<float> actionReadInputDeadZone;
    public UnityEvent<float> actionReadUnitMoveSpeed;
    public UnityEvent<float> actionReadUnitJumpForce;
    public UnityEvent<float> actionReadUnitHorizontalMultiply;
    public UnityEvent<float> actionReadUnitDownSpeed;
    public UnityEvent<float> actionReadAiMoveDeadZone;
    public UnityEvent<float> actionReadAiJumpDeadZone;
    public UnityEvent<float> actionReadBallPushForce;
    public UnityEvent<float> actionReadBallMaxValocity;
    public UnityEvent<float> actionSetDefaultValue;

    private void OnEnable()
    {
        ReadValue();
    }

    private void ReadValue()
    {
        actionReadMusicValue.Invoke(Preference.Singleton.musicValue);
        actionReadSfxValue.Invoke(Preference.Singleton.sfxValue);
        actionReadInputDeadZone.Invoke(Preference.Singleton.InputDeadZone);
        actionReadUnitMoveSpeed.Invoke(Preference.Singleton.moveSpeed);
        actionReadUnitJumpForce.Invoke(Preference.Singleton.jumpForce);
        actionReadUnitHorizontalMultiply.Invoke(Preference.Singleton.horizontalMultiply);
        actionReadUnitDownSpeed.Invoke(Preference.Singleton.downSpeed);
        actionReadAiMoveDeadZone.Invoke(Preference.Singleton.deadzoneForMove);
        actionReadAiJumpDeadZone.Invoke(Preference.Singleton.deadzoneForJump);
        actionReadBallPushForce.Invoke(Preference.Singleton.pushForce);
        actionReadBallMaxValocity.Invoke(Preference.Singleton.maxMagnetude);
    }

    public void SliderChangeMaxMagnetude(float value)
    {
        Preference.Singleton.maxMagnetude = value;
    }

    public void SliderChangePushForce(float value)
    {
        Preference.Singleton.pushForce = value;
    }

    public void SliderChangeDeadzoneForJump(float value)
    {
        Preference.Singleton.deadzoneForJump = value;
    }

    public void SliderChangeDeadzoneForMove(float value)
    {
        Preference.Singleton.deadzoneForMove = value;
    }

    public void SliderChangeDownSpeed(float value)
    {
        Preference.Singleton.downSpeed = value;
    }

    public void SliderChangeHorizontalMultiply(float value)
    {
        Preference.Singleton.horizontalMultiply = value;
    }

    public void SliderChangeJumpForce(float value)
    {
        Preference.Singleton.jumpForce = value;
    }

    public void SliderChangeInputMoveSpeed(float value)
    {
        Preference.Singleton.moveSpeed = value;
    }

    public void SliderChangeMusicValue(float value)
    {
        Preference.Singleton.musicValue = value;
    }

    public void SliderChangeSfxValue(float value)
    {
        Preference.Singleton.sfxValue = value;
    }

    public void SliderChangeInputDeadZone(float value)
    {
        Preference.Singleton.InputDeadZone = value;
    }

    public void SetDefaultValue()
    {
        Preference.Singleton.SetDefaultValue();
        ReadValue();
    }


}
