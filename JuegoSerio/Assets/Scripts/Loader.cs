using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public static class Loader {

    public enum Scene {
        MainMenuScene,
        GameScene,
        LoadingScene,
        Dream,
        InformalScene1,
        InformalScene2,
        InspectorSpeech,
        Rats2,
        RatsInspector,
        InspectorCloseDown,
        BadEnding,
        Formalizacion,
        Formal,
        FormalInspector,
        InspectorVictory,
        GoodEnding

    }
    
    private static Scene targetScene; 
    public static void Load(Scene targetScene) {
        Loader.targetScene = targetScene;

        SceneManager.LoadScene(Scene.LoadingScene.ToString());
    }

    public static void LoaderCallback() {
        SceneManager.LoadScene(targetScene.ToString());
    }

}
