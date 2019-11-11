FROM microsoft/dotnet:2.2-aspnetcore-runtime AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM microsoft/dotnet:2.2-sdk AS build
WORKDIR /src
COPY ["IdentityAPI/IdentityAPI.csproj", "IdentityAPI/"]
RUN dotnet restore "IdentityAPI/IdentityAPI.csproj"
COPY . .
WORKDIR "/src/IdentityAPI"
RUN dotnet build "IdentityAPI.csproj" -c Release -o /app

FROM build AS publish
RUN dotnet publish "IdentityAPI.csproj" -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "IdentityAPI.dll"]