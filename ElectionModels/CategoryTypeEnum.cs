namespace ElectionModels
{
    public enum CategoryTypeEnum
    {
        undefined,
        measure,
        federal,
        state,
        legislative,
        judicial
    }

    public static class CategoryTypeEnumExtension
    {
        public static string ToDisplay(this CategoryTypeEnum type)
        {
            switch (type)
            {
                case CategoryTypeEnum.measure:
                    return "Measures / Propositions";
                case CategoryTypeEnum.federal:
                    return "Federal";
                case CategoryTypeEnum.state:
                    return "State";
                case CategoryTypeEnum.legislative:
                    return "Legislative";
                case CategoryTypeEnum.judicial:
                    return "Judicial";
                default:
                    return "Undefined";
            }
        }
    }
}
