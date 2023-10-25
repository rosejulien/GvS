using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Tilemaps;

public class MooveCamera : MonoBehaviour
{
    private Camera mainCamera;
    private bool next = false;
    public bool already = false;

    public float transitionDuration;
    public float valX;
    public float valY;
    private float startTime;
    private Vector3 initialPosition;

    public GameObject room1;
    public GameObject room2;

    public GameObject tilemap1;
    private TilemapRenderer tilemapRender1;
    private TilemapCollider2D tilemapCollider2D1;

    public GameObject tilemap2;
    private TilemapRenderer tilemapRender2;
    private TilemapCollider2D tilemapCollider2D2;

    public GameObject tilemap3;
    private TilemapRenderer tilemapRender3;
    private TilemapCollider2D tilemapCollider2D3;

    public GameObject tilemap4;
    private TilemapRenderer tilemapRender4;
    private TilemapCollider2D tilemapCollider2D4;

    public GameObject moveCamera;
    private MooveCamera moveCameraRoom;

    private void Start()
    {
        mainCamera = Camera.main;

        initialPosition = mainCamera.transform.position;

        tilemapRender1 = tilemap1.GetComponent<TilemapRenderer>();
        tilemapCollider2D1 = tilemap1.GetComponent<TilemapCollider2D>();

        tilemapRender2 = tilemap2.GetComponent<TilemapRenderer>();
        tilemapCollider2D2 = tilemap2.GetComponent<TilemapCollider2D>();

        tilemapRender3 = tilemap3.GetComponent<TilemapRenderer>();
        tilemapCollider2D3 = tilemap3.GetComponent<TilemapCollider2D>();

        tilemapRender4 = tilemap4.GetComponent<TilemapRenderer>();
        tilemapCollider2D4 = tilemap4.GetComponent<TilemapCollider2D>();

        moveCameraRoom = moveCamera.GetComponent<MooveCamera>();
    }

    private void Update()
    {
        if(next)
        {
            float elapsedTime = Time.time - startTime;
            float t = Mathf.Clamp01(elapsedTime / transitionDuration);

            float newX = Mathf.Lerp(initialPosition.x, valX, t);
            float newY = Mathf.Lerp(initialPosition.y, valY, t);

            Vector3 newPosition = new Vector3(newX, newY, initialPosition.z);

            mainCamera.transform.position = newPosition;

            if (t >= 1.0f)
            {
                next = false;

                moveCameraRoom.enabled = true;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!already)
        {
            if (other.CompareTag("Player"))
            {
                room1.SetActive(false);
                room2.SetActive(true);

                next = true;
                already = true;
                startTime = Time.time;

                tilemapRender1.enabled = true;
                tilemapCollider2D1.enabled = true;

                tilemapRender2.enabled = false;
                tilemapCollider2D2.enabled = false;

                tilemapRender3.enabled = false;
                tilemapCollider2D3.enabled = false;

                tilemapRender4.enabled = false;
                tilemapCollider2D4.enabled = false;
            }
        }
        else
        {
            if (other.CompareTag("Player"))
            {
                Debug.Log("You can't return");
            }
        }
    }
}
