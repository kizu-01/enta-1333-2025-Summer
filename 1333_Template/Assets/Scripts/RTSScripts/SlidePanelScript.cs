using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class SlidePanelScript : MonoBehaviour
{
    public enum SlideDirection { Horizontal, Vertical }

    [Header("Direction Settings")]
    [SerializeField] private SlideDirection _direction = SlideDirection.Horizontal;

    [Header("Animation Timings")]
    [SerializeField] private float _transitionDuration = 0.6f;
    [SerializeField] private float _buttonPopDuration = 0.4f;

    // Added custom ease options for flexible changes
    [Header("Custom Easing")]
    [SerializeField] private Ease _openEase = Ease.OutBack;
    [SerializeField] private Ease _closeEase = Ease.InCubic;
    [SerializeField] private Ease _buttonEase = Ease.OutBack;

    private float _openPosition;
    private float _closedPosition;
    private RectTransform rectTransform;
    private CanvasGroup canvasGroup;
    private Button[] _menuButtons;

    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
        canvasGroup = GetComponent<CanvasGroup>();

        // Find all buttons inside the panel automatically
        _menuButtons = GetComponentsInChildren<Button>(true);

        if (_direction == SlideDirection.Horizontal)
        {
            _closedPosition = -rectTransform.rect.width;
            _openPosition = 0;
        }
        else
        {
            _closedPosition = rectTransform.rect.height;
            _openPosition = 0;
        }
    }

    public void Open()
    {
        // Hide all buttons before panel slides down
        foreach (var button in _menuButtons)
        {
            button.transform.localScale = Vector3.zero;
        }

        // Slide panel in
        if (_direction == SlideDirection.Horizontal)
        {
            rectTransform.DOAnchorPosX(_openPosition, _transitionDuration).SetEase(_openEase).SetUpdate(true).OnComplete(AnimateButtonsIn);
        }
        else
        {
            rectTransform.DOAnchorPosY(_openPosition, _transitionDuration).SetEase(_openEase).SetUpdate(true).OnComplete(AnimateButtonsIn);
        }
    }

    public void Close()
    {
        if (canvasGroup != null) canvasGroup.blocksRaycasts = false;

        if (_direction == SlideDirection.Horizontal)
        {
            rectTransform.DOAnchorPosX(_closedPosition, _transitionDuration).SetEase(_closeEase).SetUpdate(true);
        }
        else
        {
            rectTransform.DOAnchorPosY(_closedPosition, _transitionDuration).SetEase(_closeEase).SetUpdate(true);
        }
    }

    private void AnimateButtonsIn()
    {
        if (canvasGroup != null) canvasGroup.blocksRaycasts = true;

        // Smoothly show each button into existence one by one
        for (int i = 0; i < _menuButtons.Length; i++)
        {
            _menuButtons[i].transform.DOScale(Vector3.one, _buttonPopDuration)
                .SetEase(_buttonEase)
                .SetUpdate(true)
                .SetDelay(i * 0.1f);
        }
    }
}