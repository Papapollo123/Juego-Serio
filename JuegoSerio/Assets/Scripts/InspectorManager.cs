using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InspectorManager : MonoBehaviour
{
    [SerializeField] private GameObject inspector;
    private float inspectorSpawnTime;

    void LateUpdate()
    {
        if (KitchenGameManager.Instance.IsGamePlaying()){
            inspectorSpawnTime = 15f;
        }
        if(KitchenGameManager.Instance.GetGamePlayingTimer() < inspectorSpawnTime) {
            inspector.SetActive(true);
        }
    }
}
