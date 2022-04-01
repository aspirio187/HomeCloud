using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.AccessControl;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Runtime.InteropServices;
using System.Security.Principal;

namespace HomeCloud.FSWatcher.Helpers
{
    internal static class DirectoryHelper
    {
        /// <summary>
        /// Check if current user has R/W rights on the folder
        /// </summary>
        /// <param name="fullPath">Directory absolute path</param>
        /// <param name="ntAccountName">Os account name required if actual operating system is Windows</param>
        /// <returns>true If user has R/W rights. false Otherwise</returns>
        /// <exception cref="ArgumentNullException"></exception>
        public static bool DirectoryHasWriteAccess(string fullPath, string? ntAccountName = null)
        {
            if (string.IsNullOrEmpty(fullPath)) throw new ArgumentNullException(nameof(fullPath));

            if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
            {
                if (string.IsNullOrEmpty(ntAccountName)) throw new ArgumentNullException(nameof(ntAccountName));

                DirectoryInfo directoryInfo = new DirectoryInfo(fullPath);
                DirectorySecurity directorySecurity = directoryInfo.GetAccessControl(AccessControlSections.All);
                AuthorizationRuleCollection authorizationRuleCollection = directorySecurity
                    .GetAccessRules(true, true, typeof(NTAccount));

                AuthorizationRule? authorizationRule = null;

                foreach (AuthorizationRule rule in authorizationRuleCollection)
                {
                    if (rule.IdentityReference.Value.Equals(ntAccountName, StringComparison.CurrentCultureIgnoreCase))
                    {
                        authorizationRule = rule;
                        break;
                    }
                }

                if (authorizationRule is null) return false;

                FileSystemAccessRule? fsAccessRule = (FileSystemAccessRule)authorizationRule;

                if ((fsAccessRule.FileSystemRights & FileSystemRights.WriteData) > 0 && fsAccessRule.AccessControlType != AccessControlType.Deny)
                {
                    return true;
                }

                return false;
            }
            else if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
            {
                // TODO : Add verification for Linux
            }
            else if (RuntimeInformation.IsOSPlatform(OSPlatform.OSX))
            {
                // TODO : Add Verification for OSX
            }

            return true;
        }
    }
}
