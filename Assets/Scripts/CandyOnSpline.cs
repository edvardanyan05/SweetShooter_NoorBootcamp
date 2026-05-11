using UnityEngine;
using UnityEngine.Splines;

public class CandyOnSpline : MonoBehaviour
{
    [HideInInspector] public float position;
    [HideInInspector] public CandySplineManager manager;

    void Update()
    {
        if (manager == null || manager.splineContainer == null) return;

        float looped = Mathf.Repeat(position, 1f);
        transform.position = (Vector3)manager.splineContainer.EvaluatePosition(looped);
    }
}