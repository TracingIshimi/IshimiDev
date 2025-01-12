using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpStage2_condition : MonoBehaviour
{
    [SerializeField] GameObject doorActive;
    [SerializeField] GameObject doorDeactive;
    bool doorOn = false;

    public void DoorActive(){
        if(doorOn){
            return;
        }
        doorActive.SetActive(true);
        doorDeactive.SetActive(false);
        doorOn = true;
    }
}
