using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.ApplicationInsights.DataContracts;
using Microsoft.ApplicationInsights;
using Example.Rest.Entities.Interfaces;
using Example.Rest.Entities.Models;

namespace Example.Rest.Service.Controllers
{
    [Route("api/[controller]")]
    public class ModelController : Controller, IModelController
    {
        private readonly IBusinessContext _businessContext;
        private readonly ILogger _logger;
        private readonly TelemetryClient _telemetry = new TelemetryClient();

        public ModelController(IBusinessContext businessContext, ILogger<ModelController> logger)
        {
            _businessContext = businessContext;
            _logger = logger;
        }

        /// <summary>
        /// GET by identifier method
        /// </summary>
        /// <param name="id">unique identifier</param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            try
            {
                _telemetry.TrackPageView(new PageViewTelemetry("ModelController.GetById") { Timestamp = DateTime.UtcNow });
                _telemetry.Flush();
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                Model result = await _businessContext.GetAllAsyncById(id);

                return Ok(result);
            }
            catch (AggregateException ex)
            {
                ErrorTreatment("GetById", null, ex);

                return NotFound();
            }
            catch (Exception ex)
            {
                ErrorTreatment("GetById", ex, null);
                return StatusCode(500, ex);
            }
        }

        /// <summary>
        /// GET method
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                _telemetry.TrackPageView(new PageViewTelemetry("ModelController.GetAll") { Timestamp = DateTime.UtcNow });
                _telemetry.Flush();
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                IList<Model> result = await _businessContext.GetAllAsync();

                return Ok(result);
            }
            catch (AggregateException ex)
            {
                ErrorTreatment("GetAll", null, ex);
                return NotFound();
            }
            catch (Exception ex)
            {
                ErrorTreatment("GetAll", ex, null);
                return StatusCode(500, ex);
            }
        }

        /// <summary>
        /// POST method
        /// </summary>
        /// <param name="model">A Model object</param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> Create([FromBody]Model model)
        {
            try
            {
                _telemetry.TrackPageView(new PageViewTelemetry("ModelController.Create") { Timestamp = DateTime.UtcNow });
                _telemetry.Flush();
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                await _businessContext.AddAsync(model);

                return Ok(model);
            }
            catch (AggregateException ex)
            {
                ErrorTreatment("Create", null, ex);
                return NotFound();
            }
            catch (Exception ex)
            {
                ErrorTreatment("Create", ex, null);
                return StatusCode(500, ex);
            }
        }

        /// <summary>
        /// PUT method
        /// </summary>
        /// <param name="id">unique identifier</param>
        /// <param name="model">A Mmodel object</param>
        /// <returns>Returns an IActionResult filled</returns>
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody]Model model)
        {
            try
            {
                _telemetry.TrackPageView(new PageViewTelemetry("ModelController.Update") { Timestamp = DateTime.UtcNow });
                _telemetry.Flush();
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                await _businessContext.UpdateAsync(id, model);
                return Ok();
            }
            catch (AggregateException ex)
            {
                ErrorTreatment("Update", null, ex);
                return NotFound();
            }
            catch (Exception ex)
            {
                ErrorTreatment("Update", ex, null);
                return StatusCode(500, ex);
            }
        }

        /// <summary>
        /// DELETE method
        /// </summary>
        /// <param name="id">unique identifier</param>
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                _telemetry.TrackPageView(new PageViewTelemetry("ModelController.Delete") { Timestamp = DateTime.UtcNow });
                _telemetry.Flush();
                await _businessContext.DeleteAsync(id);
                return Ok();
            }
            catch (AggregateException ex)
            {
                ErrorTreatment("Delete", null, ex);
                return NotFound();
            }
            catch (Exception ex)
            {
                ErrorTreatment("Delete", ex, null);
                return StatusCode(500, ex);
            }
        }

        private void ErrorTreatment(string method, Exception exception, AggregateException aggregateException)
        {
            TrackTelemetryError(exception, aggregateException);
            TrackLoggerError(method, exception, aggregateException);
        }

        private void TrackTelemetryError(Exception exception, AggregateException aggregateException)
        {
            if (!string.IsNullOrEmpty(exception.Message))
            {
                _telemetry.TrackException(new ExceptionTelemetry(exception)
                {
                    Timestamp = DateTime.UtcNow,
                    SeverityLevel = SeverityLevel.Critical
                });
            }
            else
            {
                _telemetry.TrackException(new ExceptionTelemetry(aggregateException)
                {
                    Timestamp = DateTime.UtcNow,
                    SeverityLevel = SeverityLevel.Critical
                });
            }
        }

        private void TrackLoggerError(string method, Exception exception, AggregateException aggregateException)
        {
            string logError = $"{this.GetType().FullName}. On {method} error : {exception.Message}{aggregateException.Message}"; // todo.
            _logger.LogError(logError); // todo.
        }
    }
}
