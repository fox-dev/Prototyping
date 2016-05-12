using UnityEngine;
using System.Collections;

[RequireComponent (typeof (PlayerController))]
public class PlayerMovement : MonoBehaviour {

    public float jumpHeight = 3.5f;
    public float timeToJumpApex = .4f;
    float accelerationTimeAirborne = .1f;
    float accelerationTimeGrounded = .1f;
    float moveSpd = 30f;

    float gravity;
    float jumpVelocity;
    Vector3 velocity;
    float velocityXSmoothing;

    PlayerController controller;

    Rigidbody rb;


	// Use this for initialization
	void Start () {
        //rb = GetComponent<Rigidbody>();

        controller = GetComponent<PlayerController>();

        gravity = -(2 * jumpHeight) / Mathf.Pow(timeToJumpApex, 2);
        jumpVelocity = Mathf.Abs(gravity) * timeToJumpApex;
        print("Gravity: " + gravity + "  Jump Velocity: " + jumpVelocity);

        //rb.AddForce(new Vector3(-1000, 0, 0));
        //rb.velocity = new Vector3(-40, 0, 0);


    }

    /*
	// Update is called once per frame
	void Update () {

        if (Input.GetKey("w"))
        {
            transform.Translate((Vector3.forward) * moveSpd * Time.deltaTime);
        }
        if (Input.GetKey("a"))
        {
            transform.Translate((Vector3.left) * moveSpd * Time.deltaTime);
        }
        if (Input.GetKey("s"))
        {
            transform.Translate((Vector3.back) * moveSpd * Time.deltaTime);
        }
        if (Input.GetKey("d"))
        {
            transform.Translate((Vector3.right) * moveSpd * Time.deltaTime);
        }

        // transform.Translate((Vector3.left) * moveSpd * Time.deltaTime);

    }

    */

    void Update()
    {
        if(velocity.y > 0)
        {
            print("VELOCITY_Y: " + velocity.y);
        }
       
        //print("GRAVITY: " + gravity);
        //print(controller.collisions.below);
        if (controller.collisions.above || controller.collisions.below)
        {
            velocity.y = 0;
        }

        Vector2 input = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));

        if (Input.GetKeyDown(KeyCode.Space) && controller.collisions.below)
        {
            velocity.y = jumpVelocity;
        }


        if (controller.collisions.below)
        {
            //float targetVelocityX_input = input.x * moveSpd;
            float targetVelocityX_input = 30f;
            float targetVelocityX = Mathf.Sin(controller.collisions.currentSlopeAngle * Mathf.Deg2Rad) * -gravity;
            velocity.x = Mathf.SmoothDamp(velocity.x, targetVelocityX + targetVelocityX_input, ref velocityXSmoothing, (controller.collisions.below) ? accelerationTimeGrounded : accelerationTimeAirborne);
            controller.collisions.speedBeforeJump = velocity.x;
        }
        else
        {
            //float targetVelocityX_input = input.x * moveSpd;
            float targetVelocityX_input = controller.collisions.speedBeforeJump/1.75f;
            velocity.x = Mathf.SmoothDamp(velocity.x, targetVelocityX_input, ref velocityXSmoothing, (controller.collisions.below) ? accelerationTimeGrounded : accelerationTimeAirborne);
        }
        //velocity.x = Mathf.SmoothDamp(velocity.x, targetVelocityX, ref velocityXSmoothing, (controller.collisions.below) ? accelerationTimeGrounded : accelerationTimeAirborne);
        //velocity.x = Mathf.SmoothDamp(velocity.x, 30f, ref velocityXSmoothing, (controller.collisions.below) ? accelerationTimeGrounded : accelerationTimeAirborne);
        //velocity.x = Mathf.SmoothDamp(velocity.x, targetVelocityX + targetVelocityX_input, ref velocityXSmoothing, (controller.collisions.below) ? accelerationTimeGrounded : accelerationTimeAirborne);
        

        //print("CALC: " + Mathf.Sin(controller.collisions.currentSlopeAngle * Mathf.Deg2Rad) * gravity);
        // print("VEL: " + velocity.x);





        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);
    }
}
