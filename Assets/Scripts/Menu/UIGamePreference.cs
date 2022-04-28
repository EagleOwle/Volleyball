using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIGamePreference : MonoBehaviour
{
    [SerializeField] private GameObject prefab;
    [SerializeField] private RectTransform content;
    [SerializeField] private int minContentCount = 6;

    private GameObject[] items; 

    private void OnEnable()
    {
        
    }

}
