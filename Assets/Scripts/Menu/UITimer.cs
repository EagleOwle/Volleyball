using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Animator))]
public class UITimer : UIPanel
{
    [SerializeField] Sprite defaultSprite;
    [SerializeField] Sprite[] numbers;
    [SerializeField] Image _image;
    [SerializeField] private AudioClip tickClip;
    [SerializeField] private AudioClip tickEndClip;

    private Animator _animator;
    private int _nextIndex = 0;
    private int _showNumberParamID;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
        _showNumberParamID = Animator.StringToHash("Show");
    }

    private void OnEnable()
    {
        _image.sprite = defaultSprite;
        _nextIndex = 0;
        _animator.SetTrigger(_showNumberParamID);
    }

    public void EndShow()
    {
        _nextIndex++;

        if (_nextIndex < numbers.Length)
        {
            _image.sprite = numbers[_nextIndex];
            _animator.SetTrigger(_showNumberParamID);
            AudioController.Instance.PlayClip(tickClip);
        }
        else
        {
            UIHud.Singletone.OnChangePanel(UIPanelName.Game);
            AudioController.Instance.PlayClip(tickEndClip);
            StateMachine.SetState<GameState>();
        }
    }

    
}
