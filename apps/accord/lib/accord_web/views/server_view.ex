defmodule AccordWeb.ServerView do
  use AccordWeb, :view
  alias AccordWeb.ServerView

  def render("index.json", %{server: server}) do
    %{data: render_many(server, ServerView, "server.json")}
  end

  def render("show.json", %{server: server}) do
    %{data: render_one(server, ServerView, "server.json")}
  end

  def render("server.json", %{server: server}) do
    %{id: server.id,
      name: server.name,
      description: server.description,
      privacy: server.privacy}
  end
end
