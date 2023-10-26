using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
public class TrajectoryRender : MonoBehaviour
{
    private LineRenderer lineRenderer;
    private Ball _ball;

    public void Initialise(Ball ball)
    {
        lineRenderer = GetComponent<LineRenderer>();
        _ball = ball;
    }

    private void LateUpdate()
    {
        if (_ball == null) return;

        if (StateMachine.currentState is GameState)
        {
            Show();
        }
        else
        {
            Hide();
        }
    }

    private void Show()
    {
        lineRenderer.positionCount = _ball.TrajectoryPoints.Count;
        lineRenderer.SetPositions(_ball.TrajectoryPoints.ToArray());
    }

    private void Hide()
    {
        lineRenderer.positionCount = 0;
    }

}
