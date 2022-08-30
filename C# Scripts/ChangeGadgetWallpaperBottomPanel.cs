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
using System.IO;

public class ChangeGadgetWallpaperBottomPanel : BottomPanel
{

    [SerializeField]
    private ChangeGadgetWallpaperBottomPanelRow row_prefab;

    [SerializeField]
    private RectTransform row_container;


    [SerializeField]
    private FileChooserPanel file_chooser_prefab;


    protected override void OnEnable()
    {
        base.OnEnable();
    }

    public override void initializePanel()
    {
        base.initializePanel();
        // remove all rows that may be in this container
        foreach (ChangeGadgetWallpaperBottomPanelRow row in row_container.GetComponentsInChildren<ChangeGadgetWallpaperBottomPanelRow>())
        {
            Destroy(row.gameObject);
        }

        // create a row for every screen in the found gadget
        foreach (MeshRenderer screen in FindObjectOfType<GadgetWithScreen>().screens)
        {
            ChangeGadgetWallpaperBottomPanelRow row = Instantiate(row_prefab);
            row.transform.SetParent(row_container, false);
            row.title_text.text = screen.gameObject.name;

            row.dropdown.ClearOptions();
            row.dropdown.AddOptions(WallpaperManager.Instance.getScreenTypes()); // add all wallpaper types as options


            row.upload_image_button.gameObject.SetActive(isRowCustomValue(row)); // only show upload button when dropdown is set to custom

            row.dropdown.onValueChanged.AddListener(
                delegate 
                {
                    row.upload_image_button.gameObject.SetActive(isRowCustomValue(row)); // only show upload button when dropdown is set to custom


                    screen.material.mainTexture = WallpaperManager.Instance.getTextureFromWallpaperType(row.dropdown.options[row.dropdown.value].text);

                    // if there is a texture or it is custom then set material color to white. if texture is null, set material color to black
                    screen.material.color = screen.material.mainTexture || isRowCustomValue(row)? Color.white : Color.black;


                });

            row.upload_image_button.onClick.AddListener(()=> openFileChooser(screen));
        }


    }


    public bool isRowCustomValue(ChangeGadgetWallpaperBottomPanelRow row)
    {
        return row.dropdown.options[row.dropdown.value].text == WallpaperManager.custom;
    }
    
    private void openFileChooser(MeshRenderer screen)
    {
        FileChooserPanel file_chooser = Instantiate(file_chooser_prefab);

        file_chooser.transform.SetParent(FindObjectOfType<Canvas>().GetComponent<RectTransform>(), false);

        file_chooser.ok_button.onClick.AddListener(() => loadImage(file_chooser.chosen_file, screen,
                           file_chooser.gameObject));

        file_chooser.cancel_button.onClick.AddListener(() => Destroy(file_chooser.gameObject));

    }

    private void loadImage(string file, MeshRenderer screen, GameObject to_destroy)
    {
       
        byte[] image_in_bytes = File.ReadAllBytes(file);

        Texture2D image = new Texture2D(1, 1, TextureFormat.ARGB32, false);

        image.LoadImage(image_in_bytes);

        image.Apply();

        screen.material.mainTexture = image;

        Destroy(to_destroy);
    }

}
