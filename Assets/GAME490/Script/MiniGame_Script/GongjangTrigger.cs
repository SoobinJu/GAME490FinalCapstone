﻿using UnityEngine;
using UnityEngine.SceneManagement;

public class GongjangTrigger : MonoBehaviour
{
    public float minTimeBeforeTrigger = 60f; // 2분
    public float checkInterval = 5f; // 5초마다 한 번씩 체크
    public float triggerChance = 0.2f; // 20% 확률

    private float timeSinceStart = 0f;
    private float timeSinceLastCheck = 0f;
    private bool triggered = false;

    public AudioClip triggerSound;
    private AudioSource audioSource;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        if (triggered) return;

        timeSinceStart += Time.deltaTime;
        timeSinceLastCheck += Time.deltaTime;

        if (timeSinceStart >= minTimeBeforeTrigger && timeSinceLastCheck >= checkInterval)
        {
            timeSinceLastCheck = 0f;

            if (Random.value < triggerChance)
            {
                TriggerMinigame();
            }
        }
    }

    void TriggerMinigame()
    {
        triggered = true;

        // 효과음 재생
        if (audioSource != null && triggerSound != null)
        {
            audioSource.PlayOneShot(triggerSound);
        }

        // 씬 이름 저장해서 복귀용
        PlayerPrefs.SetString("PreviousScene", SceneManager.GetActiveScene().name);

        // 약간의 delay 후 전환하면 효과음 끝나고 이동
        Invoke("GoToMinigame", 1f);
    }

    void GoToMinigame()
    {
        SceneManager.LoadScene("Minigame");
    }
}
