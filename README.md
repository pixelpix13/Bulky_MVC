Creating a New Web MVC Project in Visual Studio
1. Install and Connect to SQL Server
Install SQL Server and SQL Server Management Studio (SSMS).
Connect to your local server in SSMS.
2. Add Connection String in appsettings.json
json
"ConnectionStrings": {
  "DefaultConnection": "Server=DESKTOP-PSE8GDB;Database=Bulky;Trusted_Connection=True;TrustServerCertificate=True"
}
3. Create Data Folder and ApplicationDbContext Class
Create a folder named Data.
Create a class named ApplicationDbContext in the Data folder, inheriting from DbContext:
csharp
 
public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {
    }
}
4. Register ApplicationDbContext in Program.cs
csharp
 
builder.Services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer());
Notes on Dependency Injection (DI) and Usage in Code
What Is Dependency Injection (DI)?
Definition: Design pattern to manage object and service creation and provision.
Purpose:
Simplifies Code: Reduces complexity by externalizing dependency creation.
Improves Flexibility: Allows easy swapping of implementations.
Enhances Testability: Facilitates providing mock implementations for testing.
How DI Works
Service Registration:

Define Services: Register services with a DI container (e.g., Program.cs or Startup.cs).
Configuration: Specify service creation and configuration.
Service Injection:

Provide Dependencies: DI container supplies dependencies, typically via constructor injection.
Using DI with ApplicationDbContext
Constructor of ApplicationDbContext:
csharp
 
public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
{
}
Purpose: Initializes ApplicationDbContext with DbContextOptions from the DI container.
How It Works: Passes configuration settings to the base DbContext class.
Applying Migrations and Updating Database
Open NuGet Package Manager Console:
Run update-database to apply migrations.
View your database in SSMS.
Creating a Table Using EF Core
Add DbSet in ApplicationDbContext:
csharp
 
public DbSet<Category> Categories { get; set; }
Create and Apply Migration:
Run add-migration AddCategoryTableToDb.
Run update-database.
CRUD Operations with Category
1. Create a Controller
Category Controller Constructor:
csharp
 
private readonly ApplicationDbContext _db;
public CategoryController(ApplicationDbContext db)
{
    _db = db;
}
2. Create and Link Views
Index Action in Category Controller:
csharp
 
public IActionResult Index()
{
    List<Category> objCategoryList = _db.Categories.ToList();
    return View(objCategoryList);
}
Link to _Layout.cshtml and Create Views:
Create Category view with @model List<Category>.
Add Create button in Index view to navigate to Create view.
Create Create view with @model Category.
3. Implement Create Functionality
Create Action in Category Controller:
csharp
 
[HttpGet]
public IActionResult Create()
{
    return View();
}

[HttpPost]
[ValidateAntiForgeryToken]
public IActionResult Create(Category obj)
{
    if (ModelState.IsValid)
    {
        _db.Categories.Add(obj);
        _db.SaveChanges();
        TempData["success"] = "Category created successfully";
        return RedirectToAction("Index");
    }
    return View(obj);
}
4. Implement Edit Functionality
Edit Action in Category Controller:
csharp
 
[HttpGet]
public IActionResult Edit(int? id)
{
    if (id == null || id == 0)
    {
        return NotFound();
    }
    Category? categoryFromDb = _db.Categories.Find(id);
    if (categoryFromDb == null)
    {
        return NotFound();
    }
    return View(categoryFromDb);
}

[HttpPost]
[ValidateAntiForgeryToken]
public IActionResult Edit(Category obj)
{
    if (ModelState.IsValid)
    {
        _db.Categories.Update(obj);
        _db.SaveChanges();
        TempData["success"] = "Category updated successfully";
        return RedirectToAction("Index");
    }
    return View(obj);
}
5. Implement Delete Functionality
Delete Action in Category Controller:
csharp
 
[HttpPost, ActionName("Delete")]
[ValidateAntiForgeryToken]
public IActionResult DeletePOST(int? id)
{
    Category? obj = _db.Categories.Find(id);
    if (obj == null)
    {
        return NotFound();
    }
    _db.Categories.Remove(obj);
    _db.SaveChanges();
    TempData["success"] = "Category deleted successfully";
    return RedirectToAction("Index");
}
Adding Toastr Notifications
1. Modify _Layout.cshtml
html
 
<link rel="stylesheet" href="//cdnjs.cloudflare.com/ajax/libs/toastr.js/latest/css/toastr.min.css" />
<script src="~/lib/jquery/dist/jquery.min.js"></script>
<script src="//cdnjs.cloudflare.com/ajax/libs/toastr.js/latest/js/toastr.min.js"></script>
2. Create _Notification.cshtml Partial View
_Notification.cshtml:
html
 
@if (TempData["success"] != null)
{
    <script type="text/javascript">
        toastr.success('@TempData["success"]');
    </script>
}
3. Include Partial View in _Layout.cshtml
html
 
<body>
    @RenderBody()
    <partial name="_Notification" />
    @RenderSection("Scripts", required: false)
</body>
Summary
Project Overview: This project demonstrates CRUD operations using .NET MVC and Entity Framework.
Dependency Injection (DI):
Simplifies code, improves flexibility, and enhances testability.
Manages how ApplicationDbContext and other services are created and provided.
Toastr Notifications:
Provides simple JavaScript toast notifications.
Utilizes jQuery and Toastr for displaying messages.
