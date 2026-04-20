using AutoMapper;
using ForenserBackend.Application.UseCases.User.Get.Interfaces;
using ForenserBackend.Communication.Response.Detailed;
using ForenserBackend.Domain.RepositoriesInterfaces;

namespace ForenserBackend.Application.UseCases.User.Get
{
    public class FindUserByCpfUseCase : IFindUserByCpfUseCase
    {

        private readonly IUserRepository _repository;
        private readonly IMapper _mapper;

        public FindUserByCpfUseCase(IUserRepository repository, IMapper mapper)
        {
            _mapper = mapper;
            _repository = repository;
        }


        public async Task<DetailedUserResponseJson> GetUserDetailsByCpf(string cpf)
        {
            var user = await _repository.GetUserByCpf(cpf);
            return _mapper.Map<DetailedUserResponseJson>(user);
        }
    }
}
