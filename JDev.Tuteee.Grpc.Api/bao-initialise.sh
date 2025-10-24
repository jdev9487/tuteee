export BAO_ADDR=https://infra.bao.johngould.net:3001

postgresToken=$(bao write auth/approle/login -format=json \
    role_id="$(cat /run/secrets/grpc_role_id)" \
    secret_id="$(cat /run/secrets/grpc_secret_id)" | jq -r '.auth.client_token')

bao login -no-print $postgresToken

postgresLogin=$(bao read database/creds/jdev-tuteee-grpc -format=json)
postgresUsername=$(echo $postgresLogin | jq '.data.username' )
postgresPassword=$(echo $postgresLogin | jq '.data.password' )

echo "{ \"ConnectionStrings\": { \"Tuteee\": \"Host=a369850-akamai-prod-6636035-default.g2a.akamaidb.net;Port=23590;Username=$postgresUsername;Password=$postgresPassword;Database=JDev.Tuteee.Rest;Sslmode=require\" } }" | jq "." >> appsettings.Secret.json

dotnet JDev.Tuteee.Grpc.Api.dll