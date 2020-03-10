namespace Mwa.Infra.Transactions
{
    public interface IUoW
    {
         void Commit();
         void Rollback();
    }
}