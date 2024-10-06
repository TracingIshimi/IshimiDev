using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PuzzleRoom : MonoBehaviour
{
    [SerializeField] GameObject hintPanel;
    public Image hintImg;
    Sprite[] sprites = new Sprite[2];

    private void Start() {
        sprites[0] = Resources.Load<Sprite>("TestImage/Hint1") as Sprite;
        sprites[1] = Resources.Load<Sprite>("TestImage/Hint2") as Sprite;
    }

    void OnMouseUp() {
        HintPanelOn();
        SpriteChange();
    }

    public void SpriteChange() {
        hintImg.sprite = sprites[1];
    }

    public void HintPanelOn() {
        hintPanel.SetActive(true);
    }

    public void HintPanelOff() {
        hintPanel.SetActive(false);
    }
}
