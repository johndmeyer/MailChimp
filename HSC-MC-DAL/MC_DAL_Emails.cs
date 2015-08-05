using System.Data;

namespace HSC_MC_DAL
{
    // The data acccess layer class concered with getting emails from the database
    public class MC_DAL_Emails
    {
        private MC_DAL_Base _dalBase;

        public MC_DAL_Emails()
        {
            _dalBase = new MC_DAL_Base();
        }

        // Pull emails from specified Architype Accounts
        public DataTable pull_emls(string acnt1)
        {
            var sprcPrms = new[] { _dalBase.pram_make_strn(acnt1, "@acnt1") };

            var dtbl = _dalBase.sprc_exec("sp_mail_chmp_mail_Select", sprcPrms);

            return dtbl;

        } // end

    } // end class

} // end namespace
