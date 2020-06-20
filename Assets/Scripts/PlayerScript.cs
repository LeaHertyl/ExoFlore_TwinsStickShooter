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

    [SerializeField] private GameObject LoseGameOverCanvas;

    private Vector2 LeftStickDirection;
    private Rigidbody2D myRigidbody;
    private PlayerBehaviour inputs;

    private Vector2 PlayerPosition;
    private Vector2 MousePosition;
    public Vector2 VecteurVisee;
    private float AngleVise;

    private float aim_angle;

    private Transform magique;

    private Vector3 point;
    Event currentEvent;
    //private Vector2 mousepos;

    private Camera mainCam;


    //public UnityEvent OnDeath;

    // Start is called before the first frame update
    void Start()
    {
        inputs = new PlayerBehaviour();
        inputs.Enable();
        inputs.Player.Move.performed += OnMovePerformed;
        inputs.Player.Move.canceled += OnMoveCanceled;

        inputs.Player.Shoot.performed += OnShootPerformed;

        inputs.Player.Turn.performed += OnTurnPerformed;
        inputs.Player.Turn.canceled += OnTurnCanceled;

        myRigidbody = GetComponent<Rigidbody2D>();
        mainCam = Camera.main;

        aim_angle = 0.0f;
    }
    private void FixedUpdate()
    {

        var direction = new Vector2(LeftStickDirection.x, LeftStickDirection.y);
        myRigidbody.velocity = direction * (speed * Time.fixedDeltaTime);

        PlayerPosition = new Vector2(transform.position.x, transform.position.y);

        VecteurVisee = (MousePosition - PlayerPosition).normalized;
        Debug.Log(VecteurVisee + "");
        
        aim_angle = Mathf.Atan2(VecteurVisee.y, VecteurVisee.x) * Mathf.Rad2Deg;
        gameObject.transform.rotation = Quaternion.AngleAxis(aim_angle, Vector3.forward);

        ClampPosition();
    }

    private void OnMovePerformed(InputAction.CallbackContext obj)
    {
        LeftStickDirection = obj.ReadValue<Vector2>();
        //Debug.Log(LeftStickDirection + ""); //ca fonctionne
    }

    private void OnMoveCanceled(InputAction.CallbackContext obj)
    {
        LeftStickDirection = Vector2.zero;
    }

    private void OnShootPerformed(InputAction.CallbackContext obj)
    {
        Instantiate(projectile, transform.position, Quaternion.identity);
    }

    private void OnTurnPerformed(InputAction.CallbackContext obj)
    {
        MousePosition = obj.ReadValue<Vector2>();
        //Debug.Log(MousePosition + ""); //ca fonctionne
        MousePosition = mainCam.ScreenToWorldPoint(MousePosition);
    }

    private void OnTurnCanceled(InputAction.CallbackContext obj)
    {

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

    private void OnCollisionEnter2D(Collision2D other)
    {
        // Si on rentre dans un ennemi, on lance la fonction Die()
        if (other.gameObject.CompareTag("Enemy"))
        {
            //Debug.Log("collision");
            Die();
        }
    }

    private void Die()
    {
        Destroy(gameObject);
        Instantiate(LoseGameOverCanvas);
    }
}
