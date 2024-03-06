using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ForceField : MonoBehaviour
{
    public GameObject player;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = player.transform.position;

        Vector3 newScale = new Vector3(6, 6, -0.6f);
        transform.localScale = Vector3.Lerp(transform.localScale, newScale, 5 * Time.deltaTime);
    }
}
