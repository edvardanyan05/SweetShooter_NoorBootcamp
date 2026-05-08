using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class PanelShower : MonoBehaviour
{
    [SerializeField] private RectTransform _rectTransform;
    [SerializeField] private Button _closeButton;
    [SerializeField] private float _duration;
    [SerializeField] private float _tweenY;

    private Tween _activeTween;
    private float _originalY;

    private void Awake()
    {
        _originalY = _rectTransform.anchoredPosition.y;
        _closeButton.onClick.AddListener(Hide);
    }

    private void OnDestroy()
    {
        _closeButton.onClick.RemoveListener(Hide);
    }
    
    public void Show()
    {
        gameObject.SetActive(true);
        _activeTween?.Kill();
        _rectTransform.position = new Vector3(_rectTransform.position.x, _tweenY, _rectTransform.position.z);
        _activeTween = _rectTransform.DOAnchorPosY(_originalY, _duration).SetEase(Ease.InOutSine);
    }

    private void Hide()
    {
        _activeTween = _rectTransform.DOAnchorPosY(_tweenY, _duration).SetEase(Ease.InOutSine);
        _activeTween.onComplete += () => 
        {
            gameObject.SetActive(false);
        };
    }
}
