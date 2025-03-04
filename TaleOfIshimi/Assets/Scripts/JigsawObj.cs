using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class JigsawObj : MonoBehaviour, IDragHandler, IEndDragHandler
{
    public RectTransform target; // Snap 대상이 되는 RectTransform
    public int idx;
    private RectTransform rectTransform;
    private Canvas canvas;

    private void Awake() {
        rectTransform = GetComponent<RectTransform>();
        canvas = GetComponentInParent<Canvas>();
    }

    public void OnDrag(PointerEventData eventData) {
        JigsawPuzzle.jigsawPuzzle.answer[idx] = 0;

        if (canvas == null) return;

        // 드래그된 위치 계산
        Vector2 movePosition;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(
            canvas.transform as RectTransform,
            eventData.position,
            canvas.worldCamera,
            out movePosition
        );

        // 위치 적용
        rectTransform.anchoredPosition = movePosition;
    }

    public void OnEndDrag(PointerEventData eventData) {
        JigsawPuzzle.jigsawPuzzle.Snap(rectTransform);
        JigsawPuzzle.jigsawPuzzle.IsRightPos(rectTransform, target, idx);
        JigsawPuzzle.jigsawPuzzle.CheckAnswer();

        Debug.Log(JigsawPuzzle.jigsawPuzzle.answer[0] + "," 
        + JigsawPuzzle.jigsawPuzzle.answer[1] + ","
        + JigsawPuzzle.jigsawPuzzle.answer[2] + ","
        + JigsawPuzzle.jigsawPuzzle.answer[3] + ",");

    }
}