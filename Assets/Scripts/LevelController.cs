using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelController : MonoBehaviour
{
    private int sceneIdx;
    private static int MAX_LEVELS;
    private bool hasNextLevel = false;
    public UIDialog uiDialog;
    public Animator sceneTransition;
    public float transitionTime, countdown = 3;

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
            Debug.Log("All Monsters Squished!");
            uiDialog.dialogText.gameObject.SetActive(true);
            if (hasNextLevel)
            {
                countdown -= Time.deltaTime;
                StartCoroutine(LoadLevel(sceneIdx));
                uiDialog.dialogText.text = "Great Job!\n" + countdown.ToString("0");
            }
            else
            { uiDialog.dialogText.text = "Game\n Over!"; }
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
        sceneTransition.SetTrigger("Start");
        yield return new WaitForSeconds(1);
        SceneManager.LoadScene(currentLevelIdx);
    }
}
