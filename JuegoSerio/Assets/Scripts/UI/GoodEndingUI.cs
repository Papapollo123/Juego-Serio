using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UI;

public class GoodEndingUI : MonoBehaviour{

    [SerializeField] private Button mainMenuButton;

    private void Awake() {
        mainMenuButton.onClick.AddListener(() => {
            Loader.Load(Loader.Scene.MainMenuScene);
        });
    }

}
