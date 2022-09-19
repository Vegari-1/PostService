FROM mcr.microsoft.com/dotnet/aspnet:6.0-focal AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:6.0-focal AS build
ARG github_username
ARG github_pat
WORKDIR /src
COPY PostService/PostService.csproj PostService/
COPY config/NuGet.Config .
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