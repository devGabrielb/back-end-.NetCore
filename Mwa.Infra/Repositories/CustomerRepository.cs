using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using Dapper;
using Mwa.Domain.Comand.Inputs;
using Mwa.Domain.Comand.Results;
using Mwa.Domain.Entities;
using Mwa.Domain.Repositories;
using Mwa.Domain.ValueObjects;
using Mwa.Infra.Contexts;

namespace Mwa.Infra.Repositories
{
    public class CustomerRepository : ICustumerRepository
    {

        private readonly MwaDataContext _context;


        public CustomerRepository(MwaDataContext context)
        {
            _context = context;
        }


        
        public bool DocumentExists(string document)
        {
            return _context.Connection
                    .Query<bool>(
                        "CustomerDocumentExists",
                        new {Document = document},
                        transaction: _context._transaction,
                        commandType: CommandType.StoredProcedure)
                        .FirstOrDefault();
        }

        public Customer Get(Guid id)
        {
            return _context.Connection
            .Query<Customer>(
            "CustomerGetId",
            new {Id = id},
            transaction: _context._transaction,
            commandType: CommandType.StoredProcedure)
            .FirstOrDefault();
                   
        }

        public GetCustomerCommandResult Get(string username)
        {
           return _context.Connection
            .Query<GetCustomerCommandResult>(
            "UsernameGet",
            new {Username = username},
            transaction: _context._transaction,
            commandType: CommandType.StoredProcedure)
            .FirstOrDefault();
        }

        // public Customer GetByUserName(string username)
        // {
            

        //     var result = _context.Connection
        //     .Query<RegisterCustomerComand>(
        //     "getByUserName",
        //     new {Username = username},
        //     transaction: _context._transaction,
        //     commandType: CommandType.StoredProcedure)
        //     .FirstOrDefault();

        //     var name = new Name(result.FirstName, result.LastName);
        //     var document = new Document(result.Document);
        //     var email = new Email(result.Email);
        //     var user = new User(result.UserName, result.Password, result.Password);

        //     var customer = new Customer(name,email, document, user);

        //     return customer;



        // }

        public void Save(Customer customer)
        {
           //int boll_bit = customer.User.boolean_bit();
            _context.Connection
            .Execute(
            "CustomerSave",
            new
            {
                Id = customer.Id,
                FirstName = customer.Name.FirstName,
                LastName = customer.Name.LastName,
                Email = customer.Email.Address,
                Document = customer.Document.Number,
                UserName = customer.User.UserName,
                Password = customer.User.Password,
                Active =  customer.User.Active
            },
            transaction: _context._transaction,
            commandType: CommandType.StoredProcedure);
        }

        public void Update(Customer customer)
        {
            _context.Connection
            .Query<Customer>(
            "CustomerUpdate",
            commandType: CommandType.StoredProcedure);
                        
        }

        
    }
}