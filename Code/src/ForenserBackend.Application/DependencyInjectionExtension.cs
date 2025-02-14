using ForenserBackend.Application.UseCases.User.Delete.interfaces;
using ForenserBackend.Application.UseCases.User.Get.Interfaces;
using ForenserBackend.Application.UseCases.User.Post.Interfaces;
using ForenserBackend.Application.UseCases.User.Put.Interfaces;
using ForenserBackend.Application.UseCases.User.Delete;
using ForenserBackend.Application.UseCases.User.Get;
using ForenserBackend.Application.UseCases.User.Post;
using ForenserBackend.Application.UseCases.User.Put;
using Microsoft.Extensions.DependencyInjection;

namespace ForenserBackend.Application
{
    public static class DependencyInjectionExtension
    {
        public static void AddApplication(this IServiceCollection service)
        {
            AddAutoMapper(service);
        }

        public static void AddAutoMapper(IServiceCollection service)
        {
            service.AddAutoMapper(typeof(AutoMapper));
        }

        private static void AddUseCases(IServiceCollection service)
        {
            service.AddScoped<IDeleteUserUseCase, DeleteUserUseCase>();
            service.AddScoped<IFindUserByCpfUseCase, FindUserByCpfUseCase>();
            service.AddScoped<IFindUserByEmailUseCase, FindUserByEmailUseCase>();
            service.AddScoped<IFindUserByIdUseCase, FindUserByIdUseCase>();
            service.AddScoped<IAuthenticateUserUseCase, AuthenticateUserUseCase>();
            service.AddScoped<ICreateUserUseCase, CreateUserUseCase>();
            service.AddScoped<IUpdateUserUseCase, UpdateUserUseCase>();
        }
    }
}
