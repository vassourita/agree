defmodule AccordWeb.ServerController do
  use AccordWeb, :controller

  alias Accord.Servers
  alias Accord.Servers.Server
  alias Accord.AllowIntegration.User, as: Allow

  action_fallback AccordWeb.FallbackController

  def index(conn, _params) do
    server = Servers.list_server()
    render(conn, "index.json", server: server)
  end

  def create(conn, %{"server" => server_params}) do
    user = %{id: conn.assigns[:user].id}

    with {:ok, %Server{} = server} <- Servers.create_server(server_params, user) do
      conn
      |> put_status(:created)
      |> put_resp_header("location", Routes.server_path(conn, :show, server))
      |> render("show.json", server: server)
    end
  end

  def show(conn, %{"id" => id}) do
    server = Servers.get_server!(id)
    render(conn, "show.json", server: server)
  end

  def update(conn, %{"id" => id, "server" => server_params}) do
    server = Servers.get_server!(id)

    with {:ok, %Server{} = server} <- Servers.update_server(server, server_params) do
      render(conn, "show.json", server: server)
    end
  end

  def delete(conn, %{"id" => id}) do
    server = Servers.get_server!(id)

    with {:ok, %Server{}} <- Servers.delete_server(server) do
      send_resp(conn, :no_content, "")
    end
  end
end
