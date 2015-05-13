using System.Management.Automation;

namespace Dell.Boomi.Cmdlets
{
    [Cmdlet(VerbsCommon.Get, "BoomiAccount")]
    public class GetAccountCmdlet : BoomiBaseCmdlet
    {
        [Parameter(HelpMessage = "Also return deleted accounts")]
        public bool ShouldIncludeDeleted { get; set; }

        [Parameter(Position = 0, 
            ValueFromPipeline = true,
            ValueFromPipelineByPropertyName = true, 
            HelpMessage = "ID of account to get")]
        
        public string Id { get; set; }

        protected override void ProcessRecord()
        {
            if (Id == null)
            {
                WriteObject(Client.GetAllAccounts(ShouldIncludeDeleted),true);
            }
            else
            {
                WriteObject(Client.GetAccount(Id));    
            }
        }
    }
}
