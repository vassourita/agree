defmodule AccordWeb.MemberController do
  use AccordWeb, :controller

  alias Accord.Servers
  alias Accord.Servers.Member

  action_fallback AccordWeb.FallbackController

  def index(conn, _params) do
    member = Servers.list_member()
    render(conn, "index.json", member: member)
  end

  def create(conn, %{"member" => member_params}) do
    with {:ok, %Member{} = member} <- Servers.create_member(member_params) do
      conn
      |> put_status(:created)
      |> put_resp_header("location", Routes.member_path(conn, :show, member))
      |> render("show.json", member: member)
    end
  end

  def show(conn, %{"id" => id}) do
    member = Servers.get_member!(id)
    render(conn, "show.json", member: member)
  end

  def update(conn, %{"id" => id, "member" => member_params}) do
    member = Servers.get_member!(id)

    with {:ok, %Member{} = member} <- Servers.update_member(member, member_params) do
      render(conn, "show.json", member: member)
    end
  end

  def delete(conn, %{"id" => id}) do
    member = Servers.get_member!(id)

    with {:ok, %Member{}} <- Servers.delete_member(member) do
      send_resp(conn, :no_content, "")
    end
  end
end
