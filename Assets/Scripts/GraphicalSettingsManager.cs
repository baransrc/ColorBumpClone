using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GraphicalSettingsManager : MonoBehaviour
{
    [SerializeField] private bool _overrideResolution = false;
    [SerializeField] private int _mobileFrameRate = 60;
    [SerializeField] private int _editorFrameRate = 60;
    [SerializeField] private Vector2Int _mobileResolution = new Vector2Int(1280, 720);
    [SerializeField] private Vector2Int _editorResolution = new Vector2Int(1920, 1080);

    private void Awake()
    {
        FixGraphicalSettings();
    }

    private Vector2Int GetAspectRatio()
    {
        var height = Camera.main.pixelHeight;
        var width = Camera.main.pixelWidth;
        var ratio = (Mathf.CeilToInt(100f * (float)width / (float)height));
        var aspectRatio = new Vector2Int(16, 9);

        switch (ratio)
        {
            case 126:
            case 125:
            case 124: // 5:4
                aspectRatio.x = 5;
                aspectRatio.y = 4;
                break;

            case 134:
            case 133:
            case 132: // 4:3
                aspectRatio.x = 4;
                aspectRatio.y = 3;
                break;

            case 151:
            case 150:
            case 149: // 3:2
                aspectRatio.x = 3;
                aspectRatio.y = 2;
                break;

            case 162:
            case 161:
            case 160: // 16:10
                aspectRatio.x = 16;
                aspectRatio.y = 10;
                break;

            case 167:
            case 166:
            case 165: // 5:3
                aspectRatio.x = 5;
                aspectRatio.y = 3;
                break;

            case 179:
            case 178:
            case 177: // 16:9
                aspectRatio.x = 16;
                aspectRatio.y = 9;
                break;

            case 201:
            case 200:
            case 199: // 18:9
                aspectRatio.x = 18;
                aspectRatio.y = 9;
                break;

            case 218:
            case 217:
            case 216: // 13:6
                aspectRatio.x = 13;
                aspectRatio.y = 6;
                break;
        }

        return aspectRatio;
    }

    private void FixGraphicalSettings()
    {
        var frameRate = 60;
        var resolution = new Vector2Int(1920, 1080);
        var aspectRatio = GetAspectRatio();
        var multiplier = resolution.x / aspectRatio.x;

#if UNITY_ANDROID || UNITY_IOS
        frameRate = _mobileFrameRate;
        multiplier = _mobileResolution.x / aspectRatio.x;
        resolution = multiplier * aspectRatio;
#endif

#if UNITY_EDITOR
        frameRate = _editorFrameRate;
        multiplier = _editorResolution.x / aspectRatio.x;
        resolution = multiplier * aspectRatio;
#endif

        QualitySettings.vSyncCount = 0;
        Application.targetFrameRate = frameRate;
        if (!_overrideResolution) return;

        Screen.SetResolution(resolution.x, resolution.y, true);
    }
}
