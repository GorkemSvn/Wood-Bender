using UnityEngine;

using GrandManagement;

public class UIManager : MonoBehaviour
{
    public static UIManager instance;

    public Panel startPanel;
    public Panel gamePanel;
    public Panel finalPanel;
    [SerializeField] int debugLevel;

    public void Awake()
    {
        if (instance)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;

            startPanel.gameObject.SetActive(true);
            gamePanel.gameObject.SetActive(true);
            finalPanel.gameObject.SetActive(true);

            DontDestroyOnLoad(gameObject);

            GrandManager.load += GreetGame;
            GrandManager.finish += EndGame;
            GrandManager.data.Load();

            Debug.Log("UI will load target level");
            if (debugLevel > 0)
                GrandManager.Level.Load(debugLevel);
            else if(GrandManager.data.maxLevel>1)
                GrandManager.Level.LoadPlayerLevel();
        }
    }


    private void GreetGame()
    {
        OpenPanel(startPanel);
    }
    public void StartGame()
    {
        OpenPanel(gamePanel);
        GrandManager.CallStartEvent();
    }
    private void EndGame()
    {
        OpenPanel(finalPanel);
    }

    private void OpenPanel(Panel panel)
    {
        CloseAll();

        panel.Appear();
    }
    public void CloseAll()
    {//use this when there is a custom UI in scene
        startPanel.Disappear(true);
        gamePanel.Disappear(true);
        finalPanel.Disappear(true);
    }
}