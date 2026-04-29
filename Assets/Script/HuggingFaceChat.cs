using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using TMPro;
using System.Text;
using UnityEngine.Networking;
using UnityEditor.MPE;
using NUnit.Framework.Constraints;

public class HuggingFaceChat : MonoBehaviour
{
    [Header("UI")]
    public TMP_InputField inputField;
    public TMP_Text chatText;
    public Button enviarButton;

    [Header("Animación")]
    public Animator unityChanAnimator;

    [Header("HuggingFace Config")]
    [TextArea ] public string apiKey;
    private const string URL = "https://router.huggingface.co/v1/chat/completions";
    private const string MODELO = "openai/gpt-oss-120b:groq";
    private const string PERSONALIDAD = "Eres Unity-chan;una asistente virtual" + "Hablas con orgullo"+
        "Tus respuestas son cortas, menos de 10 palabras"+
        "Además de responder, analiza tu emoción y responde solo e  este formato JSON exacto:"+
        "{respuesta:texto que dirás, emoción:feliz o enojada o hablar}."+
        "No agregues nada fuera del JSON";

    void Start()
    {
        enviarButton.onClick.AddListener(EnviarMensaje);
    }

    public void EnviarMensaje ()
    {
        string mensaje= inputField.text;
        if(string.IsNullOrEmpty(mensaje) )
        {
            return;
        }
        inputField.text = "";
        StartCoroutine(EnviarRequest(mensaje));
    }
    
   IEnumerator EnviarRequest(string mensaje)
    {
        //Construir el JSON de forma segura con la clase de plantilla
        var requestData = new HFRequest
        {
            model = MODELO,
            max_tokens=1024,
            messages=new HFMessage[]
            {
                new HFMessage {role ="system", content=PERSONALIDAD },
                new HFMessage{role ="user", content=mensaje }
            }
        };

        string body=JsonUtility.ToJson(requestData);

        var request = new UnityWebRequest(URL, "POST")
        {
            uploadHandler =new UploadHandlerRaw(Encoding.UTF8.GetBytes(body)),
            downloadHandler=new DownloadHandlerBuffer()
        };
        request.SetRequestHeader("Content-Type", "application/json");
        request.SetRequestHeader("Authorization", "Bearer" + apiKey);
        yield return request.SendWebRequest();
    }

    // Request
    [System.Serializable]
    private class HFRequest
    {
        public string model;
        public int max_tokens;
        public HFMessage[] messages;
    }

    private class HFMessage
    {
        public string role;
        public string content;
    }

    //Response
    [System.Serializable]
    private class HFResponse
    {
        public Choice[] choices;
    }



    [System.Serializable]
    private class Choice
    {
        public HFMessage message;
    }

    [System.Serializable]
    private class RespuestaAI
    {
        public string respuesta;
        public string emocion;
    }


}
