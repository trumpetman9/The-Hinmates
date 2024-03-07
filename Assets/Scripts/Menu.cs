using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    // Start is called before the first frame update

    public AudioSource soundPlayer;
    public AudioSource mainTheme;

    public IEnumerator PlaySounds()
    {
        // Play a sound here
        soundPlayer.Play();
        StartCoroutine(FadeOut(mainTheme, 3f));
        yield return new WaitForSeconds(3);
        SceneManager.LoadScene("DriverScene");
    }

    public IEnumerator FadeOut(AudioSource audioSource, float FadeTime)
    {
        float startVolume = audioSource.volume;

        while (audioSource.volume > 0)
        {
            audioSource.volume -= startVolume * Time.deltaTime / FadeTime;

            yield return null;
        }

        audioSource.Stop();
        audioSource.volume = startVolume;
    }

    void Start()
    {
        mainTheme.Play();
    }

    public void StartGame()
    {
        StartCoroutine(PlaySounds());
    }

    public void QuitGame()
    {
        Application.Quit();

    }
}