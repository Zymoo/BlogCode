using System.Data.Entity;

namespace TDD.DbTestHelpers
{
    public interface IDbFixture
    {
        void PrepareDatabase();
        void FillFixtures();
        bool RefillBeforeEachTest { get; }
        bool UseTransactionScope { get; }
        DbContext GetContext { get; }
    }
}