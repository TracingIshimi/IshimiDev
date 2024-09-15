using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzleRoomCamera : MonoBehaviour
{
    public Transform[] roomViews;
    private int currentRoomIndex = 0;
    public Camera mainCamera;
    
    void Start() {
        Vector3 newPosition = roomViews[currentRoomIndex].position;
        newPosition.z = -10f;
        mainCamera.transform.position = newPosition;
    }

    public void MoveToNextRoom() {
        currentRoomIndex = (currentRoomIndex + 1) % roomViews.Length;
        Vector3 newPosition = roomViews[currentRoomIndex].position;
        newPosition.z = -10f;
        mainCamera.transform.position = newPosition;
    }

    public void MoveToPreviousRoom() {
        currentRoomIndex = (currentRoomIndex - 1 + roomViews.Length) % roomViews.Length;
        Vector3 newPosition = roomViews[currentRoomIndex].position;
        newPosition.z = -10f;
        mainCamera.transform.position = newPosition;
    }
}

