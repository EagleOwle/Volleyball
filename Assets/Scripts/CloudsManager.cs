using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloudsManager : MonoBehaviour
{
    private enum MoveDirection
    {
        Left,
        Right
    }

    [SerializeField] private MoveDirection moveDirection;
    [SerializeField] private float speed = 1;
    [SerializeField] private float scaleFactor = 1f;
    [SerializeField] private float minX, maxX, minY, maxY, maxZ;
    [SerializeField] private Cloud[] clouds;

    private float speedFactor = 1f;

    private void Awake()
    {
        clouds = GetComponentsInChildren<Cloud>();
    }

    private void Start()
    {
        SetPosition();
    }

    private void SetPosition()
    {
        foreach (var item in clouds)
        {
            float x = Random.Range(minX, maxX);
            float y = Random.Range(minY, maxY);
            float z = maxZ - y;

            item.transform.position = new Vector3(x, y, z);

            float scale = 1f / item.transform.position.z * scaleFactor;
            item.transform.localScale = new Vector3(scale, scale, scale);

        }
    }

    private void LateUpdate()
    {
        switch (moveDirection)
        {
            case MoveDirection.Left:
                Move(Vector3.left);
                break;
            case MoveDirection.Right:
                Move(Vector3.right);
                break;
        }
    }

    private void Move(Vector3 direction)
    {
        foreach (var item in clouds)
        {
            speedFactor = (maxZ - item.transform.position.z) / maxZ * speed;

            if (Mathf.Abs(item.transform.position.x) > maxX)
            {

                item.transform.position = new Vector3(-item.transform.position.x,
                                                        item.transform.position.y,
                                                        item.transform.position.z);
                item.transform.Translate(direction * speedFactor * Time.deltaTime);
            }
            else
            {
                item.transform.Translate(direction * speedFactor * Time.deltaTime);
            }
        }
    }

}
