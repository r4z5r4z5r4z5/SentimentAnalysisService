using System;

namespace Test.OpinionMining.Web
{
    public partial class ResultXml : PageBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            XmlToResponse( this.CurrentOpinionMiningOutputResult );

            this.CurrentOpinionMiningOutputResult = null;
        }
    }
}
