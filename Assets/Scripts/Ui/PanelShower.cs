using System;
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

    private void Awake()
    {
        EnsureInitialized();
    }

    private void EnsureInitialized()
    {
        if (_initialized) return;
        _initialized = true;

        _originalY = _rectTransform.anchoredPosition.y;
        
        if (_closeButton != null)
            _closeButton.onClick.AddListener(() => Hide());
    }

    private void OnDestroy()
    {
        if (_closeButton != null)
            _closeButton.onClick.RemoveAllListeners();
    }

    public void Show()
    {
        EnsureInitialized();
        gameObject.SetActive(true);
        UIState.IsUIOpen = true;

        _activeTween?.Kill();
        
        _rectTransform.anchoredPosition = new Vector2(_rectTransform.anchoredPosition.x, _tweenY);
        
        _activeTween = _rectTransform.DOAnchorPosY(_originalY, _duration)
            .SetEase(Ease.InOutSine)
            .SetUpdate(true); 
    }

    public void Hide(Action onFinished = null)
    {
        _activeTween?.Kill();
        
        _activeTween = _rectTransform.DOAnchorPosY(_tweenY, _duration)
            .SetEase(Ease.InOutSine)
            .SetUpdate(true);

        _activeTween.OnComplete(() =>
        {
            gameObject.SetActive(false);
            UIState.IsUIOpen = false;
            onFinished?.Invoke();
        });
    }
}
