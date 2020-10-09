using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelController : MonoBehaviour
{
    private int sceneIdx;
    private static int MAX_LEVELS;
    private bool hasNextLevel = false;
    private bool isUIActive = false;
    public UIDialog uiDialog;
    public Animator sceneTransition, buttonRotation;
    public float transitionTime, countdown = 3;

    private void Awake()
    {
        Enemy.enemyCount = 0;
    }

    private void Start()
    {
        MAX_LEVELS = SceneManager.sceneCountInBuildSettings;
        sceneIdx = SceneManager.GetActiveScene().buildIndex + 1;
        hasNextLevel = sceneIdx < MAX_LEVELS;
        uiDialog.resetArrow.onClick.AddListener(() => StartCoroutine(ResetLevel(sceneIdx - 1)));
    }

    void Update()
    {
        if(Enemy.enemyCount < 1)
        {
            if(!isUIActive)
            {
                uiDialog.dialogText.gameObject.SetActive(true);
                isUIActive = true;
            }
            
            if (hasNextLevel)
            {
                countdown -= Time.deltaTime;
                StartCoroutine(LoadLevel(sceneIdx));
                uiDialog.dialogText.text = "Great Job!\n" + countdown.ToString("0");
            }
            else
            { uiDialog.ActivateGameOver(); }
        }
    }

    IEnumerator LoadLevel(int levelIdx)
    {
        yield return new WaitForSeconds(transitionTime+1);
        sceneTransition.SetTrigger("Start");
        yield return new WaitForSeconds(transitionTime);
        SceneManager.LoadScene(levelIdx);
    }

    IEnumerator ResetLevel(int currentLevelIdx)
    {
        buttonRotation.SetTrigger("Clicked");
        yield return new WaitForSeconds(0.5f);
        sceneTransition.SetTrigger("Start");
        yield return new WaitForSeconds(1);
        SceneManager.LoadScene(currentLevelIdx);
    }
}
