using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WorldButtonController : MonoBehaviour {

    InputField field;   
    public void Selected()
    {
        
        this.GetComponent<Image>().color = Color.yellow;
    }
   
    public void Deselected()
    {
        this.GetComponent<Image>().color = Color.white;
    }
}
