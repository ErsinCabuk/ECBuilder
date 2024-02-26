namespace ECBuilder.Types
{
    public enum FilterTypes
    {
        /// <summary>
        /// Entered value and Entity value equal 
        /// </summary>
        Equal,

        /// <summary>
        /// Entered value greater than Entity value
        /// </summary>
        GreaterThan,

        /// <summary>
        /// Entered value greater than or equal Entity value
        /// </summary>
        GreaterThanOrEqual,

        /// <summary>
        /// Entered value less than Entity value
        /// </summary>
        LessThan,

        /// <summary>
        /// Entered value less than or equal Entity value
        /// </summary>
        LessThanOrEqual,

        /// <summary>
        /// Entered array and Entity value equal
        /// </summary>
        EqualArray,

        /// <summary>
        /// Entered array and not entity value equal
        /// </summary>
        NotEqualArray,

        StringContains
    }
}
