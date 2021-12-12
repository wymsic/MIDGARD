using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.AI;
using UnityEngine.UI;


public class LokiQuests : MonoBehaviour
{
    // MAKE MORE GAMEOBJECTS TO FIT NUMBER OF TEXTS NEEDED

    public GameObject TextBox; 
    public GameObject LokiText;
    public GameObject LokiText2;
    public GameObject LokiText3;
    public GameObject LokiText4;
    public GameObject LokiText5;
    public GameObject LokiText6;
    public GameObject LokiText7;
    public GameObject LokiText8;
    public GameObject LokiText9;



    private void Awake()
    {
        indexNumber.pageNumber = 0f;
    }

    void OnTriggerStay(Collider other)  // this calls every frame the player is still in contact with the NPC hitbox
    {

        if (other.tag == "Player")  // runs if a object tagged player touches
        {

            // copy this function and adjust the values for each text box needed.
            TextBox.SetActive(true);
            //page 1
            if (indexNumber.pageNumber == 0)
            {
                LokiText.SetActive(true);

                //Have to hide the other texts. Place any new ones under this. 

                LokiText2.SetActive(false);
                LokiText3.SetActive(false);
                LokiText4.SetActive(false);
                LokiText5.SetActive(false);
                LokiText6.SetActive(false);
                LokiText7.SetActive(false);
                LokiText8.SetActive(false);
                LokiText9.SetActive(false);
            }


            //page 2
            if (indexNumber.pageNumber == 1)
            {
                LokiText2.SetActive(true);

                // 
                LokiText.SetActive(false);
                LokiText3.SetActive(false);
                LokiText4.SetActive(false);
                LokiText5.SetActive(false);
                LokiText6.SetActive(false);
                LokiText7.SetActive(false);
                LokiText8.SetActive(false);
                LokiText9.SetActive(false);
            }

            //page 3 ...
            if (indexNumber.pageNumber == 2)
            {
                LokiText3.SetActive(true);

                //
                LokiText.SetActive(false);
                LokiText2.SetActive(false);
                LokiText4.SetActive(false);
                LokiText5.SetActive(false);
                LokiText6.SetActive(false);
                LokiText7.SetActive(false);
                LokiText8.SetActive(false);
                LokiText9.SetActive(false);
            }

            //page 4 ...
            if (indexNumber.pageNumber == 3)
            {
                LokiText4.SetActive(true);

                //
                LokiText.SetActive(false);
                LokiText2.SetActive(false);
                LokiText3.SetActive(false);
                LokiText5.SetActive(false);
                LokiText6.SetActive(false);
                LokiText7.SetActive(false);
                LokiText8.SetActive(false);
                LokiText9.SetActive(false);
            }

            //page 5 ...
            if (indexNumber.pageNumber == 4)
            {
                LokiText5.SetActive(true);

                //
                LokiText.SetActive(false);
                LokiText2.SetActive(false);
                LokiText3.SetActive(false);
                LokiText4.SetActive(false);
                LokiText6.SetActive(false);
                LokiText7.SetActive(false);
                LokiText8.SetActive(false);
                LokiText9.SetActive(false);
            }

            //page 6 ...
            if (indexNumber.pageNumber == 5)
            {
                LokiText6.SetActive(true);

                //
                LokiText.SetActive(false);
                LokiText2.SetActive(false);
                LokiText3.SetActive(false);
                LokiText4.SetActive(false);
                LokiText5.SetActive(false);
                LokiText7.SetActive(false);
                LokiText8.SetActive(false);
                LokiText9.SetActive(false);
            }

            //page 7 ...
            if (indexNumber.pageNumber == 6)
            {
                LokiText7.SetActive(true);

                //
                LokiText.SetActive(false);
                LokiText2.SetActive(false);
                LokiText3.SetActive(false);
                LokiText4.SetActive(false);
                LokiText5.SetActive(false);
                LokiText6.SetActive(false);
                LokiText8.SetActive(false);
                LokiText9.SetActive(false);
            }

            //page 8 ...
            if (indexNumber.pageNumber == 7)
            {
                LokiText8.SetActive(true);

                //
                LokiText.SetActive(false);
                LokiText2.SetActive(false);
                LokiText3.SetActive(false);
                LokiText4.SetActive(false);
                LokiText5.SetActive(false);
                LokiText6.SetActive(false);
                LokiText7.SetActive(false);
                LokiText9.SetActive(false);
            }

            //page 9 ...
            if (indexNumber.pageNumber == 8)
            {
                LokiText9.SetActive(true);

                //
                LokiText.SetActive(false);
                LokiText2.SetActive(false);
                LokiText3.SetActive(false);
                LokiText4.SetActive(false);
                LokiText5.SetActive(false);
                LokiText6.SetActive(false);
                LokiText7.SetActive(false);
                LokiText8.SetActive(false);

            }

            //page 10 ...
            if (indexNumber.pageNumber == 9)
            {
                SceneManager.LoadScene("Credits");
            }

        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            print("Player is leaving Loki Guard");
            TextBox.SetActive(false);

            LokiText.SetActive(false);
            LokiText2.SetActive(false);
            LokiText3.SetActive(false);
            LokiText4.SetActive(false);
            LokiText5.SetActive(false);
            LokiText6.SetActive(false);
            LokiText7.SetActive(false);
            LokiText8.SetActive(false);
            LokiText9.SetActive(false);
        }
    }
}