using MyRecipeBook.Application.Services.AutoMapper;
using MyRecipeBook.Application.Services.Cryptography;
using MyRecipeBook.Communication.Requests;
using MyRecipeBook.Communication.Responses;
using MyRecipeBook.Exceptions.ExceptionsBase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyRecipeBook.Application.UseCases.User.Register
{
    public class RegisterUserUseCase
    {
        public ResponseRegisteredUserJson Execute(RequestRegisterUserJson request)
        {
            var criptografiaDeSenha = new PasswordEncripter();

            var autoMapper = new AutoMapper.MapperConfiguration(options =>
            {
                options.AddProfile(new AutoMapping());
            }).CreateMapper();

            Validate (request);


            var user = autoMapper.Map<Domain.Entities.User>(request);

            user.Password = criptografiaDeSenha.Encrypt(request.Password);
         
            //validar a request

            //mapear a request em uma entidade

            //criptografia da senha

            //salvar no banco de dados
            return new ResponseRegisteredUserJson
            {
                Name = request.Name
            };
        }
        private void Validate(RequestRegisterUserJson request)
        {
            var validator = new RegisterUserValidator();
            var result = validator.Validate(request);

            if (result.IsValid == false)
            {
                var errorMessages = result.Errors.Select(e => e.ErrorMessage).ToList();
                throw new ErrorOnValidationException(errorMessages);
                    
            }
        }
    }
}
