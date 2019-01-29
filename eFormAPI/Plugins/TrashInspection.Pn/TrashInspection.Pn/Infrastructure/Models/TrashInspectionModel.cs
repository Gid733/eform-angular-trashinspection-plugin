﻿using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using Microting.eFormTrashInspectionBase.Infrastructure.Data.Entities;
using Microting.eFormTrashInspectionBase.Infrastructure.Data.Factories;

namespace TrashInspection.Pn.Infrastructure.Models
{
    public class TrashInspectionModel : IModel
    {
        public int Id { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        [StringLength(255)]
        public string WorkflowState { get; set; }
        public int Version { get; set; }
        public int CreatedByUserId { get; set; }
        public int UpdatedByUserId { get; set; }
        public int WeighingNumber { get; set; }
        public DateTime Date { get; set; }
        public DateTime Time { get; set; }
        public string RegistrationNumber { get; set; }
        public int TrashFraction { get; set; }
        public int EakCode { get; set; }
        public string Producer { get; set; }
        public string Transporter { get; set; }
        public string InstallationName { get; set; }
        public string Token { get; set; }
        public int InstallationId { get; set; }
        public bool MustBeInspected { get; set; }
        public int Status { get; set; }

        public void Save(TrashInspectionPnDbContext _dbContext)
        {
            Microting.eFormTrashInspectionBase.Infrastructure.Data.Entities.TrashInspection trashInspection = new Microting.eFormTrashInspectionBase.Infrastructure.Data.Entities.TrashInspection();            
            if (CreatedAt != null) {
                trashInspection.CreatedAt = (DateTime)CreatedAt;                
            }
            trashInspection.CreatedByUserId = CreatedByUserId;
            trashInspection.Date = Date;
            trashInspection.Eak_Code = EakCode;
            trashInspection.InstallationId = InstallationId;
            trashInspection.MustBeInspected = MustBeInspected;
            trashInspection.Producer = Producer;
            trashInspection.RegistrationNumber = RegistrationNumber;
            trashInspection.Time = Time;
            trashInspection.Transporter = Transporter;
            trashInspection.TrashFraction = TrashFraction;
            trashInspection.UpdatedAt = DateTime.Now;
            trashInspection.UpdatedByUserId = UpdatedByUserId;
            trashInspection.Version = Version;
            trashInspection.WeighingNumber = WeighingNumber;
            trashInspection.Status = Status;
            trashInspection.WorkflowState = eFormShared.Constants.WorkflowStates.Created;

            _dbContext.TrashInspections.Add(trashInspection);
            _dbContext.SaveChanges();

            _dbContext.TrashInspectionVersions.Add(MapTrashInspectionVersion(_dbContext, trashInspection));
            _dbContext.SaveChanges();
            Id = trashInspection.Id;
        }

        public void Update(TrashInspectionPnDbContext _dbContext)
        {
            Microting.eFormTrashInspectionBase.Infrastructure.Data.Entities.TrashInspection trashInspection = _dbContext.TrashInspections.FirstOrDefault(x => x.Id == Id);

            if (trashInspection == null)
            {
                throw new NullReferenceException($"Could not find Trash Inspection with ID: {Id}");
            }
            trashInspection.Date = Date;
            trashInspection.Eak_Code = EakCode;
            trashInspection.InstallationId = InstallationId;
            trashInspection.MustBeInspected = MustBeInspected;
            trashInspection.Producer = Producer;
            trashInspection.RegistrationNumber = RegistrationNumber;
            trashInspection.Time = Time;
            trashInspection.Transporter = Transporter;
            trashInspection.TrashFraction = TrashFraction;
            trashInspection.WeighingNumber = WeighingNumber;
            trashInspection.Status = Status;

            if (_dbContext.ChangeTracker.HasChanges())
            {
                trashInspection.UpdatedAt = DateTime.Now;
                trashInspection.Version += 1;

                _dbContext.TrashInspectionVersions.Add(MapTrashInspectionVersion(_dbContext, trashInspection));
                _dbContext.SaveChanges();
            }
        }

        public void Delete(TrashInspectionPnDbContext _dbContext)
        {
            Microting.eFormTrashInspectionBase.Infrastructure.Data.Entities.TrashInspection trashInspection = _dbContext.TrashInspections.FirstOrDefault(x => x.Id == Id);

            if(trashInspection == null)
            {
                throw new NullReferenceException($"Could not find Trash Inspection with id: {Id}");
            }

            trashInspection.WorkflowState = eFormShared.Constants.WorkflowStates.Removed;

            if (_dbContext.ChangeTracker.HasChanges())
            {
                trashInspection.UpdatedAt = DateTime.Now;
                trashInspection.Version += 1;
                //_dbContext.TrashInspections.Remove(trashInspection);
                _dbContext.TrashInspectionVersions.Add(MapTrashInspectionVersion(_dbContext, trashInspection));
                _dbContext.SaveChanges();

            }

        }

        private TrashInspectionVersion MapTrashInspectionVersion(TrashInspectionPnDbContext _dbContext, Microting.eFormTrashInspectionBase.Infrastructure.Data.Entities.TrashInspection trashInspection)
        {
            TrashInspectionVersion trashInspectionVer = new TrashInspectionVersion();

            trashInspectionVer.CreatedAt = trashInspection.CreatedAt;
            trashInspectionVer.CreatedByUserId = trashInspection.CreatedByUserId;
            trashInspectionVer.Date = trashInspection.Date;
            trashInspectionVer.EakCode = trashInspection.Eak_Code;
            trashInspectionVer.InstallationId = trashInspection.InstallationId;
            trashInspectionVer.MustBeInspected = trashInspection.MustBeInspected;
            trashInspectionVer.Producer = trashInspection.Producer;
            trashInspectionVer.RegistrationNumber = trashInspection.RegistrationNumber;
            trashInspectionVer.Time = trashInspection.Time;
            trashInspectionVer.Transporter = trashInspection.Transporter;
            trashInspectionVer.TrashFraction = trashInspection.TrashFraction;
            trashInspectionVer.UpdatedAt = trashInspection.UpdatedAt;
            trashInspectionVer.UpdatedByUserId = trashInspection.UpdatedByUserId;
            trashInspectionVer.Version = trashInspection.Version;
            trashInspectionVer.WeighingNumber = trashInspection.WeighingNumber;
            trashInspectionVer.WorkflowState = trashInspection.WorkflowState;
//            trashInspectionVer.Status = trashInspection.Status;

            trashInspectionVer.TrashInspctionId = trashInspection.Id;
            
            return trashInspectionVer;
        }
    }
}
