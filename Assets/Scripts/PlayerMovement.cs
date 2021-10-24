using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Experimental.Rendering.LWRP;
using Light2D = UnityEngine.Experimental.Rendering.Universal.Light2D;

public class PlayerMovement : MonoBehaviour
{
    private Dice _dice;
    private PlayerActions _playerActions;
    
    private Rigidbody2D _rigidbody2D;

    [SerializeField] private Light2D _globalLight;
    [SerializeField] private GameObject playerLight;

    [SerializeField] private TextMeshProUGUI objectiveText;
    
    [SerializeField] private float moveSpeed;

    [SerializeField] private float superSpeed;

    // Start is called before the first frame update
    void Start()
    {
        _dice = FindObjectOfType<Dice>();
        _playerActions = FindObjectOfType<PlayerActions>();
        _rigidbody2D = GetComponent<Rigidbody2D>();
        Time.timeScale = 1;
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
                    objectiveText.text = "eSCAPE THE iNFECTED AREA (DEFAULT CONTROLS MODE).";
                    break;
                case 1:
                    InvertedMovement();
                    objectiveText.text = "eSCAPE THE iNFECTED AREA (INVERTED CONTROLS MODE).";
                    break;
                case 2:
                    SuperSpeed();
                    objectiveText.text = "eSCAPE THE iNFECTED AREA (2X MODE).";
                    break;
                case 3:
                    NormalMovement();
                    objectiveText.text = "eSCAPE THE iNFECTED AREA (LIGHTS OUT MODE).";
                    _globalLight.intensity = 0.05f;
                    playerLight.SetActive(true);
                    break;
                // case 4:
                //     NormalMovement();
                //     objectiveText.text = "sURVIVE THROUGH THE DECONTAMINATION, FIND INFECTED GOOPS TO SURVIVE (INFECTED MODE).";
                //     _playerActions.InfectedMode();
                //     break;
                case 4:
                    NormalMovement();
                    objectiveText.text = "rEPLENISH OXYGEN WITH OXY PICKUP AND ESCAPE (LOW ON OXYGEN MODE).";
                    _playerActions.OxygenMode();
                    break;
                
                
            }
        }
        
        if (Input.GetKeyDown(KeyCode.Backslash))
        {
            _playerActions.Reset();
        }

        if (Input.GetKeyDown(KeyCode.Slash))
        {
            SceneManager.LoadScene(0);
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
            _rigidbody2D.velocity = movement * (moveSpeed);
            Time.timeScale = 2;
        }
        
        
        
}
