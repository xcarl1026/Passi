using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Passi.Pages.Models
{
    public class BackupStuff
    {

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

        /* public List<string> CheckForGroupInGroup(ADGroup adGroup)
         {
             List<string> foundGroups = new List<string>();
             foreach (ADGroupObject obj in adGroup.GroupObjects)
             {
                 if (obj.ObjectType == 268435456 || obj.ObjectType == 268435457)
                 {
                     foundGroups.Add(obj.SamAccountName);
                 }
             }

             return foundGroups;
         }*/

        /*public List<ADGroup> GetGroupList(string groupVal)
{
    List<ADGroup> adGroupList = new List<ADGroup>();
    ADGroup adGroup = GetADGroupDetails(groupVal);
    ADGroup foundGroup = new ADGroup();
    adGroupList.Add(adGroup);
    List<string> foundGroups = CheckForGroupInGroup(adGroup);
    foreach (string s in foundGroups)
    {
        adGroup = GetADGroupDetails(s);
        adGroupList.Add(adGroup);


    }
    return adGroupList;
}*/
    }

}
