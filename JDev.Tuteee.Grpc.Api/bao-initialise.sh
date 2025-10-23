export BAO_ADDR=https://infra.bao.johngould.net:3001

postgresToken=$(bao write auth/approle/login \
    role_id="$(cat /run/secrets/postgres_grpc_role_id)" \
    secret_id="$(cat /run/secrets/postgres_grpc_secret_id)" | grep -E '\btoken\b' | sed 's/^.* //')

bao login -no-print "$postgresToken"

bao read database/creds/jdev-tuteee-rest > creds
postgresUsername=$(cat creds | grep -E '\busername\b' | sed 's/^.* //')
postgresPassword=$(cat creds | grep -E '\bpassword\b' | sed 's/^.* //')

rm creds

echo "{ \"ConnectionStrings\": { \"Tuteee\": \"Host=a369850-akamai-prod-6636035-default.g2a.akamaidb.net;Port=23590;Username=$postgresUsername;Password=$postgresPassword;Database=JDev.Tuteee.Rest;Sslmode=require\" } }" | jq "." >> appsettings.Secret.json

dotnet JDev.Tuteee.Grpc.Api.dll