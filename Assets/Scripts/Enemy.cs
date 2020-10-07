using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public static int enemyCount;

    [SerializeField] private GameObject _cloudParticlePrefab;

    private void Start()
    {
        enemyCount++;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Enemy enemy = collision.collider.GetComponent<Enemy>();
        if (enemy != null)
        {
            return;
        }

        Bird bird = collision.collider.GetComponent<Bird>();
        bool hitFromAbove = collision.contacts[0].normal.y < -0.5;

        if (bird != null || hitFromAbove)
        {
            Instantiate(_cloudParticlePrefab, transform.position, Quaternion.identity);
            Destroy(gameObject);
            enemyCount--;
            return;
        }
    }
}
