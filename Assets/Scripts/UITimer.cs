using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Animator))]
public class UITimer : UIPanel
{
    [SerializeField] Sprite[] numbers;
    [SerializeField] Image _image;

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
        StateMachine.SetState<PauseState>();
        _animator.SetTrigger(_showNumberParamID);
    }

    public void EndShow()
    {
        _nextIndex++;

        if (_nextIndex < numbers.Length)
        {
            _image.sprite = numbers[_nextIndex];
            _animator.SetTrigger(_showNumberParamID);
        }
        else
        {
            UIHud.OnChangePanel(UIPanelName.Game);
        }
    }

    
}
