using UnityEngine;

/// <summary>
/// Big and slow, easy points but still costs a life if it gets through.
/// </summary>
public class LargeTarget : TargetBuilder
{
    public LargeTarget(Target prefab, Vector2 position)
    {
        target = Object.Instantiate(prefab, position, Quaternion.identity);
    }

    public override void BuildMovement()
    {
        target.fallSpeed = 0.9f;
        target.zigzagWidth = 1.5f;
        target.zigzagSpeed = 1.5f;
    }

    public override void BuildSize()
    {
        target.transform.localScale = Vector3.one * 1.2f;
    }

    public override void BuildPoints()
    {
        target.points = 10;
    }
}