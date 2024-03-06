using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    // Start is called before the first frame update

    public AudioSource soundPlayer;

    IEnumerator PlaySounds()
    {
        // Play a sound here
        soundPlayer.Play();
        yield return new WaitForSeconds(2);
        SceneManager.LoadScene("DriverScene");
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