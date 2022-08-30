/*
 * 
 * Developed by Olusola Olaoye, 2021
 * 
 * 
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BottomPanel : MonoBehaviour
{
    [SerializeField]
    private Button ok_button;

    protected virtual void OnEnable()
    {
        ok_button.onClick.RemoveAllListeners();
        ok_button.onClick.AddListener(()=>gameObject.SetActive(false));

        initializePanel();
    }

    public virtual void initializePanel() { }
}
