using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class ProjectileBehaviour : MonoBehaviour
{
    [SerializeField] float projectileDamage = 5.0f;

    SpriteRenderer spriteRenderer;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.TryGetComponent<EnemyBehaviour>(out EnemyBehaviour enemyBehaviour))
        {
            enemyBehaviour.TakeDamage(projectileDamage);
        }
        Destroy(spriteRenderer);
        Destroy(this.gameObject);
    }

}
