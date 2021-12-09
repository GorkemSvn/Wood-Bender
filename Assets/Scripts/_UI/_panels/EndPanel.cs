using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using GrandManagement;

public class EndPanel : Panel
{
    public EndPanelContainer success;
    public EndPanelContainer fail;
    private EndPanelContainer activePanel;

    protected override void OnAppearStart()
    {
        base.OnAppearStart();
        if(GameManager.instance.win)
        {
            success.Appear();
            fail.Disapear();
        }
        else
        {
            fail.Appear();
            success.Disapear();
        }
    }
    public void OnPressRestart()
    {
        GrandManager.Level.Load(GrandManager.data.maxLevel);
    }
}

[System.Serializable]
public struct EndPanelContainer
{
    public RectTransform self;
    public RectTransform title;
    public RectTransform continueButton;

    public void Appear(float duration = 0.75f)
    {
        float targetPos = title.anchoredPosition.y;

        title.anchoredPosition += new Vector2(0f, 1000f);
        continueButton.localScale = Vector3.zero;

        self.gameObject.SetActive(true);

        title.DOAnchorPosY(targetPos, duration);
        continueButton.DOScale(1f, duration).SetEase(Ease.OutBack);
    }
    public void Disapear()
    {
        self.gameObject.SetActive(false);
    }
}