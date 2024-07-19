using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EscapeRoom : MonoBehaviour
{
    public GameObject item;
    private void OnMouseUp() {
        Destroy(item);
    }
}
