defmodule AccordWeb.ServerView do
  use AccordWeb, :view

  import AccordWeb.ViewHelpers

  alias AccordWeb.ServerView
  alias Accord.Servers

  def render("index.json", %{servers: servers}) do
    %{servers: render_many(servers, ServerView, "server.json")}
  end

  def render("show.json", %{server: server}) do
    %{server: render_one(server, ServerView, "server.json")}
  end

  def render("server.json", %{server: server}) do
    %{
      id: server.id,
      name: server.name,
      description: server.description,
      privacy: server.privacy,
      privacy_str: Servers.int_to_privacy_string(server.privacy)
    }
    |> put_assoc(
      Ecto.assoc_loaded?(server.categories),
      :categories,
      fn ->
        render_many(server.categories, AccordWeb.CategoryView, "category.json", as: :category)
      end
    )
    |> put_assoc(
      Ecto.assoc_loaded?(server.roles),
      :roles,
      fn -> render_many(server.roles, AccordWeb.RoleView, "role.json", as: :role) end
    )
    |> put_assoc(
      Ecto.assoc_loaded?(server.members),
      :members,
      fn -> render_many(server.members, AccordWeb.MemberView, "member.json", as: :member) end
    )
  end
end
