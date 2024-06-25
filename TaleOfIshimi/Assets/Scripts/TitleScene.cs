using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class TitleScene : MonoBehaviour
{
    [SerializeField] GameObject modalPanel;

    public void OnClickStartBtn() {
        SceneManager.LoadScene("1_IdleRoom");
    }

    public void OnClickContinueBtn() {
        modalPanel.SetActive(true);
    }

    public void OnClickNoBtn() {
        modalPanel.SetActive(false);
    }
}
