using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MapCharacter : MonoBehaviour

{

    public float speed;
    // Start is called before the first frame update
    void Start()
    {
        //ajetaan aina kun mapcharacter luodaan map sceneen. tarkistetaan ollaanko oltu levelissä.
        if (GameManager.manager.currentLevel != "")
        {
            GameObject.Find(GameManager.manager.currentLevel).GetComponent<LoadLevel>().Cleared(true);
            //ajetaan jos ollaan läpäisty jokin kenttä.
            //asetetaan kappaleen eka child, eli spawnpoint ja asetetaan mapcharacter sinne.
            transform.position = GameObject.Find(GameManager.manager.currentLevel).transform.GetChild(0).transform.position;
        }
    }

    // Update is called once per frame

    private void Update()
    {
        // GetAxisRaw is unsmoothed input -1, 0, 1
        float v = Input.GetAxisRaw("Vertical");
        float h = Input.GetAxisRaw("Horizontal");

        // normalize so going diagonally doesn't speed things up
        Vector3 direction = new Vector3(h, v, 0f).normalized;

        // translate
        transform.Translate(direction * speed * Time.deltaTime);

    }

    //jos osuu leveltriggeriin, haetaan triggerin loadlevel component ja sieltä level to load muuttuja ja avataan sen niminen scene.
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("LevelTrigger"))
        {
            GameManager.manager.currentLevel = collision.gameObject.name;

            SceneManager.LoadScene(collision.gameObject.GetComponent<LoadLevel>().LevelToLoad);
        }
    }

}
