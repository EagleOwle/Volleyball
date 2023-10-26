using UnityEngine;
using System.Collections;
using System.Threading.Tasks;
using System.Threading;
using System;

public class EffectChangeSize : MonoBehaviour
{
    [SerializeField] private float speed = 0.1f;

    private CancellationTokenSource cts = new CancellationTokenSource();

    private async void OnEnable()
    {
        transform.localScale = Vector3.zero;

        cts = new CancellationTokenSource();
        CancellationToken token = cts.Token;

        try
        {
            //await CreateGridWorld.CreateGrid(width, height, pixels, biomData, actionZoneData, CompleteCreateGrid, token);
            await Processe(token);
        }
        catch (OperationCanceledException)
        {
            Debug.Log("CreateGrid was cancelled.");
        }
    }

    private async Task Processe(CancellationToken token)
    {
        while (transform.localScale.x < 1)
        {
            token.ThrowIfCancellationRequested();
            transform.localScale += Vector3.one * (speed * Time.deltaTime);
            await Task.Yield();
        }
        
    }

    private void OnDestroy()
    {
        cts.Cancel();
    }

}
