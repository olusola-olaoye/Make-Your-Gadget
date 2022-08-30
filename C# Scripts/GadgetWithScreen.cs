/*
 * 
 * Developed by Olusola Olaoye, 2021
 * 
 
 * 
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GadgetWithScreen : Gadget
{
    public MeshRenderer[] screens;

    protected override void OnEnable()
    {
        base.OnEnable();
        initializeScreen();
    }

    protected void initializeScreen()
    {
        // all screens should be black by default
        foreach (MeshRenderer screen in screens)
        {
            screen.material = new Material(Shader.Find("Standard"));
            screen.material.color = Color.black;


            //to make material more shiny and metal-like
            screen.material.SetFloat("_Metallic", 0.877f);
            screen.material.SetFloat("_Smoothness", 0.78f);
        }
    }
}
