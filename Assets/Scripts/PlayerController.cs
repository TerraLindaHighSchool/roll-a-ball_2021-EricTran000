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
            winTextObject.SetActive(true);
        }
    }

    private void FixedUpdate()
    {
        Vector3 movement = new Vector3(movementX, 0.0f, movementY);

        rb.AddForce(movement * speed);
        Restart();
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

   
    }

    private void OnCollisionExit(Collision collision)
    {

        if (collision.gameObject.name == "Ground")
        {
            isOnGround = false;
        }
    }
}

