defmodule AccordWeb.ServerView do
  use AccordWeb, :view
  alias AccordWeb.ServerView

  alias Accord.Servers

  def render("index.json", %{server: server}) do
    %{data: render_many(server, ServerView, "server.json")}
  end

  def render("show.json", %{server: server}) do
    %{data: render_one(server, ServerView, "server.json")}
  end

  def render("server.json", %{server: server}) do
    %{
      id: server.id,
      name: server.name,
      description: server.description,
      privacy: server.privacy,
      privacy_str: Servers.int_to_privacy_string(server.privacy)
    }
    |> Map.put_new(
      :categories,
      render_many(server.categories, AccordWeb.CategoryView, "category.json", as: :category)
    )
    |> Map.put_new(
      :roles,
      render_many(server.roles, AccordWeb.RoleView, "role.json", as: :role)
    )
    |> Map.put_new(
      :members,
      render_many(server.members, AccordWeb.MemberView, "member.json", as: :member)
    )
  end
end
