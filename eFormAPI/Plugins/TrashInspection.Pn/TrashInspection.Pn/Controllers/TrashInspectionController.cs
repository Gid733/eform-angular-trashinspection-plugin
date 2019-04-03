﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using eFormData;
using TrashInspection.Pn.Abstractions;
using TrashInspection.Pn.Infrastructure.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity.UI.V3.Pages.Account.Internal;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microting.eFormApi.BasePn.Infrastructure.Database.Entities;
using Microting.eFormApi.BasePn.Infrastructure.Models.API;
using Microting.eFormTrashInspectionBase.Infrastructure.Data.Factories;
using Newtonsoft.Json;

namespace TrashInspection.Pn.Conrtrollers
{
    public class TrashInspectionController : Controller
    {
        private readonly ITrashInspectionService _trashInspectionService;
        private readonly TrashInspectionPnDbContext _dbContext;

        public TrashInspectionController(ITrashInspectionService trashInspectionService)
        {
            _trashInspectionService = trashInspectionService;
        }

        [HttpGet]
        [Authorize]
        [Route("api/trash-inspection-pn/inspections")]
        public async Task<OperationDataResult<TrashInspectionsModel>> GetAllTrashInspections(TrashInspectionRequestModel requestModel)
        {
            return await _trashInspectionService.GetAllTrashInspections(requestModel);
        }

        [HttpGet]
        [AllowAnonymous]
        [Route("api/trash-inspection-pn/inspection-results/{weighingNumber}", Name = "token")]
        public async Task<IActionResult> DownloadEFormPdf(string weighingNumber, string token)
        {
            try
            {
                string filePath = await _trashInspectionService.DownloadEFormPdf(weighingNumber, token);
                
                if (!System.IO.File.Exists(filePath))
                {
                    return NotFound();
                }
                var fileStream = new FileStream(filePath, FileMode.Open, FileAccess.Read);
                return File(fileStream, "application/pdf", Path.GetFileName(filePath));
            }
            catch (UnauthorizedAccessException)
            {
                return Forbid();
            }
            catch 
            {
                return BadRequest();
                
            }

        }

        [HttpGet]
        [Authorize]
        [Route("api/trash-inspection-pn/inspections/{id}")]
        public async Task<OperationDataResult<TrashInspectionModel>> GetSingleTrashInspection(int id)
        {
            return await _trashInspectionService.GetSingleTrashInspection(id);
        }

        [HttpPost]
        [AllowAnonymous]
        [DebuggingFilter]
        [Route("api/trash-inspection-pn/inspections")]
        public async Task<OperationResult> CreateTrashInspection([FromBody] TrashInspectionModel createModel)
        {
            return await _trashInspectionService.CreateTrashInspection(createModel);
        }

        [HttpPut]
        [Authorize]
        [Route("api/trash-inspection-pn/inspections")]
        public async Task<OperationResult> UpdateTrashInspection([FromBody] TrashInspectionModel updateModel)
        {
            return await _trashInspectionService.UpdateTrashInspection(updateModel);
        }

        [HttpDelete]
        [Authorize]
        [Route("api/trash-inspection-pn/inspections/{id}")]
        public async Task<OperationResult> DeleteTrashInspection(int id)
        {
            return await _trashInspectionService.DeleteTrashInspection(id);
        }
                
        [HttpDelete]
        [AllowAnonymous]
        [Route("api/trash-inspection-pn/inspection-results/{weighingNumber}", Name = "token")]
        public async Task<OperationResult> DeleteTrashInspection(string weighingNumber, string token)
        {
            return await _trashInspectionService.DeleteTrashInspection(weighingNumber, token);

        }
    }
    
    public class DebuggingFilter : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            if (filterContext.HttpContext.Request.Method != "POST")
            {
                return;
            }
            
            filterContext.HttpContext.Request.Body.Seek(0, SeekOrigin.Begin);

            string text = new StreamReader(filterContext.HttpContext.Request.Body).ReadToEndAsync().Result;

            filterContext.HttpContext.Request.Body.Seek(0, SeekOrigin.Begin);
            
            Console.WriteLine(text);

            base.OnActionExecuting(filterContext);
        }
    }
}
