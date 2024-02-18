using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class HealthBar : MonoBehaviour
{
    [SerializeField] private HealthSystem ObjectHealthSystem;

    private Transform BarTransform;
    private Transform SeparatorContainer;

    private void Awake()
    {
        BarTransform = transform.Find("Bar");
        SeparatorContainer = transform.Find("SeparatorContainer");

    }

    private void Start()
    {
        ConfigureHealthBarSeparators();

        ObjectHealthSystem.OnDamaged += ObjectHealthSystem_OnDamaged;
        ObjectHealthSystem.OnHealed += ObjectHealthSystem_OnHealed;
        ObjectHealthSystem.OnMaxHealthChanged += ObjectHealthSystem_OnMaxHealthChanged;

        UpdateHealthBar();
        UpdateHealthBarVisibility();
    }

    private void ObjectHealthSystem_OnMaxHealthChanged(object sender, System.EventArgs e)
    {
        ConfigureHealthBarSeparators();
    }

    private void ObjectHealthSystem_OnHealed(object sender, System.EventArgs e)
    {
        UpdateHealthBar();
        UpdateHealthBarVisibility();
    }

    private void ObjectHealthSystem_OnDamaged(object sender, System.EventArgs e)
    {
        UpdateHealthBar();
        UpdateHealthBarVisibility();
    }

    private void ConfigureHealthBarSeparators()
    {
        Transform SeparatorTemplate = SeparatorContainer.Find("SeparatorTemplate");
        SeparatorTemplate.gameObject.SetActive(false);

        foreach (Transform SeparatorTransform in SeparatorContainer)
        {
            if(SeparatorTransform == SeparatorTemplate)
            {
                continue;
            }
            Destroy(SeparatorTransform.gameObject);
        }

        int MaxHealth = ObjectHealthSystem.GetHealthAmountMax();
        int NumberOfSeparators = Mathf.FloorToInt(MaxHealth / 10);

        float BarSize = 3f;
        float Offset = BarSize / NumberOfSeparators;

        for (int i = 1; i < NumberOfSeparators; i++)
        {
            Transform NewSeparatorTemplate = Instantiate(SeparatorTemplate, SeparatorContainer);
            NewSeparatorTemplate.localPosition = new Vector3(Offset * i, 0, 0);

            NewSeparatorTemplate.gameObject.SetActive(true);
        }
    }

    private void UpdateHealthBar()
    {
        BarTransform.localScale = new Vector3(ObjectHealthSystem.GetHealthAmountNormalized(), 1.0f, 1.0f);
    }

    private void UpdateHealthBarVisibility()
    {
        if (ObjectHealthSystem.IsFullHealth()) { gameObject.SetActive(false); }
        else { gameObject.SetActive(true);}

        gameObject.SetActive(true);
    }
}
