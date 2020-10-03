using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    public GameObject planet;
    public float speed = 4f;
    public float jumpHeight = 1.2f;
    
    private float gravity = 100;
    private bool onGround = false;
    private Rigidbody rb;
    private float distanceToGround;
    private Vector3 groundNormal;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;
    }

    // Update is called once per frame
    void Update()
    {
        
        //movement
        float x = Input.GetAxis("Horizontal") * Time.deltaTime * speed;

        float z = Input.GetAxis("Vertical") * Time.deltaTime * speed;
        
        transform.Translate(x,0,z);
        
        //local rotation
        if (Input.GetKey(KeyCode.E))
        {
            transform.Rotate(0,150 * Time.deltaTime,0);
            
        }

        if (Input.GetKey(KeyCode.Q))
        {
            transform.Rotate(0,-150 * Time.deltaTime,0);
        }
        
        //ground control
        RaycastHit hit = new RaycastHit();
        if (Physics.Raycast(transform.position, -transform.up, out hit, 10))
        {
            distanceToGround = hit.distance;
            groundNormal = hit.normal;

            if (distanceToGround <= 0f) onGround = true;
            else onGround = false;
        }
        
        //jump
        if (Input.GetKeyDown(KeyCode.Space))
        {
            rb.AddForce(transform.up * 80000 * jumpHeight * Time.deltaTime);
        }
        
        //gravity and rotation
        Vector3 gravDirection = (transform.position - planet.transform.position).normalized;

        if (onGround == false)
        {
            rb.AddForce(gravDirection * -gravity);
        }

        Quaternion toRotation = Quaternion.FromToRotation(transform.up, groundNormal) * transform.rotation;
        transform.rotation = toRotation;
        
        


    }
}
