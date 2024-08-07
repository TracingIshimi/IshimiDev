using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EscapeRoom : MonoBehaviour
{
    [SerializeField] PuzzleManager testObject; 

    void OnMouseUp() {
        testObject.OnClickJustItem();
    }
}
