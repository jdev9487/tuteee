export BAO_ADDR=https://infra.bao.johngould.net:3001

identityToken=$(bao write auth/approle/login \
    role_id="$(cat /run/secrets/identity_role_id)" \
    secret_id="$(cat /run/secrets/identity_secret_id)" | grep -E '\btoken\b' | sed 's/^.* //')

bao login -no-print "$identityToken"

bao read database/creds/jdev-tuteee-identity > creds
postgresUsername=$(cat creds | grep -E '\busername\b' | sed 's/^.* //')
postgresPassword=$(cat creds | grep -E '\bpassword\b' | sed 's/^.* //')

identityLogin=$(bao kv get -mount=secret identity-login)
identityUsername=$(echo "$identityLogin" | jq '.data.username' )
identityPassword=$(echo "$identityLogin" | jq '.data.password' )

echo "{ \"AdminAuth\": { \"Username\": \"$identityUsername)\", \"Password\": \"$identityPassword\" }, \"ConnectionStrings\": { \"Identity\": \"Host=a369850-akamai-prod-6636035-default.g2a.akamaidb.net;Port=23590;Username=$postgresUsername;Password=$postgresPassword;Database=JDev.Tuteee.Identity;Sslmode=require\" }}" | jq "." >> appsettings.Secret.json

dotnet JDev.Tuteee.Identity.dll