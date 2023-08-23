using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class ProjectileBehaviour : MonoBehaviour
{
    SpriteRenderer spriteRenderer;
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Destroy(spriteRenderer);
        Destroy(this.gameObject);
    }
}
