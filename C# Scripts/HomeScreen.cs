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

public class HomeScreen : MonoBehaviour
{
    [SerializeField]
    private Dropdown category_dropdown;

    [SerializeField]
    private Button select_category_button;

    [SerializeField]
    private Image category_image;

    [SerializeField]
    private Sprite phones_wallpaper;

    [SerializeField]
    private Sprite tablet_wallpaper;

    [SerializeField]
    private Sprite laptop_wallpaper;

    [SerializeField]
    private Sprite headphones_wallpaper;

    [SerializeField]
    private Sprite watches_wallpaper;

    [SerializeField]
    private Sprite monitor_wallpaper;


    private void OnEnable()
    {
        category_dropdown.ClearOptions();
        category_dropdown.AddOptions(GadgetCategories.getCategories());

        select_category_button.onClick.AddListener(() => selectCategoryButton());

    }

    // when the select category button is clickes
    private void selectCategoryButton()
    {
        // set value of the category chosen
        ApplictionController.Instance.category = category_dropdown.options[category_dropdown.value].text;

        // change screen to gadget maker screen
        ApplictionController.Instance.current_application_state = ApplictionController.Appstate.GadgetMaker;

    }

    private void Update()
    {
        updateCategoryImage();
    }

    private void updateCategoryImage()
    {
        // updatw category images based on the text in the category dropdown
        switch(category_dropdown.options[category_dropdown.value].text)
        {
            case GadgetCategories.headphones_label:
                category_image.sprite = headphones_wallpaper;
                break;

            case GadgetCategories.tablets_label:
                category_image.sprite = tablet_wallpaper;
                break;

            case GadgetCategories.laptops_label:
                category_image.sprite = laptop_wallpaper;
                break;

            case GadgetCategories.phones_label:
                category_image.sprite = phones_wallpaper;
                break;

            case GadgetCategories.watches_label:
                category_image.sprite = watches_wallpaper;
                break;

            case GadgetCategories.monitors_label:
                category_image.sprite = monitor_wallpaper;
                break;


        }
    }
}
