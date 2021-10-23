using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SneezeEnemy : MonoBehaviour
{
    [SerializeField] float sneezeStartTime;
    [SerializeField] float timeBtwSneezeMinValue;
    [SerializeField] float timeBtwSneezeMaxValue;
    [SerializeField] GameObject sneezeGO;
    [SerializeField] Transform sneezePoint;

    public float timeBtwSneeze;

    void Start()
    {

        Invoke("EnemySneeze", sneezeStartTime);
    }

    public void EnemySneeze()
    {
        timeBtwSneeze = Random.Range(timeBtwSneezeMinValue, timeBtwSneezeMaxValue);

        Instantiate(sneezeGO, sneezePoint);

        Invoke("EnemySneeze", timeBtwSneeze);
    }
}
