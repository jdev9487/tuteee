export BAO_ADDR='http://openbao:8200'

token=$(bao write auth/approle/login \
  role_id="$(cat /app/roles/role)" \
  secret_id="$(cat /app/roles/secret)" | grep -E '\btoken\b' | sed 's/^.* //')
  
bao login "$token"

username=$(bao kv get -mount=secret -field=username rabbit-mq)
password=$(bao kv get -mount=secret -field=password rabbit-mq)

export RABBIT_MQ_USERNAME="$username"
export RABBIT_MQ_PASSWORD="$password"

dotnet JDev.Tuteee.Grpc.Api.dll