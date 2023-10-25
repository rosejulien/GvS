using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectMover : MonoBehaviour
{
    private Rigidbody2D rb;
    private SpriteRenderer spriteRenderer;
    private Color originalColor;
    private Color highlightColor;
    private Color falseColor = Color.red;
    private string hexHighlightColor = "#DBDBDB";

    public GameObject player;
    private Rigidbody2D rbPlayer;

    private Vector3 initialPosition;
    private bool isDragging = false;
    public bool isColliding = false;

    public int collisionCount = 0;

    private void Start()
    {
        initialPosition = transform.position;

        rb = GetComponent<Rigidbody2D>();
        rbPlayer = player.GetComponent<Rigidbody2D>();
        

        spriteRenderer = GetComponent<SpriteRenderer>();
        originalColor = spriteRenderer.color;

        ColorUtility.TryParseHtmlString(hexHighlightColor, out highlightColor);
    }

    private void Update()
    {
        if (isDragging)
        {
            Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector3 targetPosition = new Vector3(mousePosition.x, mousePosition.y, transform.position.z);
            transform.position = targetPosition; 
        }
    }

    private void OnMouseDown()
    {

        isDragging = true;

        PlayerController playerController = player.GetComponent<PlayerController>();
        playerController.stop = true;

        rbPlayer.constraints = RigidbodyConstraints2D.FreezePositionX | RigidbodyConstraints2D.FreezePositionY | RigidbodyConstraints2D.FreezeRotation;
    }

    private void OnMouseUp()
    {
        isDragging = false;

        PlayerController playerController = player.GetComponent<PlayerController>();
        playerController.stop = false;

        rbPlayer.constraints &= ~RigidbodyConstraints2D.FreezePositionX;
        rbPlayer.constraints &= ~RigidbodyConstraints2D.FreezePositionY;

        if (isColliding)
        {
            transform.position = initialPosition;
            collisionCount = 0;
            spriteRenderer.color = originalColor;
            isColliding = false;
        }
        else
        {
            initialPosition = transform.position;
            collisionCount = 0;
            isColliding = false;
        }
    }

    private void OnMouseEnter()
    {
        if (isDragging && collisionCount > 0)
        {
            spriteRenderer.color = falseColor;
        }
        else
        {
            spriteRenderer.color = highlightColor;
        }
    }

    private void OnMouseExit()
    {
        if (isDragging && collisionCount > 0)
        {
            spriteRenderer.color = falseColor;
        }
        else
        {
            spriteRenderer.color = originalColor;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (isDragging)
        {
            spriteRenderer.color = falseColor;
            collisionCount++;
            isColliding = true;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (isDragging)
        {
            collisionCount--;
            if (collisionCount <= 0)
            {
                collisionCount = 0;
                spriteRenderer.color = highlightColor;
                isColliding = false;
            }
        }
    }
}
