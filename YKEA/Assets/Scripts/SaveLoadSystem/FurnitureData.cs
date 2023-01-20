using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public enum FurnitureType
{
    Cube,
    Sphere,
    Cylinder
}

[System.Serializable]
public class FurnitureData
{
    public string id;
    public float px, py, pz;
    public float rx, ry, rz, rw;
    public float r, g, b, a;
    public FurnitureType type;

    public FurnitureData(string id, Vector3 position, Quaternion rotation, Color color)
    {
        this.id = id;

        this.px = position.x;
        this.py = position.y;
        this.pz = position.z;

        this.rx = rotation.x;
        this.ry = rotation.y;
        this.rz = rotation.z;
        this.rw = rotation.w;

        this.r = color.r;
        this.g = color.g;
        this.b = color.b;
        this.a = color.a;
    }

    public Vector3 GetPosition()
    {
        return new Vector3(px, py, pz);
    }

    public void SetPosition(Vector3 position)
    {
        px = position.x;
        py = position.y;
        pz = position.z;
    }

    public Quaternion GetRotation()
    {
        return new Quaternion(rx, ry, rz, rw);
    }

    public void SetRotation(Quaternion rotation)
    {
        rx = rotation.x;
        ry = rotation.y;
        rz = rotation.z;
        rw = rotation.w;
    }

    public Color GetColor()
    {
        return new Color(r, g, b, a);
    }

    public void SetColor(Color color)
    {
        r = color.r;
        g = color.g;
        b = color.b;
        a = color.a;
    }
}
