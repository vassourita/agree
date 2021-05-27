defmodule AccordWeb.PermissionController do
  use AccordWeb, :controller

  alias Accord.Roles

  action_fallback AccordWeb.FallbackController

  def index(conn, %{"id" => server_id}) do
    user = conn.assigns[:user]

    permissions = Roles.get_member_permissions_on_server(user.id, server_id)

    conn
    |> put_view(AccordWeb.PermissionView)
    |> render("show.json", permission: permissions)
  end
end
