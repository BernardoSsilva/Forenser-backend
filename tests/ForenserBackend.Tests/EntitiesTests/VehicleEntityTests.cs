using ForenserBackend.Domain.entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ForenserBackend.Tests.EntitiesTests
{
    public class VehicleEntityTests
    {

        [Fact]
        public void CreateANewVeicleEntityTest()
        {
            var newVehicle = new VehicleEntity { 
                Model= "test model", 
                OcurrenceId="test id", 
                VehicleMark="test mark"
            };

            Assert.NotNull(newVehicle.Id);
            Assert.NotNull(newVehicle.Model);
            Assert.NotNull(newVehicle.OcurrenceId);
            Assert.NotNull(newVehicle.VehicleMark);
            Assert.Empty(newVehicle.VehicleYear);

     
            Assert.Equal(newVehicle.Model, "test model");
            Assert.Equal(newVehicle.OcurrenceId, "test id");
            Assert.Equal(newVehicle.VehicleMark, "test mark");
        }

        [Fact]
        public void AddictVehicleYear()
        {
            var newVehicle = new VehicleEntity
            {
                Model = "test model",
                OcurrenceId = "test id",
                VehicleMark = "test mark"
            };

            newVehicle.VehicleYear = "2000";

            Assert.NotNull(newVehicle.Id);
            Assert.NotNull(newVehicle.Model);
            Assert.NotNull(newVehicle.OcurrenceId);
            Assert.NotNull(newVehicle.VehicleMark);
            Assert.Equal(newVehicle.VehicleYear, "2000");
        }
    }
}
