/*
 * 
 * Developed by Olusola Olaoye, 2021
 * 
 
 * 
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GadgetCategories
{
    public const string phones_label = "Phones";
    public const string laptops_label = "Laptops";
    public const string headphones_label = "Headphones";
    public const string tablets_label = "Tablets";
    public const string monitors_label = "Monitors";
    public const string watches_label = "Watches";

    public static List<string> getCategories()
    {
        return new List<string>() { phones_label, laptops_label, headphones_label, tablets_label, monitors_label, watches_label };
    }
}