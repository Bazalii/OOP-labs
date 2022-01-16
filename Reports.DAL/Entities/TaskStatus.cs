namespace Reports.DAL.Entities
{
    /// <summary>
    /// Represents task statuses.
    /// </summary>
    public enum TaskStatus
    {
        /// <summary>
        /// Represents open task.
        /// </summary>
        Open,

        /// <summary>
        /// Represents task with some changes.
        /// </summary>
        Active,

        /// <summary>
        /// Represents done task.
        /// </summary>
        Resolved,
    }
}