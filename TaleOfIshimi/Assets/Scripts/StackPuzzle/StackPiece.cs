using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class StackPiece : MonoBehaviour,IPointerClickHandler
{
    int id;
    public void InitPiece(string path, int id){
        this.id = id;
        GetComponent<Image>().sprite = Resources.Load<Sprite>(path+"/"+id.ToString());
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        StackPuzzle.stkPuzzle.StackPush(id);
        gameObject.SetActive(false);
    }
}
