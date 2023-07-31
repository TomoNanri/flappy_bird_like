using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BirdScript : MonoBehaviour
{
    float upForce = 200f;
    public float forwardSpeed = 1.5f;
    bool flap = false;

    Animator anim;
    public bool isDead = false;

    AudioSource jumpSound;
    AudioSource collisionSound;

    // Start is called before the first frame update
    void Start()
    {
        GetComponent<Rigidbody2D>().velocity = new Vector2(forwardSpeed, 0);
        anim = GetComponent<Animator>();
        AudioSource[] audioSources = GetComponents<AudioSource>();
        jumpSound = audioSources[0];
        collisionSound = audioSources[1];
    }

    // Update is called once per frame
    void Update()
    {
        if (isDead)  return;

        if (Input.anyKeyDown)
        {
            flap = true;
        }
    }

    void FixedUpdate()
    {
        if (flap)
        {
            flap = false;
            GetComponent<Rigidbody2D>().velocity = new Vector2(GetComponent<Rigidbody2D>().velocity.x, 0);
            GetComponent<Rigidbody2D>().AddForce(new Vector2(0, upForce));
            anim.SetTrigger("Flap");
            jumpSound.Play();
        }
    }

    void OnCollisionEnter2D(Collision2D otherObject)
    {
        if (!isDead) collisionSound.Play();
        isDead = true;
        anim.SetTrigger("Die");
        GameObject.Find("GameManager").GetComponent<GameControl>().BirdDied();
    }
}
