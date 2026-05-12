using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Splines;
using DG.Tweening;

public class CandySplineManager : MonoBehaviour
{
    public static CandySplineManager Instance;
    [SerializeField] private PanelShower _winPanel;

    public SplineContainer splineContainer;
    public List<CandyOnSpline> candies;
    public float spacing = 0.03f;
    public float moveSpeed = 0.005f;
    public float closeDuration = 0.3f;


    void Awake()
    {
        Instance = this;
        for (int i = 0; i < candies.Count; i++)
        {
            candies[i].manager = this;
            candies[i].position = i * spacing;
        }
    }
    private bool isClosing = false;

    void Update()
    {
        if (isClosing) return;
        foreach (var c in candies)
            c.position += moveSpeed * Time.deltaTime;
    }

    public void InsertCandy(GameObject newCandyObj, CandyOnSpline hitCandy)
    {
        int index = candies.IndexOf(hitCandy);
        if (index < 0) return;

        float insertPos = hitCandy.position;

        for (int i = index; i < candies.Count; i++)
            candies[i].position += spacing;

        CandyOnSpline newOnSpline = newCandyObj.GetComponent<CandyOnSpline>();
        if (newOnSpline == null)
            newOnSpline = newCandyObj.AddComponent<CandyOnSpline>();

        newOnSpline.position = insertPos;
        newOnSpline.manager = this;

        Rigidbody rb = newCandyObj.GetComponent<Rigidbody>();
        if (rb != null) rb.isKinematic = true;

        candies.Insert(index, newOnSpline);
        CheckAndExplode(index);
    }

    void CheckAndExplode(int insertedIndex)
    {
        if (insertedIndex < 0 || insertedIndex >= candies.Count) return;

        string type = candies[insertedIndex].tag;
        if (type == "Untagged") return;

        int left = insertedIndex;
        int right = insertedIndex;

        while (left - 1 >= 0 && candies[left - 1].tag == type)
            left--;
        while (right + 1 < candies.Count && candies[right + 1].tag == type)
            right++;

        int count = right - left + 1;
        if (count < 3) return;

        float totalShift = count * spacing;

        for (int i = right; i >= left; i--)
        {
            Destroy(candies[i].gameObject);
            candies.RemoveAt(i);
        }

        if (MusicManager.instance != null)
            MusicManager.instance.PlayPop();


        if(candies.Count == 0)
        {
            if (_winPanel != null) _winPanel.Show();
            MusicManager.instance.PlayWin();
            Debug.Log("Win!");
            return;
        }

        isClosing = true;

        Sequence seq = DOTween.Sequence();

        for (int i = left; i < candies.Count; i++)
        {
            CandyOnSpline candy = candies[i];
            float target = candy.position - totalShift;

            seq.Join(
                DOTween.To(
                    () => candy.position,
                    x => candy.position = x,
                    target,
                    closeDuration
                ).SetEase(Ease.OutQuad)
            );
        }

        
        int checkIndex = left;
        seq.OnComplete(() =>
        {
            isClosing = false;
            CheckAndExplode(checkIndex);
        });
    }

    public bool HasCandies() => candies.Count > 0;
}