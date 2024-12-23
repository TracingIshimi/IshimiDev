using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using UnityEngine.EventSystems;

public class AnimalObj : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] Image selectEff;
    [SerializeField] Image pieceImg;

    private int idx;
    private bool isSelected = false;

    public void OnPointerClick(PointerEventData eventData){
        AnimalPuzzle.aniPuzzle.ClickPiece(idx);
    }

    public void SelectPiece(){
        isSelected = true;
        selectEff.gameObject.SetActive(true);
    }
    public void DeselectPiece(){
        isSelected = false;
        selectEff.gameObject.SetActive(false);
    }
    public void InitPiece(int idx, string path){
        this.idx = idx;
        selectEff.gameObject.SetActive(false);
        pieceImg.sprite = Resources.Load<Sprite>(path+'/'+idx.ToString());
    }
    public int GetIdx(){
        return idx;
    }
}
