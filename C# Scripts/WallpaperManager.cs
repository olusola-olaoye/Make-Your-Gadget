/*
 * 
 * Developed by Olusola Olaoye, 2021
 * 
 
 * 
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallpaperManager : MonoBehaviour
{
    private static WallpaperManager instance;

    public static WallpaperManager Instance
    {
        get
        {
            return instance;
        }
    }

    public const string off_screen = "Off Screen";
    
    public const string watch_os_1 = "Watch OS 1";

    public const string custom = "Custom";




    [SerializeField]
    private Texture watch_os_1_texture;


    private void Start()
    {
        if(instance == null)
        {
            instance = this;
        }
    }

    // get list of screen types
    public List<string> getScreenTypes()
    {
        return new List<string>()
        {
            off_screen, watch_os_1, custom
        };
    }

    // get based on wallpaper type
    public Texture getTextureFromWallpaperType(string wallpaper_type)
    {
        switch(wallpaper_type)
        {
          

            case watch_os_1:
               return watch_os_1_texture;

            default:
                return null;
        }
    }

}
