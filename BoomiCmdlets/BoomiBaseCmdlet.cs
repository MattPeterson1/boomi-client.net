using System.Management.Automation;
using Dell.Boomi.Client;

namespace Dell.Boomi.Cmdlets
{
    public abstract class BoomiBaseCmdlet : Cmdlet
    {
        [Parameter(Mandatory = true, HelpMessage = "Your AtomSphere Account ID")]
        public string AccountId { get; set; }

        [Parameter(Mandatory = true, HelpMessage = "Your AtomSphere username and password")]
        [Credential]
        public PSCredential Credential { get; set; }

        protected override void BeginProcessing()
        {
            Client = new BoomiClient(AccountId,Credential.GetNetworkCredential());
        }

        protected BoomiClient Client;

    }   
}
