using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelController : MonoBehaviour
{
    private int sceneIdx;
    private static int MAX_LEVELS;
    private bool hasNextLevel = false;
    public SuccessText centerText;
    public Animator sceneTransition;
    public float transitionTime, countdown = 3;

    private void Start()
    {
        MAX_LEVELS = SceneManager.sceneCountInBuildSettings;
        sceneIdx = SceneManager.GetActiveScene().buildIndex + 1;
        hasNextLevel = sceneIdx < MAX_LEVELS;
    }

    void Update()
    {
        if(Enemy.enemyCount < 1)
        {
            Debug.Log("All Dead!");
            centerText.gameObject.SetActive(true);
            if (hasNextLevel)
            {
                countdown -= Time.deltaTime;
                StartCoroutine(LoadLevel(sceneIdx));
                centerText.successText.text = "Great Job!\n" + countdown.ToString("0");
            }
            else
            { centerText.successText.text = "Game\n Over!"; }
        }
    }

    IEnumerator LoadLevel(int levelIdx)
    {
        yield return new WaitForSeconds(transitionTime+1);
        sceneTransition.SetTrigger("Start");
        yield return new WaitForSeconds(transitionTime);
        SceneManager.LoadScene(levelIdx);
    }
}
