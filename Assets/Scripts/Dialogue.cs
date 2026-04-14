using UnityEngine;
using System.Collections.Generic;
using System.Collections;
using TMPro;
using UnityEngine.UI;
using static UnityEngine.GameObject;

public class Dialogue : MonoBehaviour
{

    [SerializeField]
    private GameObject dialogueCanvas; 



   [SerializeField]
    private TMP_Text speakerText;


    [SerializeField]
    private TMP_Text dialogueText;

    //[SerializeField]
    //private Image portraitImage;


    [SerializeField]
    private string[] speaker;


    [SerializeField]
    [TextArea]
    private string[] dialogueWords;

    
    //[SerializeField]
    //private Sprite[] portrait;

    private bool dialogueActivited;

    private int step =0; 


    // Start is called once before the first execution of Update after the MonoBehaviour is created
  

    // Update is called once per frame
    void Update()
    {

        if (Input.GetKeyDown(KeyCode.E) && dialogueActivited == true)
        {
            if (step >= dialogueWords.Length)
            {
                dialogueCanvas.SetActive(false);
                step = 0;
            }

            else
            {

                dialogueCanvas.SetActive(true);
                speakerText.text = speaker[0];
                dialogueText.text = dialogueWords[step];
                //portraitImage.sprite = portrait[0];
                step +=1 ;

            }



         

        }

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            dialogueActivited = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        dialogueCanvas.SetActive(false);
        dialogueActivited = false;
    }


}
