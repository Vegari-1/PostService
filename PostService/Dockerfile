FROM mcr.microsoft.com/dotnet/aspnet:6.0-focal AS base
WORKDIR /app
EXPOSE 5000

FROM mcr.microsoft.com/dotnet/sdk:6.0-focal AS build
WORKDIR /src
COPY PostService/PostService.csproj PostService/
RUN dotnet restore PostService/PostService.csproj
COPY . .
WORKDIR /src/PostService
RUN dotnet build PostService.csproj -c Release -o /app

FROM build AS publish
RUN dotnet publish PostService.csproj -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "PostService.dll"]