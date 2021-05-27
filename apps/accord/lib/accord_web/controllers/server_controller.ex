defmodule AccordWeb.ServerController do
  use AccordWeb, :controller

  alias Accord.Servers
  alias Accord.Servers.Server

  action_fallback AccordWeb.FallbackController

  def index(conn, params) do
    user = conn.assigns[:user]

    servers =
      Servers.search_servers(user,
        query: params["q"],
        limit: String.to_integer(params["limit"] || "10"),
        page: String.to_integer(params["page"] || "1"),
        sort_by: params["sort_by"] || "name",
        order: params["order"] || "asc"
      )

    render(conn, "index.json", servers: servers)
  end

  def create(conn, %{"server" => server_params}) do
    user = conn.assigns[:user]

    with {:ok, %Server{} = server} <- Servers.create_server(server_params, user) do
      conn
      |> put_status(:created)
      |> put_resp_header("location", Routes.server_path(conn, :show, server))
      |> render("show.json", server: server)
    end
  end

  def show(conn, %{"id" => id}) do
    user = conn.assigns[:user]

    with {:ok, server} <- Servers.get_server!(id, user) do
      conn
      |> render("show.json", server: server)
    end
  end
end
