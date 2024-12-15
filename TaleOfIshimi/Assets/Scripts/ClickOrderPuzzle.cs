using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using System;
public class ClickOrderPuzzle : MonoBehaviour, IPointerClickHandler
{
    public int id;
    public int matchId=-1;
    private int answer = 231;
    private int output = 0;

    [SerializeField] GameObject[] deactivateItems;
    [SerializeField] GameObject[] activateItems;

    public void MakeAFire(int id) {
        if(Inventory.imanager.GetTarget()<0||Inventory.imanager.GetSlotItem(Inventory.imanager.GetTarget()).getId()!=matchId){
            return;
        }
        Room.room.fire[id-1].SetActive(true);
    }

    public void PutOutFire()  {
        for(int i = 0; i < 3; i++) {
            Room.room.fire[i].SetActive(false);
        }
    }

    public void OnPointerClick(PointerEventData eventData) {
        if(!Convert.ToBoolean(Room.room.isClickedAlready[id-1])) {
            MakeAFire(id);
            Room.room.clickInput[Room.room.clickNum] = id;
            Room.room.isClickedAlready[id-1] = 1;

            if(Room.room.clickNum < 2) {
                Room.room.clickNum++;
            }

            else {
                output = Room.room.clickInput[0]*100 + Room.room.clickInput[1]*10 + Room.room.clickInput[2]; 

                if (output == answer) {
                    ClearFunc();
                    return;
                }

                else {
                    Invoke("PutOutFire", 0.2f);
                    Room.room.clickNum = 0;
                    System.Array.Clear(Room.room.clickInput, 0, Room.room.clickInput.Length);
                    System.Array.Clear(Room.room.isClickedAlready, 0, Room.room.isClickedAlready.Length);
                }
            }
        }

        else {
            return;
        }
        
    }

    void ClearFunc(){
        for(int i = 0; i<deactivateItems.Length; i++){
            deactivateItems[i].gameObject.SetActive(false);
        }
        for(int j = 0; j<activateItems.Length; j++){
            activateItems[j].gameObject.SetActive(true);
        }
    }
    


}
