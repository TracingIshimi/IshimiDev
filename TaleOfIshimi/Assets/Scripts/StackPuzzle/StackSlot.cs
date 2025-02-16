using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using UnityEngine.EventSystems;

public class StackSlot : MonoBehaviour,IPointerClickHandler
{
    Image image;
    void Start(){
        image = GetComponent<Image>();
    }
    public void SetImage(string path,int id){
        image.sprite = Resources.Load<Sprite>(path+"/"+id.ToString());
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        StackPuzzle.stkPuzzle.StackPop();
        gameObject.SetActive(false);
    }
}
