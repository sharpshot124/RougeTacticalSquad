﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LibNoise.Generator;
using System;

[CreateAssetMenu(fileName = "New_VoronoiNearestNoise", menuName = "Custom/Noise/VoronoiNearest")]
public class VoronoiNearestGenerator : ScriptableObject, INoise
{
    [SerializeField] Voronoi _generator = null;
    public Voronoi Generator
    {
        get
        {
            if (_generator == null)
                _generator = new Voronoi(Frequency, Lacunarity, Seed, true);

            return _generator;
        }
    }

    [SerializeField] private float frequency;
    public float Frequency
    {
        get => frequency;
        set { frequency = value; _generator = null; }
    }

    [SerializeField] private float lacunarity;
    public float Lacunarity
    {
        get => lacunarity;
        set { lacunarity = value; _generator = null; }
    }

    public float Persistence
    {
        get => throw new NotImplementedException();
        set => throw new NotImplementedException();
    }

    public int Octaves
    {
        get => throw new NotImplementedException();
        set => throw new NotImplementedException();
    }

    [SerializeField] private int seed;
    public int Seed
    {
        get => seed;
        set { seed = value; _generator = null; }
    }

    public IEnumerable<float> GetBlock(Vector2 start, float sampleSize, Vector2Int size)
    {
        for (int y = 0; y < size.y; y++)
        {
            for (int x = 0; x < size.x; x++)
            {
                var pos = start + new Vector2(sampleSize * x, sampleSize * y);
                yield return GetValue(pos.x, pos.y);
            }
        }
    }

    public float GetValue(float x, float y)
    {
        return (float)Generator.GetValue(x, y, 0);
    }

    public Texture2D GetTexture(Vector2 start, float sampleSize, Vector2Int size)
    {
        Texture2D tex = new Texture2D(size.x, size.y);
        Color[] pixels = new Color[size.x * size.y];

        int i = 0;
        foreach (var value in GetBlock(Vector2.zero, .1f, size))
        {
            pixels[i++] = new Color(value, value, value);
        }

        tex.SetPixels(pixels);
        tex.Apply();
        return tex;
    }
}