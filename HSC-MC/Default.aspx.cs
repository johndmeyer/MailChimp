using System;
using HSC_MC_WS;

namespace HSC_MC
{
    public partial class Default : System.Web.UI.Page
    {
        protected void Page_PreInit(object sender, EventArgs e)
        {
            // Call the config service to execute the current configuration
            new ConfigService().UploadEmails();

        } // end

    } // end class

} // end namespace
