using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitAnimation : MonoBehaviour
{
    public Sprite[] frames;         // assign your 5 sprites here
    public float frameTime = 0.04f; // 25 FPS, adjust if needed

    private SpriteRenderer sr;
    private int currentFrame = 0;
    private float timer = 0f;

    private void Awake()
    {
        sr = GetComponent<SpriteRenderer>();
        sr.sprite = frames[0];
    }

    private void Update()
    {
        timer += Time.deltaTime;

        if (timer >= frameTime)
        {
            timer -= frameTime;
            currentFrame++;

            if (currentFrame >= frames.Length)
            {
                Destroy(gameObject);
                return;
            }

            sr.sprite = frames[currentFrame];
        }
    }
}

