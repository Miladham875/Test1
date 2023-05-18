using DataAccess.Repository;
using Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class FruitService: IFruitService
    {

        #region Property  
        private IRepository<Fruit> _repository;
        #endregion

        #region Constructor  
        public FruitService(IRepository<Fruit> repository)
        {
            _repository = repository;
        }
        #endregion


        public async  Task<List<Fruit>> GetAll()
        {
            return _repository.GetAll()?.ToList();
        }

        public async Task<Fruit> GetById(int id)
        {
            return _repository.Get(id);
        }


        public async Task Delete(int id)
        {
            var entity=_repository.Get(id);
            _repository.Delete(entity);
           
        }


        public async Task Update(Fruit model)
        {            
            _repository.Update(model);
           
        }



        public async Task<string> CreateReport(Fruit model)
        {
            var entity=new Fruit();
            entity.Name = model.Name;
            entity.CreatedDateTime = model.CreatedDateTime;
            entity.Color = model.Color;
            entity.CreatorUserId = model.CreatorUserId;
            _repository.Insert(entity);
            return "Success";
            
        }




    }
}
