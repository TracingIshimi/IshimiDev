using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimalPuzzle : MonoBehaviour
{
    public static AnimalPuzzle aniPuzzle;
    [SerializeField] string imgPath;
    [SerializeField] int maxidx;
    [SerializeField] string pwAnswer;
    [SerializeField] GameObject slotObj;
    [SerializeField] GameObject peiceObj;
    [SerializeField] AnimalObj piecePrefab;
    [SerializeField] AnimalSlot slotPrefab;

    [SerializeField] GameObject clearActive; 
    
    private AnimalObj[] pieces;
    private AnimalSlot[] slots;

    private int[] currPW;
    private int target = -1;

    private void Awake(){
        if(aniPuzzle == null){
            aniPuzzle = this;
        }
        else if(aniPuzzle != this){
            Destroy(this);
            return;
        }
    }

    private void Start(){
        pieces = new AnimalObj[maxidx];
        slots = new AnimalSlot[maxidx];
        currPW = new int[maxidx];

        for(int i = 0; i<maxidx; i++){
            AnimalSlot tmpslot = Instantiate(slotPrefab);
            tmpslot.transform.SetParent(slotObj.transform);
            tmpslot.InitSlot(i,this);
            slots[i] = tmpslot;
            currPW[i] = -1;
        }
        for(int j = 0; j<maxidx; j++){
            AnimalObj tmpobj = Instantiate(piecePrefab);
            tmpobj.transform.SetParent(peiceObj.transform);
            tmpobj.InitPiece(j,imgPath);
            pieces[j] = tmpobj;
        }
    }

    public void ClickPiece(int idx){
        if(target>=0){
            pieces[target].DeselectPiece();
        }
        pieces[idx].SelectPiece();
        target = idx;
    }

    public void ClickSlot(int idx){
        if(target<0){
            int pieceIdx = slots[idx].GetPieceIdx();
            if(pieceIdx<0){
                return;
            }
            pieces[pieceIdx].gameObject.SetActive(true);
            pieces[pieceIdx].DeselectPiece();
            slots[idx].ClearSlot();
            currPW[idx] = -1;
            return;
        }
        if(slots[idx].GetPieceIdx()<0){
            pieces[target].DeselectPiece();
            pieces[target].gameObject.SetActive(false);
            slots[idx].SetSlot(target);
            currPW[idx] = target;
            target = -1;
            CheckAnswer();
        }
    }

    private void CheckAnswer(){
        string currAns = "";
        for(int i = 0; i<maxidx; i++){
            if(currPW[i]<0){
                Debug.Log("Piece not assigned");
                return;
            }
            currAns+=currPW[i].ToString();
        }
        if(currAns==pwAnswer){
            Debug.Log("PW correct: "+pwAnswer);
            clearActive.SetActive(true);
        }
        else{
            Debug.Log("PW wrong: "+currAns);
            Invoke("ResetPuzzle",0.3f);
        }
    }

    public void ResetPuzzle(){
        for(int i = 0; i<slots.Length;i++){
            int pieceIdx = slots[i].GetPieceIdx();
            if(pieceIdx<0){
                continue;
            }
            pieces[pieceIdx].gameObject.SetActive(true);
            pieces[pieceIdx].DeselectPiece();
            slots[i].ClearSlot();
            currPW[i] = -1;
        }
    }

    public void DeselectTarget(){
        if(target<0){
            return;
        }
        pieces[target].DeselectPiece();
        target = -1;
    }

    public string GetPath(){
        return imgPath;
    }

}
