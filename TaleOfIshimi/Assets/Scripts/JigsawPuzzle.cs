using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class JigsawPuzzle : MonoBehaviour, IEndDragHandler, IDragHandler
{
    public Transform target;
	
    void IDragHandler.OnDrag(PointerEventData eventData)
    {
    	float distance = Camera.main.WorldToScreenPoint(transform.position).z;

        Vector3 mousePos = new Vector3(Input.mousePosition.x, Input.mousePosition.y, distance);
        Vector3 objPos = Camera.main.ScreenToWorldPoint(mousePos);

        transform.position = objPos;
    }

    void IEndDragHandler.OnEndDrag(PointerEventData eventData) {
        Snap();
    }


    void Snap() {
        if(Vector3.Distance(target.position, transform.position) < 1f) {
            transform.position = new Vector3(target.position.x, target.position.y);
        }
    }

}
