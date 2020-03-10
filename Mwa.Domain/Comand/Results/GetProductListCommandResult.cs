using System;
using Mwa.Shared.Comands;

namespace Mwa.Domain.Comand.Results
{
    public class GetProductListCommandResult : IComandResult
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Image { get; set; }
        public string Price { get; set; }
        
    }
}