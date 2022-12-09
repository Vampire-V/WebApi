using Microsoft.AspNetCore.Mvc;
using WebApi.Models;
using WebApi.Services.Interface;
using Microsoft.AspNetCore.Authorization;
using Hangfire;
using WebApi.Extensions;
using WebApi.Middleware.Exceptions;
using WebApi.Data.UserContext.Repositories.Interfaces;

namespace WebApi.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class JobsController : ControllerBase
    {
        private readonly IMailService _mailServise;
        private readonly IFileService _fileService;
        private readonly IBackUpDBService _backupDB;
        private readonly IJobService _jobService;
        private readonly IContractCompletedRepository _contractCPRepository;
        private readonly IBackgroundJobClient _backgroundJobClient;
        private readonly IRecurringJobManager _recurringJobManager;
        private readonly ILogger<JobsController> _logger;

        public JobsController(ILogger<JobsController> logger, IMailService mailServise, IFileService fileService, IBackUpDBService backupDB, IBackgroundJobClient backgroundJobClient, IRecurringJobManager recurringJobManager, IJobService jobService, IContractCompletedRepository contractCPRepository)
        {
            _logger = logger;
            _mailServise = mailServise;
            _fileService = fileService;
            _backupDB = backupDB;
            _backgroundJobClient = backgroundJobClient;
            _recurringJobManager = recurringJobManager;
            _jobService = jobService;
            _contractCPRepository = contractCPRepository;
        }

        // [AllowAnonymous]
        [HttpPost("[action]")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult BackUpLaravel([FromBody] JobSchedulerRequest jobScheduler)
        {
            try
            {
                _jobService.HaierKpiJob(jobScheduler.Name, jobScheduler.CronFormat);

                return Ok();
            }
            catch (Exception ex)
            {
                _logger.LogError(jobScheduler.Name, ex, ex.Message);
                throw new BadRequestException(ex.Message);
            }
        }

        [HttpPost("[action]")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult BackUpDB([FromBody] JobSchedulerRequest jobScheduler)
        {
            try
            {
                // RecurringJob.AddOrUpdate<IBackUpDBService>(jobScheduler.Name, b => b.BackupMysql(), jobScheduler.CronFormat, DateTimeSystem.TimeZone);
                _jobService.ReccuringJob(jobScheduler.Name, jobScheduler.CronFormat);

                return Ok();
            }
            catch (Exception ex)
            {
                _logger.LogError(jobScheduler.Name, ex, ex.Message);
                throw new BadRequestException(ex.Message);
            }
        }

        // [AllowAnonymous]
        [HttpPost("[action]")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult Continuation([FromBody] JobSchedulerRequest jobScheduler)
        {
            try
            {
                // _jobService.ReccuringJob(jobScheduler.Name, jobScheduler.CronFormat);

                return Ok();
            }
            catch (Exception ex)
            {
                _logger.LogError(jobScheduler.Name, ex, ex.Message);
                throw new BadRequestException(ex.Message);
            }
        }

        [AllowAnonymous]
        [HttpGet("[action]")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> ContractAlert()
        {
            try
            {
                var items = await _contractCPRepository.NinetyDaysExpire();
                return Ok(items);
            }
            catch (Exception ex)
            {
                throw new BadRequestException(ex.Message);
            }
        }
    }
}