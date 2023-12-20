﻿using System.Linq.Expressions;
namespace WebApplication7.Models
{
    public class IRepository<T> where T : class
    {
        IEnumerable<T> GetAll(string? includeProps = null);

        T Get(Expression<Func<T, bool>> filtre, string? includeProps = null);

        void Ekle(T entitiy);

        void Sil(T entitiy);

        void SilAralik(IEnumerable<T> entities);
    }
}
