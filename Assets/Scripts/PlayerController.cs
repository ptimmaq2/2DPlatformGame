using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class PlayerController : MonoBehaviour
{

    public float moveSpeed;
    public float jumpForce;


    public Animator animator;
    public Rigidbody2D rb2D;

    public Transform groundCheckPosition;
    public float groundCheckRadius;
    public LayerMask groundCheckLayer;
    public bool grounded;

    public Image filler;

    public float counter;
    public float maxCounter; 




    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        rb2D = GetComponent<Rigidbody2D>();

    }

    

    // Update is called once per frame
    void Update()
    {
        // Hahmoa voi liikuttaa monella tapaa. Lyhyesti esimerkkej‰:
        /*
            1. transform.Translate
            2. Muutetaan Rigidobodyn velocitya
            3. K‰ytet‰‰n Rigidobodyn movePosition
            4. K‰ytet‰‰n jotain FixedUpdatessa. 
        */

        if(Physics2D.OverlapCircle(groundCheckPosition.position, groundCheckRadius, groundCheckLayer))
        {
            grounded = true;
        }
        else
        {
            grounded = false;
        }

        transform.Translate(Input.GetAxis("Horizontal") * moveSpeed * Time.deltaTime, 0, 0);

        if(Input.GetAxisRaw("Horizontal") != 0)
        {
            // t‰m‰ if toteutuu silloin kun liikutaan
            transform.localScale = new Vector3(Input.GetAxisRaw("Horizontal"), 1, 1); // Hahmon flippi
            animator.SetBool("Walk", true); // Laittaa walk animaation p‰‰lle
        }
        else
        {
            // ollaan paikallaan
            animator.SetBool("Walk", false);
        }

        if (Input.GetButtonDown("Jump") && grounded == true) 
        {
            // Hypyn voi tehd‰ kahdella tapaa: Rigidbodyn addforcea k‰ytt‰en tai muokkaamalla rigidbodyn velocity‰
            rb2D.velocity = new Vector2(0, jumpForce);
            animator.SetTrigger("Jump");
            // Kotil‰ksy: Katso youtubesta "Better jumping with four lines of code". 
            
        }

        // Counter, joka sahaa 0 ja maxCounterin v‰li‰. Aloittaa aina nollasta. 
        if(counter > maxCounter)
        {
            GameManager.manager.previousHealth = GameManager.manager.health;
            counter = 0;
        }
        else
        {
            counter += Time.deltaTime;
        }


        filler.fillAmount = Mathf.Lerp(GameManager.manager.previousHealth / GameManager.manager.maxHealth, GameManager.manager.health / GameManager.manager.maxHealth, counter / maxCounter);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Trap"))
        {
            TakeDamage(20);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("LevelEnd"))
        {
            SceneManager.LoadScene("Map");
        }
    }

    void TakeDamage(float dmg)
    {
        GameManager.manager.previousHealth = filler.fillAmount * GameManager.manager.maxHealth;
        counter = 0;
        GameManager.manager.health -= dmg;
    }

    /*
        Kotiteht‰vi‰.
        - Tutkiskele Lerpi‰ halutessasi
        - Tee peliin ker‰ilt‰v‰ syd‰n, jonka ker‰tty‰‰n pelaaja saa +10 healthia. Syd‰n tuhoutuu kun se ker‰t‰‰n
        - Tee peliin ker‰ilt‰v‰ syd‰n, jonka ker‰tty‰‰n pelaaja saa +20 maxHealthia. Syd‰n tuhoutuu.
        - Tee peliin ominaisuus, kun pelaaja painaa painiketta f, pelaajahahmon eteen instansioituu nuotio. Jos pelaaja on nuotion ‰‰ress‰, health kasvaa hitaasti.
        - Nuotio voi olla sprite tai partikkeliefekti.
        - Tee kaikista Ground palasista prefabit ja toteuta v‰hint‰‰n 3 erilaista tasoa. 
        
    */

    private void OnGUI()
    {
        GUI.Label(new Rect(10, 10, 100, 200), "RAW: " + Input.GetAxisRaw("Horizontal"));
        GUI.Label(new Rect(10, 30, 100, 200), "NOT RAW: " + Input.GetAxis("Horizontal"));
    }
}
