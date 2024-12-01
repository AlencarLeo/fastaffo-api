using fastaffo_api.src.Application.DTOs;
using fastaffo_api.src.Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace fastaffo_api.src.Api.Controllers;
public class JobsController : ControllerBase
{
    public static Job job = new Job();

    public JobsController()
    {

    }

    [HttpGet("nextjob")]
    public ActionResult<Job> GetNextJob()
    {

        return Ok(job);
    }

    [HttpPost("job")]
    public ActionResult<List<JobDto>> CreateJob(JobDto request)
    {
        job.Title = request.Title;
        job.Company = request.Company;
        job.BaseRate = request.BaseRate;
        job.DateTime= request.DateTime;
        job.Location = request.Location;

        return Ok();
    }

}