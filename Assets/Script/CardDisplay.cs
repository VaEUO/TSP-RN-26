using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Firebase.Extensions;
using Firebase.Firestore;
using System.Threading.Tasks;
using UnityEngine.Networking;
using System;



public class CardDisplay : MonoBehaviour
{
    public TMP_Text displayText;
    public Firestoreinicialize cardAcces;
    void Start()
    {
        cardAcces = GameObject.FindGameObjectWithTag("BD").GetComponent<Firestoreinicialize>();
    }

    public void OnTargetFound(Transform imageTargetTransform)
    {
        string cardName=imageTargetTransform.name;
        Debug.Log($"Carta encontrada {cardName}");

        displayText = imageTargetTransform.Find("Text").GetComponent<TextMeshPro>();
        if (displayText == null )
        {
            //Actividad 2 recuperar datos desde firebase
            cardAcces.FetchCardDataFromFirestore(cardName, displayText);

        }
        else
        {
            Debug.LogError("Objeto de texto no encontrado");
        }
    }
    public void OnTargetLost(Transform imageTargetTransform)
    {
        displayText = imageTargetTransform.GetComponent("Text").GetComponent<TextMeshPro>();
        if(displayText == null )
        {
            displayText.text = "Buscando carta...";
        }
        else
        {
            Debug.LogError("Objeto de texto no encontrad");
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
