defmodule AccordWeb.MemberRoleController do
  use AccordWeb, :controller

  alias Accord.Roles
  alias Accord.Roles.MemberRole

  action_fallback AccordWeb.FallbackController

  def index(conn, _params) do
    member_role = Roles.list_member_role()
    render(conn, "index.json", member_role: member_role)
  end

  def create(conn, %{"member_role" => member_role_params}) do
    with {:ok, %MemberRole{} = member_role} <- Roles.create_member_role(member_role_params) do
      conn
      |> put_status(:created)
      |> put_resp_header("location", Routes.member_role_path(conn, :show, member_role))
      |> render("show.json", member_role: member_role)
    end
  end

  def show(conn, %{"id" => id}) do
    member_role = Roles.get_member_role!(id)
    render(conn, "show.json", member_role: member_role)
  end

  def update(conn, %{"id" => id, "member_role" => member_role_params}) do
    member_role = Roles.get_member_role!(id)

    with {:ok, %MemberRole{} = member_role} <- Roles.update_member_role(member_role, member_role_params) do
      render(conn, "show.json", member_role: member_role)
    end
  end

  def delete(conn, %{"id" => id}) do
    member_role = Roles.get_member_role!(id)

    with {:ok, %MemberRole{}} <- Roles.delete_member_role(member_role) do
      send_resp(conn, :no_content, "")
    end
  end
end
