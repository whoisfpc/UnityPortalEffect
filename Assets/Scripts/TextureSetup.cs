using UnityEngine;

public class TextureSetup : MonoBehaviour {
    public enum AntiAliasingLevel {
        level1 = 1,
        level2 = 2,
        level4 = 4,
        level8 = 8
    }
    public AntiAliasingLevel antiLevel = AntiAliasingLevel.level2;
    public Camera receiverCamera;
    public Material receiverCameraMat;

    int lastWidth;
    int lastHeight;

    // When game starts remove current camera textures and set new textures with the dimensions of the players screen
    void Start()
    {
        SetTexture();
    }

    /// <summary>
    /// Update is called every frame, if the MonoBehaviour is enabled.
    /// </summary>
    void Update()
    {
        if (lastWidth != Screen.width || lastHeight != Screen.height)
        {
            SetTexture();
        }
    }

    public void SetTexture()
    {
        if (receiverCamera.targetTexture != null)
        {
            receiverCamera.targetTexture.Release();
        }
        lastWidth = Screen.width;
        lastHeight = Screen.height;
        receiverCamera.targetTexture = new RenderTexture(lastWidth, lastHeight, 24);
        receiverCamera.targetTexture.antiAliasing = (int)antiLevel;
        receiverCameraMat.mainTexture = receiverCamera.targetTexture;
    }

}
