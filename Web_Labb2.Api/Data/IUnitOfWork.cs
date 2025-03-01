﻿using Web_Labb2.Repositories;

namespace Web_Labb2.Data
{
    public interface IUnitOfWork : IDisposable
    {
        ICustomerRepository Customers { get; }
        IProductRepository Products { get; }


        Task<int> SaveChangesAsync();
    }
}
