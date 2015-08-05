using System;
using System.Data;
using System.Data.Common;

namespace HSC_MC_DAL
{
    /// <summary>
    /// The data access layer class concerned with maipulating the config table
    /// </summary>
    public class MC_DAL_Config
    {
        private MC_DAL_Base _dalBase;

        // Constructor
        public MC_DAL_Config()
        {
            _dalBase = new MC_DAL_Base();

        } // end

        // Create a new configuration
        public bool cret_cnfg(int accountId, string accountName, string campaignId, string campaignName)
        {
            try
            {
                var sprcPrms = new[]
                    {
                        _dalBase.pram_make_inte(accountId, "@acccountNumber"),
                        _dalBase.pram_make_strn(accountName, "@accountName"),
                        _dalBase.pram_make_strn(campaignId, "@listId"),
                        _dalBase.pram_make_strn(campaignName, "@listName")
                    };

                _dalBase.nonq_exec("sp_mail_chmp_cnfg_Insert", sprcPrms);
            }
            catch (Exception)
            {
                return false;
            }

            return true;

        } // end

        // Update a configuration
        public bool updt_cnfg(int id, int accountId, string accountName, string campaignId, string campaignName)
        {
            try
            {
                var sprcPrms = new[] { 
                    _dalBase.pram_make_inte(id, "@id"),
                    _dalBase.pram_make_inte(accountId, "@acccountNumber"),
                    _dalBase.pram_make_strn(accountName, "@accountName"),
                    _dalBase.pram_make_strn(campaignId, "@listId"),
                    _dalBase.pram_make_strn(campaignName, "@listName")
                };  

                _dalBase.nonq_exec("sp_mail_chmp_cnfg_Update", sprcPrms);
            }
            catch (Exception)
            {
                return false;
            }

            return true;

        } // end

        // Delete a configuration
        public bool delt_cnfg(int id)
        {
            try
            {
                var sprcPrms = new[]
                    {
                        _dalBase.pram_make_inte(id, "@id")
                    };

                _dalBase.nonq_exec("sp_mail_chmp_cnfg_Delete", sprcPrms);
            }
            catch (Exception)
            {
                return false;
            }

            return true;

        } // end

        // Get configurations from database
        public DataTable get_cnfg()
        {
            try
            {
                _dalBase = new MC_DAL_Base();
                var sprcPrms = new DbParameter[] { };
                var dtbl = _dalBase.sprc_exec("sp_mail_chmp_cnfg_Select", sprcPrms);
                return dtbl;
            }
            catch (Exception)
            {
                return null;
            }
            

        } // end
        
        // Get accounts from database
        public DataTable get_acnt()
        {
            try
            {
                _dalBase = new MC_DAL_Base();
                var sprcPrms = new DbParameter[] { };
                var dtbl = _dalBase.sprc_exec("sp_mail_chmp_acnt_Select", sprcPrms);
                return dtbl;
            }
            catch (Exception)
            {
                return null;
            }
        }

    } // end class

} // end namespace
