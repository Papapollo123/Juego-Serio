using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class RatManager : MonoBehaviour
{
    [SerializeField] private GameObject ratCombo;
    private float ratSpawnTime;


    void Update()
    {
        if (KitchenGameManager.Instance.IsGamePlaying()) {
            ratSpawnTime = 30f;
        }
        if (KitchenGameManager.Instance.GetGamePlayingTimer() < ratSpawnTime) {
            ratCombo.SetActive(true);
        }
    }
}
