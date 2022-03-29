using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Animator))]
public class UITimer : MonoBehaviour
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
        Game.Instance.SetStatus(Game.Status.pause);
        _animator.SetTrigger(_showNumberParamID);
        transform.parent.SetAsLastSibling();
    }

    private void OnDisable()
    {
        if (Game.Instance != null)
        {
            Game.Instance.SetStatus(Game.Status.game);
        }
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
           transform.parent.gameObject.SetActive(false);
        }
    }

}
