using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class PlayerShooting : MonoBehaviour
{
    [SerializeField] GameObject projectile;
    [SerializeField] Camera cameraObject;

    [SerializeField] float projectileSpeed = 1f;
    [SerializeField] float shootCooldown = 1f;

    float lastShot = 0f;

    Vector2 target;

    void Update()
    {
        LookAtCursor();
    }

    void LookAtCursor()
    {
        target = cameraObject.ScreenToWorldPoint(new Vector2(Input.mousePosition.x, Input.mousePosition.y));

        Vector2 difference = target - (Vector2)transform.position;
        transform.rotation = Quaternion.LookRotation(difference);


        if (Input.GetMouseButtonDown(0))
        {
            difference.Normalize();
            fireProjectile(difference);
        }
    }

    void fireProjectile(Vector2 direction)
    {
        if (Time.time - lastShot < shootCooldown)
        {
            return;
        }
        GameObject p = Instantiate(projectile) as GameObject;
        p.transform.position = transform.position;
        p.GetComponent<Rigidbody2D>().AddForce(direction * projectileSpeed, ForceMode2D.Impulse);

        lastShot = Time.time;
    }
}
