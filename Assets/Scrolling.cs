using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scrolling : MonoBehaviour
{
    private float groundHorizontalLength;
    private float startPosition;
    public GameObject bird;

    private void Awake()
    {
        float width = GameObject.Find("SkyTileSprite").GetComponent<SpriteRenderer>().bounds.size.x;
        groundHorizontalLength = width;
        Debug.Log("groundHorizontalLength: " + groundHorizontalLength);
    }

    // Start is called before the first frame update
    void Start()
    {
        float speed = bird.GetComponent<BirdScript>().forwardSpeed;
        startPosition = transform.position.x;
    }

    // Update is called once per frame
    void Update()
    {
        float birdPosition = bird.transform.position.x;
        if (startPosition + groundHorizontalLength < birdPosition)
        {
            RepositionBackground();
        }
    }

    public void RepositionBackground()
    {
        Vector2 groundOffSet = new Vector2(groundHorizontalLength * 2, 0);
        transform.position = (Vector2)transform.position + groundOffSet;
        startPosition = transform.position.x;
    }
}
