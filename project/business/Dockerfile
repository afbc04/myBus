FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build

WORKDIR /business
COPY *.csproj ./
RUN dotnet restore
COPY . ./
RUN dotnet publish -c Release -o out

FROM mcr.microsoft.com/dotnet/aspnet:7.0
WORKDIR /business
COPY --from=build /business/out .
EXPOSE 80
ENTRYPOINT ["dotnet", "business.dll"]

