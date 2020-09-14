FROM microsoft/dotnet:2.2-sdk AS build-env
WORKDIR /app

COPY *.csproj .
RUN dotnet restore

COPY . .
RUN dotnet publish -c Release -o /out

FROM microsoft/dotnet:2.2-aspnetcore-runtime
COPY --from=build-env /out .

EXPOSE 80
EXPOSE 443
ENTRYPOINT ["dotnet","Library.API.dll"]