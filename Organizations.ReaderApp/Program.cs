
using Organizations.DbProvider.Config.Implementations;
using Organizations.DbProvider.Repositories.Contracts;
using Organizations.DbProvider.Repositories.Implementations;
using Organizations.DbProvider.Services.Implementations;
using Organizations.DbProvider.Tools.Implementations;
using Organizations.Models.Models;
using Organizations.ReaderApp.Services.Implementations;
using Organizations.Services.Implementations;
using Organizations.Services.Interfaces;
using System.Diagnostics;


Stopwatch sw = Stopwatch.StartNew();
ConfigLoader loader = new ConfigLoader();

SqliteTableManager sqliteTableManager = new SqliteTableManager(loader);

SqliteDbManager sqliteDbManager = new SqliteDbManager(new SqliteLogger(), sqliteTableManager);
sqliteDbManager.LoadDb();
Reader<Organization> reader = new Reader<Organization>(new ReaderTracker(new FileLocator("C:\\Users\\mrart\\Desktop"), new FilePathRepository()));
DbSeeder dbSeeder = new DbSeeder(new CountryRepository(), new IndustryRepository(), new OrganizationRepository(new CountryRepository(), new IndustryRepository()));
dbSeeder.Seed(reader.Read());
sw.Stop();
Console.WriteLine(sw.ElapsedMilliseconds);

//IPasswordHasher passwordHasher = new PasswordHasher(); // Replace with your actual implementation
//IJwtGenerator jwtService = new JwtGenerator(); // Replace with your actual implementation
//IAccountRepository accountRepository = new AccountRepository(); // Replace with your actual implementation

//UserManagement userManagement = new UserManagement(passwordHasher,accountRepository);
//AccountService accountService = new AccountService(userManagement, jwtService);

//// Register a user
//accountService.RegisterUser("exampleUser", "password123");

//// Login a user
//string loginResult = accountService.LoginUser("exampleUser", "password123");
//Console.WriteLine(loginResult);

