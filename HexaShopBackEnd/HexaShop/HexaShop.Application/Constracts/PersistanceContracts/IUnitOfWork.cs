using HexaShop.Application.Constracts.InfrastructureContracts;
using HexaShop.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HexaShop.Application.Constracts.PersistanceContracts
{
    public interface IUnitOfWork
    {
        void Commit();
        Task CommitAsync();
        IDbContextTransaction BeginTransaction();
        Task<IDbContextTransaction> BeginTransactionAsync();
        void RollBackTrasaction();
        Task RollBackTransactionAsync();
        bool IsBeganTransaction();
        Task<int> SaveChangesAsync();
        int SaveChanges();

        #region Repositories
        public IAppUserRepository AppUserRepository { get; }
        public IProductRepository ProductRepository { get; }
        public IDetailRepsitory DetailRepository { get; }
        public IImageSourceRepository ImageSourceRepository { get; }
        public IFileRepository FileRepository { get; }
        public ICategoryRepository CategoryRepository { get; }

        #endregion Repositories








    }
}
