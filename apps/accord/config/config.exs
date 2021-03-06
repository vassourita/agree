# This file is responsible for configuring your application
# and its dependencies with the aid of the Mix.Config module.
#
# This configuration file is loaded before any dependency and
# is restricted to this project.

# General application configuration
use Mix.Config

config :accord,
  ecto_repos: [Accord.Repo],
  generators: [binary_id: true]

# Configures the endpoint
config :accord, AccordWeb.Endpoint,
  url: [host: "localhost"],
  secret_key_base: "oS4WEEW5hol1eneJMtiB+VRJCAwuGUTpM4LYSymHQk0gJs8llrN5IeAIh5XBZY/I",
  render_errors: [view: AccordWeb.ErrorView, accepts: ~w(json), layout: false],
  pubsub_server: Accord.PubSub,
  live_view: [signing_salt: "BG2I6va9"]

# Configures Elixir's Logger
config :logger, :console,
  format: "$time $metadata[$level] $message\n",
  metadata: [:request_id]

# Use Jason for JSON parsing in Phoenix
config :phoenix, :json_library, Jason

# Import environment specific config. This must remain at the bottom
# of this file so it overrides the configuration defined above.
import_config "#{Mix.env()}.exs"
