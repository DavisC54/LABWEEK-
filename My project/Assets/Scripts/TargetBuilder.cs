using UnityEngine;

/// <summary>
/// Base for each target type. The subclasses fill in the build steps.
/// </summary>
public abstract class TargetBuilder
{
    protected Target target;

    public Target Target
    {
        get { return target; }
    }

    public abstract void BuildMovement();
    public abstract void BuildSize();
    public abstract void BuildPoints();
}