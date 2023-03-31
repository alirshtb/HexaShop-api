using AutoMapper;
using HexaShop.Application.Constracts.InfrastructureContracts;
using HexaShop.Application.Constracts.PersistanceContracts;
using HexaShop.Domain.Common;
using HexaShop.Infrastructure.Repositories;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace HexaShop.Persistance.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {

        private readonly HexaShopDbContext _dbContext;
        private readonly IWebHostEnvironment _env;

        public UnitOfWork(HexaShopDbContext dbContext,
                          IWebHostEnvironment webHostEnvironment)
        {
            _dbContext = dbContext;
            _env = webHostEnvironment;

        }






        #region Repositories

        private readonly IAppUserRepository _appUserRepository;
        public IAppUserRepository AppUserRepository => _appUserRepository ?? new AppUserRepository(_dbContext);


        private readonly IProductRepository _productRepository;
        public IProductRepository ProductRepository => _productRepository ?? new ProductRepository(_dbContext);


        private readonly IDetailRepsitory _detailRepository;
        public IDetailRepsitory DetailRepository => _detailRepository ?? new DetailRepository(_dbContext);


        private readonly IImageSourceRepository _imageSourceRepository;
        public IImageSourceRepository ImageSourceRepository => _imageSourceRepository ?? new ImageSourceRepository(_dbContext);


        private readonly IFileRepository _fileRepository;
        public IFileRepository FileRepository => _fileRepository ?? new FileRepository(_env);


        private readonly ICategoryRepository _categoryRepository;
        public ICategoryRepository CategoryRepository => _categoryRepository ?? new CategoryRepository(_dbContext);


        #endregion Repositories


        #region Methods
        public IDbContextTransaction BeginTransaction()
        {
            var transaction = _dbContext.Database.BeginTransaction();
            return transaction;
        }

        public async Task<IDbContextTransaction> BeginTransactionAsync()
        {
            var transaction = await _dbContext.Database.BeginTransactionAsync();
            return transaction;
        }

        public void Commit()
        {
            _dbContext.SaveChanges();
            _dbContext.Database.CommitTransaction();
        }

        public async Task CommitAsync()
        {
            await _dbContext.SaveChangesAsync();
            await _dbContext.Database.CommitTransactionAsync();
        }

        public bool IsBeganTransaction()
        {
            return _dbContext.Database.CurrentTransaction != null;
        }

        public async Task RollBackTransactionAsync()
        {
            await _dbContext.Database.RollbackTransactionAsync();
        }

        public void RollBackTrasaction()
        {
            _dbContext.Database.RollbackTransaction();
        }

        public Task<int> SaveChangesAsync()
        {

            foreach (var entry in _dbContext.ChangeTracker.Entries<BaseDomainEntity>())
            {
                entry.Entity.LastModifiedDate = DateTime.Now;

                if (entry.State == EntityState.Added)
                {
                    entry.Entity.DateCreated = DateTime.Now;
                }

            }
            return _dbContext.SaveChangesAsync();
        }

        public int SaveChanges()
        {
            foreach (var entry in _dbContext.ChangeTracker.Entries<BaseDomainEntity>())
            {
                entry.Entity.LastModifiedDate = DateTime.Now;

                if (entry.State == EntityState.Added)
                {
                    entry.Entity.DateCreated = DateTime.Now;
                }

            }
            return _dbContext.SaveChanges();
        }

        #endregion Methods


    }
}
