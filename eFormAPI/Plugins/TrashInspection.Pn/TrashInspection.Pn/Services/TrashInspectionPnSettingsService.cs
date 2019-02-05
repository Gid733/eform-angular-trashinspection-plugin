﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using TrashInspection.Pn.Abstractions;
using TrashInspection.Pn.Infrastructure.Models;
using eFormCore;
using eFormData;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microting.eFormApi.BasePn.Abstractions;
using Microting.eFormApi.BasePn.Infrastructure.Models.API;
using Microting.eFormTrashInspectionBase.Infrastructure;
using Microting.eFormTrashInspectionBase.Infrastructure.Data.Entities;
using Microting.eFormTrashInspectionBase.Infrastructure.Data.Factories;

namespace TrashInspection.Pn.Services
{
    public class TrashInspectionPnSettingsService : ITrashInspectionPnSettingsService
    {
        private readonly ILogger<TrashInspectionPnSettingsService> _logger;
        private readonly ITrashInspectionLocalizationService _trashInspectionLocalizationService;
        private readonly TrashInspectionPnDbContext _dbContext;
        private readonly IEFormCoreService _coreHelper;
        private readonly string _connectionString;

        public TrashInspectionPnSettingsService(ILogger<TrashInspectionPnSettingsService> logger,
            ITrashInspectionLocalizationService trashInspectionLocalizationService,
            TrashInspectionPnDbContext dbContext,
            IEFormCoreService coreHelper)
        {
            _logger = logger;
            _dbContext = dbContext;
            _coreHelper = coreHelper;
            _trashInspectionLocalizationService = trashInspectionLocalizationService;
        }

        public OperationDataResult<TrashInspectionPnSettingsModel> GetSettings()
        {
            try
            {
                TrashInspectionPnSettingsModel result = new TrashInspectionPnSettingsModel();
                List<TrashInspectionPnSetting> trashInspectionPnSetting = _dbContext.TrashInspectionPnSettings.ToList();
                if (trashInspectionPnSetting.Count < 6)
                {
                    TrashInspectionPnSettingsModel.SettingCreateDefaults(_dbContext);                    
                    trashInspectionPnSetting = _dbContext.TrashInspectionPnSettings.AsNoTracking().ToList();
                }
                result.trashInspectionSettingsList = new List<TrashInspectionPnSettingModel>();
                foreach (TrashInspectionPnSetting inspectionPnSetting in trashInspectionPnSetting)
                {
                    TrashInspectionPnSettingModel trashInspectionPnSettingModel = new TrashInspectionPnSettingModel();
                    trashInspectionPnSettingModel.Id = inspectionPnSetting.Id;
                    trashInspectionPnSettingModel.Name = inspectionPnSetting.Name;
                    trashInspectionPnSettingModel.Value = inspectionPnSetting.Value;
                    result.trashInspectionSettingsList.Add(trashInspectionPnSettingModel);
                }

                return new OperationDataResult<TrashInspectionPnSettingsModel>(true, result);
            }
            catch(Exception e)
            {
                Trace.TraceError(e.Message);
                _logger.LogError(e.Message);
                return new OperationDataResult<TrashInspectionPnSettingsModel>(false,
                    _trashInspectionLocalizationService.GetString("ErrorWhileObtainingTrashInspectionSettings"));
            }
        }

        public OperationResult UpdateSettings(TrashInspectionPnSettingsModel trashInspectionSettingsModel)
        {
            try
            {
//                TrashInspectionPnSetting trashInspectionPnSetting = _dbContext.TrashInspectionPnSettings.FirstOrDefault();
//                if (trashInspectionPnSetting == null)
//                {
                foreach (TrashInspectionPnSettingModel trashInspectionPnSettingModel in trashInspectionSettingsModel.trashInspectionSettingsList)
                {
                    trashInspectionPnSettingModel.Update(_dbContext);
                }
//                }
                return new OperationResult(true,
                    _trashInspectionLocalizationService.GetString("SettingsHaveBeenUpdatedSuccesfully"));
            }
            catch(Exception e)
            {
                Trace.TraceError(e.Message);
                _logger.LogError(e.Message);
                return new OperationResult(false, _trashInspectionLocalizationService.GetString("ErrorWhileUpdatingSettings"));
            }
        }
    }
}
