using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Room : MonoBehaviour
{
    public GameObject[] roomSides;
    private int currentRoomIndex = 0;

    public void MoveToRightSide() {
        roomSides[currentRoomIndex].SetActive(false);
        currentRoomIndex = (currentRoomIndex + 1) % roomSides.Length;
        roomSides[currentRoomIndex].SetActive(true);
    }

    public void MoveToLeftSide() {
        roomSides[currentRoomIndex].SetActive(false);
        currentRoomIndex = (currentRoomIndex - 1 + roomSides.Length) % roomSides.Length;
        roomSides[currentRoomIndex].SetActive(true);
    }

    public void MoveToNum(int num){
        roomSides[currentRoomIndex].SetActive(false);
        currentRoomIndex = num;
        roomSides[currentRoomIndex].SetActive(false);
    }

    public void ExitRoom(){
        roomSides[currentRoomIndex].SetActive(false);
    }

    void Start(){
        roomSides[0].gameObject.SetActive(true);
        for(int i = 1; i<roomSides.Length; i++){
            roomSides[i].gameObject.SetActive(false);
        }
    }

}
