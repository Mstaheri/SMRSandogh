﻿using Application.Data.MoqData;
using Application.Services;
using Application.UnitOfWork;
using Domain.Entity;
using Domain.IRepositories;
using Domain.OperationResults;
using Microsoft.Extensions.Logging;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.test.Services
{
    public class BankSafeTransactionsTest
    {
        private readonly BankSafeTransactionsMoqData _moqData;
        private readonly Mock<IBankSafeTransactionsRepositorie> _repositorMoq;
        private readonly Mock<IUnitOfWork> _unitOfWorkMoq;
        private readonly Mock<ILogger<BankSafeTransactionsService>> _loggerMoq;
        public BankSafeTransactionsTest()
        {
            _moqData = new BankSafeTransactionsMoqData();
            _repositorMoq = new Mock<IBankSafeTransactionsRepositorie>();
            _unitOfWorkMoq = new Mock<IUnitOfWork>();
            _loggerMoq = new Mock<ILogger<BankSafeTransactionsService>>();
        }

        [Fact]
        [Trait("Service", "BankSafeTransactions")]
        public async Task AddTestAsync()
        {
            var data = await _moqData.Get();
            _repositorMoq.Setup(p => p.AddAsync(It.IsAny<BankSafeTransactions>()))
                .Returns(() => ValueTask.CompletedTask);
            BankSafeTransactionsService bankSafeTransactionsService = new BankSafeTransactionsService(_unitOfWorkMoq.Object,
                _repositorMoq.Object,
                _loggerMoq.Object);


            var result = await bankSafeTransactionsService.AddAsync(data);


            Assert.NotNull(result);
            Assert.IsType<OperationResult>(result);
            if (result.Success)
            {
                Assert.Null(result.Message);
            }
            else
            {
                Assert.NotNull(result.Message);
            }
        }
        [Fact]
        [Trait("Service", "BankSafeTransactions")]
        public async Task GetAllTestAsync()
        {
            _repositorMoq.Setup(p => p.GetAllAsync())
                .Returns(_moqData.GetAll());
            BankSafeTransactionsService bankSafeTransactionsService = new BankSafeTransactionsService(_unitOfWorkMoq.Object,
                _repositorMoq.Object,
                _loggerMoq.Object);


            var result = await bankSafeTransactionsService.GetAllAsync();


            Assert.IsType<OperationResult<List<BankSafeTransactions>>>(result);
            if (result.Success)
            {
                Assert.Null(result.Message);

            }
            else
            {
                Assert.NotNull(result.Message);
                Assert.Null(result.Data);
            }

        }
        [Theory]
        [Trait("Service", "BankSafeTransactions")]
        [InlineData("3b2667f6-995c-49fb-a36b-195c965f442c")]
        [InlineData("1872255b-72dd-4cc6-84fa-50ab94677aca")]
        [InlineData("17466fd3-e221-413d-a419-dd4690bf7bc1")]
        public async Task GetTestAsync(Guid code)
        {
            _repositorMoq.Setup(p => p.GetAsync(It.IsAny<Guid>()))
                .Returns(_moqData.Get());
            BankSafeTransactionsService bankSafeTransactionsService = new BankSafeTransactionsService(_unitOfWorkMoq.Object,
                _repositorMoq.Object,
                _loggerMoq.Object);


            var result = await bankSafeTransactionsService.GetAsync(code);


            Assert.IsType<OperationResult<BankSafeTransactions>>(result);
            if (result.Success)
            {
                Assert.Null(result.Message);

            }
            else
            {
                Assert.NotNull(result.Message);
                Assert.Null(result.Data);
            }

        }

    }
}
