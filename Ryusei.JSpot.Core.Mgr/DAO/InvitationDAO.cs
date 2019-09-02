using Ryusei.JSpot.Core.Ent;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;

namespace Ryusei.JSpot.Core.Mgr.DAO
{
    /// <summary>
    /// Name: InvitationDAO
    /// Description: Data Access Object for Invitation
    /// Author: Ricardo Sanchez Romero (ricardo.sanchez@ryuseicode.com)
    /// LogBook:
    ///     2019-08-23: Creation
    /// </summary>
    internal class InvitationDAO
    {
        #region [Attributes]
        /// <summary>
        /// DAO
        /// </summary>
        internal Data.DAO DAO { get; set; }
        #endregion

        #region [Constructor]
        /// <summary>
        /// Default constructor
        /// </summary>
        public InvitationDAO()
        {
            this.DAO = new Data.DAO();
        }
        #endregion

        #region [Methods]
        /// <summary>
        /// Name: Select
        /// Description: Method to get a collection of Event
        /// </summary>
        /// <param name="top">Top</param>
        /// <param name="filter">Filter</param>
        /// <param name="order">Order</param>
        /// <param name="params">Params</param>
        /// <returns>Collection of events</returns>
        internal IEnumerable<Invitation> Select(string top = "", string filter = "", string order = "", object @params = null)
        {
            // result
            IEnumerable<Invitation> results = new List<Invitation>();
            // query
            string query = string.Format(@"
                                            select {0}
	                                            I.*,
	                                            EVT.* 
                                            from 
	                                            Core.Invitation I
                                            inner join
	                                            Core.Event EVT
                                            on
	                                            I.EventId = EVT.EventId
                                            ", top);
            // add filters
            query = (!filter.Equals("")) ? string.Format("{0} where {1}", query, filter) : query;
            // add order
            query = (!order.Equals("")) ? string.Format("{0} order by {1}", query, order) : query;
            // Execute
            using (IDbConnection dbConnection = Data.DAO.GetInstance(Data.DbType.SqlServer))
            {
                // Get results
                results = dbConnection.Query<Invitation, Event, Invitation>(query,(I, EVT) => {
                    I.Event = EVT;
                    return I;
                }, @params, splitOn: "EventId");
            }
            // list contacts
            return results;
        }
        /// <summary>
        /// Name: Save
        /// Description: Method to save a collection of invitations
        /// </summary>
        /// <param name="collectionInvitation"></param>
        internal void Save(IEnumerable<Invitation> collectionInvitation)
        {
            // Define statement
            string statement = "insert into Core.Invitation(InvitationId, EventId, Name, Email, SendDate)values(@InvitationId, @EventId, @Name, @Email, @SendDate)";
            // Define default data
            foreach (Invitation invitation in collectionInvitation)
            {
                invitation.InvitationId = Guid.NewGuid();
                invitation.SendDate = DateTime.Now.ToUniversalTime();
            }
            // Define params
            Dictionary<string, Type> dicPropertyNameType = new Dictionary<string, Type>
            {
                { "InvitationId", typeof(Guid) },
                { "EventId", typeof(Guid) },
                { "Name", typeof(string) },
                { "Email", typeof(string) },
                { "SendDate", typeof(DateTime) },
            };
            // Execute
            this.DAO.ExecuteBatch(statement, collectionInvitation, dicPropertyNameType, Data.DAO.OPERATION_TYPE_INSERT);
        }
        /// <summary>
        /// Name: Deactivate
        /// Description: Method to deactivate invitation
        /// </summary>
        /// <param name="invitationId">InvitationID</param>
        internal void Deactivate(Guid invitationId, bool answer)
        {
            // Define statement
            string statement = "update [Core].[Invitation] set Answer = @Answer, ResponseDate = @ResponseDate where InvitationId =  @InvitationId";
            // Execute
            using (IDbConnection dbConnection = Data.DAO.GetInstance(Data.DbType.SqlServer))
            {
                // Get results
                dbConnection.Execute(statement, new { Answer = answer, ResponseDate = DateTime.Now.ToUniversalTime(), InvitationId = invitationId });
            }
        }
        /// <summary>
        /// Name: Delete
        /// Description: Method to delete a collection
        /// </summary>
        /// <param name="collectionInvitation">Collection Invitation</param>
        internal void Delete(IEnumerable<Invitation> collectionInvitation)
        {
            // Define statement
            string statement = "delete from Core.Invitation where InvitationId = @InvitationId";
            // Define params
            Dictionary<string, Type> dicPropertyNameType = new Dictionary<string, Type>
            {
                { "InvitationId", typeof(Guid) }
            };
            // Execute
            this.DAO.ExecuteBatch(statement, collectionInvitation, dicPropertyNameType, Data.DAO.OPERATION_TYPE_DELETE);
        }
        #endregion
    }
}
