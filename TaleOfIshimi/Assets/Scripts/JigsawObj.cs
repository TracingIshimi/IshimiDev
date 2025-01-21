// using System.Collections;
// using System.Collections.Generic;
// using UnityEngine;
// using UnityEngine.EventSystems;

// public class JigsawObj : MonoBehaviour, IEndDragHandler, IDragHandler
// {
//     public Transform target;
	
//     void IDragHandler.OnDrag(PointerEventData eventData)
//     {
//     	float distance = Camera.main.WorldToScreenPoint(transform.position).z;

//         Vector3 mousePos = new Vector3(Input.mousePosition.x, Input.mousePosition.y, distance);
//         Vector3 objPos = Camera.main.ScreenToWorldPoint(mousePos);

//         transform.position = objPos;
//     }

//     void IEndDragHandler.OnEndDrag(PointerEventData eventData) {
//         Snap();
//     }


//     void Snap() {
//         if(Vector3.Distance(target.position, transform.position) < 1f) {
//             transform.position = new Vector3(target.position.x, target.position.y);
//         }
//     }

// }


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

    // private void Snap()
    // {
    //     // Snap 대상과의 거리 계산
    //     if (Vector2.Distance(target.anchoredPosition, rectTransform.anchoredPosition) < 50f) // 거리 기준 조정 가능
    //     {
    //         rectTransform.anchoredPosition = target.anchoredPosition;
    //     }
    // }
}