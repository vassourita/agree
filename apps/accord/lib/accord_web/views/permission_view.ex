defmodule AccordWeb.PermissionView do
  use AccordWeb, :view

  alias AccordWeb.PermissionView

  def render("show.json", %{permission: permission}) do
    %{permissions: render_one(permission, PermissionView, "permission.json")}
  end

  def render("permission.json", %{permission: permission}) do
    %{
      can_add_users: permission.can_add_users,
      can_remove_users: permission.can_remove_users,
      can_update_server_description: permission.can_update_server_description,
      can_update_server_name: permission.can_update_server_name,
      can_update_server_privacy: permission.can_update_server_privacy
    }
  end
end
