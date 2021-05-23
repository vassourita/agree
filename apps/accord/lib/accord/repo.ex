defmodule Accord.Repo do
  use Ecto.Repo,
    otp_app: :accord,
    adapter: Ecto.Adapters.Postgres
end
