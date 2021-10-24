using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CornerEnemy : MonoBehaviour
{
    private PlayerActions _playerActions;
    
    bool isPlayerInRange;

    [SerializeField] float minAngle;
    [SerializeField] float maxAngle;

    private float rotationZ;
    [SerializeField] float rotationSpeed;
    [SerializeField] bool clockwiseRotation;

    float objectOnAngle;
    [SerializeField] float waitBeforeReload = 5.0f;

    private void Start()
    {
        _playerActions = FindObjectOfType<PlayerActions>();
    }

    void Update()
    {
        if (!isPlayerInRange)
        {
            if (rotationZ <= minAngle)
            {
                clockwiseRotation = false;
            }
            if (rotationZ >= maxAngle)
            {
                clockwiseRotation = true;
            }

            if (!clockwiseRotation)
            {
                rotationZ += Time.deltaTime * rotationSpeed;
            }
            else
            {
                rotationZ += -Time.deltaTime * rotationSpeed;
            }

            transform.rotation = Quaternion.Euler(0, 0, rotationZ);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            _playerActions.Reset();
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            isPlayerInRange = true;

            Vector3 directionOfObject = collision.gameObject.transform.position - transform.position;
            objectOnAngle = Mathf.Atan2(directionOfObject.y, directionOfObject.x) * Mathf.Rad2Deg;
            gameObject.GetComponent<Rigidbody2D>().rotation = objectOnAngle - 90.0f;

            
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            isPlayerInRange = false;
        }
    }

    IEnumerator SceneReloading()
    {
        yield return new WaitForSeconds(waitBeforeReload);

        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
