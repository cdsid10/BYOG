using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class oxygenSpawner : MonoBehaviour
{
    /* 
    give player the player tag. set the oxygen thingy as spawnObject and give a time.
    */
    public GameObject spawnObject;
    public float waitTime = 10.0f;
    bool canSpawn = false;

    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("its woking");
        if (other.gameObject.tag == "Player")
        {
            spawnObject.SetActive(false);
            canSpawn = true;
            StartCoroutine(Spawn());
        }

    }



    IEnumerator Spawn()
    {
        while (canSpawn)
        {
            yield return new WaitForSeconds(waitTime);
            spawnObject.SetActive(true);
            canSpawn = false;
        }
    }
}
