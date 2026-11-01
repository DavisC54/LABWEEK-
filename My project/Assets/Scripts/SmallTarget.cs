using UnityEngine;

/// <summary>
/// Small and fast, worth the most.
/// </summary>
public class SmallTarget : TargetBuilder
{
    public SmallTarget(Target prefab, Vector2 position)
    {
        target = Object.Instantiate(prefab, position, Quaternion.identity);
    }

    public override void BuildMovement()
    {
        target.fallSpeed = 2.2f;
        target.zigzagWidth = 3f;
        target.zigzagSpeed = 5f;
    }

    public override void BuildSize()
    {
        target.transform.localScale = Vector3.one * 0.5f;
    }

    public override void BuildPoints()
    {
        target.points = 30;
    }
}