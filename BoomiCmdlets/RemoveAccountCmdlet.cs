using System.Management.Automation;

namespace Dell.Boomi.Cmdlets
{
    [Cmdlet(VerbsCommon.Remove, "BoomiAccount")]
    public class RemoveAccountCmdlet : BoomiBaseCmdlet
    {
        [Parameter(Position = 0,
            Mandatory = true,
            ValueFromPipeline = true,
            ValueFromPipelineByPropertyName = true,
            HelpMessage = "ID of account to remove")]
        public string Id { get; set; }

        protected override void ProcessRecord()
        {
            Client.DeleteAccount(Id);
        }
    }
}
