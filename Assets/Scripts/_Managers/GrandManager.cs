using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

/*
* this master class holds status information of game and manage major events such as win,lose,start,gold,in app purchases
* player's inventory,game configuration and other interlevel information
* also manages how to move acros levels
*/
namespace GrandManagement
{
    public static class GrandManager
    {
        public static event GameEvent load; //called by Level subclass
        public static event GameEvent start; //called by UIManager
        public static event GameEvent finish; //Called by GameManager
        public static event GameEvent uiUpdate; //Called by GameManager to notice UI (gold collection, damage taken)
        public delegate void GameEvent();

        public static Data data;

        static bool OnLoadHooked;
        static bool firstLoadDone;

        public static void CallStartEvent()
        {
            start?.Invoke();
        }
        public static void CallFinishEvent()
        {
            finish?.Invoke();
        }
        public static void CallStatusUpdate()
        {
            uiUpdate?.Invoke();
        }
        static void OnLoadAsync(Scene scene,LoadSceneMode LSM)
        {
            if (!firstLoadDone)
                firstLoadDone = true;
            else
                load?.Invoke();
        }

        [System.Serializable]
        public struct Data
        {
            public bool playMusic;
            public bool vibration;
            public bool tutored;

            public int money;
            public int maxLevel;

            public float maxScore;

            bool load;

            public void Save()
            {
                SaveSerializedObject("data", this);
            }
            public void Load()
            {
                try
                {
                    this = (Data)LoadSerializedObject("data");
                }
                catch (System.Exception e) { };

                if(!load)
                {
                    this = new Data();
                    playMusic = true;
                    vibration = false;

                    money = 0;
                    maxLevel = 1;
                    maxScore = 0;
                    load = true;
                }
            }

            public static object LoadSerializedObject(string name)
            {
                string path = Application.persistentDataPath + "/" + name;
                if (File.Exists(path))
                {
                    BinaryFormatter formatter = new BinaryFormatter();
                    FileStream stream = new FileStream(path, FileMode.Open);

                    var n = formatter.Deserialize(stream);
                    stream.Close();

                    return n;
                }

                return null;
            }
            public static void SaveSerializedObject(string fileName, object serializedObject)
            {
                BinaryFormatter formatter = new BinaryFormatter();
                FileStream stream = new FileStream(Application.persistentDataPath + "/" + fileName, FileMode.Create);

                formatter.Serialize(stream, serializedObject);
                stream.Close();
            }
        }
        public static class Level
        {
            public static int activeLevel { get; private set; }

            public static void Load(int level)
            {
                if (SceneManager.sceneCountInBuildSettings < 1)
                {
                    Debug.LogError("Template needs project to have atleast 1 scenes in build to work");
                    return;
                }

                if (!OnLoadHooked)
                {
                    SceneManager.sceneLoaded += OnLoadAsync;
                    OnLoadHooked = true;
                }

                level = Mathf.Max(1, level);
                int scene = (level - 1) % (SceneManager.sceneCountInBuildSettings);

                activeLevel = level;
                Debug.Log("Loading Level " + level + " and scene " + scene);
                SceneManager.LoadScene(scene);
            }
            public static void LoadScene(int sceneIndex)
            {
                if (!OnLoadHooked)
                {
                    SceneManager.sceneLoaded += OnLoadAsync;
                    OnLoadHooked = true;
                }

                int scene = (sceneIndex - 1) % (SceneManager.sceneCountInBuildSettings);
                Debug.Log("Loading scene " + scene);
                SceneManager.LoadScene(scene);
            }
            public static void Restart()
            {
                Load(activeLevel);
            }
            public static void LoadPlayerLevel()
            {
                Load(data.maxLevel);
            }
        }
    }
}
