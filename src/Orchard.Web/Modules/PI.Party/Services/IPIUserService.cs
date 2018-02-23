using Orchard;
using PI.Party.Models;
using System.Collections.Generic;
using PS = PI.Party.Services.PIPartyEF;

namespace PI.Party.Services
{
    public interface IPIUserService : IDependency
    {
        /// <summary>
        /// Get the party user on the basis of email id
        /// </summary>
        /// <param name="email">email id</param>
        /// <returns></returns>
        PS.Party GetUser(string email);

        /// <summary>
        /// Creates a user in party 
        /// </summary>
        /// <param name="email">email id of the user</param>
        /// <returns></returns>
        PS.Party_Person CreateUser(string email);

        /// <summary>
        /// Adds user profile(firstname,lastname, phonenumber etc.) in party
        /// </summary>
        /// <param name="person"></param>
        /// <param name="aspnetProfile">User profile saved in Asp membership tables</param>
        /// <returns></returns>
        PS.Party_Person AddUserProfile(PS.Party_Person person, string aspnetProfile);

        /// <summary>
        /// get user profile from party with account number for edgers
        /// </summary>
        /// <returns>Party users</returns>
        List<EdgePartyUsers> GetPartyUsers();

        bool UpdateAccountNumber(System.Guid partyId, int accountNumber,string username);
    }
}
