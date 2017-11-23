namespace ACTester.Controller
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;
    using System.Text;
    using AcTester.Data;
    using AcTester.Helpers.Enumerations;
    using AcTester.Helpers.Utilities;
    using AcTester.Models;
    using AcTester.Models.Attributes;
    using ACTester.Database;
    using ACTester.Interfaces;
    using ACTester.ViewModels;

    public class AirConditionerTesterSystem : IAirConditionerTesterSystem
    {
        private UnitOfWork database;

        public AirConditionerTesterSystem(UnitOfWork database)
        {
            this.database = database;
        }

        public AirConditionerTesterSystem() : this(new UnitOfWork())
        {
        }

        //public IAirConditionerTesterDatabase Database { get; private set; }

        public string RegisterStationaryAirConditioner(string manufacturer, string model, string energyEfficiencyRating, int powerUsage)
        {
            EnergyEfficiencyRating rating;
            try
            {
                rating =
                    (EnergyEfficiencyRating)Enum.Parse(typeof(EnergyEfficiencyRating), energyEfficiencyRating);
            }
            catch (ArgumentException ex)
            {
                throw new ArgumentException(Constants.IncorrectEnergyEfficiencyRating, ex);
            }

            AirConditioner airConditioner = new StationaryAirConditioner()
            {
                PowerUsage = powerUsage,
                Manufacturer = manufacturer,
                Model = model,
                RequiredEnergyEfficiencyRating = rating
            };

            this.database.AirConditionersRepo.Add(airConditioner);
            this.database.Save();
            return string.Format(Constants.RegisterAirConditioner, airConditioner.Model, airConditioner.Manufacturer);
        }

        public string RegisterCarAirConditioner(string manufacturer, string model, int volumeCoverage)
        {
            AirConditioner airConditioner = new CarAirConditioner()
            {
                Manufacturer = manufacturer,
                Model = model,
                VolumeCovered = volumeCoverage
            };

            this.database.AirConditionersRepo.Add(airConditioner);
            this.database.Save();
            return string.Format(Constants.RegisterAirConditioner, airConditioner.Model, airConditioner.Manufacturer);
        }

        public string RegisterPlaneAirConditioner(string manufacturer, string model, int volumeCoverage, int electricityUsed)
        {
            AirConditioner airConditioner = new PlaneAirConditioner()
            {
                Model = model,
                Manufacturer = manufacturer,
                VolumeCovered = volumeCoverage,
                ElectricityUsed = electricityUsed
            };

            this.database.AirConditionersRepo.Add(airConditioner);
            this.database.Save();
            return string.Format(Constants.RegisterAirConditioner, airConditioner.Model, airConditioner.Manufacturer);
        }

        public string TestAirConditioner(string manufacturer, string model)
        {
            AirConditioner airConditioner = this.GetAirConditionerByManufacturerAndModel(manufacturer, model);
            AirConditionerDto air = GenerateAirCondDtoFromModel(airConditioner);

            air.Model = airConditioner.Model;
            air.Manufacturer = airConditioner.Manufacturer;

            var mark = air.Test() ? Mark.Passed : Mark.Failed;

            this.database.ReportsRepo.Add(new Report()
            {
                Manufacturer = manufacturer,
                Model = model,
                Mark = mark
            });

            this.database.Save();
            return string.Format(Constants.TestAirConditioner, model, manufacturer);
        }

        public string FindAirConditioner(string manufacturer, string model)
        {
            AirConditioner airConditioner = this.GetAirConditionerByManufacturerAndModel(manufacturer, model);
            AirConditionerDto aidDto = this.GenerateAirCondDtoFromModel(airConditioner);
            return aidDto.ToString();
        }

        public string FindReport(string manufacturer, string model)
        {
            Report report = this.GetReportByManufacturerAndModel(manufacturer, model);
            ReportDto reportDto = new ReportDto(report.Manufacturer, report.Model, report.Mark);

            return reportDto.ToString();
        }

        public string FindAllReportsByManufacturer(string manufacturer)
        {
            IList<Report> reports = this.GetReportsByManufacturer(manufacturer);
            if (reports.Count == 0)
            {
                return Constants.NoReports;
            }


            IList<ReportDto> reportDtos = new List<ReportDto>();
            foreach (Report report in reports)
            {
                reportDtos.Add(new ReportDto(report.Manufacturer, report.Model, report.Mark));
            }

            reportDtos = reportDtos.OrderBy(x => x.Model).ToList();
            StringBuilder reportsPrint = new StringBuilder();
            reportsPrint.AppendLine(string.Format("Reports from {0}:", manufacturer));
            reportsPrint.Append(string.Join(Environment.NewLine, reportDtos));
            return reportsPrint.ToString();
        }

        public string Status()
        {
            int reports = this.database.ReportsRepo.Count();

            double airConditioners = this.database.AirConditionersRepo.Count();
            if (reports == 0)
            {
                return string.Format(Constants.Status, 0);
            }

            double percent = reports / airConditioners;
            percent = percent * 100;
            return string.Format(Constants.Status, percent);
        }

        private AirConditioner GetAirConditionerByManufacturerAndModel(string manufacturer, string model)
        {
            return this.database.AirConditionersRepo.First(air => air.Manufacturer == manufacturer && air.Model == model);
        }

        private Report GetReportByManufacturerAndModel(string manufacturer, string model)
        {
            return this.database.ReportsRepo.First(air => air.Manufacturer == manufacturer && air.Model == model);
        }

        private AirConditionerDto GenerateAirCondDtoFromModel(AirConditioner airConditioner)
        {
            var propertyInfos = airConditioner.GetType().GetProperties(BindingFlags.Instance | BindingFlags.Public).Where(prop => prop.IsDefined(typeof(OrderAttribute)));
            propertyInfos = propertyInfos.OrderBy(info => info.GetCustomAttribute<OrderAttribute>().Order);
            object[] props = propertyInfos.Select(info => info.GetValue(airConditioner)).ToArray();
            Type airConType = Type.GetType("ACTester.ViewModels." + airConditioner.GetType().Name + "Dto");
            ConstructorInfo ctorInfo = airConType.GetConstructors(BindingFlags.Public | BindingFlags.Instance).FirstOrDefault();
            AirConditionerDto air = (AirConditionerDto)ctorInfo.Invoke(props);
            return air;
        }

        private IList<Report> GetReportsByManufacturer(string manufacturer)
        {
            return this.database.ReportsRepo.GetAll(report => report.Manufacturer == manufacturer).ToList();
        }
    }
}
