namespace Reports.DAL.Entities
{
    /// <summary>
    /// Statuses of report.
    /// </summary>
    public enum ReportStatus
    {
        /// <summary>
        /// Represents draft report.
        /// </summary>
        Draft,

        /// <summary>
        /// Represents ready-to-review report.
        /// </summary>
        Ready,

        /// <summary>
        /// Represents closed report.
        /// </summary>
        Closed,
    }
}