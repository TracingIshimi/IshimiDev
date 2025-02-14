using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StackPuzzle : MonoBehaviour
{
    public static StackPuzzle stkPuzzle;
    [SerializeField] StackPiece piece;
    [SerializeField] StackSlot slot;
    [SerializeField] GameObject pieceParent;
    [SerializeField] GameObject slotParent;

    [SerializeField] string path = "";
    [SerializeField] const int maxSlot = 0;
    [SerializeField] string answer = "";
    int[] currStack;
    int currTop = -1;
    StackPiece[] pieces;
    StackSlot[] slots;

    void Awake(){
        if(stkPuzzle == null){
            stkPuzzle = this;
        }
        else if(stkPuzzle != this){
            Destroy(this);
            return;
        }
    }
    void Start(){
        currStack = new int[maxSlot];
        pieces = new StackPiece[maxSlot];
        slots = new StackSlot[maxSlot];

        for(int i = 0; i<maxSlot; i++){
            currStack[i] = -1;
            StackPiece tmppiece = Instantiate(piece,pieceParent.transform);
            tmppiece.InitPiece(path,i);
            pieces[i] = tmppiece;
            StackSlot tmpSlot = Instantiate(slot,slotParent.transform);
            tmpSlot.transform.localPosition = slotParent.transform.localPosition;
            slots[i] = tmpSlot;
            slots[i].gameObject.SetActive(false);
        }
    }

    public void StackPush(int id){
        if(currTop<maxSlot-1){
            currTop++;
            currStack[currTop] = id;
            slots[currTop].gameObject.SetActive(true);
            slots[currTop].SetImage(path,id);
            if(currTop==maxSlot-1){
                if(CheckAnswer()){
                    GameClear();
                    return;
                }
                else{
                    Debug.Log("Wrong Answer");
                }
            }
        }
        else{
            pieces[id].gameObject.SetActive(true);
            return;
        }
    }

    public void StackPop(){
        if(currTop<0){
            return;
        }
        pieces[currStack[currTop]].gameObject.SetActive(true);
        currStack[currTop] = -1;
        currTop--;
    }

    bool CheckAnswer(){
        string tmp = "";
        for(int i = 0; i<maxSlot; i++){
            tmp+=currStack[i].ToString();
        }
        if(tmp == answer){
            return true;
        }
        return false;
    }

    void GameClear(){
        Debug.Log("Game Clear");
    }
}
