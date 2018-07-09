using System.Collections.Generic;
using System.Diagnostics;
using SimpleLdap.Attributes;

namespace SimpleLdap.Providers
{
    internal static class AttributeGroups
    {
        internal static Dictionary<LdapAttribute, string> EmailAttributes =>
            new Dictionary<LdapAttribute, string>
            {
                {LdapAttribute.EmailSendSize, "submissionContLength"},
                {LdapAttribute.EmailRecvSize, "delivContLength"},
                {LdapAttribute.EmailAcceptFromAuthenticatedOnly, "msExchRequireAuthToSendTo"},
                {LdapAttribute.EmailRejectFrom, "unauthOrig"},
                {LdapAttribute.EmailAcceptFrom, "authOrig"},
                {LdapAttribute.EmailSendOnBehalf, "publicDelegates"},
                {LdapAttribute.EmailForwardTo, "altRecipient"},
                {LdapAttribute.EmailDeliverAndRedirect, "deliverAndRedirect"},
                {LdapAttribute.ReciepientLimits, "msExchRecipLimit"},
                {LdapAttribute.EmailUseStoreDefaults, "mDBuseDefaults"},
                {LdapAttribute.EmailQuota, "mDBStorageQuota"},
                {LdapAttribute.EmailOverQuota, "mDBOverQuotaLimit"},
                {LdapAttribute.EmailOverHardQuota, "mDBOverHardQuotaLimit"},
                {LdapAttribute.EmailDeletedItemFlags, "deletedItemFlags"},
                {LdapAttribute.EmailGarbageCollectionPeriod, "garbageCollPeriod"},
                {LdapAttribute.EmailMobileAccess, "msExchOmaAdminWirelessEnable"},
                {LdapAttribute.EmailWebAccess, "protocolSettings"},

                {LdapAttribute.EmailAdminGroup, "msExchAdminGroup"},
                {LdapAttribute.EmailServerName, "msExchHomeServerName"},
                {LdapAttribute.EmailPoliciesExcluded, "msExchPoliciesExcluded"},
                {LdapAttribute.EmailHideFromAddressLists, "msExchHideFromAddressLists"}

            };

        internal static Dictionary<LdapAttribute, string> TerminalServicesAttributes =>
            new Dictionary<LdapAttribute, string>
            {
                {LdapAttribute.TerminalServicesAllowLogon, "tsAllowLogon"},
                {LdapAttribute.TerminalServicesProfilePath, "tsProfilePath"},
                {LdapAttribute.TerminalServicesHomeFolder, "tsHomeDir"},
                {LdapAttribute.TerminalServicesHomeDrive, "tsHomeDirDrive"},
                {LdapAttribute.TerminalServicesLoginScriptInherit, "tsInheritInitialProgram"},
                {LdapAttribute.TerminalServicesLoginScript, "tsIntialProgram"},
                {LdapAttribute.TerminalServicesWorkingDirectory, "tsWorkingDir"},
                {LdapAttribute.TerminalServicesConnectClientDrives, "tsDeviceClientDrives"},
                {LdapAttribute.TerminalServicesConnectClientPrinters, "tsDeviceClientPrinters"},
                {LdapAttribute.TerminalServicesConnectDefaultPrinter, "tsDeviceClientDefaultPrinter"},
                {LdapAttribute.TerminalServicesSessionDisconnect, "tsTimeOutSettingsDisConnections"},
                {LdapAttribute.TerminalServicesSessionActiveLimit, "tsTimeOutSettingsConnections"},
                {LdapAttribute.TerminalServicesSessionIdleLimit, "tsTimeOutSettingsIdle"},
                {LdapAttribute.TerminalServicesSessionTimeout, "tsBrokenTimeOutSettings"},
                {LdapAttribute.TerminalServicesSesionReconnection, "tsReConnectSettings"},
                {LdapAttribute.TerminalServicesRemoteControl, "tsShadowSettings"}
            };
        
        internal static Dictionary<LdapAttribute, string> Office365Attributes => 
        new Dictionary<LdapAttribute, string>
        {
            {LdapAttribute.Office365Group, "GroupMemberObjectId"},
            {LdapAttribute.Office365LigitationHoldEnabled, "LitigationHoldEnabled"},
            {LdapAttribute.Office365LigitationHoldDuration, "LitigationHoldDuration"},
            {LdapAttribute.Office365InPlaceArchiveEnabled, "InPlaceArchive"},
            {LdapAttribute.Office365InPlaceArchiveName, "ArchiveName"},
            {LdapAttribute.Office365LogonName, "O365userPrincipalName"},
        };

        internal static IDictionary<TKey, TValue> Concat<TKey, TValue>(this IDictionary<TKey, TValue> dict1,
            IDictionary<TKey, TValue> dict2)
        {
            var resultDict = new Dictionary<TKey, TValue>(dict1);
            foreach (var pair in dict2)
            {
                resultDict.Add(pair.Key, pair.Value);
            }

            return resultDict;
        }
    }
}