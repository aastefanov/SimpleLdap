using System.Collections.Generic;
using SimpleLdap.Attributes;
using static SimpleLdap.Attributes.LdapAttribute;

namespace SimpleLdap.Providers
{
    internal static class AttributeGroups
    {
        internal static Dictionary<LdapAttribute, string> EmailAttributes =>
            new Dictionary<LdapAttribute, string>
            {
                {EmailSendSize, "submissionContLength"},
                {EmailRecvSize, "delivContLength"},
                {EmailAcceptFromAuthenticatedOnly, "msExchRequireAuthToSendTo"},
                {EmailRejectFrom, "unauthOrig"},
                {EmailAcceptFrom, "authOrig"},
                {EmailSendOnBehalf, "publicDelegates"},
                {EmailForwardTo, "altRecipient"},
                {EmailDeliverAndRedirect, "deliverAndRedirect"},
                {ReciepientLimits, "msExchRecipLimit"},
                {EmailUseStoreDefaults, "mDBuseDefaults"},
                {EmailQuota, "mDBStorageQuota"},
                {EmailOverQuota, "mDBOverQuotaLimit"},
                {EmailOverHardQuota, "mDBOverHardQuotaLimit"},
                {EmailDeletedItemFlags, "deletedItemFlags"},
                {EmailGarbageCollectionPeriod, "garbageCollPeriod"},
                {EmailMobileAccess, "msExchOmaAdminWirelessEnable"},
                {EmailWebAccess, "protocolSettings"},

                {EmailAdminGroup, "msExchAdminGroup"},
                {EmailServerName, "msExchHomeServerName"},
                {EmailPoliciesExcluded, "msExchPoliciesExcluded"},
                {EmailHideFromAddressLists, "msExchHideFromAddressLists"}

            };

        internal static Dictionary<LdapAttribute, string> TerminalServicesAttributes =>
            new Dictionary<LdapAttribute, string>
            {
                {TerminalServicesAllowLogon, "tsAllowLogon"},
                {TerminalServicesProfilePath, "tsProfilePath"},
                {TerminalServicesHomeFolder, "tsHomeDir"},
                {TerminalServicesHomeDrive, "tsHomeDirDrive"},
                {TerminalServicesLoginScriptInherit, "tsInheritInitialProgram"},
                {TerminalServicesLoginScript, "tsIntialProgram"},
                {TerminalServicesWorkingDirectory, "tsWorkingDir"},
                {TerminalServicesConnectClientDrives, "tsDeviceClientDrives"},
                {TerminalServicesConnectClientPrinters, "tsDeviceClientPrinters"},
                {TerminalServicesConnectDefaultPrinter, "tsDeviceClientDefaultPrinter"},
                {TerminalServicesSessionDisconnect, "tsTimeOutSettingsDisConnections"},
                {TerminalServicesSessionActiveLimit, "tsTimeOutSettingsConnections"},
                {TerminalServicesSessionIdleLimit, "tsTimeOutSettingsIdle"},
                {TerminalServicesSessionTimeout, "tsBrokenTimeOutSettings"},
                {TerminalServicesSesionReconnection, "tsReConnectSettings"},
                {TerminalServicesRemoteControl, "tsShadowSettings"}
            };
        
        internal static Dictionary<LdapAttribute, string> Office365Attributes => 
        new Dictionary<LdapAttribute, string>
        {
            {Office365Group, "GroupMemberObjectId"},
            {Office365LigitationHoldEnabled, "LitigationHoldEnabled"},
            {Office365LigitationHoldDuration, "LitigationHoldDuration"},
            {Office365InPlaceArchiveEnabled, "InPlaceArchive"},
            {Office365InPlaceArchiveName, "ArchiveName"},
            {Office365LogonName, "O365userPrincipalName"}
        };

        internal static IDictionary<TKey, TValue> Concat<TKey, TValue>(this IDictionary<TKey, TValue> dict1,
            IDictionary<TKey, TValue> dict2)
        {
            var resultDict = new Dictionary<TKey, TValue>(dict1);
            foreach (KeyValuePair<TKey, TValue> pair in dict2)
            {
                resultDict.Add(pair.Key, pair.Value);
            }

            return resultDict;
        }
    }
}