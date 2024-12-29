using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class AnimalSlot : MonoBehaviour,IPointerClickHandler
{
    [SerializeField] Image pieceImg;

    AnimalPuzzle parent;
    int peiceIdx = -1;
    int idx;

    public void OnPointerClick(PointerEventData eventData){
        AnimalPuzzle.aniPuzzle.ClickSlot(idx);
    }

    public void InitSlot(int idx, AnimalPuzzle parent){
        pieceImg.gameObject.SetActive(false);
        this.idx = idx;
        this.parent = parent;
    }

    public void SetSlot(int idx){
        peiceIdx = idx;
        pieceImg.gameObject.SetActive(true);
        pieceImg.sprite = Resources.Load<Sprite>(parent.GetPath()+'/'+idx.ToString());
    }
    public void ClearSlot(){
        peiceIdx = -1;
        pieceImg.gameObject.SetActive(false);
    }
    public int GetPieceIdx(){
        return peiceIdx;
    }
}
