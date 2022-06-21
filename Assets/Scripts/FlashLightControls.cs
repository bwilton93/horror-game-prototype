using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlashLightControls : MonoBehaviour
{
    [SerializeField] GameObject flashlightLight;
    [SerializeField] private bool flashlightActive = false;
    [SerializeField] private bool pickedUp = false;

    [SerializeField] Material lensMaterial;
    [SerializeField] Material bulbMaterial;

    [SerializeField] GameObject playerHand;

    void Start()
    {
        flashlightLight = flashlightLight.gameObject;
        flashlightLight.SetActive(true);
        FlashlightEmitter();
    }

    void Update()
    {
        ToggleFlashlight();
        PickUp();
    }

    void ToggleFlashlight()
    {
        if(Input.GetKeyDown(KeyCode.F))
        {
            flashlightActive = !flashlightActive;
            if(!flashlightActive) 
            {
                Debug.Log(flashlightActive);
                flashlightLight.SetActive(true);
                FlashlightEmitter();
            } 
            else 
            {
                flashlightLight.SetActive(false);
                FlashlightEmitter();
            }
        }
    }

    void FlashlightEmitter() 
    {
        if(!flashlightActive) 
        {
            lensMaterial.SetColor("_EmissionColor", Color.white);
            bulbMaterial.SetColor("_EmissionColor", Color.white);
        }
        else
        {
            lensMaterial.SetColor("_EmissionColor", Color.black);
            bulbMaterial.SetColor("_EmissionColor", Color.black);
        }
    }

    void PickUp()
    {
        if(Input.GetKeyDown(KeyCode.E))
        {
            pickedUp = true;
        }

        if(pickedUp) 
        {
            transform.position = playerHand.transform.position;
        }
    }
}
