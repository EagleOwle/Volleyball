using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public static class CoroutineUtility
{
    public static IEnumerator After(this IEnumerator coroutine, float seconds)
    {
        yield return new WaitForSeconds(seconds);
        yield return coroutine;
    }
}

public class UIEditInputElement : MonoBehaviour, IDragHandler, IPointerUpHandler, IPointerDownHandler, IBeginDragHandler, IEndDragHandler
{
    [SerializeField] private CanvasGroup canvasGroup;
    [SerializeField] private Rigidbody2D rigidbody;
    private bool onCollision = false;

    private string debugCollision;

    public void OnBeginDrag(PointerEventData eventData)
    {
    }

    public void OnEndDrag(PointerEventData eventData)
    {
    }

    public void OnDrag(PointerEventData eventData)
    {
       transform.position = eventData.position;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        canvasGroup.alpha = 0.5f;
        rigidbody.isKinematic = false;
        StopCoroutine(WaitEndCollision());
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        canvasGroup.alpha = 1;
        StartCoroutine(WaitEndCollision());
    }


    private IEnumerator WaitEndCollision()
    {
        while (onCollision == true)
        {
            yield return null;
        }

        rigidbody.isKinematic = true;

    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        debugCollision = collision.collider.name;
        onCollision = true;
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        onCollision = false;
        debugCollision = "None";
    }

}
