using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ImageSpriteSetter : MonoBehaviour
{
    public Image image;
    public Sprite spriteToSet;

    public void SetImage()
    {
        image.overrideSprite = spriteToSet;
    }
}
