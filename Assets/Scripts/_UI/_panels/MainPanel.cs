using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using GrandManagement;

public class MainPanel : Panel
{
    public Text levelText;
    public Text moneyText;


    protected override void OnAppearStart()
    {
        base.OnAppearStart();
        UpdateUI();
    }

    void UpdateUI()
    {
        levelText.text = "LEVEL " + GrandManager.Level.activeLevel;
        moneyText.text = GrandManager.data.money + "";
    }

    public void OnPressStart()
    {
        UIManager.instance.StartGame();
    }
}