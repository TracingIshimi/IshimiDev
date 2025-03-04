using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class JigsawPuzzle : MonoBehaviour
{
    public static JigsawPuzzle jigsawPuzzle;
    public int maxidx;
    public int[] answer;
    public RectTransform[] target;

    private void Awake(){
        if(jigsawPuzzle == null){
            jigsawPuzzle = this;
        }
        else if(jigsawPuzzle != this){
            Destroy(this);
            return;
        }

        answer = new int[maxidx];
    }

    public void Snap(RectTransform rectTransform) {
        for (int i = 0; i<maxidx; i++) {
            if (Vector2.Distance(target[i].anchoredPosition, rectTransform.anchoredPosition) < 50f) {
                rectTransform.anchoredPosition = target[i].anchoredPosition;
            }
        }
    }

    public void IsRightPos(RectTransform rectTransform, RectTransform target, int idx) {
        if (rectTransform.anchoredPosition == target.anchoredPosition) {
            answer[idx] = 1;
        }
        else {
            return;
        }
    }

    public void CheckAnswer() {
        for (int i = 0; i<maxidx; i++) {
            if(answer[i] == 0) {
                return;
            }
        }
        Debug.Log("Right Answer");
    }


}