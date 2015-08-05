using System;
using System.Collections.Generic;
using System.Data;
using System.ServiceModel.Web;
using System.Web;
using HSC_MC_Common;
using HSC_MC_DAL;
using HSC_MC_INT;

namespace HSC_MC_WS
{
    [System.ServiceModel.ServiceBehavior(IncludeExceptionDetailInFaults = true)]
    public class ConfigService : IConfigService
    {
        private readonly MC_INT_Api _mailChimpApiIntegration;
        private readonly MC_DAL_Config _mcDalConfig;

        // Constructor
        public ConfigService()
        {
            var ctx = WebOperationContext.Current;

            if (ctx != null)
            {
                ctx.OutgoingResponse.Headers.Add("Access-Control-Allow-Origin", "*");
                ctx.OutgoingResponse.Headers.Add("Access-Control-Allow-Methods", "GET, POST, DELETE, OPTIONS");
            }

            _mailChimpApiIntegration = new MC_INT_Api();
            _mcDalConfig = new MC_DAL_Config();
            
        } // end

        // Get configurations from database
        public MailChimpConfigList GetConfigs()
        {
            var result = new MailChimpConfigList{ConfigList = new List<MailChimpConfig>()};

            try
            { 
                var configDataTable = _mcDalConfig.get_cnfg();

                foreach (DataRow row in configDataTable.Rows)
                {
                    result.ConfigList.Add(
                        new MailChimpConfig
                            {
                                Id = row[0].ToString(),
                                Campaign = new MailChimpCampaign { CampaignId = row["mail_chmp_lstid"].ToString(), CampaignName = row["mail_chmp_lstn"].ToString()},
                                Account = new MailChimpAccount { AccountId = row["mail_chmp_acntn"].ToString(), AccountName  = row["mail_chmp_acnt"].ToString()}
                            });
                }
            }
            catch(Exception ex)
            {
                LoggingHelper.WriteLog(Constants.LogType.Error, string.Empty, ex);
            }
            
            return result;

        } // end

        // Get a list of accounts from the database
        public MailChimpAccountList GetAccounts()
        {
            var result = new MailChimpAccountList {AccountList = new List<MailChimpAccount>()};

            var accountList = _mcDalConfig.get_acnt();

            foreach (DataRow row in accountList.Rows)
            {
                result.AccountList.Add
                    (
                        new MailChimpAccount
                            {
                                AccountId = row["AccountId"].ToString(),
                                AccountName = row["AccountName"].ToString()
                            });
            }

            return result;

        } // end

        // Get a list of campaigns from MailChimp
        public MailChimpCampaignList GetCampaigns()
        {
            var result = new MailChimpCampaignList{CampaignList = new List<MailChimpCampaign>()};

            var campaignList = _mailChimpApiIntegration.list_pull();

            foreach (var campaign in campaignList)
            {
                result.CampaignList.Add(
                    new MailChimpCampaign
                        {
                            CampaignId = campaign.Value,
                            CampaignName = campaign.Key
                        });
            }

            return result;

        } // end

        // Save the configurations
        public bool SaveConfigs(List<MailChimpConfig> configs)
        {
            if (configs == null)
            {
                return false;
            }

            try
            {
                foreach (var config in configs)
                {
                    if (config.Id == null)
                    {
                        // Insert new row
                        _mcDalConfig.cret_cnfg(int.Parse(config.Account.AccountId), config.Account.AccountName, config.Campaign.CampaignId, config.Campaign.CampaignName);
                    }
                    else
                    {
                        // Update row
                        _mcDalConfig.updt_cnfg(int.Parse(config.Id), int.Parse(config.Account.AccountId), config.Account.AccountName, config.Campaign.CampaignId, config.Campaign.CampaignName);
                    }
                }
            }
            catch (Exception)
            {

                return false;
            }

            return true;

        } // end

        // Delete a configuration
        public bool DeleteConfig(string id)
        {
            try
            {
                _mcDalConfig.delt_cnfg(int.Parse(id));
            }
            catch (Exception)
            {
                return false;
            }

            return true;

        } // end

        /// <summary>
        /// Saves and uploads the emails to Mailchimp
        /// </summary>
        /// <param name="configs"></param>
        /// <returns></returns>
        /// <remarks>might want to move this to another service as it does not deal with configs</remarks>
        public bool SaveAndUploadEmails(List<MailChimpConfig> configs)
        {
            if(configs == null)
            {
                return false;
            }

            if (SaveConfigs(configs))
            {
                UploadEmails();
            }
            else
            {
                return false;
            }

            return true;
        }

        public bool UploadEmails()
        {
            try
            {
                var configDataTable = _mcDalConfig.get_cnfg();

                foreach (DataRow row in configDataTable.Rows)
                {
                    // Setup varibles based on configuration
                    var acnt1 = row["mail_chmp_acntn"].ToString();
                    var lstid = row["mail_chmp_lstid"].ToString();

                    // Call the API to push the emails to Mail Chimp
                    _mailChimpApiIntegration.list_push(new MC_DAL_Emails().pull_emls(acnt1), lstid);
                }
            }
            catch (Exception ex)
            {
                LoggingHelper.WriteLog(Constants.LogType.Error, string.Empty, ex);

                return false;
            }

            return true;
        }

    } // end class

} // end namespace
