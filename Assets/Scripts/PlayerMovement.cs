using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Experimental.Rendering.LWRP;
using Light2D = UnityEngine.Experimental.Rendering.Universal.Light2D;

public class PlayerMovement : MonoBehaviour
{
    private Dice _dice;
    
    private Rigidbody2D _rigidbody2D;

    [SerializeField] private Light2D _globalLight;
    
    [SerializeField] private float moveSpeed;

    [SerializeField] private float superSpeed;

    // Start is called before the first frame update
    void Start()
    {
        _dice = FindObjectOfType<Dice>();
        _rigidbody2D = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (_dice.canPlayerMove)
        {
            switch (_dice.randomDiceSide)
            {
                case 0:
                    NormalMovement();
                    break;
                case 1:
                    InvertedMovement();
                    break;
                case 2:
                    SuperSpeed();
                    break;
                case 3:
                    NormalMovement();
                    _globalLight.intensity = 0.1f;
                    break;
            }
        }
        
        if (Input.GetKeyDown(KeyCode.R))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
        
    }
    
        //Normal Movement
        void NormalMovement()
        {
            var moveX = Input.GetAxis("Horizontal");
            var moveY = Input.GetAxis("Vertical");
            var movement = new Vector2(moveX, moveY);
            if (movement.sqrMagnitude > 1)
            {
                movement.Normalize();
            }
            _rigidbody2D.velocity = movement * (moveSpeed);
        }
        
        void InvertedMovement()
        {
            var moveX = Input.GetAxis("Horizontal");
            var moveY = Input.GetAxis("Vertical");
            var movement = new Vector2(-moveX, -moveY);
            if (movement.sqrMagnitude > 1)
            {
                movement.Normalize();
            }
            _rigidbody2D.velocity = movement * (moveSpeed);
        }
        
        void SuperSpeed()
        {
            var moveX = Input.GetAxis("Horizontal");
            var moveY = Input.GetAxis("Vertical");
            var movement = new Vector2(moveX, moveY);
            if (movement.sqrMagnitude > 1)
            {
                movement.Normalize();
            }
            _rigidbody2D.velocity = movement * (superSpeed);
        }
        
        
        
}
