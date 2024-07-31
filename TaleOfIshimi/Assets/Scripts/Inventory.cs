using System.Collections;
using System.Collections.Generic;
using System.Data;
using TMPro;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public static Inventory imanager;

    private Item[] inventory = new Item[Const.ITEM_MAX_IDX];
    private Item[] puzzleItems = new Item[Const.ITEM_MAX_IDX];
    private int nextIdx = 0;
    private int target = -1;

    public ItemSlot[] itemSlots = new ItemSlot[4];
    [SerializeField] TextMeshProUGUI itemText;

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
        InitInventory();
    }

    public void SetItemText(string text){
        itemText.text = text;
    }

    public void SetItemTextTarget(){
        if(target<0){
            itemText.text = "";
        }
        else{
            itemText.text = inventory[target].getName();
        }
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
            puzzleItems[tmpId] = new Item(tmpId, tmpName, tmptType);
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
    void AddItem(int itemIdx){
        inventory[nextIdx] = puzzleItems[itemIdx];
        itemSlots[nextIdx].SetSlot(inventory[nextIdx]);
        Debug.Log(nextIdx+" :: "+inventory[nextIdx].getName());
        nextIdx++;
    }

    public void ClickSlot(int slotId){
        if(itemSlots[slotId].SlotEmpty()){
            return;
        }
        if(target == slotId){
            //아이템 사용
            Debug.Log("Use Item: "+inventory[slotId].getName());
        }
        else{
            if(target>=0){
                itemSlots[target].DeselectSlot();
            }
            itemSlots[slotId].SelectSlot();
            target = slotId;
        }
    }


    public void SortInventory(){
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

    public void SetSlotGraphics(){
        for(int i = 0; i<itemSlots.Length; i++){
            if(inventory[i] == null){
                itemSlots[i].ClearSlot();
            }
            else{
                itemSlots[i].SetSlot(inventory[i]);
            }
        }

    }
}
