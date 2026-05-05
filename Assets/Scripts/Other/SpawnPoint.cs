using System;
using UnityEngine;

public class SpawnPoint : MonoBehaviour
{


    [SerializeField]
    private GameObject playerPrefab;

    private GameObject player;
    public static Action<GameObject> OnPlayerSpawned;

    private void Awake()
    {
        player = Instantiate(playerPrefab);
    }




}
