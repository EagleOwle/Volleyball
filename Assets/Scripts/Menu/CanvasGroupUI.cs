using UnityEngine;
using System.Collections;
using System;
using System.Threading.Tasks;


public class CanvasGroupUI : UICanvasGroupBase
{
	[SerializeField] private float fadeTime = 0.5f;

	public async virtual void Show()
	{
		gameObject.SetActive(true);
		CanvasGroup.alpha = 0;

		while (CanvasGroup.alpha < 1)
		{
			await Task.Yield();
			CanvasGroup.alpha += fadeTime * Time.deltaTime;
		}
	}

	public async virtual void Hide()
	{
		CanvasGroup.alpha = 1;

		while (CanvasGroup.alpha > 0)
		{
			CanvasGroup.alpha -= fadeTime * Time.deltaTime;
			await Task.Yield();
		}

		gameObject.SetActive(false);

	}

    private void OnDestroy()
    {
		CanvasGroup.alpha = 1;
	}

}