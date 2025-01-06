FROM mcr.microsoft.com/dotnet/aspnet:8.0

WORKDIR /app
EXPOSE 8080

ENV ASPNETCORE_URLS=http://+:8080

# Use a non-root user with an explicit UID
USER "64198"

COPY . .

ENTRYPOINT ["dotnet", "Dirs21.Bookings.dll"]