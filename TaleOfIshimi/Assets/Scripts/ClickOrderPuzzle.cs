using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
public class ClickOrderPuzzle : MonoBehaviour, IPointerClickHandler
{
    public int id;
    private int answer = 231;
    private int output = 0;

    public void OnPointerClick(PointerEventData eventData) {
        if(Room.room.clickNum < 2) {
            Debug.Log("현재 i 값: " + Room.room.clickNum);
            Room.room.fire[id-1].SetActive(true);
            Room.room.clickInput[Room.room.clickNum] = id;
            Room.room.clickNum++;
            Debug.Log("현재 clickInput 배열: " + string.Join(", ", Room.room.clickInput));
        }

        else {
            Debug.Log("현재 i 값: " + Room.room.clickNum);
            Room.room.fire[id-1].SetActive(true);
            Room.room.clickInput[Room.room.clickNum] = id;
            Debug.Log("현재 clickInput 배열: " + string.Join(", ", Room.room.clickInput));
            output = Room.room.clickInput[0]*100 + Room.room.clickInput[1]*10 + Room.room.clickInput[2]; 
            if (output == answer) {
                Debug.Log("That's right~~!!");
                Room.room.clickNum = 0;
                System.Array.Clear(Room.room.clickInput, 0, Room.room.clickInput.Length);
            }
            else {
                Debug.Log("This is not answer;;");
                Room.room.clickNum = 0;
                System.Array.Clear(Room.room.clickInput, 0, Room.room.clickInput.Length);
            }
            for(int i = 0; i < 3; i++) {
                    Room.room.fire[i].SetActive(false);
            }
        }
        
    }


}
