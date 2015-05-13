using System.Management.Automation;

namespace Dell.Boomi.Cmdlets
{
    [Cmdlet(VerbsCommon.Set, "BoomiEnvironment")]
    public class SetEnvironmentCmdlet : BoomiBaseCmdlet
    {
        [Parameter(Position = 0,
            Mandatory = true,
            ValueFromPipeline = true,
            ValueFromPipelineByPropertyName = true,
            HelpMessage = "ID of environment to modify")]
        public string Id { get; set; }
        
        [Parameter(HelpMessage = "Classification for environment.  Must be: \"PROD\" or \"TEST\"")]
        [ValidatePattern("PROD|TEST")]
        public string Classification { get; set; }

        [Parameter( HelpMessage = "Short descriptive name for environment")]
        public string Name { get; set; }

        protected override void ProcessRecord()
        {
            WriteObject(Client.SetEnvironment(Id, Name, Classification));
        }
    }
}
