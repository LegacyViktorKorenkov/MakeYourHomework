FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY ["MakeYourHomework.AuthService/MakeYourHomework.AuthService.csproj", "MakeYourHomework.AuthService/"]
RUN dotnet restore "MakeYourHomework.AuthService/MakeYourHomework.AuthService.csproj"
COPY . .
WORKDIR "/src/MakeYourHomework.AuthService"
RUN dotnet build "MakeYourHomework.AuthService.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "MakeYourHomework.AuthService.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "MakeYourHomework.AuthService.dll"]
