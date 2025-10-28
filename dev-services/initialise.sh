echo 'Cjw7Gk10CetFyv95Piqon1z3mEq0' | rabbitmqctl add_user 'openbao'

rabbitmqctl set_permissions -p ".*" openbao ".*" ".*" ".*"