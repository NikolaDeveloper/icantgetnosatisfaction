using UnityEngine;
using UnityEditor;
using System.Collections;
using System.Collections.Generic;

public class PreProcessSpriteImport : AssetPostprocessor {

    private void OnPreprocessTexture()
    {
        if (AssetDatabase.LoadAssetAtPath<Object>(assetPath) != null)
        {
            return;
        }
        TextureImporter textureImporter = (TextureImporter)assetImporter;
        TextureImporterSettings settings = new TextureImporterSettings();
        textureImporter.ReadTextureSettings(settings);

        if (assetPath.Contains("Sprites"))
        {
            // its a sprite
            Debug.Log(assetPath + " imported as sprite");
            settings.mipmapEnabled = false;
            settings.maxTextureSize = 2048;
            settings.filterMode = FilterMode.Trilinear;
            settings.textureFormat = TextureImporterFormat.AutomaticTruecolor;
            settings.spritePixelsPerUnit = 1;
            settings.spriteAlignment = (int)SpriteAlignment.TopLeft;
            settings.spritePivot = Vector2.zero;
        }
        textureImporter.SetTextureSettings(settings);
    }
}