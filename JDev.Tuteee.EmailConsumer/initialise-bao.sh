export BAO_ADDR='http://openbao:8200'

token=$(bao write auth/approle/login \
  role_id="$(cat /app/roles/role)" \
  secret_id="$(cat /app/roles/secret)" | grep -E '\btoken\b' | sed 's/^.* //')
  
bao login "$token"

rabbit_username=$(bao kv get -mount=secret -field=username rabbit-mq)
rabbit_password=$(bao kv get -mount=secret -field=password rabbit-mq)

protonmail_username=$(bao kv get -mount=secret -field=username protonmail)
protonmail_password=$(bao kv get -mount=secret -field=password protonmail)

export RABBIT_MQ_USERNAME="$rabbit_username"
export RABBIT_MQ_PASSWORD="$rabbit_password"

export PROTONMAIL_USERNAME="$protonmail_username"
export PROTONMAIL_PASSWORD="$protonmail_password"

dotnet JDev.Tuteee.EmailConsumer.dll