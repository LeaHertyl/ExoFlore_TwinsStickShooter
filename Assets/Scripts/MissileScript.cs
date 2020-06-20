using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissileScript : MonoBehaviour
{
    [SerializeField] private float speed;
    private Rigidbody2D myRigidbody;

    private Vector2 VecteurMove;


    // Start is called before the first frame update
    void Start()
    {
        myRigidbody = GetComponent<Rigidbody2D>();

        var playerGameObject = GameObject.FindWithTag("Player");
        var playerScript = playerGameObject.GetComponent<PlayerScript>();
        VecteurMove = playerScript.VecteurVisee;
    }

    private void FixedUpdate()
    {
        //myRigidbody.velocity = new Vector2(speed * Time.fixedDeltaTime, 0);
        //myRigidbody.velocity = new Vector2(0, speed * Time.fixedDeltaTime);
        myRigidbody.velocity = new Vector2(VecteurMove.x, VecteurMove.y);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Si on rentre dans un ennemi, on lance la fonction Die()
        if (collision.gameObject.CompareTag("Enemy"))
        {
            Debug.Log("collision");
            Die();
        }
    }

    private void Die()
    {
        Destroy(gameObject);
    }
}
