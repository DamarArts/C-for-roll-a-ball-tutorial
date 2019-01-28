using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    public float speed;
    public Text countText;
    public Text winText;
    public float jumpMultiplier;     // these are things  
    public float jumpStart;          // i am working on for
    private bool coolDown = false;   // our next challenge

    private Rigidbody rb;
    private int count;


    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        count = 0;
        SetCountText();
        winText.text = "";
    }

    private void Update()
    {
        if (coolDown == false)  // cooldown for jumping so that it can't be exploited
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                    rb.AddForce(Vector3.up * jumpMultiplier);
            

                Invoke("ResetCooldown", 0.5f);
                coolDown = true;
            }

        }

    }
    void ResetCooldown()
    {
        coolDown = false;
    }

    void FixedUpdate()    // i am still unsure as to why the movement controls had to be in FixedUpdate, i had the jumping code in here at first but it was laging and unresponsive at times.
    {
  
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        Vector3 movement = new Vector3(moveHorizontal, 0.0f, moveVertical);

        if (transform.position.y <= jumpStart)
            rb.AddForce(movement * speed);
        if (transform.position.y > jumpStart)
            rb.AddForce(movement);

        if (Input.GetKey("escape"))
            Application.Quit();
        

    }
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Pickup"))
        {
            other.gameObject.SetActive(false);
            count = count + 1;
            SetCountText();
        }
    }
        void SetCountText ()
        {
            countText.text = "Count: " + count.ToString();
        if (count >= 12)
        {
            winText.text = "VICTORIOUS!";
        }
        }
    
}