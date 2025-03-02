using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using TMPro;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public static Inventory imanager;

    private Item[] inventory = new Item[Const.INVEN_MAX_IDX];
    private Item[] puzzleItems = new Item[Const.ITEM_MAX_IDX];
    private int nextIdx = 0;
    private int target = -1;

    public ItemSlot[] itemSlots = new ItemSlot[6];
    [SerializeField] TextMeshProUGUI itemText;
    [SerializeField] GameObject spManager;
    [SerializeField] StorySystem conv;


/////////////////////// Manager 객체 초기화 ///////////////////////
    private void Awake(){
        if(imanager == null){
            imanager = this;
        }
        else if(imanager != this){
            Destroy(this);
            return;
        }
        for(int i = 0; i<itemSlots.Length; i++){
            itemSlots[i].SetSlotID(i);
        }
    }

/////////////////////// 인벤토리 초기화 ///////////////////////
    private void Start(){
        itemText.text = "";
        InitInventory();
    }

    void InitInventory(){
        Debug.Log("InitInventory Start");
        DBManager.dbManager.OpenDBConnection();

        Debug.Log("Item DB Read Start");
        IDbCommand itemCommand = DBManager.dbManager.dbConnection.CreateCommand();
        itemCommand.CommandText = "SELECT * FROM "+Const.ITEM_TABLE+" WHERE stage_id = "+StageManager.stageManager.GetStageNum().ToString();
        IDataReader itemReader = itemCommand.ExecuteReader();
        while(itemReader.Read()){
            int tmpId = itemReader.GetInt32(Const.ITEM_ATTRIBUTE["item_id"]);
            Debug.Log("id: "+tmpId);
            string tmpName = itemReader.GetString(Const.ITEM_ATTRIBUTE["name"]);
            string tmptType = itemReader.GetString(Const.ITEM_ATTRIBUTE["interaction_type"]);
            string tmpEtc = "";
            if(!itemReader.IsDBNull(Const.ITEM_ATTRIBUTE["etc"])){
                tmpEtc = itemReader.GetString(Const.ITEM_ATTRIBUTE["etc"]);
            }
            puzzleItems[tmpId] = new Item(tmpId, tmpName, tmptType, tmpEtc);
        }
        itemReader.Close();
        itemCommand.Dispose();
        Debug.Log("Item DB Read End");

        Debug.Log("Inventory DB Read Start");
        IDbCommand invenCommand = DBManager.dbManager.dbConnection.CreateCommand();
        invenCommand.CommandText = "SELECT * FROM "+Const.INVEN_TABLE+" WHERE stage_id = "+StageManager.stageManager.GetStageNum().ToString();
        IDataReader invenReader = invenCommand.ExecuteReader();
        while(invenReader.Read()){
            int tmpSlotId = invenReader.GetInt32(Const.INVEN_ATTRIBUTE["slot_id"]);
            Debug.Log(tmpSlotId);
            if(!invenReader.IsDBNull(Const.INVEN_ATTRIBUTE["item_id"])){
                int tmpItemId = invenReader.GetInt32(Const.INVEN_ATTRIBUTE["item_id"]);
                AddItem(tmpItemId);
            }
        }
        invenReader.Close();
        invenCommand.Dispose();
        Debug.Log("Inventory DB Read End");

        DBManager.dbManager.CloseDBConnection();
        for(int i = nextIdx; i<inventory.Length; i++){
            inventory[i] = null;
        }
        SortInventory();
    }

/////////////////////// 인벤토리 기능 ///////////////////////
    public void AddItem(int itemIdx){
        if(nextIdx >= Const.ITEM_MAX_IDX){
            return;
        }
        inventory[nextIdx] = puzzleItems[itemIdx];
        itemSlots[nextIdx].SetSlot(inventory[nextIdx]);
        Debug.Log(nextIdx+" :: "+inventory[nextIdx].getName());
        nextIdx++;
    }

    public void DeleteItem(int slotIdx){
        inventory[slotIdx] = null;
        itemSlots[slotIdx].ClearSlot();
        Debug.Log("Delete Item Call");
        nextIdx--;
        SortInventory();
    }

    bool CombineItems(int slotA, int slotB){
        Debug.Log("CombineItem Call");
        string[] stringA = itemSlots[slotA].GetItem().getEtc().Split('_');
        string[] stringB = itemSlots[slotB].GetItem().getEtc().Split('_');
        int idA = itemSlots[slotA].GetItem().getId();
        int idB = itemSlots[slotB].GetItem().getId();

        if(stringA[0]==idB.ToString() && stringB[0]==idA.ToString()){
            Debug.Log("ADD Item: "+itemSlots[slotA].GetItem().getName()+" + "+itemSlots[slotB].GetItem().getName());
            DeleteItem(Math.Max(slotA,slotB));
            DeleteItem(Math.Min(slotA,slotB));
            AddItem(int.Parse(stringA[1]));
            ResetTarget();
            return true;
        }
        return false;
    }


