﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TronNet.Protocol;

namespace TronNet
{
    public interface ITransactionClient : StowayNet.IStowayDependency
    {
        Task<TransactionExtention> CreateTransactionAsync(string from, string to, long amount);

        string GetTransactionHash(Transaction transaction);

        Transaction GetTransactionSign(Transaction transaction, string privateKey);

        Task<Return> BroadcastTransactionAsync(Transaction transaction);
    }
}
