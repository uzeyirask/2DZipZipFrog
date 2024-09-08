using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class basamak : MonoBehaviour
{
    public GameObject targetObject;
    void Start()
    {
 
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "dusman") //basamak dusman objeye carpinca dusman objeyi yok et
        {
            other.gameObject.SetActive(false);
        }
    }
 
}
