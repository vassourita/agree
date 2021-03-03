echo "[entrypoint] Initializing Agree.Athens..." &&

cd ./src/Agree.Athens.Infrastructure.Data &&
echo "[entrypoint] Running Migrations..." &&
dotnet ef database update &&

cd ../Agree.Athens.Presentation.WebApi &&
echo "[entrypoint] Running WebApi..." &&
dotnet watch run