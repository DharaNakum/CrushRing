using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New RingData", menuName = "RingData List")]
public class RingData : ScriptableObject
{
    public List<ColorData> colorData;
    public List<SizeData> sizeData;
    public List<int> ringsSize = new List<int>()
    { (int)SpriteType.SMALL,
      (int)SpriteType.MEDIUM,
      (int)SpriteType.LARGE,
      (int)SpriteType.SMALL * (int)SpriteType.MEDIUM,
      (int)SpriteType.SMALL * (int)SpriteType.LARGE,
      (int)SpriteType.MEDIUM * (int)SpriteType.LARGE};

    public Color GetColor(ColorType colorType)
    {
        return colorData.Find(x => x.colorType == colorType).color;
    }


    internal Sprite GetSpritThroughIndex(int sizeIndex)
    {
        return sizeData.Find(x => (int)x.spriteType == sizeIndex).sprite;
    }
    internal SpriteType GetSpritTypeThroughIndex(int sizeIndex)
    {
        return sizeData.Find(x => (int)x.spriteType == sizeIndex).spriteType;
    }
}
[System.Serializable]
public class ColorData
{
    public ColorType colorType;
    public Color color;
}
[System.Serializable]
public class SizeData
{
    public SpriteType spriteType;
    public Sprite sprite;

    
}

public enum SpriteType
{
    SMALL=3,
    MEDIUM=5,
    LARGE=7 
};
public enum ColorType
{
    WHITE,
    RED,
    GREEN,
    BLUE,
    YELLOW,
    PURPLE,
    PINK,
    SKYBLUE,
    CORAL,
    BROWN,
    TEAL
};
