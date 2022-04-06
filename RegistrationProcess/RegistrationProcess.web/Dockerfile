FROM mcr.microsoft.com/dotnet/core/sdk:3.1 AS build
WORKDIR /src
COPY ["RegistrationProcess.web/*.csproj", "RegistrationProcess.web/"]
COPY ["DataAccess/*.csproj", "DataAccess/"]
COPY ["Framework/*.csproj", "Framework/"]
COPY ["Membership/*.csproj", "Membership/"]
RUN dotnet restore "RegistrationProcess.web/RegistrationProcess.web.csproj"
COPY . .
WORKDIR "/src/RegistrationProcess.web"
RUN dotnet build "RegistrationProcess.web.csproj" -c Release -o /app

FROM build AS publish
RUN dotnet publish "RegistrationProcess.web.csproj" -c Release -o /app

FROM build AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "RegistrationProcess.web.dll"]