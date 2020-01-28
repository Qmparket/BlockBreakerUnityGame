using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Block : MonoBehaviour
{
    // config params
    [SerializeField] AudioClip destroySound;
    [SerializeField] GameObject sparklesVFX;
    [SerializeField] int maxHits;
    [SerializeField] Sprite[] hitSprites;

    // cached reference
    Level level;
    GameSession gameStatus;

    // State variables
    [SerializeField] int timesHit; // Only serialized for debug purposes

    private void Start()
    {
        CountBreakableBlocks();

        gameStatus = FindObjectOfType<GameSession>();
    }

    private void CountBreakableBlocks()
    {
        level = FindObjectOfType<Level>();
        if (tag == "Breakable")
        {
            level.CountBlocks();
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (tag == "Breakable")
        {
            HandleHit();

        }

    }

    private void HandleHit()
    {
        timesHit++;
        if (timesHit >= maxHits)
        {
            DestroyBlock();
        } else
        {
            ShowNextHitSprite();
        }
    }

    private void ShowNextHitSprite()
    {
        int spriteIndex = timesHit - 1;
        if (hitSprites[spriteIndex] != null)
        {
            GetComponent<SpriteRenderer>().sprite = hitSprites[spriteIndex];
        }
        
    }

    private void DestroyBlock()
    {
        TriggerSparkleVFX();
        PlayBlockDestroySFX();
        level.BlockDestroyed();
        Destroy(gameObject);
    }

    private void PlayBlockDestroySFX()
    {
        gameStatus.IncreaseScore();
        AudioSource.PlayClipAtPoint(destroySound, Camera.main.transform.position);
    }

    private void TriggerSparkleVFX()
    {
        GameObject sparkles = Instantiate(sparklesVFX, transform.position, transform.rotation);
        Destroy(sparkles, 1f);
    }

}
