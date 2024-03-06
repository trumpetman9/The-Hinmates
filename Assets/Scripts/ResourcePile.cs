using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourcePile : MonoBehaviour
{
    private bool isEmpty;
    public int healAmount;
    public Sprite newSprite;
    public SpriteRenderer spriteRenderer;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Player player = collision.GetComponent<Player>();
        
        if(player != null && !isEmpty)
        {
            spriteRenderer.sprite = newSprite;
            isEmpty = true;
            player.HealDamage(healAmount);
        }
    }
}
