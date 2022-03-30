using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private Transform groundCheckTransform = null;
    [SerializeField] private LayerMask playerMask;
    private bool jumpKeyWasPressed =true;
    private float horizontalInput;
    private Rigidbody rigidbodyComponent;
    private int superJumpsRemaings;



    // Start is called before the first frame update
    void Start()
    {
        rigidbodyComponent =   GetComponent<Rigidbody>();  
    }

    // Update is called once per frame
    void Update()
    {
        //check if space key is pressed 
        if (Input.GetKeyDown(KeyCode.Space) == true){
            jumpKeyWasPressed = true;
        }

        horizontalInput = Input.GetAxis("Horizontal");
    }

    //fixed update is called once every physics update 
    private void FixedUpdate(){

        //left right movement of [y axix ]
        rigidbodyComponent.velocity = new Vector3(horizontalInput,rigidbodyComponent.velocity.y,0); //why new keyword;

        if(Physics.OverlapSphere(groundCheckTransform.position, 0.1f,playerMask).Length == 0    )
        {
            return;
        }
        if(jumpKeyWasPressed)
        {
            float jumpPower = 5f;
            if(superJumpsRemaings>1){
                jumpPower=7f;
                superJumpsRemaings-=2;
            } 
            rigidbodyComponent.AddForce(Vector3.up*jumpPower ,ForceMode.VelocityChange);
            jumpKeyWasPressed = false;
            jumpPower=5f;
        }
        

    }

    // to vanish coins when touched with player
        private void OnTriggerEnter(Collider other){
        if (other.gameObject.layer == 7)
        {
            Destroy(other.gameObject);
//    super jump implemntation
            superJumpsRemaings++;
        }
    }

    
}
