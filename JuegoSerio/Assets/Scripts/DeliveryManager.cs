using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeliveryManager : MonoBehaviour{


    public event EventHandler OnRecipieSpawned;
    public event EventHandler OnRecipieCompleted;
    public event EventHandler OnRecipieSuccess;
    public event EventHandler OnRecipieFailed;

    public static DeliveryManager Instance { get; private set; }

    [SerializeField] private RecipieListSO recipieListSO;

    private List<RecipieSO> waitingRecipieSOList;
    private float spawnRecipieTimer;
    private float spawnRecipieTimerMax = 4f;
    private int waitingRecipiesMax = 4;
    private int successfulRecipiesAmount; 

    private void Awake() {
        Instance = this;
        waitingRecipieSOList = new List<RecipieSO>();
    }

    private void Update() {
        spawnRecipieTimer -= Time.deltaTime;
        if (spawnRecipieTimer <= 0f ) {
            spawnRecipieTimer = spawnRecipieTimerMax;

            if (KitchenGameManager.Instance.IsGamePlaying() && waitingRecipieSOList.Count < waitingRecipiesMax) {
                RecipieSO waitingRecipieSO = recipieListSO.recipieSOList[UnityEngine.Random.Range(0, recipieListSO.recipieSOList.Count)];
                waitingRecipieSOList.Add(waitingRecipieSO);

                OnRecipieSpawned?.Invoke(this, EventArgs.Empty);
            }
        }
    }

    public void DeliverRecipie(PlateKitchenObject plateKitchenObject) {
        for(int i = 0;  i < waitingRecipieSOList.Count; i++) {
            RecipieSO waitingRecipieSO = waitingRecipieSOList[i];

            if(waitingRecipieSO.kitchenObjectSOList.Count == plateKitchenObject.GetKitchenObjectSOList().Count ) {
                //Tiene el mismo numero de Ingredientes
                bool plateContentsMatchesRecipie = true;
                foreach(KitchenObjectSO recipieKitchenObjectSO in waitingRecipieSO.kitchenObjectSOList) {
                    //cicla por todos los ingredientes del recipie
                    bool ingredientFound = false;
                    foreach (KitchenObjectSO plateKitchenObjectSO in plateKitchenObject.GetKitchenObjectSOList()) {
                        //cicla por todos los ingredientes en el plato
                        if(plateKitchenObjectSO == recipieKitchenObjectSO) {
                            //los ingredientes son iguales
                            ingredientFound=true;
                            break; 
                        }

                    }
                    if(!ingredientFound) {
                        //el ingrediente del recipie no se encontró en el plato
                        plateContentsMatchesRecipie=false;
                    }
                }
                if (plateContentsMatchesRecipie) {
                    //se entegó la orden correcta
                    Debug.Log("Orden correcta");
                    successfulRecipiesAmount++;
                    waitingRecipieSOList.RemoveAt(i);

                    OnRecipieCompleted?.Invoke(this, EventArgs.Empty);
                    OnRecipieSuccess?.Invoke(this, EventArgs.Empty);
                    return; 
                }
            }
        }

        //Si no se encontraron matches
        //el jugador entregó una orden incorrecta
        Debug.Log("Orden INCORRECTA");
        OnRecipieFailed?.Invoke(this, EventArgs.Empty);

    }

    public List<RecipieSO> GetWaitingRecipieSOList() {
        return waitingRecipieSOList;
    }

    public int GetSuccessfulRecipiesAmount() {
        return successfulRecipiesAmount;
    }
}