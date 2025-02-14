using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class MapFunc : MonoBehaviour, IPointerClickHandler
{
    public int id;
    public string[] interType = {"Default"};
    public string constraint = "";
    public string etc = "";

    public bool dontDeactivate = false;

    [SerializeField] GameObject MainObj;
    [SerializeField] StorySystem StoryObj;

    int spriteIdx = 0;
    int maxIdx = 0;

    private float moveFlag = 0;

    private float dy = 0;
    private float dx = 0;
    private float orgX;
    private float orgY;
    private float moveSpeed = 0;
    private bool isMoved=false;

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
        if(constraint!=""){
            string[] tmp = constraint.Split();
            for(int i = 0; i<tmp.Length; i+=2){
                if(!CheckConstraint(tmp[i],tmp[i+1])){
                    return;
                }
            }

        }
        for(int i = 0; i<interType.Length; i++){
            Debug.Log(interType[i]);
            Invoke(interType[i],0f);
        }
    }

    bool CheckConstraint(string type, string constraint){
        switch(type){
            case "have_item":
                if(Inventory.imanager.FindInven(int.Parse(constraint))){
                    return true;
                }
                break;
            default:
                break;
        }
        return false;
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
            Inventory.imanager.DeleteItem(invenTarget);
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
            Inventory.imanager.DeleteItem(invenTarget);
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
        MainObj.gameObject.SetActive(true);
    }

    void changePosition(){
        if(isMoved&&etc.Split('_')[3]=="1"){
            toOrgPosition();
            return;
        }
        Debug.Log("changPosition call");
        // etc 형식: dx_dy_moveSpeed_toOrg
        string[] thisEtc = etc.Split('_');
        dx = float.Parse(thisEtc[0]);
        dy = float.Parse(thisEtc[1]);
        moveSpeed = float.Parse(thisEtc[2]);
        orgX = transform.position.x;
        orgY = transform.position.y;
        moveFlag = moveSpeed;
        isMoved = true;
    }

    void toOrgPosition(){
        if(!isMoved){
            return;
        }
        Debug.Log("toOrgPosition call");
        dx = orgX-transform.position.x;
        dy = orgY-transform.position.y;
        orgX = transform.position.x;
        orgY = transform.position.y;
        moveFlag = moveSpeed;
        isMoved = false;
    }

    void SetConv(){
        // etc 형식: initScriptID_defaultScriptID_isDefault
        string[] thisEtc = etc.Split('_');
        int scrptId = 0;
        if(thisEtc[2]=="1"){
            scrptId = int.Parse(thisEtc[1]);
        }
        else{
            scrptId = int.Parse(thisEtc[0]);
            etc = thisEtc[0]+"_"+thisEtc[1]+"_1";
        }
        StoryObj.SetConv(scrptId);
    }

    void SpiritActivate(){
        // 메인오브젝트에 SpModeManager를 둘 것
        MainObj.SetActive(true);
    }

    void Update(){
        if(moveFlag>0){
            Debug.Log(moveFlag+"\t"+isMoved);
            float tmpX = transform.position.x + dx/moveSpeed*Time.deltaTime;
            float tmpY = transform.position.y + dy/moveSpeed*Time.deltaTime;
            transform.position = new Vector3(tmpX,tmpY,0);
            moveFlag-=Time.deltaTime;
        }
        else if(moveFlag<0){
            transform.position = new Vector3(orgX+dx,orgY+dy,0);
            moveFlag = 0;
        }
    }
}
