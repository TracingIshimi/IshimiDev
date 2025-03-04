using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ClickBeddig : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] ClickObj parent;
    [SerializeField] int id;

    public void OnPointerClick(PointerEventData eventData){
        if (id == 1) {
            parent.ClickBeddig1();
        }

        else if (id == 2) {
            parent.ClickBeddig2();
        }
    }
}
