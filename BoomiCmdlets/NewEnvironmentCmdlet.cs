using System.Management.Automation;

namespace Dell.Boomi.Cmdlets
{
   [Cmdlet(VerbsCommon.New, "BoomiEnvironment")]
    public class NewEnvironmentCmdlet : BoomiBaseCmdlet
    {
        [Parameter(Mandatory = true, HelpMessage = "Classification for new enviornment.  Must be: \"PROD\" or \"TEST\"")]
        [ValidatePattern("PROD|TEST")] 
       public string Classification { get; set; }

        [Parameter(Mandatory = true, HelpMessage = "Short descriptive name for new environment")]
        public string Name { get; set; }

        protected override void ProcessRecord()
        {
            WriteObject(Client.CreateEnvironment(Name, Classification));
        }
    }
}
