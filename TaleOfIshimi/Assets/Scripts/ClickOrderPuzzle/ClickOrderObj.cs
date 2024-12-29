using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ClickOrderObj : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] int id;
    [SerializeField] ClickOrderPuzzle parent;
    [SerializeField] GameObject fire;

    bool isOn = false;

    public void OnPointerClick(PointerEventData eventData) {
        if(isOn){
            return;
        }
        fire.SetActive(true);
        isOn = true;
        parent.SetPW(id);
    }
    public void PutOutFire(){
        isOn = false;
        fire.SetActive(false);
    }
}
