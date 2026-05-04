using UnityEngine;

public class Shooter : MonoBehaviour
{
    public GameObject[] candyPrefab;
    public Transform shootPoint;
    public Transform nextPoint;
    public float force = 10f;

    private Vector2 startTouch;
    private Vector2 currentTouch;

    private int currentCandyIndex = 0;

    private GameObject currentCandy;
    private GameObject nextCandy;

    void Start()
    {
        SpawnPreview();
    }

    void Update()
    {
        if (Input.touchCount == 1)
        {
            Touch touch = Input.GetTouch(0);

            if (touch.phase == TouchPhase.Began)
                startTouch = touch.position;

            if (touch.phase == TouchPhase.Moved)
            {
                currentTouch = touch.position;
                RotateShooter();
            }

            if (touch.phase == TouchPhase.Ended)
            {
                currentTouch = touch.position;
                Shoot();
            }
        }

        if (Input.GetMouseButtonDown(0))
            startTouch = Input.mousePosition;

        if (Input.GetMouseButton(0))
        {
            currentTouch = Input.mousePosition;
            RotateShooter();
        }

        if (Input.GetMouseButtonUp(0))
        {
            currentTouch = Input.mousePosition;
            Shoot();
        }
    }

    void Shoot()
    {
        if (currentCandy == null) return;

        Vector2 direction = transform.up;
        GameObject bullet = currentCandy;
        bullet.transform.parent = null;

        Rigidbody rb = bullet.GetComponent<Rigidbody>();
        rb.isKinematic = false;
        rb.linearVelocity = direction * force;

        currentCandyIndex++;

        if (nextCandy != null)
            Destroy(nextCandy);

        SpawnPreview();
    }

    void SpawnPreview()
    {
        if (currentCandyIndex < candyPrefab.Length)
        {
            currentCandy = Instantiate(
                candyPrefab[currentCandyIndex],
                shootPoint.position,
                Quaternion.identity,
                shootPoint
            );

            Rigidbody rb = currentCandy.GetComponent<Rigidbody>();
            if (rb != null)
                rb.isKinematic = true;
        }
        else
        {
            currentCandy = null;
        }

        if (currentCandyIndex + 1 < candyPrefab.Length)
        {
            nextCandy = Instantiate(
                candyPrefab[currentCandyIndex + 1],
                nextPoint.position,
                Quaternion.identity,
                nextPoint
            );

            Rigidbody rb = nextCandy.GetComponent<Rigidbody>();
            if (rb != null)
                rb.isKinematic = true;
        }
        else
        {
            nextCandy = null;
        }
    }

    void RotateShooter()
    {
        Vector2 direction = (startTouch - currentTouch).normalized;

        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        transform.rotation = Quaternion.Euler(0, 0, angle - 90f);
    }
}