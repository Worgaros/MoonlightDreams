﻿using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;


public class ObjectsCollector : MonoBehaviour
{
    [SerializeField] int value = 1;
    [SerializeField] [Range(0, 5)] float floatingDistance_ = 1;

    Vector3 originalPosition_;

    [SerializeField] AnimationCurve animationCurve_;

    void Start()
    {
        originalPosition_ = transform.position;
    }

    void Update()
    {
        float offsetY = animationCurve_.Evaluate(Time.time);
        transform.position = new Vector3(originalPosition_.x, originalPosition_.y + offsetY * floatingDistance_);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Player") && gameObject.CompareTag("Page"))
        {
            other.GetComponent<PlayerController>().AddBookPage(value);
            Destroy(gameObject);
        }
        
        else if (other.gameObject.layer == LayerMask.NameToLayer("Player") && gameObject.CompareTag("Book"))
        {
            
            Destroy(gameObject);
            SceneManager.LoadScene("VictoryScene");
        }
        
        else if (other.gameObject.layer == LayerMask.NameToLayer("Player") && gameObject.CompareTag("Necklace"))
        {
            other.GetComponent<PlayerController>().AddSpeed(value);
            Destroy(gameObject);
        }
    }

    void OnDrawGizmos()
    {
        if (originalPosition_ == Vector3.zero)
        {
            Gizmos.DrawLine(transform.position,
                new Vector2(transform.position.x, transform.position.y + floatingDistance_));
            Gizmos.DrawLine(transform.position,
                new Vector2(transform.position.x, transform.position.y - floatingDistance_));
        }
        else
        {
            Gizmos.DrawLine(originalPosition_,
                new Vector2(originalPosition_.x, originalPosition_.y + floatingDistance_));
            Gizmos.DrawLine(originalPosition_,
                new Vector2(originalPosition_.x, originalPosition_.y - floatingDistance_));
        }
    }
}