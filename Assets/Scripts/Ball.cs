using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{

    [SerializeField] Paddle paddle1;
    [SerializeField] AudioClip[] ballSounds;

    [SerializeField] float xOffset = 2f;
    [SerializeField] float yOffset = 15f;

    Vector2 paddleToBallVector;
    Boolean isBallLaunched = false;
    int signChanger = 1;
    AudioSource myAudioSource;

    // Start is called before the first frame update
    void Start()
    {
        paddleToBallVector = transform.position - paddle1.transform.position;
        myAudioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!isBallLaunched)
        {
            LockBallToPaddle();
            LaunchOnMouseClick();
        }

    }

    private void LaunchOnMouseClick()
    {
        signChanger = -signChanger;
        if (Input.GetMouseButton(0))
        {
            GetComponent<Rigidbody2D>().velocity = new Vector2(xOffset * signChanger, yOffset);
            isBallLaunched = true;
        }
    }

    private void LockBallToPaddle()
    {
        Vector2 paddlePos = new Vector2(paddle1.transform.position.x, paddle1.transform.position.y);
        transform.position = paddlePos + paddleToBallVector;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (isBallLaunched)
        {
            AudioClip clip = ballSounds[UnityEngine.Random.Range(0, ballSounds.Length)];
            myAudioSource.PlayOneShot(clip);
        }
        
    }
}
