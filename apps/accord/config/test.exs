use Mix.Config

# Configure your database
#
# The MIX_TEST_PARTITION environment variable can be used
# to provide built-in test partitioning in CI environment.
# Run `mix help test` for more information.
config :accord, Accord.Repo,
  username: "docker",
  password: "docker",
  database: "accord_db_test#{System.get_env("MIX_TEST_PARTITION")}",
  hostname: "localhost",
  port: 4001,
  pool: Ecto.Adapters.SQL.Sandbox

# We don't run a server during test. If one is required,
# you can enable the server option below.
config :accord, AccordWeb.Endpoint,
  http: [port: 5000],
  server: false

# Print only warnings and errors during test
config :logger, level: :warn