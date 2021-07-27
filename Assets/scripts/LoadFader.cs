using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

public class LoadFader : MonoBehaviour
{
    public event System.Action OnFadeComplete;
    [SerializeField] private Image loadImage;
    [SerializeField] private float fadeDuration;
    [SerializeField] private bool fadeOnStart;

    private float alpha;
    private Tweener tween;
    public bool IsFadedOut { get; private set; }
    private void Start()
    {
        if (fadeOnStart)
        {
            var col = loadImage.color;
            col.a = alpha = 1f;
            loadImage.color = col;
            FadeIn();
        }
    }
    private void SetImageAlpha()
    {
        if (loadImage != null)
        {
            var col = loadImage.color;
            col.a = alpha;
            loadImage.color = col;
        }
    }
    private void FadeComplete()
    {
        IsFadedOut = !IsFadedOut;
        OnFadeComplete?.Invoke();
    }
    public void FadeOut(bool startFromOne = false)
    {
        if (startFromOne)
        {
            alpha = 1;
            SetImageAlpha();
        }
        IsFadedOut = false;
        tween.Kill();
        tween = DOTween.To(() => alpha, x => alpha = x, 1f, fadeDuration);
        tween.onUpdate = SetImageAlpha;
        tween.onComplete = FadeComplete;
    }
    public void FadeIn()
    {
        IsFadedOut = true;
        tween.Kill();
        tween = DOTween.To(() => alpha, x => alpha = x, 0f, fadeDuration);
        tween.onUpdate = SetImageAlpha;
        tween.onComplete = FadeComplete;
    }
}
