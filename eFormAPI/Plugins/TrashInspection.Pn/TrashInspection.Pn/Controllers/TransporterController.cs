using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microting.eFormApi.BasePn.Infrastructure.Models.API;
using TrashInspection.Pn.Abstractions;
using TrashInspection.Pn.Infrastructure.Models;

namespace TrashInspection.Pn.Controllers
{
    public class TransporterController : Controller
    {

        private readonly ITransporterService _transporterService;

        public TransporterController(ITransporterService transporterService)
        {
            _transporterService = transporterService;
        }
        [HttpGet]
        [Route("api/trash-inspection-pn/transporters")]
        public async Task<OperationDataResult<TransportersModel>> GetAllTransporters(TransporterRequestModel requestModel)
        {
            return await _transporterService.GetAllTransporters(requestModel);
        }

        [HttpGet]
        [Route("api/trash-inspection-pn/transporters/{id}")]
        public async Task<OperationDataResult<TransporterModel>> GetSingleTransporter(int id)
        {
            return await _transporterService.GetSingleTransporter(id);
        }
        
        [HttpGet]
        [Route("api/trash-inspection-pn/transporters/{id}/{year}")]
        public async Task<OperationDataResult<StatByMonth>> GetSingleTransporterByMonth(int id, int year)
        {
            return await _transporterService.GetSingleTransporterByMonth(id, year);
        }

        [HttpPost]
        [Route("api/trash-inspection-pn/transporters")]
        public async Task<OperationResult> CreateTransporter([FromBody] TransporterModel createModel)
        {
            return await _transporterService.CreateTransporter(createModel);
        }

        [HttpPut]
        [Route("api/trash-inspection-pn/transporters")]
        public async Task<OperationResult> UpdateTransporter([FromBody] TransporterModel updateModel)
        {
            return await _transporterService.UpdateTransporter(updateModel);
        }

        [HttpDelete]
        [Route("api/trash-inspection-pn/transporters/{id}")]
        public async Task<OperationResult> DeleteTransporter(int id)
        {
            return await _transporterService.DeleteTransporter(id);
        }

        [HttpPost]
        [Route("api/trash-inspection-pn/transporters/import")]
        public async Task<OperationResult> ImportTransporter([FromBody] TransporterImportModel transporterImportModel)
        {
            return await _transporterService.ImportTransporter(transporterImportModel);
        }

        [HttpGet]
        [Route("api/trash-inspection-pn/transporters/year/{year}")]
        public async Task<OperationDataResult<StatsByYearModel>> GetTransportersStatsByYear(TransportersYearRequestModel requestModel)
        {
            return await _transporterService.GetTransportersStatsByYear(requestModel);
        }
    }
}