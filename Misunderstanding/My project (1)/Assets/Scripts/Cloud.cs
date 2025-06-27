using System;
using UnityEngine;

public class Cloud : MonoBehaviour
{
    [SerializeField] float speed;
    [SerializeField] bool goingLeft = true;
    [SerializeField] BoxCollider2D boxCollider;


    void Update()
    {
        var direction = goingLeft ? Vector3.left : Vector3.right;
        this.transform.position += direction * speed * Time.deltaTime;
    }
}
