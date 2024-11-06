using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PWDButton : MonoBehaviour,IPointerClickHandler
{
    public int buttonType;
    [SerializeField] PWDPrefab pref;
    public void OnPointerClick(PointerEventData eventData){
        if(buttonType==0){
            pref.IdxUp();
        }
        else{
            pref.IdxDown();
        }
    }
}
