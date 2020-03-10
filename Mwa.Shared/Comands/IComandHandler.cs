using System;
using System.Collections.Generic;
using System.Text;

namespace Mwa.Shared.Comands
{
    public interface IComandHandler<T> where T : Icomand
    {
        IComandResult Handle(T comand);
    }
}
