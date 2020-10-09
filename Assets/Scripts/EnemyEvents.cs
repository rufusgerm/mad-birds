using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyEvents : MonoBehaviour
{
    public static EnemyEvents instance;

    private void Awake()
    {
        instance = this;
    }

    public event Action OnEnemyCreated;
    public void EnemyCreated() => OnEnemyCreated?.Invoke();

    public event Action OnEnemyDestroyed;
    public void EnemyDestroyed() => OnEnemyDestroyed?.Invoke();
}
