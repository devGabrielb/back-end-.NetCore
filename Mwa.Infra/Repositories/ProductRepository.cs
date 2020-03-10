using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using Dapper;
using Mwa.Domain.Comand.Results;
using Mwa.Domain.Entities;
using Mwa.Domain.Repositories;
using Mwa.Infra.Contexts;

namespace Mwa.Infra.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly MwaDataContext _context;


        public ProductRepository(MwaDataContext context)
        {
            _context = context;
        }
        



        public Product GetU(Guid id)
        {
             return _context.Connection
                    .Query<Product>(
                        "ProductGetId",
                        new {Id = id},
                        transaction: _context._transaction,
                        commandType: CommandType.StoredProcedure)
                        .FirstOrDefault();
        }

        public IEnumerable<GetProductListCommandResult> Get()
        {
            return _context.Connection
            .Query<GetProductListCommandResult>(
            "getProduct", new {},
            transaction: _context._transaction,
            commandType: CommandType.StoredProcedure);
        }
    }
}