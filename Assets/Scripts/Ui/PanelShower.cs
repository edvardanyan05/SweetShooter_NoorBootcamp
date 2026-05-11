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
    private bool _initialized = false;

    private void Initialize()
    {
        if (_initialized) return;
        _initialized = true;

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
        Initialize();
        UIState.IsUIOpen = true;

        _activeTween?.Kill();
        _rectTransform.anchoredPosition = new Vector2(
            _rectTransform.anchoredPosition.x, _tweenY
        );
        _activeTween = _rectTransform.DOAnchorPosY(_originalY, _duration).SetEase(Ease.InOutSine);
    }

    private void Hide()
    {
        _activeTween?.Kill();
        _activeTween = _rectTransform.DOAnchorPosY(_tweenY, _duration).SetEase(Ease.InOutSine);
        _activeTween.OnComplete(() =>
        {
            gameObject.SetActive(false);
            UIState.IsUIOpen = false;
        });
    }
}