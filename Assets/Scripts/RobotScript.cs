using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RobotScript : MonoBehaviour
{
    [SerializeField] private float speed;
    private Rigidbody2D myRigidbody;

    public Transform playerGameObject;
    private Vector2 Target;

    private Camera mainCam;


    // Start is called before the first frame update
    void Start()
    {
        myRigidbody = GetComponent<Rigidbody2D>();

        playerGameObject = GameObject.FindWithTag("Player").transform;

        //var playerScript = playerGameObject.GetComponent<PlayerScript>();

        mainCam = Camera.main;
    }

    private void FixedUpdate()
    {
        Target = new Vector2(playerGameObject.position.x, playerGameObject.position.y);
        myRigidbody.velocity = Vector2.MoveTowards(Target, transform.position, speed * Time.fixedDeltaTime);

        ClampPosition();
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
