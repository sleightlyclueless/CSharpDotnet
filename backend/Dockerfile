FROM mcr.microsoft.com/dotnet/sdk:7.0 as build-env
WORKDIR /src
EXPOSE 80
COPY *.csproj .
RUN dotnet restore
COPY . .
RUN dotnet publish -c Release -o /publish

FROM mcr.microsoft.com/dotnet/aspnet:7.0 as runtime
WORKDIR /publish
COPY --from=build-env /publish .
ENTRYPOINT ["dotnet", "mywebapi.dll"]