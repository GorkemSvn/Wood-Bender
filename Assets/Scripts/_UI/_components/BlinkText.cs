using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class BlinkText : MonoBehaviour
{
    public bool fadeOut;

    private void Start()
    {
        if (fadeOut)
        {
            Text text = GetComponent<Text>();
            text.DOFade(0.3f, 1f).SetLoops(-1, LoopType.Yoyo).SetEase(Ease.Linear);
        }
        else
        {
            RectTransform rect = GetComponent<RectTransform>();
            rect.pivot = new Vector2(0.5f, 0.5f);
            rect.DOScale(1.2f, 1f).SetLoops(-1, LoopType.Yoyo).SetEase(Ease.Linear);
        }
    }
}