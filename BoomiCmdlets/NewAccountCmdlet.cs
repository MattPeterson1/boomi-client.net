using System;
using System.Management.Automation;

namespace Dell.Boomi.Cmdlets
{
    [Cmdlet(VerbsCommon.New, "BoomiAccount")]
    public class NewAccountCmdlet: BoomiBaseCmdlet
    {
        [Parameter(HelpMessage = "Expiration date for new account")]
        public DateTime ExpirationDate { get; set; }

        [Parameter(Mandatory = true, HelpMessage = "Short descriptive name for new account")]
        public string Name { get; set; }

        public NewAccountCmdlet()
        {
            // TODO: Seems that we can only create trial accounts.
            ExpirationDate = DateTime.UtcNow.AddDays(90);
        }

        protected override void ProcessRecord()
        {
            WriteObject(Client.CreateAccount(Name,ExpirationDate));
        }
    }
}
