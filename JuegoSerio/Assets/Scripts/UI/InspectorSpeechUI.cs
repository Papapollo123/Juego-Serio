using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InspectorSpeechUI : MonoBehaviour{

    [SerializeField] private Button formalButton;
    [SerializeField] private Button informalButton;

    private void Awake() {
        formalButton.onClick.AddListener(() => {
            Loader.Load(Loader.Scene.Formalizacion);
        });
        informalButton.onClick.AddListener(() => {
            Loader.Load(Loader.Scene.RatsInspector);
        });
    }

}
