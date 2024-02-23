using UnityEngine;
using TMPro; // Import the TextMeshPro namespace
using System.Collections;
using UnityEngine.SceneManagement;

public class TypewriterEffect : MonoBehaviour
{
    public TMP_Text textDisplay; // Change the type to TMP_Text
    public string fullText;
    public float typingSpeed = 0.05f;

    private void Start()
    {
        StartCoroutine(TypeText());
    }

    IEnumerator TypeText()
    {
        Debug.Log("typewriter working");
        textDisplay.text = ""; // Initialize text to be empty
        foreach (char letter in fullText.ToCharArray())
        {
            textDisplay.text += letter;
            yield return new WaitForSeconds(typingSpeed);
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            SceneManager.LoadScene("Tutorial"); // Load the scene named "demo"
        }
    }
}
