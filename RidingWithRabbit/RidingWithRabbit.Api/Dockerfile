FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-buster-slim AS base
WORKDIR /app
EXPOSE 5001

FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS build
WORKDIR /src

COPY *.csproj ./
RUN dotnet restore

COPY "./" "./"
RUN dotnet publish -c Release -o out

WORKDIR "./"
RUN dotnet build "RidingWithRabbit.Api.csproj" -c Release -o /app

FROM build AS publish
RUN dotnet publish "RidingWithRabbit.Api.csproj" -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "RidingWithRabbit.Api.dll"]