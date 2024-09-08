using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections.Generic;

public class playerLvl2 : MonoBehaviour
{
    //Muzik ile ilgili degiskenler
    public AudioSource trmuzik;
    public AudioSource bomba;
    public AudioSource bomba1;
    public AudioSource muzik;
    private Rigidbody2D rb; // Fizik icin verilen
    public float hareketHizi; //Karaker icin verilen degerler
    public float ziplamaGucu;
    public GameObject bicak;
    private int elmasayisi;
    public Text elmasayisitext;
    bool isGround = true;
    //sure ilgili degiskenler
    public float totalTime ; // Toplam süre
    private float currentTime;  //anlik sure
    public Text countdownText;  //kalan sure
    // public string nextSceneName = "StartQuitMenu"; // Geçilecek sahnenin adı seklindede yapilabilir
    public GameObject Patlama;
    //bayrakYukselmesi hakkinda kodlar
    public string bayrakTag = "BAYRAK"; // Bayrak etiketi
    public float yukselecekMesafe = -2f; // Yükselme mesafesi
    public float yukselecekSure = 10f;   // Yükselme süresi
    private bool carpti = false;

    void Start()
    {
        Patlama.SetActive(false);  //patlama efektini basta kapatilir
        rb = GetComponent<Rigidbody2D>();  //fizik ozelliklerini kullanmak icin rb olusturulur

        elmasayisi = 0;
        elmasayisitext.text = elmasayisi.ToString();  //elma sayisi

        //ekrandaki sure bitince 
        totalTime = 90f; // süreyi 90 saniyeye ayarla
        currentTime = totalTime;
        UpdateTime();
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
        //ekrandaki sure bitince 
        if (currentTime > 0)
        {
            currentTime -= Time.deltaTime;
            UpdateTime();
        }
        else
        {
            TimeOver();  //TimeOver fonksiyonuna gidip diger sahneye gecisini saglar
        }
    }

    //Patlama ile ilgili
    IEnumerator ActivateObjectForTime(float duration)
    {
        // Objeyi etkinleştir
        Patlama.SetActive(true);
        // Belirtilen süre boyunca bekle
        yield return new WaitForSeconds(duration);
        // Belirtilen süre sonunda objeyi devre dışı bırak
        Patlama.SetActive(false);
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
        if (other.gameObject.tag == "tuzak")
        {
            other.gameObject.SetActive(false);
        }
        if (other.gameObject.tag == "Kupa")
        {
            SceneManager.LoadScene(4);
        }
        if (other.gameObject.tag == "apple") //elmalara degince elma sayisini arttirmak icin gereken kod
        {
            elmasayisi += 1;
            elmasayisitext.text = elmasayisi.ToString();
            other.gameObject.SetActive(false);
            if (elmasayisi == 5)
            {
                Firlat();
            }
        }
        if (other.gameObject.tag == "BAYRAK" && !carpti) //bayrak ile ilgili kodlamalar
        {
            carpti = true;
            StartCoroutine(YukselemeSuresi(other.gameObject));
            trmuzik.Play();//tr muzigi baslat 
            muzik.Pause();// sahne muzigini kapat

        }

        if (other.gameObject.tag == "basamak")
        {
            bomba.Play();
            //Ziplatma Kodlari
            GetComponent<Rigidbody2D>().velocity = new Vector2(0f, 10f);
            StartCoroutine(ActivateObjectForTime(1f));
        }
        if (other.gameObject.tag == "ground") //yere basmadan birden fazla ziplayamaz
        {
            isGround = true;
        }
        if (other.gameObject.tag == "block")
        {
            GetComponent<Rigidbody2D>().velocity = new Vector2(0f, 15f);
            other.gameObject.SetActive(false);
        }
        if (other.gameObject.tag == "jumper")
        {
            GetComponent<Rigidbody2D>().velocity = new Vector2(0f, 20f);
        }
    }
    // Bayrak ile ilgili kodlamalar
    IEnumerator YukselemeSuresi(GameObject bayrak)
    {
        Rigidbody2D rbBayrak = bayrak.GetComponent<Rigidbody2D>();
       

        float baslangicY = rbBayrak.position.y;   // Bayrağın başlangıç yüksekliği kaydediliyor.
        float bitisY = baslangicY + yukselecekMesafe;  // Bayrağın yükseleceği maksimum yükseklik hesaplanıyor.
        float zamanGecenSure = 0f;      // Zaman geçen süre başlangıçta sıfır olarak ayarlanıyor.

        while (zamanGecenSure < yukselecekSure)
        {
            float yukselecekY = Mathf.Lerp(baslangicY, bitisY, zamanGecenSure / yukselecekSure);  // Bayrağın yükseleceği yükseklik, zaman geçtikçe belirlenen süre içinde lineer bir şekilde (Lerp) hesaplanıyor.
            rbBayrak.position = new Vector2(rbBayrak.position.x, yukselecekY);  // Bayrağın pozisyonu güncellenerek yukarı doğru hareket ettiriliyor.
            zamanGecenSure += Time.deltaTime;  // Zaman geçen süre, her çerçeve boyunca geçen zaman (delta time) kadar artırılıyor.
            yield return null;
        }
    }
    void UpdateTime()
    {
        // Ekrana kalan süreyi yazdır
        int seconds = Mathf.CeilToInt(currentTime);
        countdownText.text = seconds.ToString();
    }
    void TimeOver()
    {
        // Hedef sahneye geçiş 
        SceneManager.LoadScene(0);
    }
}
//Muhammed Uzeyir ASKIN_._41957078124