defmodule AccordWeb.RoleView do
  use AccordWeb, :view
  alias AccordWeb.RoleView

  def render("index.json", %{role: role}) do
    %{data: render_many(role, RoleView, "role.json")}
  end

  def render("show.json", %{role: role}) do
    %{data: render_one(role, RoleView, "role.json")}
  end

  def render("role.json", %{role: role}) do
    %{
      id: role.id,
      name: role.name,
      can_update_server_name: role.can_update_server_name,
      can_update_server_description: role.can_update_server_description,
      can_update_server_privacy: role.can_update_server_privacy,
      can_add_users: role.can_add_users,
      can_remove_users: role.can_remove_users
    }
  end
end
