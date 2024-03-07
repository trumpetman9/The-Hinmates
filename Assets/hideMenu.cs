using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class hideMenu : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject ourStartMenu;
    public float time;

    void Start()
    {
        StartCoroutine(unhide());
    }

    public IEnumerator unhide()
    {
        Debug.Log("Waiting for 7 seconds...");
        yield return new WaitForSeconds(time);

        Debug.Log("Unhiding the Start Menu!");
        ourStartMenu.SetActive(true);
    }
}
