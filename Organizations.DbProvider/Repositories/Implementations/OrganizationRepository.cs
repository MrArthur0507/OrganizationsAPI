using Microsoft.Data.Sqlite;
using Organizations.DbProvider.Repositories.Contracts;
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
    public class OrganizationRepository : IOrganizationRepository
    {
        private readonly ICountryRepository countryRepository;
        private readonly IIndustryRepostiory industryRepository;

        public OrganizationRepository(ICountryRepository countryRepository, IIndustryRepostiory industryRepository)
        {
            this.countryRepository = countryRepository ?? throw new ArgumentNullException(nameof(countryRepository));
            this.industryRepository = industryRepository ?? throw new ArgumentNullException(nameof(industryRepository));
        }

        public void AddOrganizations(HashSet<Organization> organizations)
        {
            Stopwatch sw = Stopwatch.StartNew();
            using (SqliteConnection connection = new SqliteConnection("Data Source = C:\\Users\\mrart\\source\\repos\\OrganizationsManager\\Data\\mydb.db;"))
            {
                connection.Open();
                using (SqliteTransaction transaction = connection.BeginTransaction())
                {
                    try
                    {
                        using (SqliteCommand command = connection.CreateCommand())
                        {
                            int counter = 0;
                            string query = @"
                            INSERT INTO Organization 
                            ([Index], Name, Website, CountryId, Description, Founded, IndustryId, NumberOfEmployees) 
                            VALUES 
                            (@Index, @Name, @Website, @CountryId, @Description, @Founded, @IndustryId, @NumberOfEmployees);";
                            
                            command.CommandText = query;
                            HashSet<DBOrganization> dBOrganization = AssignIdsToOrganizations(organizations);
                            


                            foreach (DBOrganization organization in dBOrganization)
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
            sw.Stop();
            Console.WriteLine(sw.ElapsedMilliseconds);
        }


        public HashSet<DBOrganization> AssignIdsToOrganizations(HashSet<Organization> organizations)
        {
            HashSet<DBOrganization> organizationsWithIds = new HashSet<DBOrganization>();
            IndustryRepository industryRepository = new IndustryRepository();
            CountryRepository countryRepository = new CountryRepository();
            HashSet<Industry> industries = industryRepository.GetAll().ToHashSet();
            HashSet<Country> countries = countryRepository.GetAll().ToHashSet();


            foreach (Organization organization in organizations)
            {
                Country country = countries.FirstOrDefault(c => c.Name == organization.Country);
                Industry industry = industries.FirstOrDefault(c => c.Name == organization.Industry);

                int countryId = country?.CountryId ?? 0;
                int industryId = industry?.IndustryId ?? 0;

                organizationsWithIds.Add(new DBOrganization
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
