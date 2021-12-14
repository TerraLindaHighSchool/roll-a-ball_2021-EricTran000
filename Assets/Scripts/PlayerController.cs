using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using TMPro;

public class PlayerController : MonoBehaviour
{
    public float speed = 0;
    public TextMeshProUGUI countText;
    public GameObject winTextObject;
    [SerializeField] private float jumpForce = 500;

    private bool isOnGround;
    private bool isOnLaunchPad;
    private bool isOnBouncePad;
    private Rigidbody rb;
    private int count;
    private float movementX;
    private float movementY;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        count = 0;

        SetCountText();
        winTextObject.SetActive(false);
    }

    void OnMove(InputValue movementValue)
    {
        Vector2 movementVector = movementValue.Get<Vector2>();

        movementX = movementVector.x;
        movementY = movementVector.y;
  
    }

    void SetCountText()
    {
        countText.text = "Count:" + count.ToString();
        if(count >= 12)
        {
            SceneManager.LoadScene("m");
        }
    }

    private void FixedUpdate()
    {
        Vector3 movement = new Vector3(movementX, 0.0f, movementY);
        
        rb.AddForce(movement * speed);
        Restart();
        Launch();
        Bounce();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("PickUp"))
        {
            other.gameObject.SetActive(false);
            count = count + 1;

            SetCountText();
        }
    }
   
    void OnJump()
    {
        if (isOnGround)
        {
            rb.AddForce(new Vector3(0, jumpForce, 0));
        }

    }

    void Restart()
    {
        if(transform.position.y < -10)
        {
            SceneManager.LoadScene("h");
        }
    }

    void OnLose()
    {
        SceneManager.LoadScene("h");
    }

    private void OnCollisionStay(Collision collision)
    {
        if(collision.gameObject.name == "Ground")
        {
            isOnGround = true;
        }

        if (collision.gameObject.name == "Launch Pad")
        {
            isOnLaunchPad = true;
        }

        if (collision.gameObject.name == "Bounce Pad")
        {
            isOnBouncePad = true;
        }

    }

    private void OnCollisionExit(Collision collision)
    {

        if (collision.gameObject.name == "Ground")
        {
            isOnGround = false;
        }

        if (collision.gameObject.name == "Launch Pad")
        {
            isOnLaunchPad = false;
        }

        if (collision.gameObject.name == "Bounce Pad")
        {
            isOnBouncePad = false;
        }

    }

    private void Launch()
    {
        if (isOnLaunchPad)
        {
            rb.AddForce(new Vector3(0, 400, 0));
            rb.AddForce(Vector3.forward*500);
        }

    }

    private void Bounce()
    {
        if (isOnBouncePad)
        {
            rb.AddRelativeForce(Vector3.back * 200);
        }
    }
}

