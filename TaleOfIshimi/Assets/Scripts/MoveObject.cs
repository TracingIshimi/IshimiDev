using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using System;

public class MoveObject : MonoBehaviour, IPointerClickHandler
{
    public Transform target;
    public Transform origin;


    void moveObject() {
        if(Vector3.Distance(target.position, transform.position) < 0.01f) {
            transform.position = new Vector3(origin.position.x, origin.position.y);
        }
        else {
            transform.position = new Vector3(target.position.x, target.position.y);
        }
    }

    public void OnPointerClick(PointerEventData eventData) {
        moveObject();
    }
}
