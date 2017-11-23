namespace MassDeffect.ImportJson
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using AutoMapper;
    using MassDeffect.Data;
    using MassDeffect.Dtos;
    using MassDeffect.Models;
    using Newtonsoft.Json;

    class Program
    {

        private static string SolarSystemPath = "../../../datasets/solar-systems.json";
        private static string StarsPath = "../../../datasets/stars.json";
        private static string PlanetsPath = "../../../datasets/planets.json";
        private static string PersonsPath = "../../../datasets/persons.json";
        private static string AnomaliesPath = "../../../datasets/anomalies.json";
        private static string AnomalyVictimsPath = "../../../datasets/anomaly-victims.json";
        private static string Error = "Error: Invalid data.";

        static void Main()
        {
            UnitOfWork unit = new UnitOfWork();
            ConfigureMapping(unit);
            //ImportSolarSystems(unit);
            //ImportStars(unit);
            //ImportPlanets(unit);
            //ImportPersons(unit);
            //ImportAnomalies(unit);
            //ImportAnomalyVictims(unit);
        }

        private static void ConfigureMapping(UnitOfWork unit)
        {
            Mapper.Initialize(config =>
            {
                config.CreateMap<SolarSystemDto, SolarSystem>();

                config.CreateMap<StarDto, Star>()
                .ForMember(star => star.SolarSystem,
                    expression => expression
                    .MapFrom(dto => unit.SolarSystems
                        .First(sol => sol.Name == dto.SolarSystem)));

                config.CreateMap<PlanetDto, Planet>()
                    .ForMember(planet => planet.SolarSystem,
                        expression => expression
                            .MapFrom(dto => unit.SolarSystems
                                .First(system => system.Name == dto.SolarSystem)))
                    .ForMember(planet => planet.Sun,
                        expression => expression
                            .MapFrom(dto => unit.Stars
                                .First(star => star.Name == dto.Sun)));

                config.CreateMap<PersonDto, Person>()
                .ForMember(person => person.HomePlanet,
                    expression => expression
                        .MapFrom(dto => unit.Planets
                            .First(planet => planet.Name == dto.HomePlanet)));

                config.CreateMap<AnomaliesDto, Anomaly>()
                    .ForMember(anomaly => anomaly.OriginPlanet,
                        expression => expression
                            .MapFrom(dto => unit.Planets
                                .First(planet => planet.Name == dto.OriginPlanet)))
                    .ForMember(anomaly => anomaly.TeleportPlanet,
                        expression => expression
                            .MapFrom(dto => unit.Planets
                                .First(planet => planet.Name == dto.TeleportPlanet)));

            });
        }

        private static void ImportAnomalyVictims(UnitOfWork unit)
        {
            string json = File.ReadAllText(AnomalyVictimsPath);
            IEnumerable<AnomalyVictimsDto> anomalyVictimsDtos = JsonConvert.DeserializeObject<IEnumerable<AnomalyVictimsDto>>(json);
            foreach (var anomalyVictimsDto in anomalyVictimsDtos)
            {
                if (anomalyVictimsDto.Person == null || anomalyVictimsDto.Id <= 0)
                {
                    Console.WriteLine(Error);
                    continue;
                }

                Anomaly anomaly = unit.Anomalies.First(anom => anom.Id == anomalyVictimsDto.Id);
                Person victim = unit.Persons.First(per => per.Name == anomalyVictimsDto.Person);
                if (anomaly == null || victim == null)
                {
                    Console.WriteLine(Error);
                    continue;
                }

                anomaly.Victims.Add(victim);
                unit.Commit();
            }
        }

        private static void ImportAnomalies(UnitOfWork unit)
        {
            string json = File.ReadAllText(AnomaliesPath);
            IEnumerable<AnomaliesDto> anomaliesDtos = JsonConvert.DeserializeObject<IEnumerable<AnomaliesDto>>(json);
            foreach (var anomaliesDto in anomaliesDtos)
            {
                if (anomaliesDto.OriginPlanet == null || anomaliesDto.TeleportPlanet == null)
                {
                    Console.WriteLine(Error);
                    continue;
                }

                Anomaly anomaly = Mapper.Map<Anomaly>(anomaliesDto);
                if (anomaly.OriginPlanet == null || anomaly.TeleportPlanet == null)
                {
                    Console.WriteLine(Error);
                    continue;
                }

                unit.Anomalies.Add(anomaly);
                unit.Commit();
                Console.WriteLine("Successfully imported anomaly.");
            }
        }

        private static void ImportPersons(UnitOfWork unit)
        {
            string json = File.ReadAllText(PersonsPath);
            IEnumerable<PersonDto> personDtos = JsonConvert.DeserializeObject<IEnumerable<PersonDto>>(json);
            foreach (var personDto in personDtos)
            {
                if (personDto.Name == null || personDto.HomePlanet == null)
                {
                    Console.WriteLine(Error);
                    continue;
                }

                Person person = Mapper.Map<Person>(personDto);
                if (person.HomePlanet == null)
                {
                    Console.WriteLine(Error);
                    continue;
                }

                unit.Persons.Add(person);
                unit.Commit();
                Console.WriteLine($"Successfully imported Person {person.Name}.");
            }
        }

        private static void ImportPlanets(UnitOfWork unit)
        {
            string json = File.ReadAllText(PlanetsPath);
            IEnumerable<PlanetDto> planetDtos = JsonConvert.DeserializeObject<IEnumerable<PlanetDto>>(json);

            foreach (PlanetDto planetDto in planetDtos)
            {
                if (planetDto.Name == null || planetDto.Sun == null || planetDto.SolarSystem == null)
                {
                    Console.WriteLine(Error);
                    continue;
                }

                Planet planet = Mapper.Map<Planet>(planetDto);

                if (planet.Sun == null || planet.SolarSystem == null)
                {
                    Console.WriteLine(Error);
                    continue;
                }

                unit.Planets.Add(planet);
                unit.Commit();
                Console.WriteLine($"Successfully imported Planet {planet.Name}.");
            }
        }

        private static void ImportStars(UnitOfWork unit)
        {
            string json = File.ReadAllText(StarsPath);
            IEnumerable<StarDto> starDtos = JsonConvert.DeserializeObject<IEnumerable<StarDto>>(json);
            foreach (StarDto starDto in starDtos)
            {
                if (starDto.Name == null || starDto.SolarSystem == null)
                {
                    Console.WriteLine(Error);
                    continue;
                }

                Star star = Mapper.Map<Star>(starDto);
                if (star.SolarSystem == null)
                {
                    Console.WriteLine(Error);
                    continue;
                }

                unit.Stars.Add(star);
                unit.Commit();
                Console.WriteLine($"Successfully imported Star {star.Name}.");
            }
        }

        private static void ImportSolarSystems(UnitOfWork unit)
        {
            string json = File.ReadAllText(SolarSystemPath);
            IEnumerable<SolarSystemDto> solarSystemDtos = JsonConvert.DeserializeObject<IEnumerable<SolarSystemDto>>(json);
            foreach (var solarSystemDto in solarSystemDtos)
            {
                if (solarSystemDto.Name == null)
                {
                    Console.WriteLine(Error);
                    continue;
                }

                SolarSystem solarSystem = Mapper.Map<SolarSystem>(solarSystemDto);
                unit.SolarSystems.Add(solarSystem);
                unit.Commit();
                Console.WriteLine($"Successfully imported Solar System {solarSystem.Name}.");
            }
        }
    }
}
