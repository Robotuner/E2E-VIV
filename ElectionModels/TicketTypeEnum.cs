namespace ElectionModels
{
    public enum TicketTypeEnum
    {
        unspecified,
        stateSenator,
        stateLegislative
    }

    public static class TicketTypeEnumExtension
    {
        public static string ToDisplay(this TicketTypeEnum type)
        {
            switch (type)
            {
                case TicketTypeEnum.unspecified:
                    return "Undefined";
                case TicketTypeEnum.stateSenator:
                    return "State Senator";
                case TicketTypeEnum.stateLegislative:
                    return "State Legislative";
                default:
                    return "Undefined";
            }
        }
    }
}
