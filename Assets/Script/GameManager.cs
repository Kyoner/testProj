using UnityEngine;
using System.IO;
using System;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public GameObject itemPref;
    public ItemSO starterBullets;
    public Player player;
    public static event Action<SavedData> LoadEvent;
    public GameObject UICanvas;
    public GameObject UIRestart;
    void Start()
    {
        Monster.itemPref = itemPref;
        LoadData();
        player.DeathSubEvent += RestartDisplay;
        Player.Inventory.AddItem(starterBullets);
    }
    void RestartDisplay()
    {
        UICanvas.SetActive(false);
        UIRestart.SetActive(true);
    }
    public void Restart()
    {
        File.Delete(Application.persistentDataPath + "/save.dat");
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    private void OnApplicationQuit()
    {
        SaveData();
    }
    public void SaveData()
    {
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file;
        if(File.Exists(Application.persistentDataPath + "/save.dat"))
        {
            file = File.OpenWrite(Application.persistentDataPath + "/save.dat");
        }
        else
        {
            file = File.Create(Application.persistentDataPath + "/save.dat");
        }
        SavedData sd = new SavedData(Player.Inventory.ItemSlots, player.CurrHealth);
        bf.Serialize(file, sd);
        file.Close();
    }
    public void LoadData()
    {
        if(File.Exists(Application.persistentDataPath + "/save.dat"))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.OpenRead(Application.persistentDataPath + "/save.dat");
            SavedData data = (SavedData)bf.Deserialize(file);
            LoadEvent?.Invoke(data);
            file.Close();
        }
    }
}