using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class topDownController : MonoBehaviour
{
    //The key that will make Player phase through walls
    public KeyCode phasing;

    //The boolean that represents whether the Player is currently phasing
    private bool inPhase;

    //Cool down timer for phasing
    private float coolDown;

    //Value that fills the Phasing bar
    public Slider phaseSlider;

    //Set a variable for the RigidBody2D component on Player
    Rigidbody2D rb;

    //Create a public variable to control the speed
    public int speed;

    private Vector2 currDirection;

    private Color color;

    Animator anim;

    // Use this for initialization
    void Start()
    {
        //Find and set the Rigidbody2D component to rb
        rb = GetComponent<Rigidbody2D>();
        currDirection = Vector2.right;
        anim = GetComponent<Animator> ();

        //Initialize inPhase
        inPhase = false;

        //Initialize coolDown
        coolDown = 0;

        color = GetComponent<SpriteRenderer>().color;

        phaseSlider.value = 3;
        phaseSlider.maxValue = 3;
        phaseSlider.minValue = 0;

    }

    // Update is called once per frame
    void Update()
    {
        move();

        if (inPhase)
        {   
            phaseSlider.value -= Time.deltaTime;
        }
        else if (!inPhase)
        {
            phaseSlider.value += Time.deltaTime;
        }

        if (Input.GetKeyDown(phasing) && coolDown <= 0)
        {
            phase();
        }
        else
        {
            coolDown -= Time.deltaTime;
        }


    }

    private void move()
    {
        //Get the inputs for the arrow keys
        float hInput = Input.GetAxis("Horizontal");
        float vInput = Input.GetAxis("Vertical");

        //Normalize the vector from the inputs
        Vector2 velVector = new Vector2(hInput, vInput).normalized;

        if (velVector != currDirection && velVector != Vector2.zero)
        {
            currDirection = velVector;
        }

        //Add velocity(speed) to Player
        rb.velocity = currDirection * speed;

    }

    //Will make Player phase, waits for three seconds, then calls resetPhase
    private void phase()
    {
        inPhase = true;
        GetComponent<BoxCollider2D>().isTrigger = true;
        Invoke("resetPhase", 3);
        anim.SetBool("Phasing", true);
        //GetComponent<SpriteRenderer>().color = Color.magenta;
    }

    //Makes Player tangible again; will collide with walls again
    private void resetPhase()
    {
        inPhase = false;
        GetComponent<BoxCollider2D>().isTrigger = false;
        coolDown = 3.0f;
        anim.SetBool("Phasing", false);
        //GetComponent<SpriteRenderer>().color = color;
    }
}
