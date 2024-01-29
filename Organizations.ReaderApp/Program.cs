
using Organizations.DbProvider.Config.Implementations;
using Organizations.DbProvider.Repositories;
using Organizations.DbProvider.Repositories.Contracts;
using Organizations.DbProvider.Repositories.Implementations;
using Organizations.DbProvider.Services.Implementations;
using Organizations.DbProvider.Tools.Implementations;
using Organizations.Models.DTO;
using Organizations.ReaderApp.Services.Implementations;
using Organizations.Services.Implementations;
using Organizations.Services.Interfaces;
using System.Diagnostics;

//FOR REFACTORING
ConfigLoader loader = new ConfigLoader();

SqliteTableManager sqliteTableManager = new SqliteTableManager(loader);

SqliteDbManager sqliteDbManager = new SqliteDbManager(new SqliteLogger(), sqliteTableManager);
sqliteDbManager.LoadDb();
Reader<OrganizationDTO> reader = new Reader<OrganizationDTO>(new ReaderTracker(new FileLocator("C:\\Users\\Bozhidar\\Desktop"), new FilePathRepository()));
DbSeeder dbSeeder = new DbSeeder(new CountryRepository(), new IndustryRepository(), new OrganizationRepository(new OrganizationIdAssigner(new CountryRepository(), new IndustryRepository())));
dbSeeder.Seed(reader.Read());



