FROM mcr.microsoft.com/dotnet/core/sdk:3.1 as dotnet-build
COPY /src .
RUN dotnet publish Hackathon.sln -c Release -r linux-x64 --self-contained -o /app

FROM mcr.microsoft.com/dotnet/core/aspnet:3.1
COPY --from=dotnet-build /app .

ENTRYPOINT ["dotnet", "Hackathon.WebApi.dll"]