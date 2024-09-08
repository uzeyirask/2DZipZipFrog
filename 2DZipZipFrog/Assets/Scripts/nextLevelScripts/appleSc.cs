using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class appleSc : MonoBehaviour
{
    public GameObject targetObject;
    public AudioSource bomba1;

    // Start is called before the first frame update
    void Start()
    {
        targetObject.SetActive(false);
    }
    // Update is called once per frame
    void Update()
    {
        
    }
    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "anaDusman")
        {
            bomba1.Play();
            Debug.Log("Elma, Ana Dusman ile çarpıştı!");
            StartCoroutine(ActivateObjectForTime(1f));
            // Düşman objesini devre dışı bırak
            other.gameObject.SetActive(false);

        }
        else if (other.gameObject.tag == "bicak")
        {
            Debug.Log("Elma, Elma çarpıştı spam denemeyeniz!");

            // Bıçak objesini devre dışı bırak
            other.gameObject.SetActive(false);
        }
        if (other.gameObject.tag == "jumper")
        {
            GetComponent<Rigidbody2D>().velocity = new Vector2(0f, 50f);
        }
    }
    IEnumerator ActivateObjectForTime(float duration)
    {
        // Objeyi etkinleştir
        targetObject.SetActive(true);

        // Belirtilen süre boyunca bekle
        yield return new WaitForSeconds(duration);

        // Belirtilen süre sonunda objeyi devre dışı bırak
        targetObject.SetActive(false);
    }
}