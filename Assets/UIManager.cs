using UnityEngine;
using TMPro; // Use the TextMeshPro namespace

public class UIManager : MonoBehaviour
{
    public Player player; // Reference to the Player script
    public TextMeshProUGUI maskKillCountText; // Reference to the TextMeshPro UI element

    void Update()
    {
        // Update the TextMeshPro element with the current kill count
        maskKillCountText.text = "Masks Killed: " + player.MasksKilled.ToString();
    }
}
