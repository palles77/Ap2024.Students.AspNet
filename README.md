To use New Scaffolding Item you need to:

1. Call Update-Database from Nuget manager console
2. Add new public model class to Models directory in Common project.
3. Add DbSet to database context with your new model class.
4. Add-Migration <migration_name>
5. Update-Database
6. Add new scaffolded item for MVC with Entity Framework. Make sure you point towards existing DB context and existing class.
7. Move your newly created controller to Controllers directory.
8. Compile.
9. Modify _Layout.cshtml

# Last Modified
2024/04/19
