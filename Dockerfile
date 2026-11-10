FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["DatSanBanh.csproj", "./"]
RUN dotnet restore "DatSanBanh.csproj"
COPY . .
WORKDIR "/src/."
RUN dotnet build "DatSanBanh.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "DatSanBanh.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "DatSanBanh.dll"]