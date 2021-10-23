using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PatrolEnemy : MonoBehaviour
{
    private PlayerActions _playerActions;
    
    Vector3 nextPos;
    [SerializeField] private Transform pos1;
    [SerializeField] private Transform pos2;
    [SerializeField] private float enemySpeed;

    float objectOnAngle;
    [SerializeField] float stopMovementAfter = 1.0f;
    [SerializeField] float waitBeforeReload = 2.0f;

    void Start()
    {
        _playerActions = FindObjectOfType<PlayerActions>();
        nextPos = pos1.position;
    }

    void Update()
    {
        if (transform.position == pos1.position)
        {
            nextPos = pos2.position;

            FacingTowardsObject();
        }

        if (transform.position == pos2.position)
        {
            nextPos = pos1.position;

            FacingTowardsObject();
        }

        transform.position = Vector3.MoveTowards(transform.position, nextPos, enemySpeed * Time.deltaTime);        
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawLine(pos1.position, pos2.position);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            //_playerActions.Reset();
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            nextPos = collision.gameObject.transform.position;

            Vector3 directionOfObject = collision.gameObject.transform.position - transform.position;
            objectOnAngle = Mathf.Atan2(directionOfObject.y, directionOfObject.x) * Mathf.Rad2Deg;
            gameObject.GetComponent<Rigidbody2D>().rotation = objectOnAngle - 90.0f;

            StartCoroutine(StoppingEnemyMovement());
            _playerActions.Reset();
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            nextPos = pos1.position;

            FacingTowardsObject();
        }
    }

    private void FacingTowardsObject()
    {
        Vector3 directionOfObject = nextPos - transform.position;
        objectOnAngle = Mathf.Atan2(directionOfObject.y, directionOfObject.x) * Mathf.Rad2Deg;
        gameObject.GetComponent<Rigidbody2D>().rotation = objectOnAngle - 90.0f;
    }

    IEnumerator StoppingEnemyMovement()
    {
        yield return new WaitForSeconds(stopMovementAfter);

        gameObject.GetComponent<PatrolEnemy>().enabled = false;
    }

    IEnumerator SceneReloading()
    {
        yield return new WaitForSeconds(waitBeforeReload);

        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
