using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SuccessText : MonoBehaviour
{
    public Text successText;
    public float count;

    private void Start()
    {
        successText.gameObject.SetActive(false);
    }


}
