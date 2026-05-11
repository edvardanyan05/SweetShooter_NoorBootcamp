using UnityEngine;
using UnityEngine.EventSystems;

[RequireComponent(typeof(LineRenderer))]
public class Trajectory : MonoBehaviour
{
    public int pointsCount = 20;
    public float spaceBetweenPoints = 0.1f;
    public float force = 10f;
    public float startWidth = 0.15f;
    public float endWidth = 0.05f;

    private LineRenderer lr;
    private Vector2 currentTouch;

    void Start()
    {
        UIState.IsUIOpen = false;
        lr = GetComponent<LineRenderer>();

        Material mat = new Material(Shader.Find("Sprites/Default"));
        lr.material = mat;

        lr.startColor = new Color(1f, 1f, 1f, 0.4f);
        lr.endColor = new Color(1f, 1f, 1f, 0f);

        lr.startWidth = startWidth;
        lr.endWidth = endWidth;
    }

    void Update()
    {
        if (UIState.IsUIOpen) return;

        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            if (EventSystem.current.IsPointerOverGameObject(touch.fingerId))
                return;
            if (touch.phase == TouchPhase.Moved || touch.phase == TouchPhase.Stationary)
            {
                currentTouch = touch.position;
                DrawLine();
            }

            if (touch.phase == TouchPhase.Ended)
                lr.positionCount = 0;
        }
        else
        {
            if (EventSystem.current.IsPointerOverGameObject())
                return;
            if (Input.GetMouseButton(0))
            {
                currentTouch = Input.mousePosition;
                DrawLine();
            }

            if (Input.GetMouseButtonUp(0))
                lr.positionCount = 0;
        }
    }

    void DrawLine()
    {
        lr.startWidth = startWidth;
        lr.endWidth = endWidth;

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