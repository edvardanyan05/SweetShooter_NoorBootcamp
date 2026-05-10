using UnityEngine;

public class CandyBullet : MonoBehaviour
{
    private bool hasHit = false;

    void OnCollisionEnter(Collision collision)
    {
        if (hasHit) return;

        if (GetComponent<CandyOnSpline>() != null) return;

        CandyOnSpline hitCandy = collision.gameObject.GetComponent<CandyOnSpline>();
        if (hitCandy == null) return;

        hasHit = true;
        CandySplineManager.Instance.InsertCandy(gameObject, hitCandy);
    }
}