using System.Data;
using Mwa.Infra.Contexts;

namespace Mwa.Infra.Transactions
{
    public class UoW : IUoW
    {

    private readonly MwaDataContext  _context;
    

        public UoW( MwaDataContext  context)
        {
            _context = context;	
        }

        

        public void Commit()
        {
            _context._transaction.Commit();
        }

        public void Rollback()
        {
            _context._transaction.Rollback();
	        _context.Dispose(); 
        }
    }
}