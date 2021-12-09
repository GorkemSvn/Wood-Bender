using UnityEngine;
using DG.Tweening;

[RequireComponent(typeof(CanvasGroup))]
public class Panel : MonoBehaviour
{
    protected CanvasGroup cg;

    public void Awake()
    {
        cg = GetComponent<CanvasGroup>();
    }

    public void Appear(bool instant = false)
    {
        cg.DOKill();
        if (!gameObject.activeSelf)
        {
            gameObject.SetActive(true);
            cg.alpha = 0;
        }
        cg.blocksRaycasts = true;
        enabled = true;
        OnAppearStart();

        if (instant)
        {
            cg.alpha = 1f;
            cg.interactable = true;
        }
        else
        {
            cg.DOFade(1f, 0.25f).OnComplete(() =>
            {
                cg.interactable = true;
                OnAppearEnd();
            });
        }
    }

    protected virtual void OnAppearStart()
    {

    }
    protected virtual void OnAppearEnd()
    {
        //this wont shoot if appearing is instant
    }


    public void Disappear(bool instant = false)
    {
        cg.DOKill();
        cg.blocksRaycasts = false;
        OnDisappearStart();

        if (instant)
        {
            cg.alpha = 0f;
            cg.interactable = false;
        }
        else
        {
            cg.DOFade(0f, 0.25f).OnComplete(() =>
            {
                cg.interactable = false;
                OnDisappearEnd();
            });
        }
    }
    protected virtual void OnDisappearStart()
    {
    }
    protected virtual void OnDisappearEnd()
    {
        //this wont shoot if disappearing is instant
    }

}
