using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class dusmanKaraktersc : MonoBehaviour
{
    public float donmeHizi = 300f; // 30 derece/saniye olarak donme hizi

    void Update()
    {
        // Her frame'de objenin y ekseninde dönme işlemi
        transform.Rotate(Vector3.up, donmeHizi * Time.deltaTime);
        // 180 derece döndüğünde, 3 saniye bekle
        if (Mathf.Abs(transform.rotation.eulerAngles.y - 180f) < 0.1f)
        {
            // 3 saniye bekle
            Invoke("ResetRotation", 5f);
        }
    }
    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "player")
        {
            gameObject.SetActive(false);
        }
    }
        void ResetRotation()
    {
        // 180 derece döndükten sonra objeyi tekrar sıfıra döndür
        transform.rotation = Quaternion.Euler(0f, 0f, 0f);
    }
}
