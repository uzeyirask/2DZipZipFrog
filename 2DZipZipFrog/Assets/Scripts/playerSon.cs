using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections.Generic;

public class PlayerSon : MonoBehaviour
{
    private Rigidbody2D rb;
    public float hareketHizi = 5f;
    public float ziplamaGucu = 5f;
    public GameObject bicak;
    private int elmasayisi;
    public Text elmasayisitext;
    bool isGround = true;

    //sure ilgili degiskenler
    public float totalTime = 45f; // Toplam süre
    private float currentTime;
    public Text countdownText;
   // public string nextSceneName = "StartQuitMenu"; // Geçilecek sahnenin adı

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        //DontDestroyOnLoad(this.gameObject);

        elmasayisi = 0;
        elmasayisitext.text = elmasayisi.ToString();

        //ekrandaki sure bitince ile ilgili...
        totalTime = 45f; // süreyi 45 saniyeye ayarla
        currentTime = totalTime;
        GuncelleTime();
    }
    void Update()
    {
        Hareket();
        Zipla();
        // "E" tuşuna basıldığında Firlat fonksiyonunu çağır
        if (Input.GetKeyDown(KeyCode.E))
        {
            Firlat();
        }
        //ekrandaki sure bitince ile ilgili...
        if (currentTime > 0)
        {
            currentTime -= Time.deltaTime;
            GuncelleTime();
        }
        else
        {
            ZamanBitti();
        }
    }

    void Hareket()
    {
        float yatayAl = Input.GetAxis("Horizontal");
        //Debug.Log("Yatay Hareket: " + yatayAl);//bu loga a ya veya d ye basildiginda hangi degeerlerin gittigini gostermek icin kullanilan kod
        if (Input.GetKey("a") || Input.GetKey("d"))
        {
            Vector2 hareket = new Vector2(yatayAl, 0f);  //hareket diye bir nesne olustu ve sadece altta kullandi
            rb.velocity = new Vector2(hareket.x * hareketHizi, rb.velocity.y);
        }
    }

    void Zipla()
    {
        if (Input.GetKeyDown(KeyCode.Space) && isGround)
        {
            rb.velocity = new Vector2(rb.velocity.x, ziplamaGucu);
            isGround = false;
        }
    }

    void Firlat()
    {
        if (elmasayisi == 5)
        {
            GameObject yeniObje = Instantiate(bicak, transform.position, transform.rotation);
            Rigidbody2D rb = yeniObje.GetComponent<Rigidbody2D>();
            // rb.AddForce(transform.forward * 2000);
            yeniObje.transform.localScale = new Vector3(10f, 10f, 1f);
            rb.AddForce(new Vector2(3, 0), ForceMode2D.Impulse);
        }
    }
    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "dusman")
        {
            SceneManager.LoadScene(0);
        }
        if (other.gameObject.tag == "anaDusman")
        {
            SceneManager.LoadScene(0);
        }
        if (other.gameObject.tag == "Kupa")
        {
            SceneManager.LoadScene(2);
        }
        if (other.gameObject.tag =="apple")
        {
            elmasayisi += 1;
            elmasayisitext.text = elmasayisi.ToString();
            other.gameObject.SetActive(false);
            if(elmasayisi==5)
            {
                Firlat();
                //SceneManager.LoadScene(0);
            }
        }
        if (other.gameObject.tag == "ground") //yere basmadan birden fazla ziplayamaz
        {
            isGround = true;
        }

        if (other.gameObject.tag == "jumper")
        {
            GetComponent<Rigidbody2D>().velocity = new Vector2(0f, 20f);
        }
    }

    void GuncelleTime()
    {
        // Ekrana kalan süreyi yazdır
        int seconds = Mathf.CeilToInt(currentTime);
        countdownText.text =seconds.ToString();
    }
    void ZamanBitti ()
    {
        // Hedef sahneye geçiş 
        SceneManager.LoadScene(0);

    }
}
