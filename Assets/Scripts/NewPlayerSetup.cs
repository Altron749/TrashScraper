using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class NewPlayerSetup : MonoBehaviour {

    public InputField nameField;
    public Button continueButton;

    private void Start()
    {
        continueButton.onClick.AddListener(nameAdded);
    }


    private void nameAdded()
    {
        if (nameField.textComponent.text!="")
        {
            DataManager.instance.SetName(nameField.textComponent.text);
            Destroy(gameObject);
        }
    }

}
