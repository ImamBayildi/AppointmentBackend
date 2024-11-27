namespace Core.Entities.Security
{
    public class UserOperationClaim : IEntity
    {
        public int Id { get; set; }
        public int UserId { get; set; }//Hangi user'ın
        public int OperationClaimId { get; set; }//Hangi claim'i
    }
}
