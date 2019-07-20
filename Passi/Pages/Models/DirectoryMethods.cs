using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.DirectoryServices;
using System.DirectoryServices.AccountManagement;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json.Linq;
using System.IO;
using Microsoft.Extensions.FileProviders;
using Newtonsoft.Json;

namespace Passi.Pages.Models
{
    public class DirectoryMethods
    {
        private UserPrincipal userResult { get; set; }
        private PrincipalContext context { get; set; }
        private List<string> SecurityGroups { get; set; }
        private Dictionary<string, string> AppAuth { get; set; }
        //Initializes dictionary with creds
        public DirectoryMethods()
        {
            /*Explore file provider
             * var provider = new PhysicalFileProvider(AppDomain.CurrentDomain.BaseDirectory);
            var appAuthFile = provider.GetDirectoryContents(AppDomain.CurrentDomain.BaseDirectory);*/

            //Read JSON
            string file = "appauth.json";
            Console.WriteLine(File.Exists(file) ? "File exists." : "File does not exist.");
            if (File.Exists(file))
            {
                JObject o1 = JObject.Parse(File.ReadAllText(file));
                string json = o1.ToString();
                AppAuth = JsonConvert.DeserializeObject<Dictionary<string, string>>(json);
                //Console.WriteLine(AppAuth["Password"]);
            }

        }

        private PrincipalContext GetContext()
        {
            PrincipalContext context = null;
            try
            {
                context = new PrincipalContext(ContextType.Domain, AppAuth["Domain"], AppAuth["Scope"], AppAuth["Username"], AppAuth["Password"]);
            }
            catch(PrincipalException e)
            {
                Console.WriteLine(e.Message);
            }
            return context;
        }

        //Grabs domain, username, pw and validates creds against AD
        public bool Authenticate(string domain, string username, string password)
        {
            bool authenticated = false;
            PrincipalContext context = GetContext();
            if(context != null)
            {               
                authenticated = context.ValidateCredentials(username, password, ContextOptions.SimpleBind);
            }
            return authenticated;
        }
 
        //Searches specified string against AD to find a user account then compiles account info into ADUser class
        public ADUser DirectorySearch(string searchQuery, string domain)
        {
            ADUser user = new ADUser();
            PrincipalContext context = GetContext();
            if (context != null)
            {
                UserPrincipal userResult = UserPrincipal.FindByIdentity(context, searchQuery);
                if (userResult != null)
                {
                    PrincipalSearchResult<Principal> groups = userResult.GetGroups();
                    foreach (Principal p in groups)
                    {
                        if (p is GroupPrincipal)
                        {
                            user.SecurityGroups.Add(p.ToString());
                        }

                    }
                    PropertyCollection properties = ((DirectoryEntry)userResult.GetUnderlyingObject()).Properties;
                    foreach (object property in properties["proxyaddresses"])
                    {
                        string p = property.ToString();
                        user.ProxyAddresses.Add(p);
                    }
                    DirectoryEntry deUser = userResult.GetUnderlyingObject() as DirectoryEntry;
                    DirectoryEntry deUserContainer = deUser.Parent;
                    user.OU = deUserContainer.Properties["distinguishedName"].Value.ToString();
                    // Console.WriteLine(properties["distinguishedName"].Value.ToString());
                    user.DisplayName = userResult.DisplayName;
                    user.UserName = userResult.SamAccountName;
                    user.EmailAddress = userResult.EmailAddress;
                    user.LastBadPasswordAttempt = userResult.LastBadPasswordAttempt;
                    user.LastLogon = userResult.LastLogon;
                    user.AccountLocked = userResult.IsAccountLockedOut();
                    user.AccountLockoutTime = userResult.AccountLockoutTime;
                    user.UserCannotChangePassword = userResult.UserCannotChangePassword;
                    userResult.Dispose();
                    context.Dispose();
                }
            }
            return user;
        }

        //Reset user account password, returns message depending on success?failure
        public string ResetPassword(string searchQuery, string password)
        {
            string passwordStatusMessage = String.Empty;
            PrincipalContext context = GetContext();
            if(context != null)
            {
                UserPrincipal userResult = UserPrincipal.FindByIdentity(context, searchQuery);
                if (userResult != null && userResult.SamAccountName != AppAuth["Username"])
                {
                    try
                    {
                        userResult.SetPassword(password);
                        passwordStatusMessage = "Password was successfully changed.";
                    }
                    catch (Exception e)
                    {
                        passwordStatusMessage = e.Message;
                    }
                    userResult.Dispose();
                }
                else
                {
                    passwordStatusMessage = "The password for this account cannot be reset via this application.";
                }
                context.Dispose();
            }
            return passwordStatusMessage;
        }

