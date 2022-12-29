﻿using System.Collections.Generic;

namespace WebShopAsp2022.Abstract
{
    public interface IRepository<T>
    {
        T FindByName(string name);

        IEnumerable<T> GetList();

        void Create(T model);

        T Read(long id);

        T ReadWithRelations(long id);

        void Update(T model);

        void Delete(long id);
    }
}
