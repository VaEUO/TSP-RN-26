using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.IO.Ports;
using NUnit.Framework.Constraints;


public class SerialController : MonoBehaviour
{
    public float speed = 5f;
    SerialPort serialPort;
    bool portOpen = false;

    void Start()
    {
        serialPort = new SerialPort("COM5", 9600);
        serialPort.ReadTimeout = 50;

        try
        {
            serialPort.Open();
            portOpen = true;
            Debug.Log("Puerto abierto");
        }
        catch (System.Exception ex)
        {
            Debug.LogError("Error en la comunicación" + ex.Message);
        }
    }


    void Update()
    {
        if (portOpen)
        {
            try
            {
                string[] data = serialPort.ReadLine().Trim().Split('|');
                float x = float.Parse(data[0]);
                float z = float.Parse(data[0]);

                Debug.Log($"X{x} Z:{z}");
                Vector3 movement = new Vector3(x, 0, z) * speed * Time.deltaTime;
                this.transform.Translate(movement);

            }
            catch (System.Exception ex)
            {
                Debug.LogError("Error en la lectura" + ex.Message);
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("colision con" + other.name);
        serialPort.Write("1");
    }
    private void OnTriggerExit(Collider other)
    {
        Debug.Log("sale de colision con" + other.name);
        serialPort.Write("0");
    }
}