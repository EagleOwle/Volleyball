using System.Collections;
using UnityEngine;

public class PreviewSceneState : State
{
    private CameraMotion cameraMotion;

    public override void Enter()
    {
        nameState = "Previwe Scene";
        //cameraMotion = Camera.main.gameObject.AddComponent<CameraMotion>();
        cameraMotion = Camera.main.GetComponent<CameraMotion>();
        cameraMotion.eventCameraEndMotion += Camera_eventCameraEndMotion;
    }

    private void Camera_eventCameraEndMotion()
    {
        cameraMotion.eventCameraEndMotion -= Camera_eventCameraEndMotion;
        StateMachine.SetState<PauseState>();
    }

    public override void Exit()
    {
        cameraMotion.eventCameraEndMotion -= Camera_eventCameraEndMotion;
    }
}