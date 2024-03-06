using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using Cinemachine;

public class CutsceneTrigger : MonoBehaviour
{
    public GameObject dialoguePrefab;
    public PlayableDirector timeline;
    private bool hasStarted = false;
    public GameObject mortySprite;
    public GameObject playerSprite;
    public GameObject actualPlayer;
    public GameObject actualMorty;
    public GameObject playerCamera;
    public AudioClip bossMusic;

    private AudioSource audioSource;

    // Use this for initialization
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        audioSource.clip = bossMusic;
        timeline = GetComponent<PlayableDirector>();
        // Subscribe to the stopped event to know when the timeline finishes playing
        timeline.stopped += OnTimelineStopped;
    }

    void OnTimelineStopped(PlayableDirector director)
    {
        Debug.Log("Timeline stopped");
        // Add any additional logic you want to perform when the timeline stops
        // For example, you can stop other actions or trigger another event.
        // ...

        // Stop the timeline
        mortySprite.GetComponent<SpriteRenderer>().enabled = false;
        playerSprite.GetComponent<SpriteRenderer>().enabled = false;
        actualPlayer.transform.position = new Vector3(7.83f, -2.5f, 0f);
        actualPlayer.SetActive(true);
        actualMorty.GetComponent<MaskSpawner>().maxMasks = 20;
        playerCamera.GetComponent<CinemachineVirtualCamera>().Follow = actualPlayer.transform;

        timeline.Stop();

        hasStarted = false;
    }

    void OnTriggerEnter2D(Collider2D c)
    {
        Debug.Log("Player entered trigger. Playing timeline.");
        if (c.gameObject.CompareTag("Player") && !hasStarted)
        {
            // Play the timeline
            Instantiate(dialoguePrefab, transform.position, transform.rotation);
            timeline.Play();
            playerSprite.GetComponent<SpriteRenderer>().enabled = true;
            actualPlayer.SetActive(false);
            playerCamera.GetComponent<CinemachineVirtualCamera>().Follow = playerSprite.transform;


            hasStarted = true;
        }
    }

    void Update()
    {
        // Check if the timeline has started and is not playing
        if (hasStarted && timeline.state != PlayState.Playing)
        {
            // The timeline is paused or stopped, handle as needed
            OnTimelineStopped(timeline);
        }
    }

    void OnDisable()
    {
        if (timeline != null)
        {
            timeline.stopped -= OnTimelineStopped;
        }
    }
}
