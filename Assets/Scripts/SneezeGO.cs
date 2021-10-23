using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SneezeGO : MonoBehaviour
{
    Rigidbody2D sneezeGO_RB;

    [SerializeField] float sneezeGOSpeed = 1.0f;
    [SerializeField] float sneezeGODestroyTime = 3.0f;

    [SerializeField] float waitBeforeReload = 0.5f;

    void Start()
    {
        sneezeGO_RB = GetComponent<Rigidbody2D>();

        sneezeGO_RB.velocity = transform.right * sneezeGOSpeed;

        Destroy(gameObject, sneezeGODestroyTime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            StartCoroutine(SceneReloading());
        }
    }

    IEnumerator SceneReloading()
    {
        yield return new WaitForSeconds(waitBeforeReload);

        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
