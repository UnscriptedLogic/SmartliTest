using System;
using System.Numerics;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ModifyType
{
    Add,
    Subtract,
    Multiply,
    Divide,
    Set
}

public class CurrencyHandler
{
    private float currentAmount;
    private float maxAmount;
    private float minAmount;

    private bool hasNoCap;

    public float MinValue => minAmount;
    public float Value => currentAmount;
    public float MaxValue => maxAmount;

    public event Action<ModifyType, float, float> OnModified;

    public CurrencyHandler(float currentAmount, float maxAmount = 0f)
    {
        this.currentAmount = currentAmount;
        this.maxAmount = maxAmount;

        hasNoCap = maxAmount == 0f;
    }

    public void Modify(ModifyType modifyType, float amount)
    {
        switch (modifyType)
        {
            case ModifyType.Add:
                currentAmount += amount;
                break;
            case ModifyType.Subtract:
                currentAmount -= amount;
                break;
            case ModifyType.Multiply:
                break;
            case ModifyType.Divide:
                break;
            case ModifyType.Set:
                break;
            default:
                break;
        }

        if (!hasNoCap && amount > maxAmount)
        {
            currentAmount = maxAmount;
        }

        if (amount < minAmount)
        {
            currentAmount = minAmount;
        }

        OnModified?.Invoke(modifyType, amount, currentAmount);
    }
}
