using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class CloseUp : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] GameObject closeUpModal;
    [SerializeField] GameObject movieModal;

    public void OnPointerClick(PointerEventData eventData) {
        CloseUpModalOn();
    }

    void CloseUpModalOn() {
        closeUpModal.SetActive(true);
        Invoke("MovieModalOn", 1f);
    }

    void MovieModalOn() {
        closeUpModal.SetActive(false);
        movieModal.SetActive(true);
    }

}
