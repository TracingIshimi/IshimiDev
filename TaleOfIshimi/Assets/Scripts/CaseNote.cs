using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;

public class CaseNote : MonoBehaviour , IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] GameObject modalPanel;
    public TextMeshProUGUI btnTxt;
    Color originColor = new Color32(50,50,50,255);
    Color newColor = new Color32(0,0,0,255);
    

    public void ModalPanelOn() {
        modalPanel.SetActive(true);
    }

    public void ModalPanelOff() {
        modalPanel.SetActive(false);
    }

     public void OnPointerEnter(PointerEventData eventData) {
        btnTxt.color = newColor;
    }
    
    public void OnPointerExit(PointerEventData eventData) {
        btnTxt.color = originColor;
    }
}
