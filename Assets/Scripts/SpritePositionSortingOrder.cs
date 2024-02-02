using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class SpritePositionSortingOrder : MonoBehaviour
{
   [SerializeField] private bool DoesRunOnce;
   [SerializeField] private float PositionOffsetY;
   
   private SpriteRenderer GameSpriteRenderer;
   private void Awake()
   {
      GameSpriteRenderer = GetComponent<SpriteRenderer>();

   }

   private void LateUpdate()
   {
      float PrecisionMultiplier = 5.0f;
      
      GameSpriteRenderer.sortingOrder = (int)(-(transform.position.y + PositionOffsetY) * PrecisionMultiplier);

      if (DoesRunOnce)
      {
         Destroy(this);
      }
   }
}
