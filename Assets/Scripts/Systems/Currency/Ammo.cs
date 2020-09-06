﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ammo : MonoBehaviour, ICurrency
{
    [SerializeField]
    private float _currentAmmo;
    public float CurrentValue
    {
        get { return _currentAmmo; }
        set { _currentAmmo = value; }
    }
    [SerializeField]
    private float _maxAmmo;
    public float MaxValue
    {
        get { return _maxAmmo; }
        set { _maxAmmo = value; }
    }

    public void ChangeValue(float delta)
    {
        CurrentValue = Mathf.Clamp(CurrentValue + delta, 0, MaxValue);

    }
}