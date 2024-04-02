# Imagen base con SDK de .NET para compilar la aplicación
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build-env
WORKDIR /app

# Copiar los archivos csproj y restaurar las dependencias de manera individual
COPY MarcasDeAutosAPI/MarcasDeAutosAPI.csproj MarcasDeAutosAPI/
COPY MarcasDeAutosDATA/MarcasDeAutosDATA.csproj MarcasDeAutosDATA/

# Restaurar las dependencias de los proyectos específicos
RUN dotnet restore MarcasDeAutosAPI/MarcasDeAutosAPI.csproj
RUN dotnet restore MarcasDeAutosDATA/MarcasDeAutosDATA.csproj

# Copiar el resto del código fuente
COPY MarcasDeAutosAPI/ MarcasDeAutosAPI/
COPY MarcasDeAutosDATA/ MarcasDeAutosDATA/

# Publicar la aplicación API, especificando el proyecto API
RUN dotnet publish MarcasDeAutosAPI/ -c Release -o out

# Usar la imagen base de tiempo de ejecución de .NET para la imagen final
FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app
COPY --from=build-env /app/MarcasDeAutosAPI/out .
ENTRYPOINT ["dotnet", "MarcasDeAutosAPI.dll"]
