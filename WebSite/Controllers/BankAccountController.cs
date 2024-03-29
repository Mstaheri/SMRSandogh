﻿using Application.Services;
using Domain.Entity;
using Domain.ValueObjects;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebSite.Controllers
{
    [Route("api/BankAccount")]
    [ApiController]
    public class BankAccountController : ControllerBase
    {
        private readonly BankAccountService _bankAccountService;
        public BankAccountController(BankAccountService bankAccountService)
        {
            _bankAccountService = bankAccountService;
        }
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var result = await _bankAccountService.GetAllAsync();
            if (result.Success)
            {
                return Ok(result.Data);
            }
            else
            {
                return BadRequest(result.Message);
            }
        }
        [HttpGet("{AccountNumber}")]
        public async Task<IActionResult> Get([FromRoute] string AccountNumber)
        {
            
            var result = await _bankAccountService.GetAsync(AccountNumber);
            if (result.Success)
            {
                return Ok(result.Data);
            }
            else
            {
                return BadRequest(result.Message);
            }
        }
        [HttpPost]
        public async Task<IActionResult> Insert([FromBody] BankAccount bankAccount)
        {
            var result = await _bankAccountService.AddAsync(bankAccount);
            if (result.Success)
            {
                string url = Url.Action(nameof(Get), "bankAccount", new { accountNumber = bankAccount.AccountNumber.Value }, Request.Scheme);
                return Created(url, result.Success);
            }
            else
            {
                return BadRequest(result.Message);
            }
        }
        [HttpPut]
        public async Task<IActionResult> Update([FromBody] BankAccount bankAccount)
        {
            var result = await _bankAccountService.UpdateAsync(bankAccount);
            if (result.Success)
            {
                return Ok(result.Success);
            }
            else
            {
                return BadRequest(result.Message);
            }
        }
        [HttpDelete("{AccountNumber}")]
        public async Task<IActionResult> Delete([FromRoute] string AccountNumber)
        {
            var result = await _bankAccountService.DeleteAsync(AccountNumber);
            if (result.Success)
            {
                return Ok(result.Success);
            }
            else
            {
                return BadRequest(result.Message);
            }
        }
    }
}
