using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KusSc : MonoBehaviour
{
    public float hareketHizi = 100f; // Hızı artırmak için
    public float hedefX = 91.8f; // X eksenindeki hedef konum
    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        HedefeGit();
    }

    void HedefeGit()
    {
        Vector2 hedefNokta = new Vector2(hedefX, transform.position.y);
        transform.position = Vector2.MoveTowards(transform.position, hedefNokta, hareketHizi * Time.deltaTime);
    }
}
