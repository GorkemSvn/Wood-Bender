using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GrandManagement;

/*
 * GameManager manages in-level events and takes reports from in-level objects
 * holds temporary(in level) data, does not edit Grand Data until game ends
 * GameManager works with grand manager 
 */

    //derive new managers from this class
public class GameManager : MonoBehaviour
{
    public static GameManager instance { get; private set; }

    public bool win { get; private set; }
    public int collectedGold { get; private set; }
    
    protected virtual void Awake()
    {
        instance = this;
    }

    protected virtual void Start()
    {
        GrandManager.start += OnStart;

        if (UIManager.instance == null)
        {
            Debug.Log("Couldn't find UI, loading scene 0");
            GrandManager.Level.LoadScene(0);
        }

    }

    protected virtual void OnStart()
    {
        GrandManager.CallStatusUpdate();
        //enable character etc game mechanics here
    }

    public virtual void GetGold(int q)
    {
        collectedGold += q;
        GrandManager.CallStatusUpdate();
    }

    public virtual void ReportEnd(bool win)
    {
        this.win = win;

        if (win)
        {
            GrandManager.data.maxLevel++;
            GrandManager.data.Save();
        }

        GrandManager.CallFinishEvent();
    }

    public void OnDestroy()
    {
        GrandManager.start -= OnStart;
    }
}


