namespace Capstone.Actions
{
    public class VoiceMemoAction : Action
    {
        private string VoiceMemoTitle { get; set; }

        public VoiceMemoAction(string CommandString)
        {
            this.CommandString = CommandString;
        }

        private void AskForVoiceMemoTitle()
        {
            // TODO
        }

        public override void PerformAction()
        {
            // TODO start recording voice and show controls on the dynamic area
        }
    }
}
