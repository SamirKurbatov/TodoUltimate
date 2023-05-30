using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TodoList.CLI.BaseAbstractions
{
    public abstract class BaseModelFactory : IModelFactory
    {
        public abstract BaseModel Create();
        protected virtual string CheckModelField(string fieldName)
        {
            string? value;
            do
            {
                value = Console.ReadLine();
                if (string.IsNullOrWhiteSpace(value))
                {
                    Console.WriteLine($"Вы не ввели значение {fieldName} попробуйте еще раз!");
                }
            } while (string.IsNullOrWhiteSpace(value));
            return value;
        }
    }
}