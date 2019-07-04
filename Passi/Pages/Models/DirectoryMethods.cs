﻿using System;
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
        //Grabs domain, username, pw and validates creds against AD
        public bool Authenticate(string domain, string username, string password)
        {
            bool authenticated = false;
            try
            {
                PrincipalContext context = new PrincipalContext(ContextType.Domain, domain);
                authenticated = context.ValidateCredentials(username, password, ContextOptions.SimpleBind);
                context.Dispose();
            }
            catch (PrincipalException e)
            {
                Console.WriteLine(e.Message);
            }
            return authenticated;
        }
        //Searches specified OU against AD and finds all enabled user accounts
        public List<string> GetADUserList(string domain)
        {
            List<string> ADActiveUserList = new List<string>();
            try
            {
                PrincipalContext context = new PrincipalContext(ContextType.Domain, domain, AppAuth["Username"], AppAuth["Password"]);
                UserPrincipal userPrin = new UserPrincipal(context);
                PrincipalSearcher searcher = new PrincipalSearcher(userPrin);
                foreach (var result in searcher.FindAll())
                {
                    DirectoryEntry de = result.GetUnderlyingObject() as DirectoryEntry;
                    UserPrincipal u = UserPrincipal.FindByIdentity(context, IdentityType.SamAccountName, de.Properties["samAccountName"].Value.ToString());
                    if (u.Enabled == true)
                    {
                        ADActiveUserList.Add(u.SamAccountName);
                    }

                }
                searcher.Dispose();
                userPrin.Dispose();
                context.Dispose();
            }
            catch(Exception)
            {
                Console.WriteLine("Something went wrong initializing the context");
            }
            ADActiveUserList.Sort();
            return ADActiveUserList;
        }
        //Searches specified string against AD to find a user account then compiles account info into ADUser class
        public ADUser DirectorySearch(string searchQuery, string domain)
        {
            ADUser user = new ADUser();
            try
            {
                PrincipalContext context = new PrincipalContext(ContextType.Domain, domain, AppAuth["Username"], AppAuth["Password"]);
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
                }
               
                userResult.Dispose();
                context.Dispose();
            }
            catch (PrincipalException e)
            {
                Console.WriteLine(e.Message);
            }
            return user;
        }
        //Reset user account password, returns message depending on success?failure
        public string ResetPassword(string searchQuery, string password)
        {
            string passwordStatusMessage = String.Empty;
            try
            {
                PrincipalContext context = new PrincipalContext(ContextType.Domain, AppAuth["Domain"], AppAuth["Username"], AppAuth["Password"]);
                UserPrincipal userResult = UserPrincipal.FindByIdentity(context, searchQuery);
                try
                {
                    if (userResult != null)
                    {
                        userResult.SetPassword(password);
                        passwordStatusMessage = "Password was successfully changed.";
                    }
                    userResult.Dispose();
                    context.Dispose();
                }
                catch (PasswordException pEx)
                {
                    passwordStatusMessage = pEx.Message;
                }
            }
            catch (PrincipalException e)
            {
                Console.WriteLine(e.Message);
            }
            return passwordStatusMessage;
        }
        //Unlocks account returns message depending on success?failure
        public string UnlockAccount(string searchQuery)
        {
            string accountUnlockStatus = string.Empty;
            try
            {
                PrincipalContext context = new PrincipalContext(ContextType.Domain, AppAuth["Domain"], AppAuth["Username"], AppAuth["Password"]);
                UserPrincipal userResult = UserPrincipal.FindByIdentity(context, searchQuery);
                try
                {
                    if (userResult != null)
                    {
                        userResult.UnlockAccount();
                        accountUnlockStatus = "Account was unlocked.";
                    }
                    userResult.Dispose();
                    context.Dispose();
                }
                catch (PrincipalOperationException pEx)
                {
                    accountUnlockStatus = pEx.Message;
                }
            }
            catch (PrincipalException e)
            {
                Console.WriteLine(e.Message);
            }
            return accountUnlockStatus;
        }
        //Retireves a list of members of a group
       /*public List<string> GetGroupMembers(string domain, string groupVal)
        {
            List<string> gMembers = new List<string>();
            try
            {
                PrincipalContext context = new PrincipalContext(ContextType.Domain, domain, AppAuth["Username"], AppAuth["Password"]);
                GroupPrincipal group = GroupPrincipal.FindByIdentity(context, groupVal);
                foreach(Principal p in group.GetMembers())
                {
                    if(p.SamAccountName != null)
                    {
                        gMembers.Add(p.SamAccountName);
                    }
                }
            }
            catch(PrincipalException e)
            {
                Console.WriteLine(e.Message);
            }
            return gMembers;
        }*/

       /* public List<ADGroupObject> GetGroupMembers(string domain, string groupVal)
        {
            List<ADGroupObject> gMembers = new List<ADGroupObject>();
            try
            {
                PrincipalContext context = new PrincipalContext(ContextType.Domain, domain, AppAuth["Username"], AppAuth["Password"]);
                GroupPrincipal group = GroupPrincipal.FindByIdentity(context, groupVal);
                DirectoryEntry deGroupObject = group.GetUnderlyingObject() as DirectoryEntry;
                foreach (Principal p in group.GetMembers())
                {
                    if (p.SamAccountName != null)
                    {
                        ADGroupObject adGroupObject = new ADGroupObject();
                        adGroupObject.SamAccountName = p.SamAccountName;
                        adGroupObject.ObjectType = (int)deGroupObject.Properties["sAMAccountType"].Value;
                        gMembers.Add(adGroupObject);
                    }
                }
                group.Dispose();
                context.Dispose();
            }
            catch (PrincipalException e)
            {
                Console.WriteLine(e.Message);
            }
            return gMembers;
        }*/

        public ADGroup GetADGroupDetails(string groupVal)
        {
            ADGroup adGroup = new ADGroup();
            adGroup.GroupName = groupVal;
            adGroup.GroupObjects = new List<ADGroupObject>();
            adGroup.GroupObjectsNames = new List<string>();
            try
            {
                PrincipalContext context = new PrincipalContext(ContextType.Domain, AppAuth["Domain"], AppAuth["Username"], AppAuth["Password"]);
                GroupPrincipal group = GroupPrincipal.FindByIdentity(context, groupVal);
                DirectoryEntry deGroupObject = group.GetUnderlyingObject() as DirectoryEntry;
                foreach (Principal p in group.GetMembers())
                {
                    if (p.SamAccountName != null)
                    {
                        DirectoryEntry deP = p.GetUnderlyingObject() as DirectoryEntry;
                        adGroup.GroupObjectsNames.Add(p.SamAccountName);
                        ADGroupObject adGroupObject = new ADGroupObject();
                        adGroupObject.SamAccountName = p.SamAccountName;
                        adGroupObject.ObjectType = (int)deP.Properties["sAMAccountType"].Value;
                        adGroup.GroupObjects.Add(adGroupObject);
                    }
                }
                group.Dispose();
                context.Dispose();
            }
            catch (PrincipalException e)
            {
                Console.WriteLine(e.Message);
            }
            return adGroup;
        }

        /* public ADGroup CheckForGroupInGroup(ADGroup adGroup)
         {
             ADGroup foundGroup = null;
             foreach (ADGroupObject obj in adGroup.GroupObjects)
             {
                 if(obj.ObjectType == 268435456 || obj.ObjectType == 26843545)
                 {
                     foundGroup = GetADGroupDetails(obj.SamAccountName);
                 }
             }

             return foundGroup;
         }*/

        /*public List<ADGroup> GetGroupList(string groupVal)
        {
            List<ADGroup> adGroupList = new List<ADGroup>();
            ADGroup adGroup = GetADGroupDetails(groupVal);
            ADGroup foundGroup = new ADGroup();
            adGroupList.Add(adGroup);
            while(foundGroup != null)
            {
                foundGroup = CheckForGroupInGroup(adGroup);
                if(foundGroup != null)
                {
                    adGroupList.Add(foundGroup);
                    adGroup = GetADGroupDetails(foundGroup.GroupName);
                }

            }
            return adGroupList;
        }*/

        public List<string> CheckForGroupInGroup(ADGroup adGroup)
        {
            List<string> foundGroups = new List<string>();
            foreach (ADGroupObject obj in adGroup.GroupObjects)
            {
                if (obj.ObjectType == 268435456 || obj.ObjectType == 26843545)
                {
                    foundGroups.Add(obj.SamAccountName);
                }
            }

            return foundGroups;
        }

        public List<ADGroup> GetGroupList(string groupVal)
        {
            int ct = -1;
            List<ADGroup> adGroupList = new List<ADGroup>();
            ADGroup adGroup = GetADGroupDetails(groupVal);
            ADGroup foundGroup = new ADGroup();
            adGroupList.Add(adGroup);
            List<string> foundGroups = CheckForGroupInGroup(adGroup);
            while(ct <= -1)
            {
                foreach (string s in foundGroups)
                {
                    adGroup = GetADGroupDetails(s);
                    adGroupList.Add(adGroup);
                    List<string> foundGroups2 = new List<string>();
                    foundGroups2 = CheckForGroupInGroup(adGroup);

                }
            }
            
            
            return adGroupList;
        }
    }
}
