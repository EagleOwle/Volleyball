using UnityEngine;
using System.Collections;
using System.Threading.Tasks;

public class EffectChangeSize : MonoBehaviour
{
    [SerializeField] private float speed = 0.1f;

    private void OnEnable()
    {
        transform.localScale = Vector3.zero;
        Processe();
    }

    private async void Processe()
    {
        while (transform.localScale.x < 1)
        {
            await Task.Yield();

            transform.localScale += Vector3.one * (speed * Time.deltaTime);
        }
        
    }
}
