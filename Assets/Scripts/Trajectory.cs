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
        Vector3 shooterScreenPos = Camera.main.WorldToScreenPoint(transform.position);
        Vector2 dir = ((Vector2)currentTouch - (Vector2)shooterScreenPos).normalized;

        lr.positionCount = pointsCount;

        for (int i = 0; i < pointsCount; i++)
        {
            float t = i * spaceBetweenPoints;
            Vector3 pos = transform.position + new Vector3(dir.x, dir.y, 0f) * force * t;

            lr.SetPosition(i, pos);
        }
    }
}