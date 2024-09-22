using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class MapFunc : MonoBehaviour, IPointerClickHandler
{
    public int id;
    public string[] interType = {"Default"};
    public string etc = "";

    public bool dontDeactivate = false;

    [SerializeField] GameObject MainObj;

    int spriteIdx = 0;
    int maxIdx = 0;

    void Start(){
        if(interType[0]=="ChangeForm"){
            maxIdx = transform.childCount;
            for(int i = 0; i< maxIdx; i++){
                transform.GetChild(i).gameObject.SetActive(false);
            }
            transform.GetChild(spriteIdx).gameObject.SetActive(true);
        }
    }

    public void OnPointerClick(PointerEventData eventData){
        for(int i = 0; i<interType.Length; i++){
            Debug.Log(interType[i]);
            Invoke(interType[i],0f);
        }
    }

    void Default(){
        return;
    }
    
    void ObtainItem(){
        Inventory.imanager.AddItem(int.Parse(etc));
    }

    void CombineWithItem(){
        int target = int.Parse(etc.Split('_')[0]);
        int result = int.Parse(etc.Split('_')[1]);

        int invenTarget = Inventory.imanager.GetTarget();
        if(invenTarget<0){
            return;
        }
        Item invenItem = Inventory.imanager.GetSlotItem(invenTarget);
        if(invenItem.getItemType() != InteractionType.ADDWITH_MAP){
            return;
        }
        string[] invenEtc = invenItem.getEtc().Split('_');
        if( invenItem.getId() == target && int.Parse(invenEtc[0]) == id){
            Inventory.imanager.DeleteItem(target);
            Inventory.imanager.AddItem(result);
            Inventory.imanager.ResetTarget();
            DeactivateSelf();
        }
    }

    void ActionWithItem(){
        string[] thisEtc = etc.Split('_');
        int target = int.Parse(thisEtc[0]);
        string action = thisEtc[1];

        int invenTarget = Inventory.imanager.GetTarget();
        if(invenTarget<0){
            return;
        }
        Item invenItem = Inventory.imanager.GetSlotItem(invenTarget);
        if(invenItem.getItemType() != InteractionType.ADDWITH_MAP){
            return;
        }
        string[] invenEtc = invenItem.getEtc().Split('_');
        if( invenItem.getId() == target && int.Parse(invenEtc[0]) == id){
            Inventory.imanager.DeleteItem(target);
            Inventory.imanager.ResetTarget();
            etc = "";
            for(int i = 2; i<thisEtc.Length; i++){
                etc+=thisEtc[i];
                if(i!=thisEtc.Length-1){
                    etc+="_";
                }
            }
            Invoke(action,0f);
        }
    }

    void ChangeForm(){
        transform.GetChild(spriteIdx).gameObject.SetActive(false);
        spriteIdx++;
        spriteIdx%=maxIdx;
        transform.GetChild(spriteIdx).gameObject.SetActive(true);
    }

    void DeactivateSelf(){
        if(dontDeactivate){
            return;
        }
        gameObject.SetActive(false);
    }

    void ActivateObj(){
        MainObj.transform.GetChild(int.Parse(etc)).gameObject.SetActive(true);
    }

    void SpiritActivate(){
        Inventory.imanager.spCam.SetSpResolution();
    }
}
