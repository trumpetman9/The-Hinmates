using UnityEngine;
using UnityEngine.UI; // Include UI namespace if you're using UI elements

public class InteractPopup : MonoBehaviour
{
    public GameObject popupText; // Assign your Text or TextMeshPro element in the inspector
    private bool isPlayerInsideTrigger = false; // Flag to track player's presence inside the trigger
    public Yarn.Unity.DialogueRunner dialogueRunner;

    private void Start()
    {
        Debug.Log("start");
        popupText.SetActive(false); // Ensure the popup is hidden at start
    }

    private void Update()
    {
        // Check if player is inside trigger and 'F' key is pressed
        if (isPlayerInsideTrigger && Input.GetKeyDown(KeyCode.F))
        {
            // Perform your interaction here, e.g., running the dialogue system
            Debug.Log("Dialogue system activated");
            StartDialogue("Level1_1Script");
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player")) // Check if the collider is tagged as Player
        {
            Debug.Log("On trigger enter 2d is true");
            isPlayerInsideTrigger = true; // Set flag to true
            popupText.SetActive(true); // Show the popup
        }
    }

    void StartDialogue(string nodeName)
    {
        if (dialogueRunner != null && !dialogueRunner.IsDialogueRunning)
        {
            dialogueRunner.StartDialogue(nodeName);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player")) // Check if the collider is tagged as Player
        {
            isPlayerInsideTrigger = false; // Set flag to false
            popupText.SetActive(false); // Hide the popup
        }
    }
}


