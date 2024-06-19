using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class Inventory : MonoBehaviour
{
    private Item[] inventory = new Item[Const.ITEM_MAX_IDX];
    private Item[] puzzleItems = new Item[Const.ITEM_MAX_IDX];
    private int nextIdx = 0;
    private int puzzleIdx = -1;

    void InitInventory(int puzzleIdx){
        this.puzzleIdx = puzzleIdx;
        string filePath = Const.ITEM_PATH_BASE+puzzleIdx.ToString()+".json";
        string jsonStr = File.ReadAllText(filePath);
        Item tmpItem = JsonUtility.FromJson<Item>(jsonStr);
    }
    void AddItem(int itemIdx){
        inventory[nextIdx] = puzzleItems[itemIdx];
        nextIdx++;
    }
}
