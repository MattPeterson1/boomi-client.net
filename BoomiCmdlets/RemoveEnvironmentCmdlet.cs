using System.Management.Automation;

namespace Dell.Boomi.Cmdlets
{
    [Cmdlet(VerbsCommon.Remove, "BoomiEnvironment")]
    public class RemoveEnvironmentCmdlet : BoomiBaseCmdlet
    {
        [Parameter(Position = 0,
            Mandatory = true,
            ValueFromPipeline = true,
            ValueFromPipelineByPropertyName = true,
            HelpMessage = "ID of environment to remove")]
        public string Id { get; set; }

        protected override void ProcessRecord()
        {
            Client.DeleteEnvironment(Id);
        }
    }
}
