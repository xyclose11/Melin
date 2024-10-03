To setup the database I am using the code-first approach.

1. Get an instance of PostgreSQL running on the same machine that you are hosting the application from
1a. Optional: Install PgAdmin4 (Makes things easier)
2. Setup base users for the DB (Admin, etc.)
3. Create a new database - Remember the exact name of the DB (Melin)
4. Inside Melin.Server -> appsettings.json && appsettings.Development.json add the following:
  "ConnectionStrings": {
  "MelinDatabase": "Host=localhost;Username=postgres;Password={password};Database=melin"
}
5. Apply migrations


References: https://www.npgsql.org/doc/index.html