using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class InspectorVictoryDialogue : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI textComponent;
    public string[] lines;
    public float textSpeed;
    private int index;

    void Start() {
        textComponent.text = string.Empty;
        StartDialogue();
    }
    void Update() {
        if (Input.GetMouseButtonDown(0)) {
            if (textComponent.text == lines[index]) {
                NextLine();
            }
            else {
                StopAllCoroutines();
                textComponent.text = lines[index];
            }
        }
    }
    void StartDialogue() {
        index = 0;
        StartCoroutine(TypeLine());
    }

    IEnumerator TypeLine() {
        //escribe los caracteres 1 por 1
        foreach (char c in lines[index].ToCharArray()) {

            textComponent.text += c;
            yield return new WaitForSeconds(textSpeed);
        }
    }

    void NextLine() {
        if (index < lines.Length - 1) {
            index++;
            textComponent.text = string.Empty;
            StartCoroutine(TypeLine());
        }
        else {
            gameObject.SetActive(false);
            Loader.Load(Loader.Scene.GoodEnding);
        }

    }
}
