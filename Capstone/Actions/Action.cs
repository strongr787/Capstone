using Windows.UI.Xaml.Controls;

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
        /// <summary>
        /// Used in the event the action requires sending data to the computer speakers
        /// </summary>
        protected MediaElement MediaElement { get; set; }
        public abstract void PerformAction();

        public void PerformAction(MediaElement element)
        {
            this.MediaElement = element;
            this.PerformAction();
        }
    }
}
