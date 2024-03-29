﻿using Application.UnitOfWork;
using Domain.Entity;
using Domain.IRepositories;
using Domain.Message;
using Domain.OperationResults;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services
{
    public class BankSafeService
    {
        private readonly IBankSafeRepositorie _bankSafeRepositorie;
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<BankSafeService> _logger;
        public BankSafeService(IBankSafeRepositorie bankSafeRepositorie,
            IUnitOfWork unitOfWork
            , ILogger<BankSafeService> logger)
        {
            _bankSafeRepositorie = bankSafeRepositorie;
            _unitOfWork = unitOfWork;
            _logger = logger;
        }
        public async Task<OperationResult> AddAsync(BankSafe bankSafe,
            CancellationToken cancellationToken = default)
        {
            try
            {
                await _bankSafeRepositorie.AddAsync(bankSafe);
                await _unitOfWork.SaveChangesAsync(cancellationToken);
                string message = string.Format(ConstMessages.Successfully
                    , bankSafe.Name.Value
                    , nameof(AddAsync));
                _logger.LogInformation(message);
                return new OperationResult(true, null);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return new OperationResult(false, ex.Message);
            }

        }
        public async Task<OperationResult> UpdateAsync(BankSafe bankSafe,
            CancellationToken cancellationToken = default)
        {
            try
            {
                var result = await _bankSafeRepositorie.GetAsync(bankSafe.Name);
                if (result != null)
                {
                    result.Update(bankSafe.SharePrice);
                    await _unitOfWork.SaveChangesAsync(cancellationToken);
                    string message = string.Format(ConstMessages.Successfully
                        , bankSafe.Name.Value
                        , nameof(UpdateAsync));
                    _logger.LogInformation(message);
                    return new OperationResult(true, null);
                }
                else
                {
                    string message = string.Format(ConstMessages.NotFound, bankSafe.Name.Value);
                    throw new Exception(message);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return new OperationResult(false, ex.Message);
            }
        }
        public async Task<OperationResult> DeleteAsync(string name,
            CancellationToken cancellationToken = default)
        {
            try
            {
                await _bankSafeRepositorie.DeleteAsync(name);
                await _unitOfWork.SaveChangesAsync(cancellationToken);
                string message = string.Format(ConstMessages.Successfully
                        , name
                        , nameof(DeleteAsync));
                _logger.LogInformation(message);
                return new OperationResult(true, null);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return new OperationResult(false, ex.Message);
            }
        }
        public async Task<OperationResult<List<BankSafe>>> GetAllAsync()
        {
            try
            {
                var result = await _bankSafeRepositorie.GetAllAsync();
                string message = string.Format(ConstMessages.Successfully
                        , nameof(GetAllAsync)
                        , "");
                _logger.LogInformation(message);
                return new OperationResult<List<BankSafe>>(true, null, result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return new OperationResult<List<BankSafe>>(false, ex.Message, null);
            }

        }
        public async Task<OperationResult<BankSafe>> GetAsync(string name)
        {
            try
            {
                var result = await _bankSafeRepositorie.GetAsync(name);
                string message = string.Format(ConstMessages.Successfully
                        , nameof(GetAsync)
                        , "");
                _logger.LogInformation(message);
                return new OperationResult<BankSafe>(true, null, result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return new OperationResult<BankSafe>(false, ex.Message, null);
            }

        }
    }
}
