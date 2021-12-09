using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestPanel : Panel
{
    //public ProgressBar pb;
    public void AddGold(int x)
    {
        GameManager.instance.GetGold(x);
    }

    public void EndGame(bool win)
    {
        GameManager.instance.ReportEnd(win);
    }

    float f = 0;
    int t = 0;/*
    public void Prograte()
    {
        f += 0.1f;
        if (f > 1)
        {
            f = 0;
            t++;
        }

        if (t >= pb.phasesCount)
            t = 0;

        pb.FillBars(t, f);
    }*/
}
