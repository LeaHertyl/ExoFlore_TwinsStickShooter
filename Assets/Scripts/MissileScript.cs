using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissileScript : MonoBehaviour
{
    [SerializeField] private float speed;
    private Rigidbody2D myRigidbody;

    // Start is called before the first frame update
    void Start()
    {
        myRigidbody = GetComponent<Rigidbody2D>();

        var playerGameObject = GameObject.FindWithTag("Player");
        var playerScript = playerGameObject.GetComponent<PlayerScript>();
    }

    private void FixedUpdate()
    {
        myRigidbody.velocity = new Vector2(speed * Time.fixedDeltaTime, 0);
        myRigidbody.velocity = new Vector2(0, speed * Time.fixedDeltaTime);
    }
}
