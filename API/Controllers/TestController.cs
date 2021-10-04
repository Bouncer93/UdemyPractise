using BLL.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Controllers
{
    
    public class TestController : MainApiController
    {
        private readonly ITestService _testService;
        private readonly ITransactionService _transactionService;

        public TestController(ITestService testService , ITransactionService transactionService)
        {
            _testService = testService;
           _transactionService = transactionService;
        }
        [HttpGet]
        public async Task<IActionResult>SeedData()
        {
            await _transactionService.FinancialTransaction();
            
            return Ok("hello world");
        }

       

        
    }
}
