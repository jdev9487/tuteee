rabbit_username=$(cat /app/rabbitmq/username)
rabbit_password=$(cat /app/rabbitmq/password)

export RABBIT_MQ_USERNAME="$rabbit_username"
export RABBIT_MQ_PASSWORD="$rabbit_password"

dotnet JDev.Tuteee.Grpc.Api.dll