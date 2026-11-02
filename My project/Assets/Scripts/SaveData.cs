using System;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Saved position, rotation, and scale for one object.
/// </summary>
[Serializable]
public class TransformData
{
    public string saveID;
    public Vector3 localPosition;
    public Quaternion localRotation;
    public Vector3 localScale;
}

// Holds every saved object for the json file.
[Serializable]
public class SaveFile
{
    // JsonUtility can't save a list by itself so it has to sit inside a class
    public List<TransformData> objects = new List<TransformData>();
}