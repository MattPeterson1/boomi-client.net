using System.Management.Automation;

namespace Dell.Boomi.Cmdlets
{
    [Cmdlet(VerbsCommon.Get, "BoomiEnvironment")]
    public class GetEnvironmentCmdlet : BoomiBaseCmdlet
    {
        [Parameter(Position = 0,
            ValueFromPipeline = true,
            ValueFromPipelineByPropertyName = true,
            HelpMessage = "ID of account to get")]
        public string Id { get; set; }

        protected override void ProcessRecord()
        {
            if (Id == null)
            {
                WriteObject(Client.GetAllEnvironments(),true);
            }
            else
            {
                WriteObject(Client.GetEnvironment(Id),true);
            }
        }
    }
}
