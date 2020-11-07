using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour

{
    /// Zorgt voor de snelheid van de cube
    public float speed = 10; 
    /// Zorgt voor de zwaardekracht binnen het spel
    public float gravity = 10;
    /// De maximale draai snelheid.
    public float maxVelocityChange = 10;
    /// Dit is hoe hoog de cube kan springen
    public float jumpHeight = 2;

    /// Gezondheid van speler
    public int health;
    /// Kijken of de cube op de grond is of niet
    private bool grounded;
    /// Kijken of de cube leeft of niet
    private bool dead;
    private Transform playerTransform;
    /// voor de Collisions heb je een rigidbody nodig
    private Rigidbody _rigidbody;


    /// <summary>
    /// Hier willen we de speler variablen geven. Sinds de variablen private zijn wordt het hier gedaan. Hier heb ik kenmerken van _rigidbody met code veranderd. Dit kan ook in Unity UI.
    /// </summary>
    void Start()
    {
        playerTransform = GetComponent<Transform>();
        _rigidbody = GetComponent<Rigidbody>();
        _rigidbody.useGravity = false;
        _rigidbody.freezeRotation = true;
    }

    /// <summary>
    /// Hierin geven we rotaties aan. En hoe de speler beweegt en springt. 
    /// </summary>
    void FixedUpdate()
    {
        /// Zo beweegt de speler links en rechts. 
        playerTransform.Rotate(0, Input.GetAxis("Horizontal"), 0);

        /// We bewegen niet op de x en de y dus de laatste wordt ingevuld door de Vertical. Dit betekent dat we vooruit en achteruit kunnen.
        Vector3 targetVelocity = new Vector3(0, 0, Input.GetAxis("Vertical"));
        targetVelocity = playerTransform.TransformDirection(targetVelocity);
        targetVelocity = targetVelocity * speed;

        Vector3 velocity = _rigidbody.velocity;
        Vector3 velocityChange = targetVelocity - velocity;
        velocityChange.x = Mathf.Clamp(velocityChange.x, -maxVelocityChange, maxVelocityChange);
        velocityChange.z = Mathf.Clamp(velocityChange.z, -maxVelocityChange, maxVelocityChange);
        velocityChange.y = 0;
        _rigidbody.AddForce(velocityChange, ForceMode.VelocityChange);

        if (Input.GetButton("Jump"))
        {
            _rigidbody.velocity = new Vector3(velocity.x, CalculateJump(), velocity.z);
        }

        _rigidbody.AddForce(new Vector3(0, -gravity * _rigidbody.mass, 0));
        grounded = false;

    }

    /// <summary>
    /// Hier wordt berekend hoe hoog de cube springt. Dit doet hij door de hoogte x de zwaartekracht te doen. 
    /// </summary>
    /// <returns>
    /// een berekening van de sprong
    /// </returns>

    float CalculateJump()
    {
        float jump = Mathf.Sqrt(2 * jumpHeight * gravity);
        return jump;
    }

    void OnCollisionStay()
    {
        grounded = true;

    }
}
