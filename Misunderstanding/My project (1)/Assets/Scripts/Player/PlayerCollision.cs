using System;
using UnityEngine;

public class PlayerCollision : MonoBehaviour
{
    [SerializeField] BoxCollider2D boxCollider;
    [SerializeField] UIManager uiManager;
    private void Awake()
    {
        uiManager = GameObject.FindAnyObjectByType<UIManager>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log(collision.gameObject.tag);
        if (collision.gameObject.tag == "Obstacle")
        {
            uiManager.ShowGameOver();
        }
    }
}
