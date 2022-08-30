/*
 * 
 * Developed by Olusola Olaoye, 2021
 * 
 * 
 */


using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ApplictionController : MonoBehaviour
{
    private static ApplictionController instance;
    public static ApplictionController Instance
    {
        get
        {
            return instance;
        }
    }
    public enum Appstate
    {
        Home,
        GadgetMaker,
    }
    public Appstate current_application_state
    {
        get;
        set;
    }

    [SerializeField]
    private Page[] pages;

    public string category
    {
        get;
        set;
    }
    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
        current_application_state = Appstate.Home;
    }
    private void Update()
    {
        updateCurrentScreen();
    }
    private void updateCurrentScreen()
    {
        foreach (Page page in pages)
        {
            page.screen.SetActive(page.app_state == current_application_state);
        }
    }
    [System.Serializable]
    private class Page
    {
        public Appstate app_state;
        public GameObject screen;
    }
}
