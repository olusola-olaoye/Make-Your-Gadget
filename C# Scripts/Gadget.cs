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

public class Gadget : MonoBehaviour
{
    public ColorClass[] color_parts; 

    public GameObject[] spare_parts;

    public Text[] texts_parts;

    public GameObject avatar;

    public string category;

    protected virtual void OnEnable()
    {
        initializeColor();
        hideAvatar();
    }

    protected void hideAvatar()
    {
        if(avatar)
        {
            avatar.SetActive(false);
        }
    }

    protected void initializeColor()
    {
        foreach(ColorClass color_class in color_parts)
        {
            foreach(MeshRenderer mr in color_class.color_parts)
            {
                mr.material = new Material(Shader.Find("Standard"));
                mr.material.color = color_class.default_color;
                
                //to make material more shiny and metal-like
                mr.material.SetFloat("_Metallic", 0.77f);
                mr.material.SetFloat("_Smoothness", 0.68f);

            }
        }
    }

    public void showSparePartIndex(int index)
    {
        for(int i= 0; i < spare_parts.Length; i++)
        {
            spare_parts[i].SetActive(i == index);
        }
    }
}

[System.Serializable]
public class ColorClass 
{
    public string color_class_name;
    public MeshRenderer[] color_parts;
    public Color default_color;
}

[System.Serializable]
public class TextClass
{
    public string text_name;
    public Text text;
    
}
