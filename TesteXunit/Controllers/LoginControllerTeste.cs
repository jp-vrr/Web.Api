using Chapter.web.api.Controllers;
using Chapter.web.api.interfaces;
using Chapter.web.api.models;
using Chapter.web.api.ViewModel;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace TesteXunit.Controllers
{
    public class LoginControllerTeste
    {
        [Fact]
        public void Login_Controller_Retornar_UsuarioInvalido()
        {
            //arrange
            var repositoryFalso = new Mock<IUsuarioRepository>();
            //criando uma copia dos dados para o mock(teste)

            repositoryFalso.Setup(x => x.Login(It.IsAny<string>(), It.IsAny<string>())).Returns((Usuario)null);
            //configurando informações 
            //vai receber duas strings, (retunr) define o que irá retornar 

            LoginViewModels dadosUsario = new LoginViewModels();
            dadosUsario.email = "email@email.com";
            dadosUsario.senha = "1234";

            var controller = new LoginController(repositoryFalso.Object);

            //act
            var resultado = controller.Login(dadosUsario);

            //assert
            Assert.IsType<UnauthorizedObjectResult>(resultado);
        }

        [Fact]
        public void LoginController_Retornar_Usuario()
        {
            //arrange
            string issuerValidacao = "Chapter.web.api";
            Usuario usuarioFake = new Usuario();
            usuarioFake.Email = "email@email.com";
            usuarioFake.Senha = "1234";


            var repositorioFalso = new Mock<IUsuarioRepository>();
            repositorioFalso.Setup(x => x.Login(It.IsAny<string>(), It.IsAny<string>())).Returns(usuarioFake);

            var controller = new LoginController(repositorioFalso.Object);

            LoginViewModels dadosUsuario = new LoginViewModels();
            dadosUsuario.email = "email@email.com";
            dadosUsuario.senha = "123";

            //act
            OkObjectResult result = (OkObjectResult)controller.Login(dadosUsuario); 

            var token = result.Value.ToString().Split(' ')[3];

            var jstHandler = new JwtSecurityTokenHandler();
            var jwtToken = jstHandler.ReadJwtToken(token);

            //var token = resultado.ToString();

            //assert
            //Assert.IsType<OkObjectResult>(result);

            Assert.Equal(issuerValidacao, jwtToken.Issuer);
        }
    }
}
