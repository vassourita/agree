defmodule AccordWeb.CategoryView do
  use AccordWeb, :view
  alias AccordWeb.CategoryView

  def render("index.json", %{category: category}) do
    %{data: render_many(category, CategoryView, "category.json")}
  end

  def render("show.json", %{category: category}) do
    %{data: render_one(category, CategoryView, "category.json")}
  end

  def render("category.json", %{category: category}) do
    %{id: category.id, name: category.name}
    |> Map.put_new(
      :channels,
      render_many(category.channels, AccordWeb.ChannelView, "channel.json", as: :channel)
    )
  end
end
