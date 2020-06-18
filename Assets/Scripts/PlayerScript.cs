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
    private Rigidbody2D myRigidbody;
    private PlayerBehaviour inputs;


    // Start is called before the first frame update
    void Start()
    {
        inputs = new PlayerBehaviour();
        inputs.Enable();
        inputs.Player.Move.performed += OnMovePerformed;
        inputs.Player.Move.canceled += OnMoveCanceled;


        //inputs.Player.Shoot.performed += OnShootPerformed;

        myRigidbody = GetComponent<Rigidbody2D>();

    }
    private void FixedUpdate()
    {
        myRigidbody.velocity = direction * (speed * Time.fixedDeltaTime);
    }

    private void OnMovePerformed(InputAction.CallbackContext obj)
    {
        direction = obj.ReadValue<Vector2>();
    }

    private void OnMoveCanceled(InputAction.CallbackContext obj)
    {
        direction = Vector2.zero;
    }

}
