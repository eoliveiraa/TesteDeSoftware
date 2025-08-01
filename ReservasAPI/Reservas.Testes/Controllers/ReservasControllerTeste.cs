﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentAssertions;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Reservas.Api.Controllers;
using Reservas.Api.Interfaces;
using Reservas.Api.Models;
using Reservas.Testes.MockData;

namespace Reservas.Testes.Controllers
{
    public class ReservasControllerTeste
    {
        [Fact]
        public void GetTodasReservas_DeveRetornar200Status()
        {
            // Arrange = organizar 

            var reservaService = new Mock<IReservaRepository>();
            reservaService.Setup(i => i.Reservas).Returns(ReservasMockData.GetReservas());
            var sut = new ReservasController(reservaService.Object);

            // Act = agir
            var result = (OkObjectResult)sut.Get();

            // Assert = afirmar 
            result.StatusCode.Should().Be(200);
        }

        [Fact]
        public void GetReservaPorId_DeveRetornar200_QuandoEncontrar()
        {
            //Arrange = Organizar
            var reservaMock = ReservasMockData.GetReservas()[0];
            var reservasService = new Mock<IReservaRepository>();
            reservasService.Setup(i => i[reservaMock.ReservaId]).Returns(reservaMock);
            var sut = new ReservasController(reservasService.Object);

            // Act = Agir
            var result = sut.Get(reservaMock.ReservaId);

            //Assert = Afirmar
            var OkResult = result.Result as OkObjectResult;
            OkResult.Should().NotBeNull();
            OkResult.StatusCode.Should().Be(200);
        }

        [Fact]

        public void GetReservaPorId_DeveRetornar404_QuandoNaoEncontrar()
        {
            // Arrange = Organizar
            var reservasService = new Mock<IReservaRepository>();
            reservasService.Setup(i => i[900]).Returns((Reserva)null);
            var sut = new ReservasController(reservasService.Object);

            // Act = Agir
            var result = sut.Get(900);

            // Assert = Afirmar
            result.Result.Should().BeOfType<NotFoundResult>();

        }

        [Fact]
        public void PostReserva_DeveRetornar201()
        {
            // Arrange = Organizar
            var novaReserva = new Reserva
            { Nome = "Teste" };

            var reservasService = new Mock<IReservaRepository>();
            reservasService.Setup(i => i.AddReserva(It.IsAny<Reserva>())).Returns(novaReserva);
            var sut = new ReservasController(reservasService.Object);

            // Act = Agir
            var result = sut.Post(novaReserva);

            // Assert = Afirmar
            var createdResult = result.Result as CreatedAtActionResult;
            createdResult.Should().NotBeNull();
            createdResult.StatusCode.Should().Be(201);

        }

        [Fact]

        public void PutReserva_DeveRetornar200_QuandoAtualizada()
        {
            // Arrange = Organizar
            var reservaAtualizada = new Reserva
            {
                ReservaId = 1,
                Nome = "Atualizada"
            };
            var resrvasService = new Mock<IReservaRepository>();
            resrvasService.Setup(i => i.UpdateReserva(It.IsAny<Reserva>())).Returns(reservaAtualizada);
            var sut = new ReservasController(resrvasService.Object);

            // Act = Agir
            var result = sut.Put(reservaAtualizada);

            // Assert = Afirmar
            var okResult = result.Result as OkObjectResult;
            okResult.Should().NotBeNull();
            okResult.StatusCode.Should().Be(200);

        }

        [Fact]

        public void PatchReserva_DeveRetornar200_QuandoAtualizada()
        {
            // Arrange = Organizar
            var reservaOriginal = new Reserva { ReservaId = 1, Nome = "Original" };
            var patch = new JsonPatchDocument<Reserva>();
            patch.Replace(r => r.Nome, "Atualizada");
            var reservaSErvice = new Mock<IReservaRepository>();
            reservaSErvice.Setup(i => i[1]).Returns(reservaOriginal);
            var sut = new ReservasController(reservaSErvice.Object);

            // Act = Agir
            var result = sut.Patch(1, patch);

            // Assert = Afirmar
            result.Should().BeOfType<OkResult>();
        }

        [Fact]

        public void PatchReserva_DeveRetornar404_QuandoNaoEncontrada()
        {
            // Arrange = Organizar
            var patch = new JsonPatchDocument<Reserva>();
            var reservaService = new Mock<IReservaRepository>();
            reservaService.Setup(i => i[999]).Returns((Reserva)null);
            var sut = new ReservasController(reservaService.Object);

            // Act = Agir
            var result = sut.Patch(999, patch);

            // Assert = Afirmar
            result.Should().BeOfType<NotFoundResult>();
        }

        [Fact]

        public void DeleteReserva_DeveRetornar204()
        {
            // Arrange = Organizar
            var reservaService = new Mock<IReservaRepository>();
            var sut = new ReservasController(reservaService.Object);

            // Act = Agir
            var result = sut.Delete(1);

            // Assert = Afirmar
            result.Should().BeOfType<NoContentResult>();
        }
    }
}