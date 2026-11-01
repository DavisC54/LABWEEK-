using UnityEngine;

/// <summary>
/// Middle target.
/// </summary>
public class MediumTarget : TargetBuilder
{
    public MediumTarget(Target prefab, Vector2 position)
    {
        target = Object.Instantiate(prefab, position, Quaternion.identity);
    }

    public override void BuildMovement()
    {
        target.fallSpeed = 1.5f;
        target.zigzagWidth = 2.5f;
        target.zigzagSpeed = 3f;
    }

    public override void BuildSize()
    {
        target.transform.localScale = Vector3.one * 0.8f;
    }

    public override void BuildPoints()
    {
        target.points = 20;
    }
}