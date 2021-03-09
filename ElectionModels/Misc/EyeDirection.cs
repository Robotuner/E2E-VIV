namespace ElectionModels.Misc
{
    public enum EyeDirection
    {
        unknown,
        right,
        center,
        left
    }

    public static class EyeDirectionExtension
    {
        public static string ToDisplay(this EyeDirection eyeDirection)
        {
            switch (eyeDirection)
            {
                case EyeDirection.right: return "Looking right";
                case EyeDirection.center: return "Looking center";
                case EyeDirection.left: return "Looking left";
                default:
                    return "Unknown";
            }
        }
    }
}
