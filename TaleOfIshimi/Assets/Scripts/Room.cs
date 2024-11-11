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

    public static Room room;
    public int clickNum = 0;
    public int[] clickInput = new int[3];
    public int[] isClickedAlready = new int[3];
    public GameObject[] fire;

    private void Awake() {
        if(Room.room == null) {
            Room.room = this;
        }
    }

}
