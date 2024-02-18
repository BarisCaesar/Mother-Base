using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BuildingTypeSelectUI : MonoBehaviour
{
   [SerializeField] private Sprite ArrowSprite;
   [SerializeField] private List<BuildingTypeSO> IgnoreBuildingTypeList;

   private Dictionary<BuildingTypeSO, Transform> ButtonTransformDictionary;
   private Transform ArrowButton;
   
   private void Awake()
   {
      BuildingTypeListSO BuildingTypeList = Resources.Load<BuildingTypeListSO>(nameof(BuildingTypeListSO));

      ButtonTransformDictionary = new Dictionary<BuildingTypeSO, Transform>();
      
      Transform BtnTemplate = transform.Find("BtnTemplate");
      BtnTemplate.gameObject.SetActive(false);
      
      ArrowButton = Instantiate(BtnTemplate, transform);
      ArrowButton.gameObject.SetActive(true);

      float OffsetAmount = 130.0f;

      ArrowButton.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, 0);
      ArrowButton.Find("Image").GetComponent<Image>().sprite = ArrowSprite;
      ArrowButton.Find("Image").GetComponent<RectTransform>().sizeDelta = new Vector2(0, -30);

      ArrowButton.GetComponent<Button>().onClick.AddListener(() =>
      {
         BuildingManager.Instance.SetActiveBuildingType(null);
         UpdateActiveBuildingTypeButton();
      });
      
      MouseEnterExitEvents MouseEvents = ArrowButton.GetComponent<MouseEnterExitEvents>();
      MouseEvents.OnMouseEnter += ((sender, args) =>
      {
         TooltipUI.Instance.Show("Arrow");
      });
         
      MouseEvents.OnMouseExit += ((sender, args) =>
      {
         TooltipUI.Instance.Hide();
      });

      int Index = 1;
      foreach (BuildingTypeSO BuildingType in BuildingTypeList.List)
      {
         if (IgnoreBuildingTypeList.Contains(BuildingType)) continue;
         
         Transform ButtonTransform = Instantiate(BtnTemplate, transform);
         ButtonTransform.gameObject.SetActive(true);

         OffsetAmount = 130.0f;

         ButtonTransform.GetComponent<RectTransform>().anchoredPosition = new Vector2(Index * OffsetAmount, 0);
         ButtonTransform.Find("Image").GetComponent<Image>().sprite = BuildingType.BuildingSprite;

         ButtonTransform.GetComponent<Button>().onClick.AddListener(() =>
         {
            BuildingManager.Instance.SetActiveBuildingType(BuildingType);
         });
         
        MouseEvents = ButtonTransform.GetComponent<MouseEnterExitEvents>();
         MouseEvents.OnMouseEnter += ((sender, args) =>
         {
            TooltipUI.Instance.Show(BuildingType.name + "\n" + BuildingType.GetConstructionResourceCostString());
         });
         
         MouseEvents.OnMouseExit += ((sender, args) =>
         {
            TooltipUI.Instance.Hide();
         });

         ButtonTransformDictionary[BuildingType] = ButtonTransform;
         Index++;

      }
      
   }

   private void OnOnMouseEnter(object sender, EventArgs e)
   {
      throw new NotImplementedException();
   }

   private void Start()
   {
      UpdateActiveBuildingTypeButton();
      BuildingManager.Instance.OnActiveBuildingTypeChanged += BuildingManager_OnActiveBuildingTypeChanged;

   }

   private void BuildingManager_OnActiveBuildingTypeChanged(object Sender,
      BuildingManager.OnActiveBuildingTypeChangedEventArgs e)
   {
      UpdateActiveBuildingTypeButton();
   }

   private void UpdateActiveBuildingTypeButton()
   {
      ArrowButton.Find("Selected").gameObject.SetActive(false);
      foreach (BuildingTypeSO BuildingType in ButtonTransformDictionary.Keys)
      {
         Transform ButtonTransform = ButtonTransformDictionary[BuildingType];
         ButtonTransform.Find("Selected").gameObject.SetActive(false);
      }

      BuildingTypeSO ActiveBuildingType = BuildingManager.Instance.GetActiveBuildingType();
      if (ActiveBuildingType == null)
      {
         ArrowButton.Find("Selected").gameObject.SetActive(true);
         return;
      }
      ButtonTransformDictionary[ActiveBuildingType].Find("Selected").gameObject.SetActive(true);

   }
   
   
}
