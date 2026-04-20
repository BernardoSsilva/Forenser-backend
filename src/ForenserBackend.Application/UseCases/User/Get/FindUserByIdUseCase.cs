using AutoMapper;
using ForenserBackend.Application.UseCases.User.Get.Interfaces;
using ForenserBackend.Communication.Response.Detailed;
using ForenserBackend.Domain.entities;
using ForenserBackend.Domain.RepositoriesInterfaces;

namespace ForenserBackend.Application.UseCases.User.Get
{
    public class FindUserByIdUseCase:IFindUserByIdUseCase
    {
        private readonly IUserRepository _repository;
        private readonly IMapper _mapper;

        public FindUserByIdUseCase(IUserRepository repository, IMapper mapper)
        {
            _mapper = mapper;
            _repository = repository;
        }

        public async Task<DetailedUserResponseJson> GetUserDetailsById(string id)
        {

            UserEntity user = await _repository.GetUserById(id);
            return _mapper.Map<DetailedUserResponseJson>(user);
        }
    }
}
