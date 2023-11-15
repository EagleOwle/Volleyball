using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkyboxRotation : MonoBehaviour
{
    public float speed = 0.2f;

    private int RotationProperty;
    private float initRot;
    private Material skyMat;

    private void OnDisable() => skyMat.SetFloat(RotationProperty, initRot);

    private void Start()
    {
        RotationProperty = Shader.PropertyToID("_Rotation");
        skyMat = RenderSettings.skybox;
        initRot = skyMat.GetFloat(RotationProperty);
    }

    private void Update() => skyMat.SetFloat(RotationProperty, Time.time * speed);

}
