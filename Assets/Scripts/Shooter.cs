using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
 
public class Shooter : MonoBehaviour
{
    public GameObject[] candyPrefab;
    public Transform shootPoint;
    public Transform nextPoint;
    public float force = 10f;
    [SerializeField] private PanelShower _losePanel;
    private Vector2 currentTouch;
    private int currentCandyIndex = 0;
    private GameObject currentCandy;
    private GameObject nextCandy;
    private bool isReady = false;
 
    void Start()
    {
        UIState.IsUIOpen = false;
        Input.simulateMouseWithTouches = false;
        SpawnPreview();
        StartCoroutine(EnableInputNextFrame());
    }

    IEnumerator EnableInputNextFrame()
    {
        yield return null;
        yield return null;
        isReady = true;
    }
 
    void Update()
    {
        if (!isReady) return;
        if (UIState.IsUIOpen) return;

        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            if (EventSystem.current.IsPointerOverGameObject(touch.fingerId))
                return;

            if (touch.phase == TouchPhase.Moved || touch.phase == TouchPhase.Stationary)
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
        else
        {
            if (EventSystem.current.IsPointerOverGameObject())
                return;

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
    }

    private float lastShotTime = 0f;
    public float shootCooldown = 0.3f;
    void Shoot()
    {
        if (currentCandy == null) return;
        if (Time.time - lastShotTime < shootCooldown) return;
        lastShotTime = Time.time;

        GameObject bullet = currentCandy;
        Vector3 dir = new Vector3(transform.up.x, transform.up.y, 0f);
        bullet.transform.parent = null;

        bullet.transform.position = shootPoint.position + dir * 0.5f;
        Rigidbody rb = bullet.GetComponent<Rigidbody>();
        rb.isKinematic = false;
        rb.useGravity = false;
        rb.linearDamping = 0f;
        rb.angularDamping = 0f;
        rb.linearVelocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;
 
        
        rb.linearVelocity = dir * force;
 
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
            currentCandy.transform.localPosition = Vector3.zero;
            currentCandy.transform.localRotation = Quaternion.identity;

            Rigidbody rb = currentCandy.GetComponent<Rigidbody>();
            if (rb != null)
                rb.isKinematic = true;
        }
        else
        {
            currentCandy = null;
            StartCoroutine(CheckLoseDelayed());

        }
 
        if (currentCandyIndex + 1 < candyPrefab.Length)
        {
            nextCandy = Instantiate(
                candyPrefab[currentCandyIndex + 1],
                nextPoint.position,
                Quaternion.identity,
                nextPoint
            );
            nextCandy.transform.localPosition = Vector3.zero;
            nextCandy.transform.localRotation = Quaternion.identity;

            Rigidbody rb = nextCandy.GetComponent<Rigidbody>();
            if (rb != null)
                rb.isKinematic = true;
        }
        else
        {
            nextCandy = null;
        }
    }

    IEnumerator CheckLoseDelayed()
    {
        yield return new WaitForSeconds(2f);

        if (CandySplineManager.Instance.HasCandies())
        {
            if (_losePanel != null) _losePanel.Show();
            MusicManager.instance.PlayLose();
            Debug.Log("Lose!");
        }
    }
 
    void RotateShooter()
    {
        Vector3 shooterScreenPos = Camera.main.WorldToScreenPoint(transform.position);
        Vector2 shootDirection = ((Vector2)currentTouch - (Vector2)shooterScreenPos).normalized;
 
        float angle = Mathf.Atan2(shootDirection.y, shootDirection.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, angle - 90f);
    }
}