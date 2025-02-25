using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KitchenDoor : MonoBehaviour
{
    [SerializeField] Room room;

    void OnEnable(){
        room.ExitRoom();
    }
    void OnDisable(){
        room.MoveToNum(0);
    }
}
