using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using UnityEditor.U2D.Aseprite;
using UnityEngine.SceneManagement;
using System.IO;

public class GameController : MonoBehaviour
{

    private string saveLocation;


    [SerializeField]
    private GameObject playerPrefab;

    private GameObject player;
    public static Action<GameObject> OnPlayerSpawned;
    


    private void Awake()
    {
        player = Instantiate(playerPrefab); 


    }




    private void Start()
    {
        LoadGame();

        OnPlayerSpawned?.Invoke(player);
        saveLocation = Path.Combine(Application.persistentDataPath, "saveData.json");

    }

   private void ResetScene()
    {
        Invoke("ResetSceneDelay", 2f);
    }


    private void ResetSceneDelay()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    private void OnEnable()
    {
        PlayerHealth.OnPlayerDie += ResetScene;
    }

 

    private void OnDisable()
    {
        PlayerHealth.OnPlayerDie -= ResetScene;
    }

    // Update is called once per frame

    public void SaveGame()
    {
        SaveData saveData = new SaveData
        {
            playerPosition = GameObject.FindGameObjectWithTag("Player").transform.position,

        };

        File.WriteAllText(saveLocation, JsonUtility.ToJson(saveData));
    }

    public void LoadGame()
    {
        if (File.Exists(saveLocation))
        {
            SaveData saveData = JsonUtility.FromJson<SaveData>(File.ReadAllText(saveLocation));
            GameObject.FindGameObjectWithTag("Player").transform.position = saveData.playerPosition;


        }
        else
        {
            SaveGame();
        }

    }

}
