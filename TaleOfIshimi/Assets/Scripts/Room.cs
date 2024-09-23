using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Room : MonoBehaviour
{
    public GameObject[] roomSides;
    private int currentRoomIndex = 0;

    public void MoveToNextSide() {
        roomSides[currentRoomIndex].SetActive(false);
        currentRoomIndex = (currentRoomIndex + 1) % roomSides.Length;
        roomSides[currentRoomIndex].SetActive(true);
    }

    public void MoveToPreviousSide() {
        roomSides[currentRoomIndex].SetActive(false);
        currentRoomIndex = (currentRoomIndex - 1 + roomSides.Length) % roomSides.Length;
        roomSides[currentRoomIndex].SetActive(true);
    }

}
