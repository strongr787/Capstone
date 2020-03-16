namespace Capstone.Actions
{
    /// <summary>
    /// A base class for all actions that can be taken by the assistant
    /// </summary>
    public abstract class Action
    {
        /// <summary>
        /// This is the full string use to pick out the action. It can be used to provided context to the action
        /// </summary>
        protected string CommandString { get; set; }
        public abstract void PerformAction();
    }
}
