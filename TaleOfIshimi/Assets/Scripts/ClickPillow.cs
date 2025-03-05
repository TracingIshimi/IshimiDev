using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ClickPillow : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] ClickObj parent;

    public void OnPointerClick(PointerEventData eventData){
        parent.MovePillow();
    }
}
