using System;
using UnityEngine;

public class Cloud : MonoBehaviour
{
    [SerializeField] float speed;
    [SerializeField] BoxCollider2D boxCollider;
    [SerializeField] GameObject player;


    void Update()
    {
        var birdGoingRight = player.transform.localScale.x > 0;

        var direction = birdGoingRight ? Vector3.left : Vector3.right; // clouds go the opposite way.
        this.transform.position += direction * speed * Time.deltaTime;
    }
}
