using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadLevel : MonoBehaviour
{

    public string LevelToLoad;
    public bool cleared;
    // Start is called before the first frame update
    void Start()
    {
        //katsotaan aina mapsceneen mennessä onko gamemanagerissa merkattu kys.taso suoritetuksi
        //jos on läpäisty, ajetaan funktio.

        if(GameManager.manager.GetType().GetField(LevelToLoad).GetValue(GameManager.manager).ToString() == "True")
        {
            Cleared(true);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Cleared(bool isClear)
    {
        if(isClear == true)
        {
            cleared = true;
            GameManager.manager.GetType().GetField(LevelToLoad).SetValue(GameManager.manager, true);
            //laittaa raksin gamemanageriin oikeaan paikkaan
            transform.GetChild(1).gameObject.SetActive(true); //laittaa stage clear kyltin näkyviin
        }
        else
        {

        }
    }
}
