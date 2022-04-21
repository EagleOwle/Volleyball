using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIFallPanel : MonoBehaviour
{
    [SerializeField] private Button _ressumBtn;

    private void Start()
    {
        _ressumBtn.onClick.AddListener(OnButtonRessum);
    }

    private void OnButtonRessum()
    {
        Game.Instance.RestartMatch();
    }


}
