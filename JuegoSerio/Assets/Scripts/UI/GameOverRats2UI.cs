using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameOverRats2UI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI recipiesDeliveredText;
    [SerializeField] private Button continueButton;
    [SerializeField] private Button formalizarButton;

    private void Awake() {
        continueButton.onClick.AddListener(() => {
            Loader.Load(Loader.Scene.RatsInspector);
        });

        formalizarButton.onClick.AddListener(() => {
            Loader.Load(Loader.Scene.Formalizacion);
        });
    }
    private void Start() {
        KitchenGameManager.Instance.OnStateChanged += KitchenGameManager_OnStateChanged;
        Hide();
    }
    private void KitchenGameManager_OnStateChanged(object sender, System.EventArgs e) {
        if (KitchenGameManager.Instance.IsGameOver()) {
            Show();
            recipiesDeliveredText.text = DeliveryManager.Instance.GetSuccessfulRecipiesAmount().ToString();
        }
        else {
            Hide();
        }
    }
    private void Show() {
        gameObject.SetActive(true);
    }
    private void Hide() {
        gameObject.SetActive(false);
    }
}
