using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.AI;
using UnityEngine.UI;


public class HeimdallQuests : MonoBehaviour
{
    // MAKE MORE GAMEOBJECTS TO FIT NUMBER OF TEXTS NEEDED

    public GameObject TextBox;
    public GameObject HeimdallText;
    public GameObject HeimdallText2;
    public GameObject HeimdallText3;
    public GameObject HeimdallText4;
    public GameObject HeimdallText5;
    public GameObject HeimdallText6;
    public GameObject HeimdallText7;
    public GameObject HeimdallText8;
    public GameObject HeimdallText9;



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
                HeimdallText.SetActive(true);

                //Have to hide the other texts. Place any new ones under this. 

                HeimdallText2.SetActive(false);
                HeimdallText3.SetActive(false);
                HeimdallText4.SetActive(false);
                HeimdallText5.SetActive(false);
                HeimdallText6.SetActive(false);
                HeimdallText7.SetActive(false);
                HeimdallText8.SetActive(false);
                HeimdallText9.SetActive(false);
            }


            //page 2
            if (indexNumber.pageNumber == 1)
            {
                HeimdallText2.SetActive(true);

                // 
                HeimdallText.SetActive(false);
                HeimdallText3.SetActive(false);
                HeimdallText4.SetActive(false);
                HeimdallText5.SetActive(false);
                HeimdallText6.SetActive(false);
                HeimdallText7.SetActive(false);
                HeimdallText8.SetActive(false);
                HeimdallText9.SetActive(false);
            }

            //page 3 ...
            if (indexNumber.pageNumber == 2)
            {
                HeimdallText3.SetActive(true);

                //
                HeimdallText.SetActive(false);
                HeimdallText2.SetActive(false);
                HeimdallText4.SetActive(false);
                HeimdallText5.SetActive(false);
                HeimdallText6.SetActive(false);
                HeimdallText7.SetActive(false);
                HeimdallText8.SetActive(false);
                HeimdallText9.SetActive(false);
            }

            //page 4 ...
            if (indexNumber.pageNumber == 3)
            {
                HeimdallText4.SetActive(true);

                //
                HeimdallText.SetActive(false);
                HeimdallText2.SetActive(false);
                HeimdallText3.SetActive(false);
                HeimdallText5.SetActive(false);
                HeimdallText6.SetActive(false);
                HeimdallText7.SetActive(false);
                HeimdallText8.SetActive(false);
                HeimdallText9.SetActive(false);
            }

            //page 5 ...
            if (indexNumber.pageNumber == 4)
            {
                HeimdallText5.SetActive(true);

                //
                HeimdallText.SetActive(false);
                HeimdallText2.SetActive(false);
                HeimdallText3.SetActive(false);
                HeimdallText4.SetActive(false);
                HeimdallText6.SetActive(false);
                HeimdallText7.SetActive(false);
                HeimdallText8.SetActive(false);
                HeimdallText9.SetActive(false);
            }

            //page 6 ...
            if (indexNumber.pageNumber == 5)
            {
                HeimdallText6.SetActive(true);

                //
                HeimdallText.SetActive(false);
                HeimdallText2.SetActive(false);
                HeimdallText3.SetActive(false);
                HeimdallText4.SetActive(false);
                HeimdallText5.SetActive(false);
                HeimdallText7.SetActive(false);
                HeimdallText8.SetActive(false);
                HeimdallText9.SetActive(false);
            }

            //page 7 ...
            if (indexNumber.pageNumber == 6)
            {
                HeimdallText7.SetActive(true);

                //
                HeimdallText.SetActive(false);
                HeimdallText2.SetActive(false);
                HeimdallText3.SetActive(false);
                HeimdallText4.SetActive(false);
                HeimdallText5.SetActive(false);
                HeimdallText6.SetActive(false);
                HeimdallText8.SetActive(false);
                HeimdallText9.SetActive(false);
            }

            //page 8 ...
            if (indexNumber.pageNumber == 7)
            {
                HeimdallText8.SetActive(true);

                //
                HeimdallText.SetActive(false);
                HeimdallText2.SetActive(false);
                HeimdallText3.SetActive(false);
                HeimdallText4.SetActive(false);
                HeimdallText5.SetActive(false);
                HeimdallText6.SetActive(false);
                HeimdallText7.SetActive(false);
                HeimdallText9.SetActive(false);
            }

            //page 9 ...
            if (indexNumber.pageNumber == 8)
            {
                HeimdallText9.SetActive(true);

                //
                HeimdallText.SetActive(false);
                HeimdallText2.SetActive(false);
                HeimdallText3.SetActive(false);
                HeimdallText4.SetActive(false);
                HeimdallText5.SetActive(false);
                HeimdallText6.SetActive(false);
                HeimdallText7.SetActive(false);
                HeimdallText8.SetActive(false);

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

            HeimdallText.SetActive(false);
            HeimdallText2.SetActive(false);
            HeimdallText3.SetActive(false);
            HeimdallText4.SetActive(false);
            HeimdallText5.SetActive(false);
            HeimdallText6.SetActive(false);
            HeimdallText7.SetActive(false);
            HeimdallText8.SetActive(false);
            HeimdallText9.SetActive(false);
        }
    }
}