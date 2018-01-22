using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using Player;

[Serializable]
public struct scoreRecord:IComparable<scoreRecord>
{

    public int score;
    public string playerName;
    public scoreRecord(int score, string playerName)
    {
        this.score = score;
        this.playerName = playerName;
    }

    public int CompareTo(scoreRecord other)
    {
        // - kvůli řazení, chci řadit od největšího
        return -this.score.CompareTo(other.score);
    }
}

public class DataManager : MonoBehaviour {
    public GameObject NameAsk;

    public float fixedDeltaTime;
    public int endlessSceneIndex = 1;
    public int menuSceneIndex = 1;
    public int StoreSceneIndex = 4;
    public int numberOfLevels = 2;
    public  List<scoreRecord> highscores;
    public int numberOfScores = 10;
    private int money = 0;
    private string playerName;
    bool[] unlocked;
    int[] maxScores;
    public int selectedLevel = 0;

    public static  DataManager instance = null;

    void Awake() {
        Screen.SetResolution(540, 960, false);
        fixedDeltaTime = Time.fixedDeltaTime;
        if (instance == null)
        {
            instance = this;
            unlocked = new bool[numberOfLevels];
            maxScores = new int[numberOfLevels];
            LoadData();
        }
        else
        {
            Destroy(gameObject);
        }

        DontDestroyOnLoad(gameObject);

    }

    private void OnDestroy()
    {
        SaveData();
    }

    /// <summary>
    /// returns the number of the highest level the player has unlocked, so far is trivial, will be more difficult later
    /// </summary>
    /// <returns></returns>

    public int GetLastUnlockedLevel()
    {
        return 1;
    }

    public void UnlockNextLevel()
    {

    }

    void LoadData()
    {
        if (File.Exists(Application.persistentDataPath + "/PlayerInfo.panda"))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(Application.persistentDataPath + "/PlayerInfo.panda", FileMode.Open);
            PlayerData data = (PlayerData)bf.Deserialize(file);
            file.Close();
            Debug.Log("Money in LoadData: " + data.money);
            money = data.money;
            highscores = data.highscores;
            playerName = data.playerName;
            UpgradeCenter.SetPlayerUpgradeData(data.PlayerUpgradeData);
        }
        else
        {
            AskPlayerName();
        }
        if (highscores==null)
        {
            highscores = new List<scoreRecord>();
            for (int i = 0; i < numberOfScores; i++)
            {
                highscores.Add(new scoreRecord(-5 - 10 * i, "Noob"));
            }
            unlocked[0] = true;
        }
    }

    void SaveData()
    {
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Create(Application.persistentDataPath + "/PlayerInfo.panda");
        PlayerData data = new PlayerData(money, playerName, highscores);
        bf.Serialize(file, data);
        file.Close();


    }

    public void MoneyCollected(int money)
    {
        Debug.Log("Money collected");
        this.money += money;
    }

    public bool isInHighscore(int score)
    {
        if (highscores.Count==0)
        {
            return true;
        }
        if (score>highscores[highscores.Count-1].score)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public void setNewHighScore(int score)
    {
        highscores.Add(new scoreRecord(score, playerName));
        highscores.Sort();
        if (highscores.Count>numberOfScores)
        {
            highscores.RemoveAt(highscores.Count-1);
        }
    }

    public List<scoreRecord> GetHighscores()
    {
        return highscores;
    }
    
    public void AskPlayerName()
    {
        Instantiate(NameAsk);
    }

    public void SetName(string name)
    {
        this.playerName = name;
    }

    public bool Purcase(int amount)
    {
        if (money >= amount)
        {
            money -= amount;
            return true;
        }
        return false;
    }

    public int GetMoney()
    {
        return money;
    }
}

[Serializable]
class PlayerData
{
    public int money;
    public List<scoreRecord> highscores;
    public string playerName;
    public PlayerUpgradeData PlayerUpgradeData;

    public PlayerData(int money, string playerName, List<scoreRecord> hs)
    {
        this.money = money;
        this.playerName = playerName;
        this.highscores = hs;
        this.PlayerUpgradeData = UpgradeCenter.GetPlayerUpgradeData();
    }
    
    
}
