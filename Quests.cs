using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.AI;
using UnityEngine.UI;

public class indexNumber : MonoBehaviour
{
    public static float pageNumber = 0f;
}

public class Quests : MonoBehaviour
{
    // MAKE MORE GAMEOBJECTS TO FIT NUMBER OF TEXTS NEEDED

    public GameObject TextBox;
    public GameObject PriestText;
    public GameObject PriestText2;
    public GameObject PriestText3;
    public GameObject PriestText4;
    public GameObject PriestText5;
    public GameObject PriestText6;
    public GameObject PriestText7;
    public GameObject PriestText8;
    public GameObject PriestText9;
    public GameObject PriestText10;



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
                PriestText.SetActive(true);

                //Have to hide the other texts. Place any new ones under this. 

                PriestText2.SetActive(false);
                PriestText3.SetActive(false);
                PriestText4.SetActive(false);
                PriestText5.SetActive(false);
                PriestText6.SetActive(false);
                PriestText7.SetActive(false);
                PriestText8.SetActive(false);
                PriestText9.SetActive(false);
                PriestText10.SetActive(false);
            }


            //page 2
            if (indexNumber.pageNumber == 1)
            {
                PriestText2.SetActive(true);

                // 
                PriestText.SetActive(false);
                PriestText3.SetActive(false);
                PriestText4.SetActive(false);
                PriestText5.SetActive(false);
                PriestText6.SetActive(false);
                PriestText7.SetActive(false);
                PriestText8.SetActive(false);
                PriestText9.SetActive(false);
                PriestText10.SetActive(false);
            }

            //page 3 ...
            if (indexNumber.pageNumber == 2)
            {
                PriestText3.SetActive(true);

                //
                PriestText.SetActive(false);
                PriestText2.SetActive(false);
                PriestText4.SetActive(false);
                PriestText5.SetActive(false);
                PriestText6.SetActive(false);
                PriestText7.SetActive(false);
                PriestText8.SetActive(false);
                PriestText9.SetActive(false);
                PriestText10.SetActive(false);
            }

            //page 4 ...
            if (indexNumber.pageNumber == 3)
            {
                PriestText4.SetActive(true);

                //
                PriestText.SetActive(false);
                PriestText2.SetActive(false);
                PriestText3.SetActive(false);
                PriestText5.SetActive(false);
                PriestText6.SetActive(false);
                PriestText7.SetActive(false);
                PriestText8.SetActive(false);
                PriestText9.SetActive(false);
                PriestText10.SetActive(false);
            }

            //page 5 ...
            if (indexNumber.pageNumber == 4)
            {
                PriestText5.SetActive(true);

                //
                PriestText.SetActive(false);
                PriestText2.SetActive(false);
                PriestText3.SetActive(false);
                PriestText4.SetActive(false);
                PriestText6.SetActive(false);
                PriestText7.SetActive(false);
                PriestText8.SetActive(false);
                PriestText9.SetActive(false);
                PriestText10.SetActive(false);
            }

            //page 6 ...
            if (indexNumber.pageNumber == 5)
            {
                PriestText6.SetActive(true);

                //
                PriestText.SetActive(false);
                PriestText2.SetActive(false);
                PriestText3.SetActive(false);
                PriestText4.SetActive(false);
                PriestText5.SetActive(false);
                PriestText7.SetActive(false);
                PriestText8.SetActive(false);
                PriestText9.SetActive(false);
                PriestText10.SetActive(false);
            }

            //page 7 ...
            if (indexNumber.pageNumber == 6)
            {
                PriestText7.SetActive(true);

                //
                PriestText.SetActive(false);
                PriestText2.SetActive(false);
                PriestText3.SetActive(false);
                PriestText4.SetActive(false);
                PriestText5.SetActive(false);
                PriestText6.SetActive(false);
                PriestText8.SetActive(false);
                PriestText9.SetActive(false);
                PriestText10.SetActive(false);
            }

            //page 8 ...
            if (indexNumber.pageNumber == 7)
            {
                PriestText8.SetActive(true);

                //
                PriestText.SetActive(false);
                PriestText2.SetActive(false);
                PriestText3.SetActive(false);
                PriestText4.SetActive(false);
                PriestText5.SetActive(false);
                PriestText6.SetActive(false);
                PriestText7.SetActive(false);
                PriestText9.SetActive(false);
                PriestText10.SetActive(false);
            }

            //page 9 ...
            if (indexNumber.pageNumber == 8)
            {
                PriestText9.SetActive(true);

                //
                PriestText.SetActive(false);
                PriestText2.SetActive(false);
                PriestText3.SetActive(false);
                PriestText4.SetActive(false);
                PriestText5.SetActive(false);
                PriestText6.SetActive(false);
                PriestText7.SetActive(false);
                PriestText8.SetActive(false);
                PriestText10.SetActive(false);
            }

            //page 10 ...
            if (indexNumber.pageNumber == 9)
            {
                PriestText10.SetActive(true);

                //
                PriestText.SetActive(false);
                PriestText2.SetActive(false);
                PriestText3.SetActive(false);
                PriestText4.SetActive(false);
                PriestText5.SetActive(false);
                PriestText6.SetActive(false);
                PriestText7.SetActive(false);
                PriestText8.SetActive(false);
                PriestText9.SetActive(false);
            }

        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            print("Player is leaving Priest");
            TextBox.SetActive(false);

            PriestText.SetActive(false);
            PriestText2.SetActive(false);
            PriestText3.SetActive(false);
            PriestText4.SetActive(false);
            PriestText5.SetActive(false);
            PriestText6.SetActive(false);
            PriestText7.SetActive(false);
            PriestText8.SetActive(false);
            PriestText9.SetActive(false);
            PriestText10.SetActive(false);
        }
    }
}