        //Unlocks account returns message depending on success?failure
        public string UnlockAccount(string searchQuery)
        {
            string accountUnlockStatus = string.Empty;
            PrincipalContext context = GetContext();
            if(context != null)
            {
                UserPrincipal userResult = UserPrincipal.FindByIdentity(context, searchQuery);
                try
                {
                    if (userResult != null)
                    {
                        userResult.UnlockAccount();
                        accountUnlockStatus = "Account was unlocked.";
                    }  
                }
                catch (PrincipalOperationException pEx)
                {
                    accountUnlockStatus = pEx.Message;
                }
                userResult.Dispose();
                context.Dispose();
            }
            return accountUnlockStatus;
        }

        //Searches specified OU against AD and finds all enabled user accounts
        public List<string> GetADUserList(string domain)
        {
            List<string> ADActiveUserList = new List<string>();
            PrincipalContext context = GetContext();
            if (context != null)
            {
                UserPrincipal userPrin = new UserPrincipal(context);
                PrincipalSearcher searcher = new PrincipalSearcher(userPrin);
                foreach (var result in searcher.FindAll())
                {
                    DirectoryEntry de = result.GetUnderlyingObject() as DirectoryEntry;
                    UserPrincipal u = UserPrincipal.FindByIdentity(context, IdentityType.SamAccountName, de.Properties["samAccountName"].Value.ToString());
                    if (u.Enabled == true)
                    {
                        if (u.SamAccountName != AppAuth["Username"])
                        {
                            ADActiveUserList.Add(u.SamAccountName);
                        }

                    }

                }
                searcher.Dispose();
                userPrin.Dispose();
                context.Dispose();
            }
            ADActiveUserList.Sort();
            return ADActiveUserList;
        }

        //Retrieve all groups
        public List<string> GetADGroupsList()
        {
           List<string> adGroups = new List<string>();
           PrincipalContext context = GetContext();
            if(context != null)
            {
                GroupPrincipal groups = new GroupPrincipal(context);
                //searcher to search groups
                PrincipalSearcher searcher = new PrincipalSearcher(groups);
                foreach (var found in searcher.FindAll())
                {
                    DirectoryEntry deFound = (DirectoryEntry)found.GetUnderlyingObject() as DirectoryEntry;
                    if ((int)deFound.Properties["samAccountType"].Value == 536870912)
                    {
                        Console.WriteLine("Groups is Alias Object (BuiltIn) and will not be aded to list.");
                    }
                    else
                    {
                        adGroups.Add(found.ToString());
                    }
                }
                adGroups.Sort();
                groups.Dispose();
                searcher.Dispose();
                context.Dispose();
            }
            return adGroups;
        }

        //Gets group details, identify if members of the group are of type user of it's a nested group 
        public ADGroup GetADGroupDetails(string groupVal)
        {
            ADGroup adGroup = new ADGroup();
            adGroup.GroupName = groupVal;
            adGroup.GroupObjects = new List<ADGroupObject>();
            adGroup.GroupObjectsNames = new List<string>();
            PrincipalContext context = GetContext();
            if(context != null)
            {
                GroupPrincipal group = GroupPrincipal.FindByIdentity(context, groupVal);
                if (group != null)
                {
                    DirectoryEntry deGroupObject = group.GetUnderlyingObject() as DirectoryEntry;
                    foreach (Principal p in group.GetMembers())
                    {
                        if (p.SamAccountName != AppAuth["Username"])
                        {
                            if (p.SamAccountName != null)
                            {
                                DirectoryEntry deP = p.GetUnderlyingObject() as DirectoryEntry;
                                adGroup.GroupObjectsNames.Add(p.SamAccountName);
                                ADGroupObject adGroupObject = new ADGroupObject();
                                adGroupObject.SamAccountName = p.SamAccountName;
                                adGroupObject.ObjectType = (int)deP.Properties["sAMAccountType"].Value;
                                if (adGroupObject.ObjectType == 268435456 || adGroupObject.ObjectType == 268435457)
                                {
                                    adGroupObject.ObjectTypeString = "group";
                                }
                                else
                                {
                                    adGroupObject.ObjectTypeString = "user";
                                }
                                adGroup.GroupObjects.Add(adGroupObject);
                            }
                        }
                    }
                    group.Dispose();
                }
                context.Dispose();
            }
            return adGroup;
        }
    }
}
