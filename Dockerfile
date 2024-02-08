FROM mcr.microsoft.com/dotnet/aspnet:6.0 as base
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:6.0 as build
ARG BUILD_CONFIGURATION=Release
WORKDIR /FIND-YOUR-HOTEL-.NET
COPY ["explore_.net/HotelBooking.csproj", "explore_.net"]
RUN dotnet retore "explore_.net/HotelBooking.csproj"
COPY . .
WORKDIR "/explore_.net"
RUN dotnet build "explore_.net/HotelBooking.csproj" -c $BUILD_CONFIGURATION -o /app/build
FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "HotelBooking.csproj" -c $BUILD_CONFIGURATION -o /app/publish/build

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT [ "dotnet", "HotelBooking.dll" ]