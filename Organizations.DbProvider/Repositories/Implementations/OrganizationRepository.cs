using Microsoft.Data.Sqlite;
using Organizations.DbProvider.Repositories.Contracts;
using Organizations.DbProvider.Tools.Implementations;
using Organizations.Models.DTO;
using Organizations.Models.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Organizations.DbProvider.Repositories.Implementations
{
    public class OrganizationRepository : GenericRepository<Organization>, IOrganizationRepository
    {
        private readonly ICountryRepository _countryRepository;
        private readonly IIndustryRepostiory _industryRepository;
        public OrganizationRepository(ICountryRepository countryRepository, IIndustryRepostiory industryRepository)
        {
            _countryRepository = countryRepository;
            _industryRepository = industryRepository;
        }

        public void AddOrganizations(HashSet<OrganizationDTO> organizations)
        {
            using (SqliteConnection connection = new SqliteConnection($"Data Source = {DbFile}"))
            {
                connection.Open();
                using (SqliteTransaction transaction = connection.BeginTransaction())
                {
                    try
                    {
                        using (SqliteCommand command = connection.CreateCommand())
                        {
                            string query = @"
                            INSERT INTO Organization 
                            ([Index], Name, Website, CountryId, Description, Founded, IndustryId, NumberOfEmployees) 
                            VALUES 
                            (@Index, @Name, @Website, @CountryId, @Description, @Founded, @IndustryId, @NumberOfEmployees);";

                            command.CommandText = query;
                            HashSet<Organization> dBOrganization = AssignIdsToOrganizations(organizations);

                            foreach (Organization organization in dBOrganization)
                            {

                                command.Parameters.AddWithValue("@Index", organization.Index);
                                command.Parameters.AddWithValue("@Name", organization.Name);
                                command.Parameters.AddWithValue("@Website", organization.Website);
                                command.Parameters.AddWithValue("@CountryId", organization.CountryId);
                                command.Parameters.AddWithValue("@Description", organization.Description);
                                command.Parameters.AddWithValue("@Founded", organization.Founded);
                                command.Parameters.AddWithValue("@IndustryId", organization.IndustryId);
                                command.Parameters.AddWithValue("@NumberOfEmployees", organization.NumberOfEmployees);
                                command.ExecuteNonQuery();

                                command.Parameters.Clear();
                            }
                        }

                        transaction.Commit();
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("Error: " + ex.Message);
                        transaction.Rollback();
                    }

                }

            }
        }

        public bool UpdateOrganization(Organization organization)
        {
            using (SqliteConnection connection = new SqliteConnection($"Data Source = {DbFile}"))
            {
                connection.Open();

                using (SqliteCommand command = connection.CreateCommand())
                {
                    try
                    {
                        string query = @"
                        UPDATE Organization 
                        SET 
                        [Index] = @Index,
                        Name = @Name,
                        Website = @Website,
                        CountryId = @CountryId,
                        Description = @Description,
                        Founded = @Founded,
                        IndustryId = @IndustryId,
                        NumberOfEmployees = @NumberOfEmployees
                        WHERE OrganizationId = @OrganizationId;";

                        command.CommandText = query;

                        command.Parameters.AddWithValue("@OrganizationId", organization.OrganizationId);
                        command.Parameters.AddWithValue("@Index", organization.Index);
                        command.Parameters.AddWithValue("@Name", organization.Name);
                        command.Parameters.AddWithValue("@Website", organization.Website);
                        command.Parameters.AddWithValue("@CountryId", organization.CountryId);
                        command.Parameters.AddWithValue("@Description", organization.Description);
                        command.Parameters.AddWithValue("@Founded", organization.Founded);
                        command.Parameters.AddWithValue("@IndustryId", organization.IndustryId);
                        command.Parameters.AddWithValue("@NumberOfEmployees", organization.NumberOfEmployees);

                        int rowsChanged = command.ExecuteNonQuery();
                        if (rowsChanged > 0)
                        {
                            return true;
                        }
                    }
                    catch (Exception ex)
                    {
                        return false;
                    }
                }
            }
            return false;
        }


        public HashSet<Organization> AssignIdsToOrganizations(HashSet<OrganizationDTO> organizations)
        {
            HashSet<Organization> organizationsWithIds = new HashSet<Organization>();
            HashSet<Industry> industries = _industryRepository.GetAll().ToHashSet();
            HashSet<Country> countries = _countryRepository.GetAll().ToHashSet();

            foreach (OrganizationDTO organization in organizations)
            {
                Country country = countries.FirstOrDefault(c => c.Name == organization.Country);
                Industry industry = industries.FirstOrDefault(c => c.Name == organization.Industry);

                int countryId = country.CountryId;
                int industryId = industry.IndustryId;

                organizationsWithIds.Add(new Organization
                {
                    Index = organization.Index,
                    Name = organization.Name,
                    Website = organization.Website,
                    CountryId = countryId,
                    Description = organization.Description,
                    Founded = organization.Founded,
                    IndustryId = industryId,
                    NumberOfEmployees = organization.NumberOfEmployees
                });
            }

            return organizationsWithIds;
        }
    }
}
