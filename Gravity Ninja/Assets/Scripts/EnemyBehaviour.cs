using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehaviour : MonoBehaviour
{
    [SerializeField] float enemyMaxHealth = 10.0f;
    float enemyHealth;
    [SerializeField] float enemyDamage = 1.0f;

    void Start()
    {
        enemyHealth = enemyMaxHealth;
    }

    public void TakeDamage(float damage)
    {
        enemyHealth -= damage;

        Debug.Log("Enemy health: " + enemyHealth);

        if (enemyHealth <= 0)
        {
            Destroy(gameObject);
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.TryGetComponent<PlayerBehaviour>(out PlayerBehaviour playerBehaviour))
        {
            playerBehaviour.TakeDamage(enemyDamage);
            Destroy(this.gameObject);
        }


    }

}
