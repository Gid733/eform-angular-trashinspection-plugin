﻿using System.Threading.Tasks;
using TrashInspection.Pn.Infrastructure.Models;
using Microting.eFormApi.BasePn.Infrastructure.Models.API;
using Microting.eFormTrashInspectionBase.Infrastructure.Data.Entities;

namespace TrashInspection.Pn.Abstractions
{
    public interface ITrashInspectionService
    {
        Task<OperationResult> CreateTrashInspection(TrashInspectionModel model);
        Task<OperationResult> DeleteTrashInspection(int trashInspectionId);
        Task<OperationResult> DeleteTrashInspection(string weighingNumber, string token);
        Task<OperationResult> UpdateTrashInspection(TrashInspectionModel updateModel);
        Task<OperationDataResult<TrashInspectionsModel>> GetAllTrashInspections(TrashInspectionRequestModel requestModel);
        Task<string> DownloadEFormPdf(string weighingNumber, string token, string fileType);
        Task<OperationDataResult<TrashInspectionModel>> GetSingleTrashInspection(int trashInspectionId);
        Task<OperationDataResult<TrashInspectionVersionsModel>> GetTrashInspectionVersion(int trashInspectionId);
        Task<OperationDataResult<TrashInspectionCaseVersionsModel>>
            GetTrashInspectionCaseVersions(int trashInspectionId);
    }
}
