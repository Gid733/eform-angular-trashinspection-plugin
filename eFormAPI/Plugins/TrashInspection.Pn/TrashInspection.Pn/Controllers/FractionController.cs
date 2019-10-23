using System.Threading.Tasks;
using TrashInspection.Pn.Abstractions;
using TrashInspection.Pn.Infrastructure.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microting.eFormApi.BasePn.Infrastructure.Models.API;
using Microting.eFormTrashInspectionBase.Infrastructure.Const;

namespace TrashInspection.Pn.Controllers
{
    [Authorize]
    public class FractionController : Controller
    {
        private readonly IFractionService _fractionService;

        public FractionController(IFractionService fractionService)
        {
            _fractionService = fractionService;
        }
        
        [HttpGet]
        [Route("api/trash-inspection-pn/fractions")]
        public async Task<OperationDataResult<FractionsModel>> GetAllFractions(FractionRequestModel requestModel)
        {
            return await _fractionService.GetAllFractions(requestModel);
        }

        [HttpGet]
        [Route("api/trash-inspection-pn/fractions/{id}")]
        public async Task<OperationDataResult<FractionModel>> GetSingleFraction(int id)
        {
            return await _fractionService.GetSingleFraction(id);
        }

        [HttpGet]
        [Route("api/trash-inspection-pn/fractions/{id}/{year}")]
        public async Task<OperationDataResult<StatByMonth>> GetSingleFractionByMonth(int id, int year)
        {
            return await _fractionService.GetSingleFractionByMonth(id, year);
        }
        
        [HttpPost]
        [Route("api/trash-inspection-pn/fractions")]
        [Authorize(Policy = TrashInspectionClaims.CreateFractions)]
        public async Task<OperationResult> CreateFraction([FromBody] FractionModel createModel)
        {
            return await _fractionService.CreateFraction(createModel);
        }

        [HttpPut]
        [Route("api/trash-inspection-pn/fractions")]
        public async Task<OperationResult> UpdateFraction([FromBody] FractionModel updateModel)
        {
            return await _fractionService.UpdateFraction(updateModel);
        }

        [HttpDelete]
        [Route("api/trash-inspection-pn/fractions/{id}")]
        public async Task<OperationResult> DeleteFraction(int id)
        {
            return await _fractionService.DeleteFraction(id);
        }

        [HttpPost]
        [Route("api/trash-inspection-pn/fractions/import")]
        public async Task<OperationResult> ImportFraction([FromBody] FractionImportModel fractionImportModel)
        {
            return await _fractionService.ImportFraction(fractionImportModel);
        }
        
        [HttpGet]
        [Route("api/trash-inspection-pn/fractions/year/{year}")]
        public async Task<OperationDataResult<StatsByYearModel>> GetFractionsStatsByYear(FractionPnYearRequestModel requestModel)
        {
            return await _fractionService.GetFractionsStatsByYear(requestModel);
        }
    }
}
