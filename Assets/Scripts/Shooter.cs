using UnityEngine;

public class Shooter : MonoBehaviour
{
    public GameObject[] candyPrefab;
    public Transform shootPoint;
    public Transform nextPoint;
    public float force = 10f;

    private Vector2 currentTouch;

    private int currentCandyIndex = 0;

    private GameObject currentCandy;
    private GameObject nextCandy;

    private Vector2 shootDirection;
    void Start()
    {
        SpawnPreview();
    }

    void Update()
    {
        if (Input.touchCount == 1)
        {
            Touch touch = Input.GetTouch(0);

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

        GameObject bullet = currentCandy;
        bullet.transform.parent = null;

        Rigidbody rb = bullet.GetComponent<Rigidbody>();
        rb.isKinematic = false;
        rb.useGravity = false;
        rb.linearDamping = 0f;
        rb.angularDamping = 0f;
        rb.linearVelocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;
        rb.rotation = Quaternion.identity;
        rb.linearVelocity = new Vector3(shootDirection.x, shootDirection.y, 0f) * force;

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
        Vector3 shooterScreenPos = Camera.main.WorldToScreenPoint(transform.position);
        shootDirection = ((Vector2)currentTouch - (Vector2)shooterScreenPos).normalized;

        float angle = Mathf.Atan2(shootDirection.y, shootDirection.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, angle - 90f);
    }
}