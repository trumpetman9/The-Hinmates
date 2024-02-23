using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Yarn.Unity;

public class transition : MonoBehaviour
{
    public DialogueRunner dialogueRunner; // Reference to your DialogueRunner

    void Start()
    {
        // Subscribe to the DialogueComplete event
        dialogueRunner.onDialogueComplete.AddListener(OnDialogueComplete);
    }

    // This method is called when the dialogue is completed
    private void OnDialogueComplete()
    {
        // Load the Exposition scene
        SceneManager.LoadScene("Exposition");
    }
}
