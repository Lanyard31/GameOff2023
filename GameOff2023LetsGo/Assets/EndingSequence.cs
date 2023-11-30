using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndingSequence : MonoBehaviour
{
    [SerializeField] GameObject bossBarUI;
    [SerializeField] GameObject bossName;
    [SerializeField] float delayBeforeLoadingNextScene = 2f;
    [SerializeField] AudioSource audioSource;

    bool loaded;

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            RevealBossBarUI();
        }
    }


    void RevealBossBarUI()
    {
        bossBarUI.SetActive(true);
        bossName.SetActive(true);
    }

    public void OnEnemyKilled()
    {
        if (loaded) return;
        audioSource.Play();
        StartCoroutine(LoadNextSceneAfterDelay());
        loaded = true;
    }

    IEnumerator LoadNextSceneAfterDelay()
    {
        yield return new WaitForSeconds(delayBeforeLoadingNextScene);
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex + 1);
    }
}

