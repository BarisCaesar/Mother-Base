using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObjects/ResourceType")]
public class ResourceTypeSO : ScriptableObject
{
   public string NameString;
   public string NameShort;
   public Sprite ResourceSprite;
   public string ColorValueHex;

}
