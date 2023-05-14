using Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public interface IFruitService
    {
        Task<List<Fruit>> GetAll();
        Task<Fruit> GetById(int id);
        Task Delete(int id);
        Task Update(Fruit model);
        Task CreateReport(Fruit model);
    }
}
