FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /app

COPY DeutschDeck.WebAPI.csproj .
RUN dotnet restore

COPY . .

CMD ["dotnet", "watch", "run", "--project" , "DeutschDeck.WebAPI.csproj", "--no-launch-profile", "--non-interactive"]
