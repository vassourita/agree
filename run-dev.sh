#!/bin/bash

cd ./apps/allow && ./run.sh &
cd ./apps/accord && mix ecto.migrate && source ./config/.env && mix phx.server &
cd ./apps/concord && yarn start