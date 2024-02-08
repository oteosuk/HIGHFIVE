using UnityEngine;

public class Define
{
    public static Color RedColor { get; private set; } = new Color(1.0f, 0.0f, 0.0f); // 빨간색
    public static Color GreenColor  {get; private set; } = new Color(0.0f, 1.0f, 0.0f); // 초록색
    public static Color BlueColor { get; private set; } = new Color(25.0f / 255, 126.0f / 255, 248.0f / 255); // 파랑색
    public enum Scene // 0번부터시작
    {
        IntroScene,
        StartScene,
        LobbyScene,
        RoomScene,
        SelectScene,
        GameScene
    }

    public enum UIEvent
    {
        Click,
        DoubleClick
    }

    public enum Object
    {
        Character,
        Monster
    }

    public enum Camp
    {
        Blue = 8,
        Red
    }

    public enum Layer
    {
        Default = 0,
        Monster = 6,
        Ground,
        Blue,
        Red
    }
    public enum Page
    {
        Battle,
        Farming
    }
}