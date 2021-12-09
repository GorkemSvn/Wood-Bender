using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using GrandManagement;

public class GamePanel : Panel
{
    [SerializeField] private Text goldText;
    [SerializeField] private Panel tutorialPanel;

    public void Start()
    {
        GrandManager.uiUpdate += UpdateUI;
    }

    
    protected override void OnAppearStart()
    {
        UpdateUI();
        if(Application.isEditor || !GrandManager.data.tutored)
        {
            tutorialPanel.Appear();
            GrandManager.data.tutored = true;
            GrandManager.data.Save();
        }
    }
    void UpdateUI()
    {
        if (GameManager.instance)
        {
            goldText.text = GameManager.instance.collectedGold + "";
        }
    }
}