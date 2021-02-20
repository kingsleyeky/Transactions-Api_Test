using Microsoft.AspNetCore.Mvc;
using Serilog;
using System;
using System.Threading.Tasks;

namespace Transaction.API.Controllers
{

    public abstract class BaseController : ControllerBase
    {
        private readonly ILogger _logger;

        public BaseController(ILogger logger)
        {
            _logger = logger;
        }

        [ApiExplorerSettings(IgnoreApi = true)]
        public virtual async Task<ActionResult> ExecuteRequestAsync(Func<Task> executeMethod)
        {
            try
            {
                await executeMethod();

                return Ok();
            }
            catch (ApplicationException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message, ex);
                return BadRequest("An error occured while processing your request");
            }
        }

        [ApiExplorerSettings(IgnoreApi = true)]
        public virtual async Task<ActionResult<T>> ExecuteRequestAsync<T>(Func<Task<T>> executeMethod)
        {
            try
            {
                var result = await executeMethod();

                return Ok(result);
            }
            catch (ApplicationException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message, ex);
                return BadRequest("An error occured while processing your request");
            }
        }
    }
}