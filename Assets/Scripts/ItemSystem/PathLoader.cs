using UnityEngine;
using System.IO;

public class PathLoader : MonoBehaviour
{
    public static Sprite LoadSprite(string filePath)
    {
        if (!File.Exists(filePath))
        {
            Debug.LogError("File not found at path: " + filePath);
            return null;
        }

        byte[] fileData = File.ReadAllBytes(filePath);
        Texture2D texture = new Texture2D(2, 2);
        if (texture.LoadImage(fileData))
        {
            return Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), new Vector2(0.5f, 0.5f));
        }
        else
        {
            Debug.LogError("Failed to load texture from file: " + filePath);
            return null;
        }
    }
}
