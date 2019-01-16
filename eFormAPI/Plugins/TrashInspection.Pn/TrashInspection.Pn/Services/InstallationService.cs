﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using TrashInspection.Pn.Abstractions;
using TrashInspection.Pn.Infrastructure.Data;
using TrashInspection.Pn.Infrastructure.Data.Entities;
using TrashInspection.Pn.Infrastructure.Extensions;
using TrashInspection.Pn.Infrastructure.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microting.eFormApi.BasePn.Abstractions;
using Microting.eFormApi.BasePn.Infrastructure.Models.API;
using Microting.eFormApi.BasePn.Infrastructure.Extensions;

namespace TrashInspection.Pn.Services
{
    public class InstallationService : IInstallationService
    {
        private readonly IEFormCoreService _coreHelper;
        private readonly ILogger<InstallationService> _logger;
        private readonly TrashInspectionPnDbContext _dbContext;
        private readonly ITrashInspectionLocalizationService _trashInspectionLocalizationService;

        public InstallationService(ILogger<InstallationService> logger,
            TrashInspectionPnDbContext dbContext,
            IEFormCoreService coreHelper,
            ITrashInspectionLocalizationService trashInspectionLocalizationService)
        {
            _logger = logger;
            _dbContext = dbContext;
            _coreHelper = coreHelper;
            _trashInspectionLocalizationService = trashInspectionLocalizationService;
        }

        public async Task<OperationDataResult<InstallationsModel>> GetAllInstallations(InstallationRequestModel pnRequestModel)
        {
            try
            {
                var installationsModel = new InstallationsModel();

                IQueryable<Installation> installationsQuery = _dbContext.Installations.AsQueryable();
                if (!string.IsNullOrEmpty(pnRequestModel.Sort))
                {
                    if (pnRequestModel.IsSortDsc)
                    {
                        installationsQuery = installationsQuery
                            .CustomOrderByDescending(pnRequestModel.Sort);
                    }
                    else
                    {
                        installationsQuery = installationsQuery
                            .CustomOrderBy(pnRequestModel.Sort);
                    }
                }
                else
                {
                    installationsQuery = _dbContext.Installations
                        .OrderBy(x => x.Id);
                }

                if (pnRequestModel.PageSize != null)
                {
                    installationsQuery = installationsQuery
                        .Skip(pnRequestModel.Offset)
                        .Take((int)pnRequestModel.PageSize);
                }

                List<InstallationModel> installations = await installationsQuery.Select(x => new InstallationModel()
                {
                    Name = x.Name,
                }).ToListAsync();

                installationsModel.Total = await _dbContext.TrashInspections.CountAsync();
                installationsModel.InstallationList = installations;

                return new OperationDataResult<InstallationsModel>(true, installationsModel);
            }
            catch (Exception e)
            {
                Trace.TraceError(e.Message);
                _logger.LogError(e.Message);
                return new OperationDataResult<InstallationsModel>(false,
                    _trashInspectionLocalizationService.GetString("ErrorObtainingTrashInspections"));

            }
        }

        public async Task<OperationDataResult<InstallationModel>> GetSingleInstallation(int installationId)
        {
            try
            {
                var installation = await _dbContext.Installations.Select(x => new InstallationModel()
                {
                    Name = x.Name
                })
                .FirstOrDefaultAsync(x => x.Id == installationId);

                if (installation == null)
                {
                    return new OperationDataResult<InstallationModel>(false,
                        _trashInspectionLocalizationService.GetString($"TrashInspectionWithID:{installationId}DoesNotExist"));
                }

                return new OperationDataResult<InstallationModel>(true, installation);
            }
            catch (Exception e)
            {
                Trace.TraceError(e.Message);
                _logger.LogError(e.Message);
                return new OperationDataResult<InstallationModel>(false,
                    _trashInspectionLocalizationService.GetString("ErrorObtainingTrashInspection"));
            }
        }

        public async Task<OperationResult> CreateInstallation(InstallationModel createModel)
        {
            createModel.Save(_dbContext);
            return new OperationResult(true);

        }

        public async Task<OperationResult> UpdateInstallation(InstallationModel updateModel)
        {
            updateModel.Update(_dbContext);
            return new OperationResult(true);
        }

        public async Task<OperationResult> DeleteInstallation(int installationId)
        {
            InstallationModel installation = new InstallationModel();
            installation.Id = installationId;
            installation.Delete(_dbContext);
            return new OperationResult(true);

        }
    }
}
