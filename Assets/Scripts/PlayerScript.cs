using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class PlayerScript : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private GameObject projectile;

    private Vector2 direction;
    private Rigidbody myRigidbody;
    private PlayerBehaviour inputs;
    private Camera mainCam;

    // Start is called before the first frame update
    void Start()
    {
        inputs = new PlayerBehaviour();
        inputs.Enable();
        inputs.Player.Move.performed += OnMovePerformed;
        inputs.Player.Move.canceled += OnMoveCanceled;


        //inputs.Player.Shoot.performed += OnShootPerformed;

        myRigidbody = GetComponent<Rigidbody>();
        mainCam = Camera.main;
    }
    private void FixedUpdate()
    {
        
    }

    private void OnMovePerformed(InputAction.CallbackContext obj)
    {

    }

    private void OnMoveCanceled(InputAction.CallbackContext obj)
    {

    }
}
