export BAO_ADDR='http://openbao:8200'

token=$(bao write auth/approle/login \
  role_id="$(cat /app/bao/roles/role)" \
  secret_id="$(cat /app/bao/roles/secret)" | grep -E '\btoken\b' | sed 's/^.* //')
  
bao login "$token"

tuteee_username=$(bao kv get -mount=secret -field=username tuteee)
tuteee_password=$(bao kv get -mount=secret -field=password tuteee)

export TUTEEE_USERNAME="$tuteee_username"
export TUTEEE_PASSWORD="$tuteee_password"

dotnet JDev.Tuteee.Identity.dll