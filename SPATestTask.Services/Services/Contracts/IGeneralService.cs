using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using SPATestTask.Core.DataBase;

namespace SPATestTask.Services.Services.Contracts
{
    public interface IGeneralService<T, TDto>
    {
        /// <summary>
        /// Добавить запись в базу
        /// </summary>
        /// <param name="createDto">Объект добавление</param>
        /// <returns></returns>
        Task<EntityOperationResult<T>> CreateAsync(TDto createDto);


        /// <summary>
        /// Вернуть все записи
        /// </summary>
        /// <returns></returns>
        List<TDto> GetAll();
        List<TDto> GetPage(int numPage, int size);
    }

    public interface IGeneralService<T, TDto, TId> : IGeneralService<T, TDto>
        where T : IEntity<TId>
        where TId : IEquatable<TId>
    {
        /// <summary>
        /// Вернуть конкретный объект из базы
        /// </summary>
        /// <param name="id">Идентификатор записи</param>
        /// <returns></returns>
        TDto GetById(TId id);
        Task<EntityOperationResult<T>> UpdateAsync(TDto updateDto);

        Task<EntityOperationResult<T>> DeleteItemAsync(TId id);
    }
}
