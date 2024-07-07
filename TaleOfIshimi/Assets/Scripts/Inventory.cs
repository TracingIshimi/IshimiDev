using System.Collections;
using System.Collections.Generic;
using System.Data;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    private Item[] inventory = new Item[Const.ITEM_MAX_IDX];
    private Item[] puzzleItems = new Item[Const.ITEM_MAX_IDX];
    private int nextIdx = 0;

    void InitInventory(){
        DBManager.dbManager.OpenDBConnection();

        IDbCommand itemCommand = DBManager.dbManager.dbConnection.CreateCommand();
        itemCommand.CommandText = "SELECT * FROM "+Const.ITEM_TABLE+" WHERE stage_id = "+StageManager.stageManager.GetStageNum().ToString();
        IDataReader itemReader = itemCommand.ExecuteReader();
        while(itemReader.Read()){
            int tmpId = itemReader.GetInt32(Const.ITEM_ATTRIBUTE["item_id"]);
            string tmpName = itemReader.GetString(Const.ITEM_ATTRIBUTE["name"]);
            string tmptType = itemReader.GetString(Const.ITEM_ATTRIBUTE["interaction_type"]);
            puzzleItems[tmpId] = new Item(tmpId, tmpName, tmptType);
        }
        itemReader.Close();
        itemCommand.Dispose();

        IDbCommand invenCommand = DBManager.dbManager.dbConnection.CreateCommand();
        invenCommand.CommandText = "SELECT * FROM "+Const.INVEN_TABLE+" WHERE stage_id = "+StageManager.stageManager.GetStageNum().ToString();
        IDataReader invenReader = invenCommand.ExecuteReader();
        while(invenReader.Read()){
            int tmpSlotId = itemReader.GetInt32(Const.INVEN_ATTRIBUTE["slot_id"]);
            if(!invenReader.IsDBNull(Const.INVEN_ATTRIBUTE["item_id"])){
                int tmpItemId = itemReader.GetInt32(Const.INVEN_ATTRIBUTE["item_id"]);
                inventory[tmpSlotId] = puzzleItems[tmpItemId];
            }
        }
        invenReader.Close();
        invenCommand.Dispose();

        DBManager.dbManager.CloseDBConnection();
    }
    void AddItem(int itemIdx){
        inventory[nextIdx] = puzzleItems[itemIdx];
        nextIdx++;
    }
}
