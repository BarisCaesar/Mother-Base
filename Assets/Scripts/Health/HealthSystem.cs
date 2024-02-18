using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthSystem : MonoBehaviour
{
    [SerializeField] private int HealthAmountMax;

    public event EventHandler OnDamaged;
    public event EventHandler OnHealed;
    public event EventHandler OnDied;
    public event EventHandler OnMaxHealthChanged;

    private int HealthAmount;

    private void Awake()
    {
        HealthAmount = HealthAmountMax;
    }

    public void Damage(int DamageAmount)
    {
        HealthAmount -= DamageAmount;
        HealthAmount = Mathf.Clamp(HealthAmount, 0, HealthAmountMax);

        if(OnDamaged != null)
        {
            OnDamaged(this, EventArgs.Empty);
        }

        if(IsDead())
        {
            if (OnDied != null)
            {
                OnDied(this, EventArgs.Empty);
            }
        }
    }

    public void Heal(int HealthAmount)
    {   
        HealthAmount += HealthAmount;
        HealthAmount = Mathf.Clamp(HealthAmount, 0, HealthAmountMax);

        if (OnHealed != null)
        {
            OnHealed(this, EventArgs.Empty);
        }
    
    }

    public void HealFull()
    {
        HealthAmount = HealthAmountMax;

        if (OnHealed != null)
        {
            OnHealed(this, EventArgs.Empty);
        }
    }

    public bool IsDead()
    {
        return HealthAmount == 0;
    }

    public bool IsFullHealth()
    {
        return HealthAmount == HealthAmountMax;
    }

    public int GetHealthAmount() { return HealthAmount; }

    public float GetHealthAmountNormalized() { return (float)HealthAmount / HealthAmountMax; }

    public int GetHealthAmountMax() { return HealthAmountMax; }

    public void SetMaxHealth(int BuildingHealthAmount, bool UpdateHealthAmount = false)
    {  
        HealthAmountMax = BuildingHealthAmount;
        if (UpdateHealthAmount) { HealthAmount = BuildingHealthAmount; }

        if(OnMaxHealthChanged != null) { OnMaxHealthChanged(this, EventArgs.Empty); }
        
    }



}
