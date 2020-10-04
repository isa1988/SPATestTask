using SPATestTask.Core.DataBase;
using SPATestTask.Services.Dto;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using SPATestTask.Core.Repositories;
using SPATestTask.Services.Services.Contracts;

namespace SPATestTask.Services.Services
{
    public abstract class GeneralService<T, TDto> : IGeneralService<T, TDto>
        where T : class, IEntity
        where TDto : class, IDto

    {
        public GeneralService(TDto dtoEmpty,
                              IMapper mapper,
                              IRepository<T> repository)
        {
            if (dtoEmpty == null)
                throw new ArgumentNullException(nameof(dtoEmpty));

            this.dtoEmpty = dtoEmpty;
            this.mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            this.repository = repository ?? throw new ArgumentNullException(nameof(repository));
        }

        protected readonly TDto dtoEmpty;
        protected readonly IMapper mapper;
        protected readonly IRepository<T> repository;
        //this mock
        protected abstract string CheckBeforeModification(TDto value, bool isNew = true);

        public virtual async Task<EntityOperationResult<T>> CreateAsync(TDto createDto)
        {
            string errors = CheckBeforeModification(createDto);
            if (!string.IsNullOrEmpty(errors))
            {
                return EntityOperationResult<T>.Failure().AddError(errors);
            }

            try
            {
                T value = mapper.Map<T>(createDto);
                var entity = await repository.AddAsync(value);
                repository.Save();

                return EntityOperationResult<T>.Success(entity);
            }
            catch (Exception ex)
            {
                return EntityOperationResult<T>.Failure().AddError(ex.Message);
            }
        }

        public List<TDto> GetAll()
        {
            var list = repository.GetAll();

            if (list == null || list.Count == 0)
            {
                return new List<TDto>();
            }
            return mapper.Map<List<TDto>>(list);
        }

        public List<TDto> GetPage(int numPage, int size)
        {
            var page = repository.GetAllOfPage(numPage, size);

            if (page == null || page.Count == 0)
            {
                return new List<TDto>();
            }
            var list = mapper.Map<List<TDto>>(page);
            return list;
        }

    }

    public abstract class GeneralService<T, TDto, TId> : GeneralService<T, TDto>, IGeneralService<T, TDto, TId>
        where T : class, IEntity<TId>
        where TDto : class, IDto<TId>
        where TId : IEquatable<TId>
    {
        protected readonly IRepository<T, TId> repositoryId;

        public GeneralService(TDto empty,
                              IMapper mapper,
                              IRepository<T, TId> repository)
                              : base(empty, mapper, repository)
        {
            repositoryId = base.repository as IRepository<T, TId>;
        }

        public async Task<EntityOperationResult<T>> UpdateAsync(TDto updateDto)
        {
            string errors = CheckBeforeModification(updateDto, false);
            if (!string.IsNullOrEmpty(errors))
            {
                return EntityOperationResult<T>.Failure().AddError(errors);
            }

            try
            {
                T value = mapper.Map<T>(updateDto);
                repositoryId.Update(value);
                repositoryId.Save();
                var entity = repositoryId.GetById(updateDto.Id);
                return EntityOperationResult<T>.Success(entity);
            }
            catch (Exception ex)
            {
                return EntityOperationResult<T>.Failure().AddError(ex.Message);
            }
        }

        protected abstract string CkeckBeforeDelete(T value);

        public async Task<EntityOperationResult<T>> DeleteItemAsync(TId id)
        {
            try
            {
                var value = repositoryId.GetById(id);
                if (value == null)
                {
                    return EntityOperationResult<T>.Failure().AddError("Не найдена запись");
                }
                string error = CkeckBeforeDelete(value);
                if (!string.IsNullOrEmpty(error))
                {
                    return EntityOperationResult<T>.Failure().AddError(error);
                }
                repositoryId.Delete(value);
                repositoryId.Save();

                return EntityOperationResult<T>.Success(value);
            }
            catch (Exception ex)
            {
                return EntityOperationResult<T>.Failure().AddError(ex.Message);
            }
        }

        public TDto GetById(TId id)
        {
            T value = repositoryId.GetById(id);
            if (value == null) return dtoEmpty;
            TDto dto = mapper.Map<TDto>(value);
            return dto;
        }
    }
}
