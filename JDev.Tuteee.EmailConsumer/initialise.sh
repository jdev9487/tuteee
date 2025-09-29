export BAO_ADDR='http://openbao:8200'

token=$(bao write auth/approle/login \
  role_id="$(cat /app/bao/roles/role)" \
  secret_id="$(cat /app/bao/roles/secret)" | grep -E '\btoken\b' | sed 's/^.* //')
  
bao login "$token"

protonmail_username=$(bao kv get -mount=secret -field=username protonmail)
protonmail_password=$(bao kv get -mount=secret -field=password protonmail)

rabbit_username=$(cat /app/rabbitmq/username)
rabbit_password=$(cat /app/rabbitmq/password)

export RABBIT_MQ_USERNAME="$rabbit_username"
export RABBIT_MQ_PASSWORD="$rabbit_password"

export PROTONMAIL_USERNAME="$protonmail_username"
export PROTONMAIL_PASSWORD="$protonmail_password"

dotnet JDev.Tuteee.EmailConsumer.dll