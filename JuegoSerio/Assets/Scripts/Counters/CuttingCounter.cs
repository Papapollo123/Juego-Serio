using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class CuttingCounter : BaseCounter, IHasProgress{

    public static event EventHandler OnAnyCut; 

    new public static void ResetStaticData() {
        OnAnyCut = null;
    }
    public event EventHandler <IHasProgress.OnProgressChangedEventArgs> OnProgressChanged;
    public event EventHandler OnCut;


    [SerializeField] private CuttingRecipieSO[] cuttingRecipieSOArray;

    private int cuttingProgress;

    public override void Interact(Player player) {
        if (!HasKitchenObject()) {
            //There is no KitchenObject here
            if (player.HasKitchenObject()) {
                //player is carrying something
                if (HasrecipieWithInput(player.GetKitchenObject().GetKitchenObjectSO())){
                    //Player is carrying something that can be cut
                    player.GetKitchenObject().SetKitchenObjectParent(this);
                    cuttingProgress = 0;

                    CuttingRecipieSO cuttingRecipieSO = GetCuttingRecipieSOWithInput(GetKitchenObject().GetKitchenObjectSO());
                    OnProgressChanged?.Invoke(this, new IHasProgress.OnProgressChangedEventArgs {
                    progressNormalized= (float)cuttingProgress/ cuttingRecipieSO.cuttingProgresMax
                    });                
                
                 }

            }
            else {
                //player is not carrying anything
            }
        }
        else {
            //There is a KitchenObject here
            if (player.HasKitchenObject()) {
                //player is carrying something
                if (player.GetKitchenObject().TryGetPlate(out PlateKitchenObject plateKitchenObject)) {
                    //player is holding a plate
                    if (plateKitchenObject.TryAddIngredient(GetKitchenObject().GetKitchenObjectSO())) {
                        GetKitchenObject().DestroySelf();
                    }
                }
            }
            else {
                //player is not carrying anything
                GetKitchenObject().SetKitchenObjectParent(player);
            }

        }
    }

    public override void InteractAlternate(Player player) {
        if(HasKitchenObject() && HasrecipieWithInput(GetKitchenObject().GetKitchenObjectSO())) {
            //There is a KitchenObject AND It can be cut
            cuttingProgress ++;

            OnCut?.Invoke(this, EventArgs.Empty);
            OnAnyCut?.Invoke(this, EventArgs.Empty);

            CuttingRecipieSO cuttingRecipieSO = GetCuttingRecipieSOWithInput(GetKitchenObject().GetKitchenObjectSO());

            OnProgressChanged?.Invoke(this, new IHasProgress.OnProgressChangedEventArgs {
                progressNormalized = (float)cuttingProgress / cuttingRecipieSO.cuttingProgresMax

            });

            if (cuttingProgress>= cuttingRecipieSO.cuttingProgresMax) {
                KitchenObjectSO outputKitchenObjectSO = GetOputputForInput(GetKitchenObject().GetKitchenObjectSO());
                GetKitchenObject().DestroySelf();

                KitchenObject.SpawnKitchenObject(outputKitchenObjectSO, this);
            }
            

        }
    }

    private bool HasrecipieWithInput(KitchenObjectSO inputKitchenObjectSO) {
        CuttingRecipieSO cuttingRecipieSO = GetCuttingRecipieSOWithInput(inputKitchenObjectSO);
        return cuttingRecipieSO != null;

    }

    private KitchenObjectSO GetOputputForInput(KitchenObjectSO inputKitchenObjectSO) {
        CuttingRecipieSO cuttingRecipieSO = GetCuttingRecipieSOWithInput(inputKitchenObjectSO);
        if(cuttingRecipieSO != null) {
            return cuttingRecipieSO.output;
        } else {
            return null;
        }

    }

    private CuttingRecipieSO GetCuttingRecipieSOWithInput(KitchenObjectSO inputKitchenObjectSO) {
        foreach (CuttingRecipieSO cuttingRecipieSO in cuttingRecipieSOArray) {
            if (cuttingRecipieSO.input == inputKitchenObjectSO) {
                return cuttingRecipieSO;
            }
        }
        return null;
    }
    
}
