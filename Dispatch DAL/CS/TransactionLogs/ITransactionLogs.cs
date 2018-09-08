using System.Collections.Generic;
namespace Dispatch
{
    public interface ITransactionLogs
    {
        int Insert(TransactionLogsEntity transactionlogs);
        bool Update(TransactionLogsEntity transactionlogs);
        bool Delete(int id);
        List<TransactionLogsEntity> ListAll();
        List<TransactionLogsEntity> ListAllPaging(int pageIndex, int pageSize, ref int totalRow);
        List<TransactionLogsEntity> ListAllPaging(string searchString, int pageIndex, int pageSize, ref int totalRow);
        TransactionLogsEntity ViewDetail(int id);
    }

}