/////////////////////// 타겟 기능 ///////////////////////
    void SetTarget(int idx){
        if(target>=0){
            itemSlots[target].DeselectSlot();
        }
        itemSlots[idx].SelectSlot();
        target = idx;
    }

    public void ResetTarget(){
        if(target<0){
            return;
        }
        itemSlots[target].DeselectSlot();
        target = -1;
    }


/////////////////////// 인터랙션 기능 ///////////////////////
    public void ClickSlot(int slotId){
        if(itemSlots[slotId].SlotEmpty()){
            ResetTarget();
            return;
        }

        Debug.Log(itemSlots[slotId].GetItem().getName()+ " :: type ->" +itemSlots[slotId].GetItem().getItemType().ToString());

        if(target<0){
            SetTarget(slotId);
            return;
        }

        InteractionType slotType = itemSlots[slotId].GetItem().getItemType();
        Debug.Log(slotType);
        switch(slotType){
            case InteractionType.GET_ONLY:
                if(target==slotId){
                    return;
                }
                break;
            case InteractionType.ADDWITH_MAP:
                break;
            case InteractionType.ADDWITH_ITEM:
                if(itemSlots[target].GetItem().getItemType()==InteractionType.ADDWITH_ITEM){
                    bool isCombined = CombineItems(target,slotId);
                    if(isCombined){return;}
                }
                break;
            case InteractionType.SPSIGHT:
                if(target==slotId){
                    ResetTarget();
                    DeleteItem(slotId);
                    spManager.SetActive(true);
                    return;
                }
                break;
            case InteractionType.CONV:
                if(target==slotId){
                    int convId = int.Parse(itemSlots[target].GetItem().getEtc());
                    conv.SetConv(convId);
                    return;
                }
                break;
            case InteractionType.READ:
                //팝업 기능 채우기
                break;
            case InteractionType.DIRECT_USE:
            default:
                break;
        }

        // 아이템 사용
        if(target == slotId && slotType==InteractionType.DIRECT_USE){
            Debug.Log("Use Item: "+inventory[slotId].getName());
            ResetTarget();
            DeleteItem(slotId);
        }
        // 타겟 변경
        else{
            SetTarget(slotId);
        }
    }

    /////////////////////// GUI 관련 ///////////////////////
    public void SetItemText(string text){
        itemText.text = text;
    }

    public void SetItemTextTarget(){
        if(target<0 || inventory[target]==null){
            itemText.text = "";
        }
        else{
            itemText.text = inventory[target].getName();
        }
    }


    void SortInventory(){
        Debug.Log("Sort Inventory Call");
        int idx = 0;
        for(int i = 0; i<inventory.Length; i++){
            if(inventory[i] == null){
                idx = i+1;
                while(idx<=inventory.Length-1&&inventory[idx]==null){
                    idx++;
                }
                if(idx>inventory.Length-1){
                    break;
                }
                else{
                    inventory[i] = inventory[idx];
                    inventory[idx] = null;
                    idx++;
                }
            }
        }
        SetSlotGraphics();
    }

    void SetSlotGraphics(){
        for(int i = 0; i<itemSlots.Length; i++){
            if(inventory[i] == null){
                itemSlots[i].ClearSlot();
            }
            else{
                itemSlots[i].SetSlot(inventory[i]);
            }
        }

    }

/////////////////////// Getter ///////////////////////
    public int GetTarget(){
        return target;
    }
    public Item GetSlotItem(int idx){
        return itemSlots[idx].GetItem();
    }

    public bool FindInven(int idx){
        for(int i = 0; i<itemSlots.Length; i++){
            if(itemSlots[i].GetItem().getId()==idx){
                return true;
            }
        }
        return false;
    }
}
