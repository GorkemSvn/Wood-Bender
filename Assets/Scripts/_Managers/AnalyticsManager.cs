using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GrandManagement;

public class AnalyticsManager : MonoBehaviour
{
    static AnalyticsManager singleton;

    //"Hook on load,start,finish events once \n you may find necesary information under Grandmanager.Data

    public event AnalyticsEvent  load, start;
    public event AnalyticsBoolEvent finish;

    public bool win { get { return GameManager.instance.win; } }
    public GrandManager.Data data { get { return GrandManager.data; } }

    public int level { get { return GrandManager.Level.activeLevel; } }
    public int maxLevel { get { return GrandManager.data.maxLevel; } }
    public int score  { get { return GameManager.instance.collectedGold; } }

    public void Awake()
    {
        if (singleton)
        {
            Destroy(gameObject);
            return;
        }
        else
        {
            singleton = this;
            DontDestroyOnLoad(gameObject);

            GrandManager.load += Load;
            GrandManager.start += StartEvent;
            GrandManager.finish += Finish;
        }

    }

    void Load()
    {
        load?.Invoke();
        Debug.Log("load event invoked");
    }
    void StartEvent()
    {
        start?.Invoke();
        Debug.Log("Start event invoked");
    }
    void Finish()
    {
        finish?.Invoke(GameManager.instance.win);
        Debug.Log("end event invoked");
    }
    

    public delegate void AnalyticsBoolEvent(bool win);
    public delegate void AnalyticsEvent();
}
