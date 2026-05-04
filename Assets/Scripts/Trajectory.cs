using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
public class Trajectory : MonoBehaviour
{
    public int pointsCount = 20;
    public float spaceBetweenPoints = 0.1f;
    public float force = 10f;

    private LineRenderer lr;
    private Vector2 startTouch;
    private Vector2 currentTouch;

    void Start()
    {
        lr = GetComponent<LineRenderer>();
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
            startTouch = Input.mousePosition;

        if (Input.GetMouseButton(0))
        {
            currentTouch = Input.mousePosition;
            DrawLine();
        }

        if (Input.GetMouseButtonUp(0))
            lr.positionCount = 0;
    }

    void DrawLine()
    {
        Vector2 dir = (startTouch - currentTouch).normalized;

        lr.positionCount = pointsCount;

        for (int i = 0; i < pointsCount; i++)
        {
            float t = i * spaceBetweenPoints;
            Vector3 pos = transform.position + (Vector3)(dir * force * t);

            lr.SetPosition(i, pos);
        }
    }
}