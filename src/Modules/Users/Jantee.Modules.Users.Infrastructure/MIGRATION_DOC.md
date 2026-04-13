## Go to the User.Infrastructure is placed then run these commands in terminal

### Add migration:

```bash
dotnet ef migrations add [MigrationName] \
    --project ./Jantee.Modules.Users.Infrastructure.csproj \
    --startup-project ../../../Jantee.Api --output-dir ./Persistence/Migrations
```

### Update database:

```bash
dotnet ef database update \
    --project ./Jantee.Modules.Users.Infrastructure.csproj \
    --startup-project ../../../Jantee.Api
```