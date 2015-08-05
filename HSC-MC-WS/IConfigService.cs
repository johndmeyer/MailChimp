using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;

namespace HSC_MC_WS
{
    [ServiceContract(Name = "GetConfigs", Namespace = "HSC_MC_WS")]
    public interface IConfigService
    {
        [OperationContract]
        [WebInvoke(Method="GET", 
            UriTemplate ="GetConfigs/", 
            BodyStyle = WebMessageBodyStyle.Bare, 
            RequestFormat = WebMessageFormat.Json,
            ResponseFormat = WebMessageFormat.Json)]
        MailChimpConfigList GetConfigs();

        [OperationContract]
        [WebInvoke(Method = "GET",
            UriTemplate = "GetCampaigns/",
            BodyStyle = WebMessageBodyStyle.Bare,
            RequestFormat = WebMessageFormat.Json,
            ResponseFormat = WebMessageFormat.Json)]
        MailChimpCampaignList GetCampaigns();

        [OperationContract]
        [WebInvoke(Method = "GET",
            UriTemplate = "GetAccounts/",
            BodyStyle = WebMessageBodyStyle.Bare,
            RequestFormat = WebMessageFormat.Json,
            ResponseFormat = WebMessageFormat.Json)]
        MailChimpAccountList GetAccounts();

        [OperationContract]
        [WebInvoke(Method = "POST",
            UriTemplate = "SaveConfigs/",
            BodyStyle = WebMessageBodyStyle.Bare,
            RequestFormat = WebMessageFormat.Json,
            ResponseFormat = WebMessageFormat.Json)]
        bool SaveConfigs(List<MailChimpConfig> configs);

        [OperationContract]
        [WebInvoke(Method = "POST",
            UriTemplate = "DeleteConfig/{id}",
            BodyStyle = WebMessageBodyStyle.Bare,
            RequestFormat = WebMessageFormat.Json,
            ResponseFormat = WebMessageFormat.Json)]
        bool DeleteConfig(string id);

        [OperationContract]
        [WebInvoke(Method = "POST",
            UriTemplate = "SaveAndUploadEmails/",
            BodyStyle = WebMessageBodyStyle.Bare,
            RequestFormat = WebMessageFormat.Json,
            ResponseFormat = WebMessageFormat.Json)]
        bool SaveAndUploadEmails(List<MailChimpConfig> configs);

        [OperationContract]
        [WebInvoke(Method = "POST",
            UriTemplate = "UploadEmails/",
            BodyStyle = WebMessageBodyStyle.Bare,
            RequestFormat = WebMessageFormat.Json,
            ResponseFormat = WebMessageFormat.Json)]
        bool UploadEmails();

    } // end interface

    [Serializable]
    [DataContract(Name = "MailChimpAccount", Namespace = "HSC_MC_WS")]
    public class MailChimpAccount
    {
        [DataMember]
        public string AccountName { get; set; }

        [DataMember]
        public string AccountId { get; set; }

    } // end class

    [Serializable]
    [DataContract(Name = "MailChimpCampaign", Namespace = "HSC_MC_WS")]
    public class MailChimpCampaign
    {
        [DataMember]
        public string CampaignName { get; set; }

        [DataMember]
        public string CampaignId { get; set; }

    } // end class

    [Serializable]
    [DataContract(Name = "MailChimpConfig", Namespace = "HSC_MC_WS")]
    public class MailChimpConfig
    {
        [DataMember]
        public string Id { get; set; }

        [DataMember]
        public MailChimpCampaign Campaign { get; set; }

        [DataMember]
        public MailChimpAccount Account { get; set; }

    } // end class

    [Serializable]
    [DataContract(Name = "MailChimpConfigList", Namespace = "HSC_MC_WS")]
    public class MailChimpConfigList
    {
        [DataMember]
        public List<MailChimpConfig> ConfigList { get; set; }

    } // end class

    [Serializable]
    [DataContract(Name = "MailChimpAccountList", Namespace = "HSC_MC_WS")]
    public class MailChimpAccountList
    {
        [DataMember]
        public List<MailChimpAccount> AccountList { get; set; }

    } // end class

    [Serializable]
    [DataContract(Name = "MailChimpCampaignList", Namespace = "HSC_MC_WS")]
    public class MailChimpCampaignList
    {
        [DataMember]
        public List<MailChimpCampaign> CampaignList { get; set; } 

    } // end class

} // end namespace
