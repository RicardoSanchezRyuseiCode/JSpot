using Ryusei.JSpot.Core.Ent;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using Ryusei.JSpot.Auth.Ent;

namespace Ryusei.JSpot.Core.Mgr.DAO
{
    internal class ParticipantDAO
    {
        /// <summary>
        /// Name: Select
        /// Description: Method to select a collection of Assistant
        /// </summary>
        /// <param name="top"></param>
        /// <param name="filter"></param>
        /// <param name="order"></param>
        /// <param name="params"></param>
        /// <returns></returns>
        internal IEnumerable<Participant> Select(string top = "", string filter = "", string order = "", object @params = null)
        {
            // result
            IEnumerable<Participant> results = new List<Participant>();
            // query
            string query = string.Format(@"
                                            select {0}
	                                            * 
                                            from 
	                                            Core.Participant P
                                            inner join
	                                            Auth.[User] U
                                            on	
	                                            P.UserId = U.UserId
                                            ", top);
            // add filters
            query = (!filter.Equals("")) ? string.Format("{0} where {1}", query, filter) : query;
            // add order
            query = (!order.Equals("")) ? string.Format("{0} order by {1}", query, order) : query;
            // Execute
            using (IDbConnection dbConnection = Data.DAO.GetInstance(Data.DbType.SqlServer))
            {
                // Get results
                results = dbConnection.Query<Participant, User, Participant>(query, (P, U) => {
                    P.User = U;
                    return P;
                }, @params, splitOn: "UserId");
            }
            // list contacts
            return results;
        }
        /// <summary>
        /// Name: Save
        /// Description: Method to save participant
        /// </summary>
        /// <param name="participant">Participant</param>
        public void Save(Participant participant)
        {
            // Define statement
            string statement = "insert into Core.Participant(UserId, EventGroupId)values(@UserId, @EventGroupId)";
            // Execute
            using (IDbConnection dbConnection = Data.DAO.GetInstance(Data.DbType.SqlServer))
            {
                // Get results
                dbConnection.Execute(statement, participant);
            }
        }
    }
}
