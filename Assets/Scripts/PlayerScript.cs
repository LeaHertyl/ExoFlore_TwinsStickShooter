using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class PlayerScript : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private float MaxSpeed;
    [SerializeField] private GameObject projectile;

    private Vector2 StickDirection;
    private Rigidbody2D myRigidbody;
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

        myRigidbody = GetComponent<Rigidbody2D>();
        mainCam = Camera.main;

    }
    private void FixedUpdate()
    {
        
        var direction = new Vector2(StickDirection.x, StickDirection.y);

        myRigidbody.velocity = direction * (speed * Time.fixedDeltaTime);

        ClampPosition();
    }

    private void OnMovePerformed(InputAction.CallbackContext obj)
    {
        StickDirection = obj.ReadValue<Vector2>();
        Debug.Log(StickDirection + ""); //ca fonctionne
    }

    private void OnMoveCanceled(InputAction.CallbackContext obj)
    {
        StickDirection = Vector2.zero;
    }

    private void ClampPosition()
    {
        // On récupère la distance entre la camera et le joueur
        var zDistance = Mathf.Abs(transform.position.z - mainCam.transform.position.z);
        // On assigne à cette variable la position dans le monde du coin inférieur gauche de la caméra
        var leftBottomCorner = mainCam.ScreenToWorldPoint(new Vector3
        {
            x = 0,
            y = 0,
            z = zDistance
        });
        // On assigne à cette variable la position dans le monde du coin supérieur droit de la caméra
        var rightTopCorner = mainCam.ScreenToWorldPoint(new Vector3
        {
            x = mainCam.pixelWidth,
            y = mainCam.pixelHeight,
            z = zDistance
        });
        var oldPosition = myRigidbody.position;
        // On vient limiter la position en x et y pour qu’elle soit dans les limites du champ de vision de la caméra
        var newPosition = new Vector3
        {
            x = Mathf.Clamp(oldPosition.x, leftBottomCorner.x, rightTopCorner.x),
            y = Mathf.Clamp(oldPosition.y, leftBottomCorner.y, rightTopCorner.y),
            //z = oldPosition.z
        };
        myRigidbody.position = newPosition;
    }
}
