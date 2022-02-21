using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class DataManagerHandler : MonoBehaviour
{
    public static DataManagerHandler Instance;

    // Current player name.
    public string playerName;

    // Name of the person with the highest score.
    public string highScoreName;

    // Score to be tracked during a game.
    public int currentScore;

    // Highest saved score to date.
    public int highScore;

    // Loads before Start().
    private void Awake()
    {
        // Ensure Data Manager is not already created. Destroys itself if it is.
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }
        // Set Instance to this object.
        Instance = this;
        // Prevent destruction upon loading a new scene.
        DontDestroyOnLoad(gameObject);

        LoadHighScore();
    }


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    [System.Serializable]
    class SaveData
    {
        public string savedPlayerName;
        public int savedHighScore;
    }

    public void SaveHighScore()
    {
        SaveData data = new SaveData();
        data.savedHighScore = highScore;
        data.savedPlayerName = highScoreName;

        string json = JsonUtility.ToJson(data);

        File.WriteAllText(Application.persistentDataPath + "/savefile.json", json);
    }

    public void LoadHighScore()
    {
        string path = Application.persistentDataPath + "/savefile.json";
        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);
            SaveData data = JsonUtility.FromJson<SaveData>(json);

            highScoreName = data.savedPlayerName;
            highScore = data.savedHighScore;
        }
    }
}
