using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIDialog : MonoBehaviour
{
    public Text dialogText;
    public Button resetArrow;
    public bool isGameOver = false;
    

    private void Start()
    {
        dialogText.gameObject.SetActive(false);
        resetArrow.gameObject.SetActive(false);
    }

    public void ActivateResetDialog()
    {
        dialogText.gameObject.SetActive(true);
        dialogText.text = "Reset?\n";
        resetArrow.gameObject.SetActive(true);
    }

    public void ActivateGameOver()
    {
        isGameOver = true;
        dialogText.text = "Game\n Over!";
    }


}
