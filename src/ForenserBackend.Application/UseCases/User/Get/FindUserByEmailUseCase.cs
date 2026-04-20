using AutoMapper;
using ForenserBackend.Application.UseCases.User.Get.Interfaces;
using ForenserBackend.Communication.Response.Detailed;
using ForenserBackend.Domain.RepositoriesInterfaces;

namespace ForenserBackend.Application.UseCases.User.Get
{
    public class FindUserByEmailUseCase : IFindUserByEmailUseCase
    {
        private readonly IUserRepository _repository;
        private readonly IMapper _mapper;

        public FindUserByEmailUseCase(IUserRepository repository, IMapper mapper)
        {
            _mapper = mapper;
            _repository = repository;
        }
        public async Task<DetailedUserResponseJson> GetUserDetailsByEmail(string email)
        {
            var user = await _repository.GetUserByEmail(email);
            return _mapper.Map<DetailedUserResponseJson>(user);
        }
    }
}
