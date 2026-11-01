/// <summary>
/// Runs the build steps in order on whatever target type it gets.
/// </summary>
public class TargetDirector
{
    public void Construct(TargetBuilder builder)
    {
        builder.BuildMovement();
        builder.BuildSize();
        builder.BuildPoints();
    }
}