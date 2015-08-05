using System;
using System.Collections.Generic;
using System.Data;
using PerceptiveMCAPI;
using PerceptiveMCAPI.Methods;
using PerceptiveMCAPI.Types;

namespace HSC_MC_INT
{
    // The integration layer class that interfaces with MailChimp
    public class MC_INT_Api
    {
        private string apikey; 

        // Constructor
        public MC_INT_Api()
        {
            // TODO: Put this api key in a config file
            apikey = "afc1608d4987349c6fc51cb186ca5616-us5";
        }

        // Push email list up to MailChimp email campaign
        public void list_push(DataTable dtbl, string lId)
        {
            var input = new listBatchSubscribeInput
                {
                    api_Validate = true,
                    api_AccessType = EnumValues.AccessType.Serial,
                    api_OutputType = EnumValues.OutputType.JSON,
                    parms =
                        {
                            apikey = apikey,
                            id = lId,
                            double_optin = false,
                            replace_interests = true,
                            update_existing = true
                        }
                };

            var batch = new List<Dictionary<string, object>>();

            foreach (DataRow drow in dtbl.Rows)
            {
                var entry = new Dictionary<string, object>
                    {
                        {"EMAIL", drow["email"].ToString()},
                        {"EMAIL_TYPE", "html"},
                        {"FNAME", drow["first"].ToString()},
                        {"LNAME", drow["_last"].ToString()},
                        {"NEXTPAY", DateTime.Today}
                    };

                batch.Add(entry);
            }
            input.parms.batch = batch;

            // execution
            var cmd = new listBatchSubscribe(input);
            cmd.Execute();

        } // end

        // Pull a list of campaigns from MailChimp
        public IEnumerable<KeyValuePair<string, string>> list_pull()
        {
            var result = new List<KeyValuePair<string, string>>();

            // Build input parameters (apiKey)
            var input = new listsInput(apikey);

            // Execute command
            var cmd = new lists(input);
            var output = cmd.Execute();

            foreach (var item in output.result)
            {
                result.Add(new KeyValuePair<string, string>(item.name, item.id));
            }

            return result;

        } // end

    } // end class

} // end namespace
